using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//even while not using events using a trigger manager is always a good idea.
//minions die at the moment they collide with player, it can be changed into many things within dead routine
//when they reach to attack points they stay there and attack until they die. 
//currently each minion reduces only 1 hitpoint in it's life, can be easily changed, i haven't done that for testing purposes.
public class EnemyTriggerManager : MonoBehaviour
{   
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DeadRoutine());
        }
    }
    IEnumerator DeadRoutine()
    {
        var movement = gameObject.GetComponent<EnemyMovement>();
        movement.enabled = false;
        yield return new WaitForSeconds(1.0f);
        var animation = gameObject.GetComponent<AnimatorManager>();
        animation.Dead();
    }
    bool doOnce;
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("GiveArea"))
        {
            var animation = gameObject.GetComponent<AnimatorManager>();
            animation.Attack();
            if(!doOnce)
            {
                other.gameObject.GetComponent<DragonManager>().dragonHitPoint--;
                doOnce = true;
            }
        }
    }
}
