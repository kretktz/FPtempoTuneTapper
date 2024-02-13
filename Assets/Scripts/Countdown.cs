using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Text countdownText;
    public Text stageText;

    private string countdownNumber;
    private string stageNumber;

    private bool isVisible = false;
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

        if (lastMarker.Contains("Countdown"))
        {
            isVisible = true;
        }
        else
        { 
            isVisible = false; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        countdownNumber = GetCountdown();
        stageNumber = GetStage();

        if(isVisible)
        {
            stageText.text = "STAGE " + stageNumber;
            countdownText.text = "GET READY" + "\n" + countdownNumber;
        }
        else
        {
            stageText.text = "";
            countdownText.text = "";
        }
        
    }

    string GetCountdown()
    {
        string lastHitMarker = MusicManager.instance.timelineInfo.lastMarker;
        string cno = "";

        if (lastHitMarker.Contains("Countdown1"))
        {
            cno = "1";
        }
        if (lastHitMarker.Contains("Countdown2"))
        {
            cno = "2";
        }
        if (lastHitMarker.Contains("Countdown3"))
        {
            cno = "3";
        }

        return cno;
    }

    string GetStage()
    {
        string lastMarker = MusicManager.instance.timelineInfo.lastMarker;
        string sno = "";

        if (lastMarker.Contains("Stage01"))
        {
            sno = "1";
        }
        if (lastMarker.Contains("Stage02"))
        {
            sno = "2";
        }
        if (lastMarker.Contains("Stage03"))
        {
            sno = "3";
        }

        return sno;
    }
}
