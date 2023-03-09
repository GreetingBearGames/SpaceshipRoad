using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{

    [SerializeField] GameObject menuBackground;
    [SerializeField] GameObject shopWindow, shipSelectButton, planetSelectButton;
    [SerializeField] GameObject shipSelectSubMenu, planetSelectSubMenu;
    [SerializeField] Button shipBuyButton, planetBuyButton;
    [SerializeField] TextMeshProUGUI shipBuyValue, planetBuyValue;
    [SerializeField] Planet_to_Game planet_To_Game;
    [SerializeField] Ship_to_Game ship_To_Game;
    [SerializeField] GameObject optionMenu;

    public static bool isGameStarted = false;
    private int shipIndex = 0;
    private int planetIndex = 0;
    private bool isShipSelectMenu;
    private Color shipSelectButtonColor, planetSelectButtonColor;

    private int[] shipPrice = { 0, 10, 20, 30, 40, 50 };
    private int[] planetPrice = { 0, 20, 40, 60 };


    private void Start()
    {
        shopWindow.SetActive(true);

        ShipsImageControl();
        PlanetsImageControl();
        PurchasedShipSelector();
        PurchasedPlanetSelector();

        Wallet.SetAmount(1000);
    }


    public void CloseShopandStartGame()
    {
        //----------------------Aktif gemiyi bulma ve oyuna getirme
        var shipParent = transform.GetChild(0).transform.GetChild(0);
        for (int k = 0; k < 6; k++)
        {
            if (shipParent.GetChild(k).gameObject.activeSelf == true)       //böylece setactive true olanı buldu
            {
                for (int sayac = k; sayac >= 0; sayac--)
                {
                    if (PlayerPrefs.GetInt("isShipBought" + sayac) == 1)
                    {
                        ship_To_Game.SelectShiptoGame(sayac);
                        break;
                    }
                }
            }
        }


        //----------------------Aktif planeti bulma ve oyuna getirme
        var planetParent = transform.GetChild(0).transform.GetChild(1);
        for (int k = 0; k < 4; k++)
        {
            if (planetParent.GetChild(k).gameObject.activeSelf == true)       //böylece setactive true olanı buldu
            {
                for (int sayac = k; sayac >= 0; sayac--)
                {
                    if (PlayerPrefs.GetInt("isPlanetBought" + sayac) == 1)
                    {
                        planet_To_Game.SelectPlanettoGame(sayac);
                        break;
                    }
                }
            }
        }

        shopWindow.SetActive(false);
        GameManager.Instance.IsGameStarted = true;
        ObstacleLineSpawner.instance.SpawnLine();
        optionMenu.SetActive(false);
    }


    private void ShipsImageControl()    //Satın alınan ship imagelar'ını aktive eder
    {
        for (int i = 0; i < 6; i++)
        {
            if (PlayerPrefs.GetInt("isShipBought" + i) == 1)
            {
                //shipBuyButton.gameObject.SetActive(false);
                GameObject referencedShip = transform.GetChild(0).transform.GetChild(0).GetChild(i).gameObject;
                referencedShip.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }
    private void PlanetsImageControl()  //Satın alınan planet imagelar'ını aktive eder
    {
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("isPlanetBought" + i) == 1)
            {
                //planetBuyButton.gameObject.SetActive(false);
                GameObject referencedPlanet = transform.GetChild(0).transform.GetChild(1).GetChild(i).gameObject;
                referencedPlanet.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    private void PurchasedShipSelector()    //Shop açıldığında en son satın alınan ship gösterir
    {
        for (int j = 5; j >= 0; j--)
        {
            if (PlayerPrefs.GetInt("isShipBought" + j) == 1)
            {
                GameObject purchasedLastShip = transform.GetChild(0).transform.GetChild(0).GetChild(j).gameObject;
                purchasedLastShip.SetActive(true);
                break;
            }
        }
    }
    private void PurchasedPlanetSelector()    //Shop açıldığında en son satın alınan planet gösterir
    {
        for (int j = 3; j >= 0; j--)
        {
            if (PlayerPrefs.GetInt("isPlanetBought" + j) == 1)
            {
                GameObject purchasedLastPlanet = transform.GetChild(0).transform.GetChild(1).GetChild(j).gameObject;
                purchasedLastPlanet.SetActive(true);
                break;
            }
        }
    }

    public void NextandPreviousShipButton(bool isPressedNext)   //İleri geri düğmelerine basışları kontrol eder
    {
        var shipsParent = transform.GetChild(0).transform.GetChild(0);
        for (int j = 5; j >= 0; j--)
        {
            if (shipsParent.GetChild(j).gameObject.activeSelf == true)
            {
                if (isPressedNext && j <= 4)
                {
                    GameObject aktifShip = shipsParent.GetChild(j).gameObject;
                    GameObject nextShip = shipsParent.GetChild(j + 1).gameObject;
                    nextShip.SetActive(true);
                    aktifShip.SetActive(false);
                    CheckPurchasement(j + 1, true);

                    shipIndex = j + 1;
                }
                else if (!isPressedNext && j >= 1)
                {
                    GameObject aktifShip = shipsParent.GetChild(j).gameObject;
                    GameObject previousShip = shipsParent.GetChild(j - 1).gameObject;
                    previousShip.SetActive(true);
                    aktifShip.SetActive(false);
                    CheckPurchasement(j - 1, true);

                    shipIndex = j - 1;
                }
                break;
            }
        }
    }
    public void NextandPreviousPlanetButton(bool isPressedNext)    //İleri geri düğmelerine basışları kontrol eder
    {
        var PlanetParent = transform.GetChild(0).transform.GetChild(1);
        for (int j = 3; j >= 0; j--)
        {
            if (PlanetParent.GetChild(j).gameObject.activeSelf == true)
            {
                if (isPressedNext && j <= 2)
                {
                    GameObject aktifPlanet = PlanetParent.GetChild(j).gameObject;
                    GameObject nextPlanet = PlanetParent.GetChild(j + 1).gameObject;
                    nextPlanet.SetActive(true);
                    aktifPlanet.SetActive(false);
                    CheckPurchasement(j + 1, false);

                    planetIndex = j + 1;
                }
                else if (!isPressedNext && j >= 1)
                {
                    GameObject aktifPlanet = PlanetParent.GetChild(j).gameObject;
                    GameObject previousPlanet = PlanetParent.GetChild(j - 1).gameObject;
                    previousPlanet.SetActive(true);
                    aktifPlanet.SetActive(false);
                    CheckPurchasement(j - 1, false);

                    planetIndex = j - 1;
                }
                break;
            }
        }
    }

    private void CheckPurchasement(int index, bool isShip)  //İlgili objenin satın alınıp alınmadığını kontrol eder
    {
        if (isShip)
        {
            if (PlayerPrefs.GetInt("isShipBought" + index) == 1)
            {
                shipBuyButton.gameObject.SetActive(false);
            }
            else
            {
                shipBuyButton.gameObject.SetActive(true);
                shipBuyValue.text = shipPrice[index].ToString();
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("isPlanetBought" + index) == 1)
            {
                planetBuyButton.gameObject.SetActive(false);
            }
            else
            {
                planetBuyButton.gameObject.SetActive(true);
                planetBuyValue.text = planetPrice[index].ToString();
            }
        }
    }
    public void BuyItem(bool isShip)    //İlgili objenin satın alınmasını yapar
    {
        if (isShip)
        {
            if (Wallet.GetAmount() - shipPrice[shipIndex] >= 0)
            {
                PlayerPrefs.SetInt("isShipBought" + shipIndex, 1);
                Wallet.SetAmount(Wallet.GetAmount() - shipPrice[shipIndex]);

                ShipsImageControl();
                shipBuyButton.gameObject.SetActive(false);
            }
        }
        else
        {
            if (Wallet.GetAmount() - planetPrice[planetIndex] >= 0)
            {
                PlayerPrefs.SetInt("isPlanetBought" + planetIndex, 1);
                Wallet.SetAmount(Wallet.GetAmount() - planetPrice[planetIndex]);

                PlanetsImageControl();
                planetBuyButton.gameObject.SetActive(false);
            }
        }

    }

    //-----------------------------------------------------------------------------------------------------------------------------


    /*
    public void OpenSpaceshipSelectSubMenu()
        {
            isShipSelectMenu = true;
            shipSelectSubMenu.SetActive(true);
            planetSelectSubMenu.SetActive(false);


            shipSelectButtonColor = shipSelectButton.GetComponent<UnityEngine.UI.Image>().color;
            shipSelectButtonColor.a = 1f;
            shipSelectButton.GetComponent<UnityEngine.UI.Image>().color = shipSelectButtonColor;

            planetSelectButtonColor = planetSelectButton.GetComponent<UnityEngine.UI.Image>().color;
            planetSelectButtonColor.a = 0.35f;
            planetSelectButton.GetComponent<UnityEngine.UI.Image>().color = planetSelectButtonColor;


            SoundFX_Control.instance.PlayButtonSound();
            SatinAlmaButonuModu();
        }

    public void OpenPlanetSelectSubMenu()
    {
        isShipSelectMenu = false;
        planetSelectSubMenu.SetActive(true);
        shipSelectSubMenu.SetActive(false);

        shipSelectButtonColor = shipSelectButton.GetComponent<UnityEngine.UI.Image>().color;
        shipSelectButtonColor.a = 0.35f;
        shipSelectButton.GetComponent<UnityEngine.UI.Image>().color = shipSelectButtonColor;

        planetSelectButtonColor = planetSelectButton.GetComponent<UnityEngine.UI.Image>().color;
        planetSelectButtonColor.a = 1f;
        planetSelectButton.GetComponent<UnityEngine.UI.Image>().color = planetSelectButtonColor;

        SoundFX_Control.instance.PlayButtonSound();
        SatinAlmaButonuModu();
    }

    public void PreviousItem()
    {
        if (isShipSelectMenu)
        {
            if (shipIndex >= 1)
            {
                GameObject existingShip = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(2).gameObject).transform.GetChild(shipIndex).gameObject;
                existingShip.SetActive(false);

                shipIndex--;
                GameObject nextShip = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(2).gameObject).transform.GetChild(shipIndex).gameObject;
                nextShip.SetActive(true);
            }
        }

        else
        {
            if (planetIndex >= 1)
            {
                GameObject existingPlanet = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(3).gameObject).transform.GetChild(planetIndex).gameObject;
                existingPlanet.SetActive(false);

                planetIndex--;
                GameObject nextPlanet = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(3).gameObject).transform.GetChild(planetIndex).gameObject;
                nextPlanet.SetActive(true);
            }
        }

        SoundFX_Control.instance.PlayButtonSound();
        SatinAlmaButonuModu();
    }

    public void NextItem()
    {
        if (isShipSelectMenu)
        {
            if (shipIndex < 5)
            {
                GameObject existingShip = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(2).gameObject).transform.GetChild(shipIndex).gameObject;
                existingShip.SetActive(false);

                shipIndex++;
                GameObject nextShip = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(2).gameObject).transform.GetChild(shipIndex).gameObject;
                nextShip.SetActive(true);
            }
        }

        else
        {
            if (planetIndex < 3)
            {
                GameObject existingPlanet = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(3).gameObject).transform.GetChild(planetIndex).gameObject;
                existingPlanet.SetActive(false);

                planetIndex++;
                GameObject nextPlanet = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(3).gameObject).transform.GetChild(planetIndex).gameObject;
                nextPlanet.SetActive(true);
            }
        }

        SoundFX_Control.instance.PlayButtonSound();
        SatinAlmaButonuModu();
    }

    public void BuyItem()
    {
        if (isShipSelectMenu)
        {
            if (Wallet.GetAmount() - shipPrice[shipIndex] >= 0)
            {
                PlayerPrefs.SetInt("isShipBought" + shipIndex, 1);
                Wallet.SetAmount(Wallet.GetAmount() - shipPrice[shipIndex]);
            }
            else { SoundFX_Control.instance.PlayButtonSound(); return; }
        }
        else
        {
            if (Wallet.GetAmount() - planetPrice[planetIndex] >= 0)
            {
                PlayerPrefs.SetInt("isPlanetBought" + planetIndex, 1);
                Wallet.SetAmount(Wallet.GetAmount() - planetPrice[planetIndex]);
            }
            else { SoundFX_Control.instance.PlayButtonSound(); return; }
        }

        SoundFX_Control.instance.PlayBuySound();
        SatinAlmaButonuModu();
    }

    public void UseBoughtItem()
    {
        if (isShipSelectMenu)
        {
            for (int i = 0; i < 6; i++)
            {
                PlayerPrefs.SetInt("isShipUsed" + i, 0);
            }
            PlayerPrefs.SetInt("isShipUsed" + shipIndex, 1);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerPrefs.SetInt("isPlanetUsed" + i, 0);
            }
            PlayerPrefs.SetInt("isPlanetUsed" + planetIndex, 1);
        }

        SoundFX_Control.instance.PlayButtonSound();
        SatinAlmaButonuModu();
    }

    public void SatinAlmaButonuModu()
    {
        GameObject buyButton = (gameObject.transform.GetChild(1).gameObject).transform.GetChild(6).gameObject;
        GameObject useButton = (gameObject.transform.GetChild(1).gameObject).transform.GetChild(7).gameObject;
        GameObject usedButton = (gameObject.transform.GetChild(1).gameObject).transform.GetChild(8).gameObject;

        //GameObject alreadyUsed = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(7).gameObject).transform.GetChild(1).gameObject;
        //GameObject boughtButNotUsed = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(7).gameObject).transform.GetChild(0).gameObject;

        GameObject urunFiyat = (buyButton.transform.GetChild(0).gameObject).transform.GetChild(0).gameObject;
        GameObject buyBeforeUyarisi = buyButton.transform.GetChild(1).gameObject;

        buyButton.SetActive(false);
        useButton.SetActive(false);
        usedButton.SetActive(false);

        //alreadyUsed.SetActive(false);
        //boughtButNotUsed.SetActive(false);


        //GEMİ EKRANINDA SATIN ALIM İŞLEMİ
        if (isShipSelectMenu)
        {
            if (PlayerPrefs.GetInt("isShipBought" + shipIndex) == 1)
            {
                useButton.SetActive(true);

                GameObject ekrandakiGemi = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(2).gameObject).transform.GetChild(shipIndex).gameObject;
                ekrandakiGemi.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

                if (PlayerPrefs.GetInt("isShipUsed" + shipIndex) == 1)
                {
                    useButton.SetActive(false);
                    usedButton.SetActive(true);
                }
                else
                {
                    useButton.SetActive(true);
                }
            }
            else
            {
                buyButton.SetActive(true);
                if (PlayerPrefs.GetInt("isShipBought" + (shipIndex - 1)) == 1)
                {
                    buyButton.GetComponent<Button>().interactable = true;
                    buyBeforeUyarisi.SetActive(false);
                    urunFiyat.SetActive(true);
                    urunFiyat.GetComponent<TextMeshProUGUI>().text = shipPrice[shipIndex].ToString();
                }
                else        //önceki item satın alınmamış
                {
                    buyButton.GetComponent<Button>().interactable = false;
                    buyBeforeUyarisi.SetActive(true);
                    urunFiyat.SetActive(false);
                }
                GameObject ekrandakiGemi = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(2).gameObject).transform.GetChild(shipIndex).gameObject;
                ekrandakiGemi.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0.5f);
            }
        }


        //PLANET EKRANINDA SATIN ALIM İŞLEMİ
        else
        {
            if (PlayerPrefs.GetInt("isPlanetBought" + planetIndex) == 1)
            {
                useButton.SetActive(true);

                GameObject ekrandakiGezegen = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(3).gameObject).transform.GetChild(planetIndex).gameObject;
                ekrandakiGezegen.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

                if (PlayerPrefs.GetInt("isPlanetUsed" + planetIndex) == 1)
                {
                    useButton.SetActive(false);
                    usedButton.SetActive(true);
                }
                else
                {
                    useButton.SetActive(true);
                }
            }
            else
            {
                buyButton.SetActive(true);
                if (PlayerPrefs.GetInt("isPlanetBought" + (planetIndex - 1)) == 1)
                {
                    buyButton.GetComponent<Button>().interactable = true;
                    buyBeforeUyarisi.SetActive(false);
                    urunFiyat.SetActive(true);
                    urunFiyat.GetComponent<TextMeshProUGUI>().text = planetPrice[planetIndex].ToString();
                }
                else    //önceki item satın alınmamış
                {
                    buyButton.GetComponent<Button>().interactable = false;
                    buyBeforeUyarisi.SetActive(true);
                    urunFiyat.SetActive(false);
                }
                GameObject ekrandakiGezegen = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(3).gameObject).transform.GetChild(planetIndex).gameObject;
                ekrandakiGezegen.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0.5f);
            }
        }
    }
    */
}
