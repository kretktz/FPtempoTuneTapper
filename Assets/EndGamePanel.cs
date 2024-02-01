using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    public static bool isVisible = false;

    public Text scoreText;

    public GameObject endGameUI;
    private void Awake()
    {
        //subscribe to music manager

        MusicManager.MarkerUpdated += WaitForMarker;
    }

    private void OnDestroy()
    {
        //unsubscribe to music manager
        MusicManager.MarkerUpdated -= WaitForMarker;
    }

    private void WaitForMarker()
    {
        string lastMarker = MusicManager.instance.timelineInfo.lastMarker;

        if (lastMarker.Contains("trackEnd"))
        {
            isVisible = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isVisible)
        {
            endGameUI.SetActive(true);
            scoreText.text = "STAGE COMPLETE" + "\n" + "YOUR SCORE: " + ScoreKeeper.instance.FetchScore();
        }
        else
        {
            endGameUI.SetActive(false);
        }
    }
}
