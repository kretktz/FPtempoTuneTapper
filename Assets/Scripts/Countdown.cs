using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Text countdownText;
    private string countdownNumber;
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
        if(isVisible)
        {
            countdownText.text = "GET READY" + "\n" + countdownNumber;
        }
        else
        {
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
}
