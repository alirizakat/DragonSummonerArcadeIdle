using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<GameObject> hitPoints = new List<GameObject>();
    private static AnimatorManager animatorManager;
    public float speed;
    int randomNumber;
    bool isEnemySelected;
    private Vector3 goPoint;
    public GameObject lookPoint;

    // Start is called before the first frame update
    void Start()
    {
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

    void FindHitPoints()
    {
        hitPoints.Clear();
        foreach (GameObject htPoint in GameObject.FindGameObjectsWithTag("AttackPoints"))
        {
            hitPoints.Add(htPoint);
        }
    }
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
