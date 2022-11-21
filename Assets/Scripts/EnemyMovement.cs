using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is enemy movement script.
//Hit points is not a variable named perfectly tbh., hit points creates a list of dragons that are currently available.
//This script most probably is problematic and please feel free to reach me if you've ever used this project.
//How this script works is each dragon has points around them and minions randomly pick one and go to that point.
//Best way to do it is probably generating a point inside of the collider bounds of the dragon obj. 
public class EnemyMovement : MonoBehaviour
{
    public List<GameObject> hitPoints = new List<GameObject>();
    private static AnimatorManager animatorManager;
    public float speed;
    int randomNumber;
    bool isEnemySelected;
    private Vector3 goPoint;
    private GameObject lookPoint;

    // Start is called before the first frame update
    void Start()
    {
        //there is a object called look point in the hierarchy, it's just a replacement object for not to recieving null ref. errors.
        //it can be changed with anything, player, other minion etc. etc.
        lookPoint = GameObject.Find("LookPoint");
        animatorManager = gameObject.GetComponent<AnimatorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        FindHitPoints();
        FixGoPoint();
        GoToAttack();
    }
    //Each point around the dragons has the attack points tag, can be also used if we also add aggressive minions for our side.  
    void FindHitPoints()
    {
        hitPoints.Clear();
        foreach (GameObject htPoint in GameObject.FindGameObjectsWithTag("AttackPoints"))
        {
            hitPoints.Add(htPoint);
        }
    }
    //sometimes minions kill all the dragons before we do sth, in that situation to not recieve errors if hit points is null we return
    void FixGoPoint()
    {
        if(hitPoints == null)
        {
            return;
        }
        goPoint = GetRandomPoint();
    }
    Vector3 GetRandomPoint()
    {
        if(!isEnemySelected) 
        {
            lookPoint = hitPoints[Random.Range(0, hitPoints.Count)];
            var point = lookPoint.transform.position;
            isEnemySelected = true;
            return point;
        }
        else
        {
            var point = goPoint;
            return point;
        }
    }
    //transform.LookAt is not giving the best visual look when they reach to the point they are going but it's an okay option 
    void GoToAttack()
    {
        if(hitPoints == null)
        {
            return;
        }
        if(transform.position != goPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, goPoint, speed * Time.deltaTime);
            animatorManager.MoveForward();
            transform.LookAt(lookPoint.transform);
        }
    }
}
