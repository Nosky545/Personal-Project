using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public GameObject gameScreen;

    public GameObject powerupUI;
    public Slider powerupSlider;

    public bool isGameActive;
    
    public TextMeshProUGUI enemyLeftText;
    public TextMeshProUGUI roundText;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        isGameActive = false;
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        isGameActive = true;
        titleScreen.SetActive(false);
        gameScreen .SetActive(true);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
