using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Canvas IngameUI;
    public Canvas GameOverUI;
    public Canvas PauseUI;

    public int Score {
        get
        {
            return (int)(MaxPlayerHeight * 20);
        }
    }
    public float MaxPlayerHeight { get; private set; }

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        Time.timeScale = 1;
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            Debug.LogError("Не удалось найти объект игрока!");
            return;
        }

        if (player.transform.position.y > MaxPlayerHeight)
        {
            MaxPlayerHeight = player.transform.position.y;
        }
        if (Camera.main.WorldToViewportPoint(player.transform.position).y < 0)
            GameOver();
    }

    public void Pause()
    {
        if (!DebugUtil.AssertNotNull(PauseUI, IngameUI ))
        {
            Debug.LogError("Невозможно поставить на паузу! PauseUI и/или IngameUI не заданы в GameManager!");
            return;
        }
        Time.timeScale = 0;
        PauseUI.gameObject.SetActive(true);
        IngameUI.gameObject.SetActive(false);
    }

    public void Resume()
    {
        if (!DebugUtil.AssertNotNull(PauseUI, IngameUI))
        {
            Debug.LogError("Невозможно возобновить игру! PauseUI и/или IngameUI не заданы в GameManager!");
            return;
        }
        Time.timeScale = 1;
        PauseUI.gameObject.SetActive(false);
        IngameUI.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Exit()
    {
        Resume();
        SceneManager.LoadSceneAsync(0);
    }

    public void GameOver()
    {
        if (!DebugUtil.AssertNotNull(PauseUI, IngameUI))
        {
            Debug.LogError("Невозможно показать GameOverUI! GameOverUI и/или IngameUI не заданы в GameManager!");
            return;
        }
        Time.timeScale = 0;
        GameOverUI.gameObject.SetActive(true);
        IngameUI.gameObject.SetActive(false);
    }
}
