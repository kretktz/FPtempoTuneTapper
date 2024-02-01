using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper instance;

    public Text scoreText;

    int score = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    public void AddGoodPoint()
    {
        score += 1;
        scoreText.text = "SCORE: " + score.ToString();
    }

    public void AddPerfectPoint()
    {
        score += 3;
        scoreText.text = "SCORE: " + score.ToString();
    }

    public string FetchScore()
    {
        return score.ToString();
    }
}
