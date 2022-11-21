using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a bit tricky and needs some improvement since there is currently small errors in the movement logic
//What minion does for movement is collecting bodies, picking a dragon point randomly from the list, going to drop, returning to collect
//Only issue in here is, all dragons can be slayed before minion can pick one or it can die in the moment minion trying to pick
//The order in update is also really important in here. Be careful if you change or upgrade sth in here. 
public class MinionMovement : MonoBehaviour
{
    public List<GameObject> dropPoints = new List<GameObject>();
    private static AnimatorManager animatorManager;
    public int collectedBody;
    public Transform collectPoint;
    public float speed;
    int randomNumber;
    bool doneOnce;
    bool canMove;
    private int minionCollectLimit;
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        canMove = true;
        minionCollectLimit = gameObject.GetComponent<MinionCollectManager>().humanLimit;
    }
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
    //The idea in here is first creating a list of objects that we look for as a tempList
    //We create the temp list because if we lose all the dragons at some point there are no give areas
    //Which causes referance errors, so we check on the first list and if the count is bigger than 0 we actually add those points to list
    //Trick is using the canMove boolean, minion stops at gathering point if there are no dragons available, and moves again at GoToDrop() 
    void FindDropPoints()
    {
        dropPoints.Clear();
        List<GameObject> tempList = new List<GameObject>();
        tempList = new List<GameObject>(GameObject.FindGameObjectsWithTag("GiveArea"));

        if (tempList.Count > 0)
        {
            canMove = true;
            foreach (GameObject drPoint in GameObject.FindGameObjectsWithTag("GiveArea"))
            {
                dropPoints.Add(drPoint);
            }
            PickRandomDropPoint();
        }
        else
        {
            canMove = false;
        }
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
        //double checking is always a good idea but it's unnecessary in here i can say.
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
        if(collectedBody == minionCollectLimit && canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, dropPoints[randomNumber].transform.position, speed * Time.deltaTime);
            transform.LookAt(dropPoints[randomNumber].transform);
        }
        else
        {
            return;
        }
    }
    void KeepCollectCount()
    {
        collectedBody = gameObject.GetComponent<MinionCollectManager>().humanList.Count;
    }
}
