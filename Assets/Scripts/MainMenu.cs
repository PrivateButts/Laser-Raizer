using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour {
    public TMP_Text VersionText;

    private void Start() {
        VersionText.text = "GMTK Game Jam 2020 Edition, v"+Application.version;
        // Just in case
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame(){
        GameManager.instance.EndLevel();
    }

    public void QuitGame(){
        Application.Quit();
        Debug.Log("Quit!");
    }
}