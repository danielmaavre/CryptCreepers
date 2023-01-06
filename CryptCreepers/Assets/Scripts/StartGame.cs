using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string LevelName;
    public AudioSource playButton;

    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelName);
    }

    public void buttonSound()
    {
        playButton.Play();
        Invoke("LoadLevel", 0.5f);
    }
}
