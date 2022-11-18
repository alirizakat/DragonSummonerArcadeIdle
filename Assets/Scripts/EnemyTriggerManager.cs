using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerManager : MonoBehaviour
{
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
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
