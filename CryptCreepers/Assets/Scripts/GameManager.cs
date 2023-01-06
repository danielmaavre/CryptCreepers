using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int time = 30;
    public int difficulty = 1;
    [SerializeField] int score;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            UIManager.Instance.UpdateUIScore(score);
            if(score%1000 == 0)
            {
                difficulty++;
            }            
        }
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        UIManager.Instance.UpdateUITime(time);
        StartCoroutine(CountDownRoutine());
    }

    IEnumerator CountDownRoutine()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            UIManager.Instance.UpdateUITime(time);
        }    
        UIManager.Instance.ShowGameOverScreen();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void NewGame()
    {
        UIManager.Instance.StartGame(false);
        SceneManager.LoadScene("Game");
    }


}
