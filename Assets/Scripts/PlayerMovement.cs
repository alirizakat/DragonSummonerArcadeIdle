using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed=5;
    [SerializeField] private float rotationSpeed = 500;
    private static AnimatorManager animatorManager;
    private Touch _touch;

    private Vector3 _touchDown;
    private Vector3 _touchUp;

    private bool _dragStarted;
    private bool _isMoving;

    [SerializeField] private float _zLimit;
    [SerializeField] private float _xLimit;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        animatorManager = gameObject.GetComponent<AnimatorManager>();
    }
    //this script doesn't work on Game window, since game window doesn't count mouse clicks as touches
    //use Simulator screen or Unity Remote to use the script effectively.
    //create another movement speed value and multiply it with movement speed in line 56, if you want to enhance movement speed in some other way. 
    void Update()
    {
        Movement();
        Borders();
    }
    public bool IsMoving()
    {
        return _isMoving;
    }
    public void Movement()
    {
        if (Input.touchCount > 0)
        {
            animatorManager.MoveForward();
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                _dragStarted = true;
                _isMoving = true;
                _touchUp = _touch.position;
                _touchDown = _touch.position;
            }
        }
        if (_dragStarted)
        {
            if (_touch.phase == TouchPhase.Moved)
            {
                _touchDown = _touch.position;
            }

            if (_touch.phase == TouchPhase.Ended)
            {
                _touchDown = _touch.position;
                _isMoving = false;
                _dragStarted = false;
                animatorManager.Idle();
            }
            gameObject.transform.rotation=Quaternion.RotateTowards(transform.rotation,CalculateRotation(),rotationSpeed*Time.deltaTime);
            gameObject.transform.Translate(Vector3.forward*Time.deltaTime*movementSpeed);
        }
    }
    //This function is used for games that doesn't have walls or etc. 
    //Having a border function is helpful for game to not to break if user pushes for it.
    void Borders()
    {
        if(transform.position.z >= _zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _zLimit); 
        }
        else if(transform.position.z <= -_zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -_zLimit); 
        }

        if(transform.position.x >= _xLimit)
        {
            transform.position = new Vector3(_xLimit, transform.position.y, transform.position.z); 
        }
        else if(transform.position.x <= -_xLimit)
        {
            transform.position = new Vector3(-_xLimit, transform.position.y, transform.position.z); 
        }
    }
    Quaternion CalculateRotation()
    {
        Quaternion temp = Quaternion.LookRotation(CalculateDirection(),Vector3.up);
        return temp;
    }
    //if we want a movement with, when user swipes down player moves up
    //just keep the temp value in CalculateDirection() positive, otherwise keep it negative.
    Vector3 CalculateDirection()
    {
        Vector3 temp =(_touchDown - _touchUp).normalized;
        temp.z = temp.y;
        temp.y = 0;
        return -temp;
    }
}
