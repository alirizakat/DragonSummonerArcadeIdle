using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script accidently turned into something more than it should be.
//CollectManager script handles all of the body collecting stuff from the given spots
//Since it also checks for the max amount that can be carried, a small UI which pops up when you reach max limit also controlled in here 
public class CollectManager : MonoBehaviour
{
    public List<GameObject> humanList = new List<GameObject>();
    public GameObject humanPrefab;
    public Transform collectPoint;
    public static TriggerManager triggerManager;
    bool canCollect;
    public int humanLimit = 10;
    public GameObject maxPrefab;
    private Text maxObj;
    bool isMax;
    public Vector3 offset;
    void Start()
    {
        triggerManager = gameObject.GetComponent<TriggerManager>();
    }
    void Update()
    {
        canCollect = triggerManager.collectArea;
        CheckForMax();
        MoveMax();
    }
    void OnEnable()
    {
        TriggerManager.OnHumanCollect += GetHuman;
        TriggerManager.OnDropHuman += GiveHuman; 
    }
    void OnDisable()
    {
        TriggerManager.OnHumanCollect -= GetHuman;
        TriggerManager.OnDropHuman -= GiveHuman;  
    }

    //this function collects(actually creates for now, we are not currently using object pooling) human bodies from spots
    //Basicly what we do is when we go to collect spot, we instantiate a body, destroy the other one, when we drop we also do sth similar
    //This is a faster way of doing stuff but object pooling is a much better idea for an arcade idle project. if your project is bigger or sth to launch change to that   
    void GetHuman()
    {
        if(humanList.Count <= humanLimit && canCollect)
        {
            GameObject temp = Instantiate(humanPrefab, collectPoint);
            temp.transform.position = 
            new Vector3
            (collectPoint.position.x,((float)humanList.Count)+ 0.5f,collectPoint.position.z);
            humanList.Add(temp);
            if(TriggerManager.humanGenerator != null)
            {
                TriggerManager.humanGenerator.RemoveLast();
            }
        }
    }
    public void GiveHuman()
    {
        if(humanList.Count > 0)
        {
            TriggerManager.dragonManager.GetHuman();
            RemoveLast();
        }
    }
    public void RemoveLast()
    {
        if(humanList.Count > 0)
        {
            Destroy(humanList[humanList.Count - 1]);
            humanList.RemoveAt(humanList.Count - 1);
        }
    }

    //This part of the script controls the MAX UI, 
    //If UI manager is created in future, remove this section.
    void CheckForMax()
    {
        if(humanList.Count - 1 == humanLimit && !isMax)
        {
            Debug.Log("max");
            maxObj = Instantiate(maxPrefab, GameObject.Find("MaxCanvas").transform).GetComponent<Text>();
            isMax = true;
        }
        else if(humanList.Count - 1 != humanLimit)
        {
            isMax = false;
        }
    }
    void MoveMax()
    {
        if(isMax)
        {
            GameObject tempPlayer = GameObject.Find("Player");
            maxObj.transform.position = Camera.main.WorldToScreenPoint(tempPlayer.transform.position + offset);
        }
        else if(!isMax)
        {
            if(maxObj == null)
            {
                return;
            }
            if(maxObj.gameObject.activeInHierarchy)
            {
                Destroy(maxObj.gameObject);
            }
        }
    }
}
