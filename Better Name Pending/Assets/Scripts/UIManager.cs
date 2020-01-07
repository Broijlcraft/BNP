using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public AudioClip buttonSound;
    private Scene mainLevel;
    public GameObject startMenu, optionMenu;
    public void Awake() 
    {
        mainLevel = SceneManager.GetSceneByName("Main Level");
        startMenu.SetActive(true);
        optionMenu.SetActive(false);
    }
    public void StartButton()
    {
        LoadNewLevel(mainLevel);
    }
    public void Options()
    {
        AudioManager.PlaySound(buttonSound);
        startMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    public void Back()
    {
        AudioManager.PlaySound(buttonSound);
        startMenu.SetActive(true);
        optionMenu.SetActive(false);
    }
    public void LoadNewLevel(Scene sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad.name);
    }
}
