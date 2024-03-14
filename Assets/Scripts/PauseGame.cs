using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject health;
    public GameObject pauseMenu;
    public GameObject coins;

    public void Pause()
    {
        health.SetActive(false);
        coins.SetActive(false);
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        health.SetActive(true);
        coins.SetActive(true);
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
