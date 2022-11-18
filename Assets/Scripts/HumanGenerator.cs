using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanGenerator : MonoBehaviour
{
    public List<GameObject> humanList = new List<GameObject>();
    public GameObject humanPrefab;
    public Transform exitPoint;
    bool isWorking;
    int stackCount = 5;
    int humanLimit = 20;

    void Start()
    {
        StartCoroutine(GenerateHuman());    
    }
    public void RemoveLast()
    {
        if(humanList.Count > 0)
        {
            Destroy(humanList[humanList.Count - 1]);
            humanList.RemoveAt(humanList.Count - 1);
        }
    }
    IEnumerator GenerateHuman()
    {
        while(true)
        {
            float humanCount = humanList.Count;
            int rowCount = (int)humanCount / stackCount;
            if(isWorking)
            {
                GameObject temp = Instantiate(humanPrefab);
                temp.transform.position = new Vector3(exitPoint.position.x + ((float)rowCount%4)*2, (humanCount%stackCount), exitPoint.position.z);
                humanList.Add(temp);
                if(humanList.Count >= humanLimit)
                {
                    isWorking = false;
                }
            }
            else if(humanList.Count < humanLimit)
            {
                isWorking = true;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
