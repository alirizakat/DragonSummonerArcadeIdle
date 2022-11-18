using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject enemyPrefab;
    public Transform exitPoint;
    int enemyLimit = 5;
    bool isWorking;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateEnemy());
    }

    IEnumerator GenerateEnemy()
    {
        while(true)
        {
            float enemyCount = enemyList.Count;
            if(isWorking)
            {
                GameObject temp = Instantiate(enemyPrefab);
                temp.transform.position = exitPoint.position;
                enemyList.Add(temp);
                if(enemyList.Count >= enemyLimit)
                {
                    isWorking = false;
                }
            }
            else if(enemyList.Count < enemyLimit)
            {
                isWorking = true;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
    void Update()
    {
        FixEnemyList();
    }
    void FixEnemyList()
    {
        enemyList.Clear();
        if(enemyList.Count < enemyLimit)
        {
            foreach (GameObject enmy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemyList.Add(enmy);
            }
        }
    }
}
