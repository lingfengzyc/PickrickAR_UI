using System;
using UnityEngine;

// Whenever the player stops moving the timeline, the timeline will adjust to have a single event in the center of
// the timeline due to this script
public class SnapTimelineToCenter : MonoBehaviour
{  
    // A refrence to the TimelineEventManager inside this same gameObject
    TimelineEventManager timelineEventManager;

    private void Start() {
        // Look to see if TimelineEventManager is in this game object
        timelineEventManager = gameObject.GetComponent<TimelineEventManager>();
        // Throw error if TimelineEventManager can't be found
        if (timelineEventManager == null) {
            throw new Exception("Could not find TimelineEventManager inside this game object.");
        }
    }

    private void Update() {
        if (Input.touchCount > 0) {
            bool allFingersReleased = true;
            foreach (Touch touch in Input.touches) {
                // See of all fingers have been released
                if (touch.phase != TouchPhase.Ended) {
                    allFingersReleased = false;
                } 
            }
            if (allFingersReleased) {
                Debug.Log("Updating Timeline");
                ChangeTimelinePosition();
            }
        }
    }

    // Change Timeline Position to center it with the nearest event
    private void ChangeTimelinePosition() {
        float currentPosition = gameObject.transform.localPosition.x;
        float closestDistance = 9999f;
        TimelineEvent closestEvent = null;
        TimelineEvent[] eventList = timelineEventManager.eventList;
        for (int i = 0; i < eventList.Length; i++) {
            float eventPosition = eventList[i].positionWhereCentered;
            float distance = Math.Abs(currentPosition - eventPosition);
            if (distance < closestDistance) {
                closestDistance = distance;
                closestEvent = eventList[i];
                timelineEventManager.currentEventIndex = i;
            }
        }
        timelineEventManager.currentEvent = closestEvent;
        Debug.Log("Moved Timeline to " + closestEvent);
        Debug.Log("Current Position: " + currentPosition + " Closest Event: " + closestEvent.positionWhereCentered);
        timelineEventManager.ChangeCurrentEvent(closestEvent);
    }
}