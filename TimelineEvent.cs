using System;
using UnityEngine;
using UnityEngine.UI;

// Class represents an event displayed on the timeline
// These objects are contained in a list on the timeline object

public class TimelineEvent : MonoBehaviour
{   
    // The X position of the Timeline Container where this event will be centered
    [HideInInspector] 
    public float positionWhereCentered;
    
    // A reference to the timeline event manger
    TimelineEventManager timelineEventManager;
    
    // A reference to the event
    public Event timelineEvent;

    // Get the timeline event manager here and check that eventPage contains the page script
    private void Start() {
        // Try to get the timeline Event Manager event here
        try {
            // The parent object of object attached to script
            GameObject parent = gameObject.transform.parent.gameObject;
            timelineEventManager = parent.GetComponent<TimelineEventManager>();
        } catch {
            throw new Exception("Could not find TimelineEventManager. Make sure that Timeline Event has a parent with a TimelineEventManager Script");
        }

        // Get centered position 
        positionWhereCentered = (transform.localPosition.x) * -1;       
    }

    // This runs whenever the button corrsponding with this event is pressed
    public void PressOnEvent() {
        if (this == timelineEventManager.currentEvent) {
            Debug.Log("Launching Event: " + timelineEventManager.currentEvent);
            timelineEvent.LaunchEvent();
        } else {
            Debug.Log("Can Only Launch Events in Center of Timeline");
        }
    }

}
