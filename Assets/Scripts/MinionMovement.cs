using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    public List<GameObject> dropPoints = new List<GameObject>();
    private static AnimatorManager animatorManager;
    public int collectedBody;
    public Transform collectPoint;
    public float speed;
    int randomNumber;
    bool doneOnce;
    // Start is called before the first frame update
    void Start()
    {
        animatorManager = gameObject.GetComponent<AnimatorManager>();
    }
    void Update()
    {
        GoToCollect();
        FindDropPoints();
        KeepCollectCount();
        GoToDrop();        
        IdleAtCollectPoint();
    }
    void GoToCollect()
    {
        if(collectedBody == 0)
        {
            doneOnce = false;
            transform.position = Vector3.MoveTowards(transform.position, collectPoint.position, speed * Time.deltaTime);
            animatorManager.MoveForward();
            transform.LookAt(collectPoint);
        }
    }
    void IdleAtCollectPoint()
    {
        if(gameObject.transform.position == collectPoint.position)
        {
            animatorManager.Idle();
        }
    }
    void FindDropPoints()
    {
        dropPoints.Clear();
        foreach (GameObject drPoint in GameObject.FindGameObjectsWithTag("GiveArea"))
        {
            dropPoints.Add(drPoint);
        }
        PickRandomDropPoint();
    }
    void PickRandomDropPoint()
    {
        if(!doneOnce)
        {
            randomNumber = Random.Range(0, dropPoints.Count);
            doneOnce = true;
        }
        else
        {
            return;
        }
        
        if(randomNumber > 0)
        {
            GoToDrop();
        }
        else
        {
            return;
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
    void KeepCollectCount()
    {
        collectedBody = gameObject.GetComponent<MinionCollectManager>().humanList.Count;
    }
}
