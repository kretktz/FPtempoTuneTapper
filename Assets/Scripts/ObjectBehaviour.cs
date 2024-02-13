using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    // animation variables
    public float maxSize;
    public float scaleFactor;
    public float waitTime;

    public Material material;

    public GameObject FLoatingText;

    [SerializeField]
    public GameObject destroyAnimation;

    private bool waitingForString;

    //string variables required for marker detection
    private readonly string spawn = "spawn";
    private readonly string hit = "good";
    private readonly string perfect = "perfect";
    private readonly string completeMiss = "completemiss";

    private string absoluteLastMarker;
    private string localLastMarker;

    private void Awake()
    {
        waitingForString = true; //wait for start string from FMOD

        //subscribe to music manager
        MusicManager.BeatUpdated += ObjectAnimation;
        MusicManager.MarkerUpdated += WaitForMarker;
    }

    private void OnDestroy()
    {
        //unsubscribe to music manager
        MusicManager.MarkerUpdated -= WaitForMarker;
        MusicManager.BeatUpdated -= ObjectAnimation;
    }

    void Start()
    {
        //fetch and set object properties
        material = gameObject.GetComponent<Renderer>().material;
        material.SetColor("_EmissionColor", Color.magenta);
    }

    private void Update()
    {

        absoluteLastMarker = MusicManager.instance.timelineInfo.lastMarker;

        //update local marker only if it contains spawne number
        if (absoluteLastMarker.Contains(transform.parent.name))
        {
            localLastMarker = absoluteLastMarker;
        }

        CompleteMiss();
    }

    private void ObjectAnimation()
    {
       StartCoroutine(ScaleUp());  
    }

    IEnumerator ScaleUp()
    {
        float timer = 0; //reset timer

        while (true)
        {
            while(maxSize > transform.localScale.x)
            {
                //scale the object over time
                timer += Time.deltaTime;
                transform.localScale += scaleFactor * Time.deltaTime * new Vector3(100, 0, 100);

                //change the object colour once it reaches cerain size
                if (transform.localScale.x > 80)
                {
                    material.SetColor("_EmissionColor", Color.red);
                }

                yield return null;
            }

            yield return new WaitForSeconds(waitTime);

            timer = 0;
        } 
    }

    private void WaitForMarker()
    {
        string lastMarker = MusicManager.instance.timelineInfo.lastMarker;

        if (lastMarker.Contains(spawn))
        {
            waitingForString = false;
        }
    }

    //click or touch detection
    private void OnMouseDown()
    {
        if (localLastMarker.Contains(hit))
        {
            //Display Text
            ShowFloatingText("Good!!");
            //Debug.Log("Good Hit");
            Destroy(gameObject);
            Spawner.instance.noteObjectExists = false;
            ScoreKeeper.instance.AddGoodPoint();
        }

        else if (localLastMarker.Contains(perfect))
        {
            ShowFloatingText("PERFECTO !!");
            //Debug.Log("Perfect Hit");
            Destroy(gameObject);
            GameObject explosion = Instantiate(destroyAnimation, transform.position, transform.rotation);
            Destroy(explosion, 0.5f);
            Spawner.instance.noteObjectExists = false;
            ScoreKeeper.instance.AddPerfectPoint();
        }

        //else if (localLastMarker.Contains(spawn))
        //{
        //    ShowFloatingText("MISS !!");
        //    Destroy(gameObject);
        //    Spawner.instance.noteObjectExists = false;
        //    Debug.Log("Miss");
        //}

        else
        {
            ShowFloatingText("MISS !!");
            Destroy(gameObject);
            Spawner.instance.noteObjectExists = false;
            //Debug.Log("Miss");
        }
    }

    private void CompleteMiss()
    {
        
        if (localLastMarker.Contains(completeMiss))
        {
            ShowFloatingText("MISS !!");
            Destroy(gameObject);
            Spawner.instance.noteObjectExists = false;
            //Debug.Log("Miss");
            //Debug.Log(transform.parent.name);
        }
    }

    //floating text animation
    void ShowFloatingText(string hitOrMiss)
    {
        var msg = Instantiate(FLoatingText, transform.position, Quaternion.Euler(new Vector3(90, 0, 0))); //account for top-down camera view
        msg.GetComponent<TextMesh>().text = hitOrMiss;
    }
}
