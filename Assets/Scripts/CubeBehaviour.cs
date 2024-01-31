using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;

    private Vector3 scaleChange = new Vector3(-0.1f, -10f, -0.1f);

    [SerializeField]
    private bool waitingForString = true;

    [SerializeField]
    private string stringToWaitFor = "start";

    [SerializeField]
    private int pulseInterval = 4;

    private int nextPulse = 0;

    private void Awake()
    {
        waitingForString = false;
        nextPulse = pulseInterval;

        //subscribe to music manager
        
        MusicManager.BeatUpdated += CubeAnimation;
        MusicManager.MarkerUpdated += WaitForMarker;
    }

    private void OnDestroy()
    {
        //unsubscribe to music manager
        MusicManager.MarkerUpdated -= WaitForMarker;
        MusicManager.BeatUpdated -= CubeAnimation;
    }

    private void CubeAnimation()
    {
        if(!waitingForString)
        {
            if(MusicManager.lastBeat % 2 == 0)
            {
                cube.transform.localScale += scaleChange;
            }
            else
            {
                cube.transform.localScale -= scaleChange;
            }
            
        }
        
    }

    private void WaitForMarker()
    {
        if(MusicManager.instance.timelineInfo.lastMarker == stringToWaitFor)
        {
            waitingForString = false;
        }
    }
}
