using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//This script handles all things dragons do in the game for now.
//Dotween is added for scaling up animation in the beginning but anything else can be used instead of dotween
//This script instantiates human prefabs in front of the dragon
//Destroys the human prefab, creates skulls, also checks the dragons' hit point and disables itself to a buy spot at given moment
//Could be different scripts but since it's not much, should be okay. 
public class DragonManager : MonoBehaviour
{
    public List<GameObject> humanList = new List<GameObject>();
    List<GameObject> skullList = new List<GameObject>();
    public Transform givePoint, skullPoint;
    public GameObject humanPrefab, skullPrefab;
    public Animator dragonAnimator;
    public GameObject buyArea;
    public int dragonHitPoint = 10;

    //We have to use on enable since dragons can die and come back. using start would not work in setting active and disabling
    void OnEnable()
    {
        dragonHitPoint = 10;
        gameObject.transform.DOScale(new Vector3(1,1,1), 1.0f);
        dragonAnimator.SetTrigger("scream");
        StartCoroutine(GenerateSkull());
    }

    public void GetHuman()
    {
        GameObject temp = Instantiate(humanPrefab);
        temp.transform.position = new Vector3(givePoint.position.x,((float)humanList.Count)+ 0.5f,givePoint.position.z);
        humanList.Add(temp);
    }

    IEnumerator GenerateSkull()
    {
        while(true)
        {
            if(humanList.Count > 0)
            {
                GameObject temp = Instantiate(skullPrefab);
                temp.transform.position = new Vector3(skullPoint.position.x,10.0f,skullPoint.position.z);
                skullList.Add(temp);
                dragonAnimator.SetTrigger("eat");
                RemoveLast();
            }
            yield return new WaitForSeconds(1.5f);
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
    void Update()
    {
        CheckHitPoints();
    }
    void CheckHitPoints()
    {
        if(dragonHitPoint <= 0)
        {
            buyArea.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
