using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script mainly controls the animations of the each character in the game
//Mostly it's unnecessary right now, if I decide to make the project a bit bigger I'll make some changes on here.
public class AnimatorManager : MonoBehaviour
{
    private Animation animation;
    private Animator dragonAnimator;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.CompareTag("GiveArea"))
        {
            dragonAnimator = gameObject.GetComponent<Animator>();
        }
        if(gameObject.CompareTag("Player"))
        {
            animation = gameObject.GetComponentInChildren<Animation>();
        }
        else
        {
            animation = gameObject.GetComponent<Animation>();
        }
 
    }
    //All these functions are the animations of the minions and player
    //Each minion and player has animation component on it, which can be called in here
    //Using a switch statement in just one function could be a much better idea. note to myself.
    
    //Animations are called from animator manager in different scripts.
    public void MoveForward()
    {
        animation.CrossFade("move_forward");
    }
    public void Attack()
    {
        animation.CrossFade("attack_short_001");
    }
    public void Idle()
    {
        animation.CrossFade("idle_normal");
    }
    public void Dead()
    {
        animation.CrossFade("dead");
    }
}
