using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameObject tapToPlay, player, humanGenerator, camera;
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
