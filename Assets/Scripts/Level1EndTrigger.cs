using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1EndTrigger : MonoBehaviour {
    public GameObject[] objectsToDisable;
    public GameObject[] objectsToEnable;
    public SimpleSmoothMouseLook mouseLook;
    public PlayerMovement playerMovement;
    public float EndDelay = 3f;
    public Text endText;

    void OnTriggerEnter (Collider other) {
        mouseLook.ApplyJudder = false;
        playerMovement.ApplyLaserForce = false;
        foreach (GameObject obj in objectsToDisable) {
            obj.SetActive (false);
        }
        foreach (GameObject obj in objectsToEnable) {
            obj.SetActive (true);
        }
        endText.text = "Goal! Your score: $"+GameManager.instance.score+" in damages, rank "+GameManager.instance.Grade();
        Invoke("EndLevel", EndDelay);
    }

    void EndLevel(){
        GameManager.instance.EndLevel();
    }
}