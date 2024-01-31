using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickDetection : MonoBehaviour
{
    Color[] colors = new Color[] { Color.red, Color.green, Color.blue };
    private int currentColor, length;

    Camera m_Camera;



    void Awake()
    {
        m_Camera = Camera.main;

        MusicManager.MarkerUpdated += ClickScore;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentColor = 0; //Red
        length = colors.Length;
        GetComponent<Renderer>().material.color = colors[currentColor];
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Use the hit variable to determine what was clicked on.
                Debug.Log("Click!");

                currentColor = (currentColor + 1) % length;
                GetComponent<Renderer>().material.color = colors[currentColor];
            }
        }
    }

    private void OnMouseDown()
    {
        GameObject gameObject = this.gameObject;
    }

    private void OnDestroy()
    {
        MusicManager.MarkerUpdated -= ClickScore;
    }

    private void ClickScore()
    {
        if (MusicManager.instance.timelineInfo.lastMarker == "snare01_goodin" ||
            MusicManager.instance.timelineInfo.lastMarker == "snare01" ||
            MusicManager.instance.timelineInfo.lastMarker == "snare01_goodout")
        {
            Debug.Log("Good Hit");
        }
        else
        {
            Debug.Log("Miss");
        }
    }
}
