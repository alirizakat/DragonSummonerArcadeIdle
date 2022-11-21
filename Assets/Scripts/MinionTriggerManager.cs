using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Not going to mention much in here, just like collect manager this is also a copy version of player's trigger manager
//There are some differences though, minion does most of it's stuff in stay and exit triggers
public class MinionTriggerManager : MonoBehaviour
{
    public delegate void minionOnCollectArea();
    public static event minionOnCollectArea minionOnHumanCollect;
    public static HumanGenerator humanGenerator;

    public delegate void minionOnDragonArea();
    public static event minionOnDragonArea minionOnDropHuman;
    public static DragonManager dragonManager;

    public delegate void minionOnSkullArea();
    public static event minionOnSkullArea minionOnSkullCollect;

    public delegate void minionOnBuyArea();
    public static event minionOnBuyArea minionOnSummonDragon;

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
                minionOnHumanCollect();
            }
            if(isGiving == true)
            {
                minionOnDropHuman();
            }
            yield return new WaitForSeconds(0.8f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //we probably don't need this, will push this version but check and remove this part in the future. 
        if(other.gameObject.CompareTag("Skull"))
        {
            minionOnSkullCollect();
            Destroy(other.gameObject);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("BuyArea"))
        {
            minionOnSummonDragon();
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
