using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void MoveForward()
    {
        animation.CrossFade("move_forward");
    }
    public void Collect()
    {
        animation.CrossFade("attack_short_001");
    }
    public void Idle()
    {
        animation.CrossFade("idle_normal");
    }
}
