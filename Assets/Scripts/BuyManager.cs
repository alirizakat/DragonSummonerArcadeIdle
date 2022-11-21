using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script mainly controls the amount of skulls player has
//Changing the skullCount amount on editor also helps for play tests if you want to start and buy sth.

public class BuyManager : MonoBehaviour
{
    public int skullCount = 0;
    public Text skullText;

    void OnEnable()
    {
        TriggerManager.OnSkullCollect += IncreaseSkull;
        TriggerManager.OnSummonDragon += BuyArea;
    }

    void OnDisable()
    {
        TriggerManager.OnSkullCollect -= IncreaseSkull;
        TriggerManager.OnSummonDragon -= BuyArea;
    }
    void IncreaseSkull()
    {
        skullCount+=1;
    }
    void BuyArea()
    {
        if(TriggerManager.areaToBuy != null)
        {
            if(skullCount >= 1)
            {
                TriggerManager.areaToBuy.Buy(1);
                skullCount -= 1;
            }
        }
    }
    void Update()
    {
        FixAmountOnScreen();
    }
    //Normally this function shouldn't be in here imo. however since this is a prototype for now,
    //there are not all that much of UI and it's okay to just control just 1 UI in this script.
    void FixAmountOnScreen()
    {
        skullText.text = skullCount.ToString();
    }
}
