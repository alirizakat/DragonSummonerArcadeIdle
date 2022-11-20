using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    void CheckForMax()
    {
        if(humanList.Count - 1 == humanLimit && !isMax)
        {
            maxObj = Instantiate(maxPrefab, FindObjectOfType<Canvas>().transform).GetComponent<Text>();
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
            maxObj.gameObject.transform.position = Camera.main.WorldToScreenPoint(tempPlayer.transform.position + offset);
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
