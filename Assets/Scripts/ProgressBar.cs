using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private float duration;

    private float fill = 0f;
    private float speed;

    private void Start()
    {
        speed = ((duration / 10000f) / 60f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fill = fill + speed;
        fillImage.fillAmount = fill;
    }

}
