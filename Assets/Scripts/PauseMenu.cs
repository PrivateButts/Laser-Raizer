using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static PauseMenu instance = null;
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public List<MonoBehaviour> MBDisableOnPause = new List<MonoBehaviour>();
    public List<GameObject> GODisableOnPause = new List<GameObject>();


    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
        DontDestroyOnLoad (gameObject);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown (KeyCode.Escape) && GameManager.instance.currentLevel > 0) {
            if (GameIsPaused) {
                Resume ();
            } else {
                Pause ();
            }
        }
    }

    public void Resume () {
        PauseMenuUI.SetActive (false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        foreach (MonoBehaviour item in MBDisableOnPause) {
            item.enabled = true;
        }

        foreach (GameObject item in GODisableOnPause) {
            item.SetActive (true);
        }
    }

    public void Pause () {
        PauseMenuUI.SetActive (true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // Just in case
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        foreach (MonoBehaviour item in MBDisableOnPause) {
            item.enabled = false;
        }

        foreach (GameObject item in GODisableOnPause) {
            item.SetActive (false);
        }

        transform.Find("PauseMenu/Settings/MouseSensSld").GetComponent<Slider>().value = GameManager.instance.MouseSensitivity;
    }

    public void QuitGame(){
        Application.Quit();
        Debug.Log("Quit!");
    }

    public void SetMasterVolume (float volume) {
        GameManager.instance.SetMasterVolume(volume);
    }

    public void SetSFXVolume (float volume) {
        GameManager.instance.SetSFXVolume(volume);
    }

    public void SetMusicVolume (float volume) {
        GameManager.instance.SetMusicVolume(volume);
    }

    public void SetMouseSensitivity (float sens) {
        GameManager.instance.SetMouseSensitivity(sens);
    }

    public void SetRecoilStrength (float strength) {
        GameManager.instance.SetRecoilStrength(strength);
    }

    void OnEnable () {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable () {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode) {
        MBDisableOnPause.Clear();
        GODisableOnPause.Clear();
    }
}