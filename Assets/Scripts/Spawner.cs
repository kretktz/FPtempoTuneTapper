using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    [SerializeField]
    private bool waitingForString = true;

    [SerializeField]
    private string stringToWaitFor = "start";

    public GameObject noteObject;

    private readonly string spawn = "spawn";

    public bool noteObjectExists = false;

    public string spawnerNumber;
    private void Awake()
    {
        //subscribe to music manager
        
        MusicManager.MarkerUpdated += WaitForMarker;

        instance = this;
    }

    private void OnDestroy()
    {
        //unsubscribe to music manager
        MusicManager.MarkerUpdated -= WaitForMarker;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        waitingForString = false;
        noteObjectExists = false;

        GetSpawnerNumber();
    }

    // Update is called once per frame
    void Update()
    {
        string lastMarker = MusicManager.instance.timelineInfo.lastMarker;

        if (!noteObjectExists && lastMarker.Contains(spawnerNumber))
        {
            SpawnObject();  
        }

        if (noteObjectExists && lastMarker.Contains("completemiss"))
        {
            noteObjectExists = false;
        }

        
    }

    private void WaitForMarker()
    {
        if (MusicManager.instance.timelineInfo.lastMarker == stringToWaitFor)
        {
            waitingForString = false;
        }
    }

    private void SpawnObject()
    {
        string lastMarker = MusicManager.instance.timelineInfo.lastMarker;

        if (!waitingForString && lastMarker.Contains(spawn))
        {
            Instantiate(noteObject, transform.position, Quaternion.identity, this.transform);
            noteObjectExists = true;
        }
    }

    public string GetSpawnerNumber()
    {
        spawnerNumber = transform.name;
        return spawnerNumber;
    }
}
