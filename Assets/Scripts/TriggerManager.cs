using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public delegate void OnCollectArea();
    public static event OnCollectArea OnHumanCollect;
    public static HumanGenerator humanGenerator;

    public delegate void OnDragonArea();
    public static event OnDragonArea OnDropHuman;
    public static DragonManager dragonManager;

    public delegate void OnSkullArea();
    public static event OnSkullArea OnSkullCollect;

    public delegate void OnBuyArea();
    public static event OnBuyArea OnSummonDragon;

    public static BuyArea areaToBuy;

    bool isCollecting, isGiving;
    public bool collectArea;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CollectEnum());
    }
    IEnumerator CollectEnum()
    {
        while(true)
        {
            if(isCollecting == true)
            {
                OnHumanCollect();
            }
            if(isGiving == true)
            {
                OnDropHuman();
            }
            yield return new WaitForSeconds(0.8f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Skull"))
        {
            OnSkullCollect();
            Destroy(other.gameObject);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("BuyArea"))
        {
            OnSummonDragon();
            areaToBuy = other.gameObject.GetComponent<BuyArea>();
        }
        if(other.gameObject.CompareTag("CollectArea"))
        {
            isCollecting = true;
            humanGenerator = other.gameObject.GetComponent<HumanGenerator>();
            collectArea = true;
        }
        if(other.gameObject.CompareTag("GiveArea"))
        {
            isGiving = true;
            dragonManager = other.gameObject.GetComponent<DragonManager>();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("CollectArea"))
        {
            isCollecting = false;
            humanGenerator = null;
            collectArea = false;
        }
        if(other.gameObject.CompareTag("GiveArea"))
        {
            isGiving = false;
            dragonManager = null;
        }
        if(other.gameObject.CompareTag("BuyArea"))
        {
            areaToBuy = null;
        }
    }
}
