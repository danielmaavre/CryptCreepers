using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] TMP_Text fScore;
    public AudioSource buttonAudioClip;
    private void Start()
    {
        int score = PlayerPrefs.GetInt("Score",0);
        fScore.text = "" + score;
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void playButton()
    {
        buttonAudioClip.Play();
        Invoke("NewGame", 0.5f);
    }

}
