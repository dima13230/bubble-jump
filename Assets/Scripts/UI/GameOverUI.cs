using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Text CurrentScoreLabel;
    public Text RecordScoreLabel;

    private void Awake()
    {
        GameProgress progress = GameProgressManager.LoadValues();

        if (GameManager.instance.Score > progress.Record)
        {
            GameProgressManager.SaveValues(GameManager.instance.Score);
            progress.Record = GameManager.instance.Score;
        }

        if (CurrentScoreLabel)
            CurrentScoreLabel.text = "Ваш результат: " + GameManager.instance.Score;

        if (RecordScoreLabel)
            RecordScoreLabel.text = "Рекорд: " + progress.Record;
    }
}
