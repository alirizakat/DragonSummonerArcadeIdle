using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script creates enemy,for increase or reduce the time enemy created, change the amount in GenerateEnemy(), WaitForSeconds() part 
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
                //exit point is a random picked point i have picked on editor
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
            //change this to change creation time
            yield return new WaitForSeconds(1.0f);
        }
    }
    void Update()
    {
        FixEnemyList();
    }
    //since enemies die pretty easily, we always clear the list and re add all the enemies
    //not the best way of doing it but it's a clever way for keeping count if you really need the amount like in this example.
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
