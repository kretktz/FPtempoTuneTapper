using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public GameObject inGameUI;


    // Update is called once per frame
    void Update()
    {
        if(EndGamePanel.isVisible == true)
        {
            inGameUI.SetActive(false);
        }
    }
}
