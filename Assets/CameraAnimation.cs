using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    public Animator camAnimator;

    private string trigger = "CameraAnimation";
    //private bool isHit = false;

    private void Awake()
    {
        MusicManager.MarkerUpdated += WaitForMarker;
    }

    private void OnDestroy()
    {
        MusicManager.MarkerUpdated -= WaitForMarker;
    }
    // Start is called before the first frame update
    void Start()
    {
        //isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(isHit) { camAnimator.SetTrigger("AnimTrigger"); }
    }

    private void WaitForMarker()
    {
        string lastMarker = MusicManager.instance.timelineInfo.lastMarker;

        if (lastMarker.Contains(trigger))
        {
            camAnimator.SetTrigger("AnimTrigger");
        }

      
    }
}
