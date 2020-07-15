using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public float score;
    public int currentLevel = 1;
    public float MouseSensitivity = 2;
    public float RecoilStrength = .5f;
    public AudioMixer audioMixer;
    private Text scoreText;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
        DontDestroyOnLoad (gameObject);
    }

    void Start () {
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll (typeof (GameObject))) {
            if (obj.name == "ScoreBox") {
                scoreText = obj.GetComponent<Text> ();
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (scoreText != null) {
            scoreText.text = "Damages: $" + score;
        }
    }

    public void EndLevel () {
        currentLevel++;
        if (currentLevel > 1) {
            SceneManager.LoadScene (1);
            // Application.Quit ();
            // Debug.Log ("Quit!");
        } else {
            SceneManager.LoadScene (currentLevel);
        }
    }

    public void SetMasterVolume (float volume) {
        audioMixer.SetFloat ("VolMaster", volume);
    }

    public void SetSFXVolume (float volume) {
        audioMixer.SetFloat ("VolSFX", volume);
    }

    public void SetMusicVolume (float volume) {
        audioMixer.SetFloat ("VolMusic", volume);
    }

    public void SetMouseSensitivity (float sens) {
        MouseSensitivity = sens;
    }

    public void SetRecoilStrength (float strength) {
        RecoilStrength = strength;
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
        score = 0;
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll (typeof (GameObject))) {
            if (obj.name == "ScoreBox") {
                scoreText = obj.GetComponent<Text> ();
            }
        }
    }

    public string Grade(){
        if (score < 1000){
            return "S";
        }else if (score < 3000){
            return "A";
        }else if (score < 5000){
            return "B";
        }else if (score < 10000){
            return "C";
        }else if (score < 15000){
            return "D";
        }else{
            return "F";
        }
    }
}