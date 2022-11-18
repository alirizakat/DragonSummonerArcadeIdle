using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    void FixAmountOnScreen()
    {
        skullText.text = skullCount.ToString();
    }
}
