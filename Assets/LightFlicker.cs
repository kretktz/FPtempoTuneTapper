using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    Light pointLight;

    private void Awake()
    {
        //subscribe to music manager
        MusicManager.BeatUpdated += LightAnimation;
    }

    private void OnDestroy()
    {
        MusicManager.BeatUpdated -= LightAnimation;
    }

    private void LightAnimation()
    {
        if (MusicManager.lastBeat % 2 == 0)
        {
            pointLight.intensity = 200;
        }
        else
        {
            pointLight.intensity = 10;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        pointLight = GetComponent<Light>();
    }

}
