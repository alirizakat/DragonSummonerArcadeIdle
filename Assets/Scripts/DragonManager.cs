using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragonManager : MonoBehaviour
{
    public List<GameObject> humanList = new List<GameObject>();
    List<GameObject> skullList = new List<GameObject>();
    public Transform givePoint, skullPoint;
    public GameObject humanPrefab, skullPrefab;
    public Animator dragonAnimator;

    void OnEnable()
    {
        gameObject.transform.DOScale(new Vector3(1,1,1), 1.0f);
        dragonAnimator.SetTrigger("scream");
    }
    public void GetHuman()
    {
        GameObject temp = Instantiate(humanPrefab);
        temp.transform.position = new Vector3(givePoint.position.x,((float)humanList.Count)+ 0.5f,givePoint.position.z);
        humanList.Add(temp);
    }
    void Start()
    {
        StartCoroutine(GenerateSkull());
    }
    IEnumerator GenerateSkull()
    {
        while(true)
        {
            if(humanList.Count > 0)
            {
                GameObject temp = Instantiate(skullPrefab);
                temp.transform.position = new Vector3(skullPoint.position.x,((float)skullList.Count),skullPoint.position.z);
                skullList.Add(temp);
                dragonAnimator.SetTrigger("eat");
                RemoveLast();
            }
            else
            {
                dragonAnimator.SetTrigger("idle");
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
}
