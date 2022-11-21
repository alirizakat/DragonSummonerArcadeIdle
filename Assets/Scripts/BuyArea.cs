using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script handles the buy area behaviours
//Reducing the payment amount and filling the progress img etc.
public class BuyArea : MonoBehaviour
{
    public GameObject buyAreaObj, dragonObj;
    public float cost, currentSkull, progress;
    public Image progressImage;
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        currentSkull = 0;
        progress = 0;
        progressImage.fillAmount = 0;
    }
    //Enabling and disabling the object is an okay idea imo. in here 
    //since we allow dragons to die and make the player buy them again
    public void Buy(int skullAmount)
    {
        currentSkull += skullAmount;
        progress = currentSkull / cost;
        progressImage.fillAmount = progress;
        if(progress >= 1)
        {
            buyAreaObj.SetActive(false);
            dragonObj.SetActive(true);
        }
    }
}
