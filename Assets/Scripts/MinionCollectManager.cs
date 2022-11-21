using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Literally same logic with human collect manager, 
//Minion doesn't have anything unnecessary like ui control or other stuff in here, 
//Rest of the script is almost copy paste from the original CollectManager.
public class MinionCollectManager : MonoBehaviour
{
    public List<GameObject> humanList = new List<GameObject>();
    public GameObject humanPrefab;
    public Transform collectPoint;
    public static MinionTriggerManager triggerManager;
    public bool canCollect;
    public int humanLimit = 10;
    void Start()
    {
        triggerManager = gameObject.GetComponent<MinionTriggerManager>();
    }
    void Update()
    {
        canCollect = triggerManager.collectArea;
    }
    void OnEnable()
    {
        MinionTriggerManager.minionOnHumanCollect += GetHuman;
        MinionTriggerManager.minionOnDropHuman += GiveHuman; 
    }
    void OnDisable()
    {
        MinionTriggerManager.minionOnHumanCollect -= GetHuman;
        MinionTriggerManager.minionOnDropHuman -= GiveHuman;  
    }   
    void GetHuman()
    {
        if(humanList.Count <= humanLimit - 1 && canCollect)
        {
            GameObject temp = Instantiate(humanPrefab, collectPoint);
            temp.transform.position = 
            new Vector3
            (collectPoint.position.x,((float)humanList.Count)+ 0.5f,collectPoint.position.z);
            humanList.Add(temp);
            if(MinionTriggerManager.humanGenerator != null)
            {
                MinionTriggerManager.humanGenerator.RemoveLast();
            }
        }
    }
    public void GiveHuman()
    {
        if(humanList.Count > 0)
        {
            MinionTriggerManager.dragonManager.GetHuman();
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
}
