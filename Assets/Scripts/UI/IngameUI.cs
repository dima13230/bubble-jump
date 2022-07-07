using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    public Text ScoreLabel;

    // Update is called once per frame
    void Update()
    {
        if (ScoreLabel)
            ScoreLabel.text = GameManager.instance.Score.ToString();
    }
}
