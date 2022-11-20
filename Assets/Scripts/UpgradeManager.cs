using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public GameObject player, minion, camera;
    public Sprite blue, green, purple;
    public GameObject but1, but2, but3;
    private int but1Level, but2Level, but3Level;
    private int but1Price, but2Price, but3Price;
    public Text but1PriceText, but2PriceText, but3PriceText;
    public GameObject but1PriceObj, but2PriceObj, but3PriceObj;
    public int currentSkull;
    public GameObject notEnoughMoneyText;
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        notEnoughMoneyText.SetActive(false);
    }
    public void IncreasePlayerSpeed()
    {
        currentSkull = player.GetComponent<BuyManager>().skullCount;
        switch(but1Level)
        {
            case 0:
            if(currentSkull >= but1Price)
            {
                but1Level++;
                player.GetComponent<PlayerMovement>().movementSpeed = 20;
                but1.GetComponent<Button>().image.sprite = green;
                player.GetComponent<BuyManager>().skullCount = currentSkull - but1Price;
                break;
            }
            else
            {
                notEnoughMoneyText.SetActive(true);
                break;
            }
            case 1:
            if(currentSkull >= but1Price)
            {
                but1Level++;
                player.GetComponent<PlayerMovement>().movementSpeed = 25;
                but1.GetComponent<Button>().image.sprite = purple;
                player.GetComponent<BuyManager>().skullCount = currentSkull - but1Price;
                break;
            }
            else
            {
                notEnoughMoneyText.SetActive(true);
                break;
            }
            case 2:
            if(currentSkull >= but1Price)
            {
                but1Level++;
                player.GetComponent<PlayerMovement>().movementSpeed = 30;
                but1.GetComponentInChildren<Text>().text = "MAX";
                player.GetComponent<BuyManager>().skullCount = currentSkull - but1Price;
                but1PriceObj.SetActive(false);
                break;
            }
            else
            {
                notEnoughMoneyText.SetActive(true);
                break;
            }     
        }
    }
    public void IncreaseCarriage()
    {
        currentSkull = player.GetComponent<BuyManager>().skullCount;
        switch(but2Level)
        {
            case 0:
            if(currentSkull >= but2Price)
            {
                but2Level++;
                player.GetComponent<CollectManager>().humanLimit = 15;
                but2.GetComponent<Button>().image.sprite = green;
                player.GetComponent<BuyManager>().skullCount = currentSkull - but2Price;
                break;
            }
            else
            {
                notEnoughMoneyText.SetActive(true);
                break;
            }
            
            case 1:
            if(currentSkull >= but2Price)
            {
                but2Level++;
                player.GetComponent<CollectManager>().humanLimit = 20;
                but2.GetComponent<Button>().image.sprite = purple;
                player.GetComponent<BuyManager>().skullCount = currentSkull - but2Price;
                break;
            }
            else
            {
                notEnoughMoneyText.SetActive(true);
                break;
            }

            case 2:
            if(currentSkull >= but2Price)
            {
                but2Level++;
                player.GetComponent<CollectManager>().humanLimit = 25;
                but2.GetComponentInChildren<Text>().text = "MAX";
                player.GetComponent<BuyManager>().skullCount = currentSkull - but2Price;
                but2PriceObj.SetActive(false);
                break;
            }
            else
            {
                notEnoughMoneyText.SetActive(true);
                break;
            }
        }
    }
    public void IncreaseMinionCarriage()
    {
        currentSkull = player.GetComponent<BuyManager>().skullCount;
        switch(but3Level)
        {
            case 0:
            if(currentSkull >= but3Price)
            {
                but3Level++;
                minion.GetComponent<MinionCollectManager>().humanLimit = 10;
                but3.GetComponent<Button>().image.sprite = green;
                player.GetComponent<BuyManager>().skullCount = currentSkull - but3Price;
                break;
            }
            else
            {
                notEnoughMoneyText.SetActive(true);
                break;
            }

            case 1:
            if(currentSkull >= but3Price)
            {
                but3Level++;
                minion.GetComponent<MinionCollectManager>().humanLimit = 15;
                but3.GetComponent<Button>().image.sprite = purple;
                player.GetComponent<BuyManager>().skullCount = currentSkull - but3Price;
                break;
            }
            else
            {
                notEnoughMoneyText.SetActive(true);
                break;
            }

            case 2:
            if(currentSkull >= but3Price)
            {
                but3Level++;
                minion.GetComponent<MinionCollectManager>().humanLimit = 20;
                but3.GetComponentInChildren<Text>().text = "MAX";
                player.GetComponent<BuyManager>().skullCount = currentSkull - but3Price;
                but3PriceObj.SetActive(false);
                break;
            }
            else
            {
                notEnoughMoneyText.SetActive(true);
                break;
            }
        }
    }
    void UpgradePrices()
    {
        switch(but1Level)
        {
            case 0:
                but1Price = 20;
                but1PriceText.text = but1Price.ToString();
                break;
            case 1:
                but1Price = 30;
                but1PriceText.text = but1Price.ToString();
                break;
            case 2:
                but1Price = 40;
                but1PriceText.text = but1Price.ToString();
                break;
        }

        switch(but2Level)
        {
            case 0:
                but2Price = 20;
                but2PriceText.text = but2Price.ToString();
                break;
            case 1:
                but2Price = 30;
                but2PriceText.text = but2Price.ToString();
                break;
            case 2:
                but2Price = 40;
                but2PriceText.text = but2Price.ToString();
                break;
        }

        switch(but3Level)
        {
            case 0:
                but3Price = 20;
                but3PriceText.text = but3Price.ToString();
                break;
            case 1:
                but3Price = 30;
                but3PriceText.text = but3Price.ToString();
                break;
            case 2:
                but3Price = 40;
                but3PriceText.text = but3Price.ToString();
                break;
        }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        UpgradePrices();
    }
}
