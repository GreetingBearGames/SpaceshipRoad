using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{

    [SerializeField] GameObject menuBackground;
    [SerializeField] GameObject shopMenu, shipSelectButton, planetSelectButton;
    [SerializeField] GameObject shipSelectSubMenu, planetSelectSubMenu;
    private int shipIndex = 0;
    private int planetIndex = 0;
    private bool isShipSelectMenu;
    private Color shipSelectButtonColor, planetSelectButtonColor;

    private int[] shipPrice = {0, 50, 50, 100, 150, 200};
    private int[] planetPrice = {0, 100, 150, 200};


    public void OpenShopMenu()
    {
        menuBackground.SetActive(true);
        shopMenu.SetActive(true);


        OpenSpaceshipSelectSubMenu();
        SoundFX_Control.instance.PlayButtonSound();
    }




    public void CloseShopMenu()
    {
        menuBackground.SetActive(false);
        shopMenu.SetActive(false);
        SoundFX_Control.instance.PlayButtonSound();
    }



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
        if(isShipSelectMenu)
        {
            if(shipIndex >= 1)
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
            if(planetIndex >= 1)
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
        if(isShipSelectMenu)
        {
            if(shipIndex < 5)
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
            if(planetIndex < 3)
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
        if(isShipSelectMenu)
        {
            if(Wallet.GetAmount() - shipPrice[shipIndex] >= 0)
            {
                PlayerPrefs.SetInt("isShipBought" + shipIndex, 1);
                Wallet.SetAmount(Wallet.GetAmount() - shipPrice[shipIndex]);
            }
            else {SoundFX_Control.instance.PlayButtonSound(); return;}
        }
        else
        {
            if(Wallet.GetAmount() - planetPrice[planetIndex] >= 0)
            {
                PlayerPrefs.SetInt("isPlanetBought" + planetIndex, 1);
                Wallet.SetAmount(Wallet.GetAmount() - planetPrice[planetIndex]);
            }
            else {SoundFX_Control.instance.PlayButtonSound(); return;}
        }
        
        SoundFX_Control.instance.PlayBuySound();
        SatinAlmaButonuModu();
    }

    public void UseBoughtItem()
    {
        if(isShipSelectMenu)
        {
            for(int i = 0; i < 6; i++)
            {
                PlayerPrefs.SetInt("isShipUsed" + i, 0);
            }
            PlayerPrefs.SetInt("isShipUsed" + shipIndex, 1);
        }
        else
        {
            for(int i = 0; i < 4; i++)
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
        if(isShipSelectMenu)
        {
            if(PlayerPrefs.GetInt("isShipBought" + shipIndex) == 1)
            {
                useButton.SetActive(true);

                GameObject ekrandakiGemi = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(2).gameObject).transform.GetChild(shipIndex).gameObject;
                ekrandakiGemi.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

                if(PlayerPrefs.GetInt("isShipUsed" + shipIndex) == 1)
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
                if(PlayerPrefs.GetInt("isShipBought" + (shipIndex-1)) == 1)
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
            if(PlayerPrefs.GetInt("isPlanetBought" + planetIndex) == 1)
            {
                useButton.SetActive(true);

                GameObject ekrandakiGezegen = ((gameObject.transform.GetChild(1).gameObject).transform.GetChild(3).gameObject).transform.GetChild(planetIndex).gameObject;
                ekrandakiGezegen.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

                if(PlayerPrefs.GetInt("isPlanetUsed" + planetIndex) == 1)
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
                if(PlayerPrefs.GetInt("isPlanetBought" + (planetIndex-1)) == 1)
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






    /*
    void Update() 
    {
        if (Input.GetKeyDown (KeyCode.Space)) {
            StartCoroutine("DegerGosterici");
        }
    }

    IEnumerator DegerGosterici()
    {        
        for(int i = 0; i < 6; i++)
        {
            Debug.Log("Ship: " + i + " = " + PlayerPrefs.GetInt("isShipBought" + i) + " / " + PlayerPrefs.GetInt("isShipUsed" + i));
        }
        
        for(int i = 0; i < 4; i++)
        {
            Debug.Log("Planet: " + i + " = " + PlayerPrefs.GetInt("isPlanetBought" + i) + " / " + PlayerPrefs.GetInt("isPlanetUsed" + i));
        }
        yield return new WaitForSeconds(5f);
    }  
    */



}
