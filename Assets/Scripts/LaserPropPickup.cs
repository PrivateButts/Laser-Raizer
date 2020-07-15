using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPropPickup : MonoBehaviour {
    public GameObject[] ThingsToEnable;
    public GameObject[] ThingsToDestroy;
    public GameObject[] EnableOnTrigger;
    public GameObject[] DisableOnTrigger;
    public SimpleSmoothMouseLook mouseLook;
    public PlayerMovement playerMovement;

    private bool playerInZone = false;

    // Update is called once per frame
    void Update () {
        if (playerInZone && Input.GetButtonDown ("Fire1")) {
            foreach (GameObject thing in ThingsToEnable) {
                thing.SetActive (true);
            }
            mouseLook.ApplyJudder = true;
            playerMovement.ApplyLaserForce = true;
            foreach (GameObject thing in ThingsToDestroy) {
                Destroy (thing);
            }
        }
    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.CompareTag ("Player")) {
            playerInZone = true;
        }
        foreach (GameObject thing in EnableOnTrigger) {
            thing.SetActive (true);
        }
        foreach (GameObject thing in DisableOnTrigger) {
            thing.SetActive (false);
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.gameObject.CompareTag ("Player")) {
            playerInZone = false;
        }
        foreach (GameObject thing in EnableOnTrigger) {
            thing.SetActive (false);
        }
        foreach (GameObject thing in DisableOnTrigger) {
            thing.SetActive (true);
        }
    }
}