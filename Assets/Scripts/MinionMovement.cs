using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    public List<GameObject> dropPoints = new List<GameObject>();
    public int collectedBody;
    public Transform collectPoint;
    public float speed;
    int randomNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        GoToCollect();
        GoToDrop();
        KeepCollectCount();
        FindDropPoints();
    }
    void GoToCollect()
    {
        if(collectedBody == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, collectPoint.position, speed * Time.deltaTime);
            transform.LookAt(collectPoint);
            PickRandomDropPoint();
        }
    }
    void FindDropPoints()
    {
        dropPoints.Clear();
        foreach (GameObject drPoint in GameObject.FindGameObjectsWithTag("GiveArea"))
        {
            dropPoints.Add(drPoint);
        }
    }
    void GoToDrop()
    {
        if(collectedBody == 6)
        {
            transform.position = Vector3.MoveTowards(transform.position, dropPoints[randomNumber].transform.position, speed * Time.deltaTime);
            transform.LookAt(dropPoints[randomNumber].transform);
        }
    }
    void PickRandomDropPoint()
    {
        randomNumber = Random.Range(0, dropPoints.Count);
    }
    void KeepCollectCount()
    {
        collectedBody = gameObject.GetComponent<MinionCollectManager>().humanList.Count;
    }
}