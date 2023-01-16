using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAndEndGameManager : MonoBehaviour
{

    public bool isStartMenu = true;
    // Start is called before the first frame update
    void Start()
    {
        if (isStartMenu)
            SoundManager.Instance.PlayMusic(Sound.MAIN_MUSIC);
        else
            SoundManager.Instance.PlayMusic(Sound.END_MUSIC);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonPressed()
    {
        SceneManager.LoadScene("Overworld");
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    public void MainMenu()
    {       
        SceneManager.LoadScene("Start");
    }
}
