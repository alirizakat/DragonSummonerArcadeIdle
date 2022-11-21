using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple prefab generation script. this script creates 4 stacks of 5, 
//limit count and stack count can be changed easily, but if you change it check GenerateHuman method too. 
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
                temp.transform.position = new Vector3(exitPoint.position.x + ((float)rowCount%4)*2, (humanCount%stackCount)+0.5f, exitPoint.position.z);
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
            //change this float number if you want faster or slower generation.
            yield return new WaitForSeconds(1.0f);
        }
    }
}
