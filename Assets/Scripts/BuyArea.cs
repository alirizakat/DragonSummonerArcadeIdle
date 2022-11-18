using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyArea : MonoBehaviour
{
    public GameObject buyAreaObj, dragonObj;
    public float cost, currentSkull, progress;
    public Image progressImage;
    public void Buy(int skullAmount)
    {
        currentSkull += skullAmount;
        progress = currentSkull / cost;
        progressImage.fillAmount = progress;
        if(progress >= 1)
        {
            buyAreaObj.SetActive(false);
            dragonObj.SetActive(true);
            this.enabled = false;
        }
    }
}
