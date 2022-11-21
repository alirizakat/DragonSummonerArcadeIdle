using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages every trigger of player object
//Mainly try to use events
//Tried to name variables understandable on here, should be clear at least on first look.
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
    public static AnimatorManager animatorManager;
    bool isCollecting, isGiving;
    public bool collectArea;
    public bool attacking;
    public GameObject upgradeManager;
    public float waitTime;

    void Start()
    {
        animatorManager = gameObject.GetComponent<AnimatorManager>();
        StartCoroutine(CollectEnum());
    }

    IEnumerator CollectEnum()
    {
        while(true)
        {
            //manipulate wait time for changing the collect or drop times, also can turn into an upgrade, that's why it's public
            if(isCollecting == true)
            {
                OnHumanCollect();
                waitTime = 0.6f;
            }
            if(isGiving == true)
            {
                OnDropHuman();
                waitTime = 0.3f;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Skull"))
        {
            OnSkullCollect();
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            attacking = true;
            StartCoroutine(FightRoutine(other.gameObject));   
        }
        //when you enter the collider in the upgrade area menu pops up, when you leave it closes back.
        if(other.gameObject.CompareTag("UpgradeArea"))
        {
            upgradeManager.SetActive(true);
        }
    }
    IEnumerator FightRoutine(GameObject other)
    {
        //hardcoded 1.5f for the animation length, can be changed if required
        //attacking bool is also public for testing purposes, can be turned to [SerializeField] in future if more things start to attack.
        animatorManager.Attack();
        yield return new WaitForSeconds(1.5f);
        Destroy(other.gameObject);
        attacking = false;
    }

    void OnTriggerStay(Collider other)
    {
        //Buy area and summon dragon
        if(other.gameObject.CompareTag("BuyArea"))
        {
            OnSummonDragon();
            areaToBuy = other.gameObject.GetComponent<BuyArea>();
        }
        //collect human bodies from the generation point
        if(other.gameObject.CompareTag("CollectArea"))
        {
            isCollecting = true;
            humanGenerator = other.gameObject.GetComponent<HumanGenerator>();
            collectArea = true;
        }
        //give human bodies to dragons
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
        if(other.gameObject.CompareTag("UpgradeArea"))
        {
            upgradeManager.SetActive(false);
        }
    }
}
