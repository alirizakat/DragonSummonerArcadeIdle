using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//for now game manager is a really simple script, it does various stuff which can be seperated into other scripts but also can be done in here imo. not all that spaghetti
//what game manager does for now is, it handles the camera animation in the start of the game, 
//which is done by points in the hierarchy (camPosPreStart)
//also enemy generator is enabled when we have our own minion, it's controlled in here.
//Game Manager checks if we have the minion or not, it can be done with a simple boolean coming from minion and would save some memory
//However i'm planning to have multiple minions also on our side, or minions doing different stuff from each other. so checking for it is not much of a stuff.

public class GameManager : MonoBehaviour
{
    public GameObject tapToPlay, player, humanGenerator, camera, skullCount;
    public bool gameStarted;
    bool doOnce;
    bool sendMinion;
    public Transform camPosPreStart, camPosStart; 
    public GameObject enemyGenerator;
    // Start is called before the first frame update
    void Start()
    {
        camera.GetComponent<CameraFollow>().enabled = false;
        camera.transform.position = camPosPreStart.position;
        camera.transform.rotation = camPosPreStart.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        TapToPlay();
        CheckForMinion();
    }
    void StartGame()
    {
        if(gameStarted)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            humanGenerator.GetComponent<HumanGenerator>().enabled = true;
            tapToPlay.SetActive(false);
            skullCount.SetActive(true);
        }
    }
    void TapToPlay()
    {
        if(Input.GetMouseButton(0) && !doOnce)
        {
            StartCoroutine(StartRoutine());
        }
    }
    IEnumerator StartRoutine()
    {
        camera.transform.DOMove(camPosStart.position, 1.0f);
        camera.transform.DORotate(new Vector3(45,180,0), 1.0f);
        yield return new WaitForSeconds(1.0f);
        camera.GetComponent<CameraFollow>().enabled = true;
        doOnce = true;
        gameStarted = true;
        StartGame();
    }
    void CheckForMinion()
    {
        GameObject temp = GameObject.Find("Minion");
        if(temp == null)
        {
            return;
        }
        if(temp.activeInHierarchy && !sendMinion)
        {
            enemyGenerator.SetActive(true);
            sendMinion = true;
        }
    }

}
