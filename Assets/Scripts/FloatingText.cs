using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 3f; //lifespan of the animation

    public Vector3 Offset = new Vector3(0, 25, 0);
    
    void Start()
    {
        Destroy(gameObject, DestroyTime);

        transform.localPosition += Offset;
    }

}
