using UnityEngine;
using System.Linq;
using System;

public class TimelineEventManager : MonoBehaviour
{

    // A list of TimelineEvent instances representing events in the timeline 
    public TimelineEvent[] eventList;
    // The current Timeline event that can be pressed
    [HideInInspector]
    public TimelineEvent currentEvent;
    // The index in the array of the current event
    [HideInInspector]
    public int currentEventIndex;

    // A reference to current timeline infopage timeline event event on screen
    [HideInInspector]
    public Event_InfoPage currentInfoTimelineEvent;
    
    // A reference to current more infopage for each AR event on screen
    [HideInInspector]
    public Menu.Page currentMoreInfoPage;

    // A reference to current timeline AR timeline event event on screen
    [HideInInspector]
    public Event_AR currentARTimelineEvent;


    // Start is called before the first frame update
    void Start() {
        OrderEventsList();
        // The default position of the timeline is the first event in the timeline
        transform.localPosition = new Vector3(eventList[0].positionWhereCentered,gameObject.transform.localPosition.y,gameObject.transform.localPosition.z);
        //The current event is the first event on the timeline
        currentEvent = eventList[0];
        currentEventIndex = 0;

        // set currentInfoTimelineEvent because there is no info timeline event on screen
        currentInfoTimelineEvent = null;
        currentARTimelineEvent = null;

    }

    // Order the events list based on the positionWhereCentered value for each event
    // The higher the value, the farther left (or in the past) an event will be on the timeline
    void OrderEventsList() {
        TimelineEvent[] sortedEventList = eventList.OrderByDescending(c => c.positionWhereCentered).ToArray();
        eventList = sortedEventList;
    }

    // This method changes the current event to the new current event specified in the parameter
    public void ChangeCurrentEvent(TimelineEvent newCurrentEvent) {
        currentEvent = newCurrentEvent;
        LeanTween.moveLocalX(gameObject, currentEvent.positionWhereCentered , 0.5f);
    }
}