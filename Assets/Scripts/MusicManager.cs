using UnityEngine;
using System;
using System.Runtime.InteropServices;
using FMODUnity;
using FMOD.Studio;

//the below code has been sourced from the official FMOD documentation
//ref: https://www.fmod.com/docs/2.00/unity/examples-timeline-callbacks.html
//minor modifications has been made to fit the project requirements
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField]
    [EventRef]
    private string music;

    public TimelineInfo timelineInfo = null;

    private GCHandle timelineHandle; //access managed objects during runtime

    private FMOD.Studio.EVENT_CALLBACK beatCallback;
    private FMOD.Studio.EventDescription descriptionCallback;

    public FMOD.Studio.EventInstance musicInstance;

    //FMOD delegates
    public delegate void BeatEventDelegate();
    public static event BeatEventDelegate BeatUpdated;

    public delegate void MarkerListenerDelegate();
    public static event MarkerListenerDelegate MarkerUpdated;

    //variables keeping track of the playhead position in relation to events
    public static int lastBeat = 0;
    public static string lastMarkerString = null;

    //Struct based Class to store information pulled from FMOD callback
    [StructLayout(LayoutKind.Sequential)]
    public class TimelineInfo
    {
        public int currentBeat = 0;
        public int currentBar = 0;
        public float currentTempo = 0;
        public int currentPosition = 0;
        public float songLength = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    private void Awake()
    {
        instance = this; //instantiate class

        musicInstance = RuntimeManager.CreateInstance(music);
        
        musicInstance.start(); //start playback
    }

    private void Start()
    {
        if (music != null) //run only if banks are assigned
        {
            timelineInfo = new TimelineInfo();
            beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);


            //GC Handle to ignore garbage collection
            timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
            musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
            musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

            musicInstance.getDescription(out descriptionCallback);
            descriptionCallback.getLength(out int length);

            timelineInfo.songLength = length;
        }
    }

    private void Update()
    {
        //update beat position and marker

        musicInstance.getTimelinePosition(out timelineInfo.currentPosition);

        if(lastMarkerString != timelineInfo.lastMarker)
        {
            lastMarkerString = timelineInfo.lastMarker;

            if(MarkerUpdated != null)
            {
                MarkerUpdated();
            }
        }

        if (lastBeat != timelineInfo.currentBeat)
        {
            lastBeat = timelineInfo.currentBeat;

            if (BeatUpdated != null)
            {
                BeatUpdated();
            }
        }

    }

    //clear memory after quitting application
    void OnDestroy()
    {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
        timelineHandle.Free();
    }


    //private void OnGUI()
    //{
    //    GUILayout.Box($"Current Beat = {timelineInfo.currentBeat}, Last Marker = {(string)timelineInfo.lastMarker}");  
    //}


    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, FMOD.Studio.EventInstance instance, IntPtr parameterPtr)
    {
        //FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);

        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero) //System(IntPtr)
        {
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentBeat = parameter.beat;
                        //timelineInfo.currentBar = parameter.bar;
                        //timelineInfo.currentTempo = parameter.tempo;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                    }
                    break;
            }
        }
        return FMOD.RESULT.OK;
    }

   
}