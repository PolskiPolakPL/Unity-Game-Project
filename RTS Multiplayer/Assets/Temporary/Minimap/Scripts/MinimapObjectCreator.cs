using System.Collections.Generic;
using UnityEngine;

public class MinimapObjectCreator : MonoBehaviour
{
    //temporary [SerializeField] to view them in inspector
    [SerializeField] List<MinimapTracker> buisyTrackersList = new List<MinimapTracker>();
    [SerializeField] List<MinimapTracker> emptyTrackersList = new List<MinimapTracker>();

    [SerializeField] Transform minimapObjectsParent;
    [SerializeField] MinimapTracker initialTracker;
    // Start is called before the first frame update
    void Start()
    {
        initialTracker.Creator = this;
        foreach(Transform children in transform)
            CreateNewTracker(children);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount!=buisyTrackersList.Count)
            FindChildrenWithoutTracker();
    }

    public void MoveToEmptyTrackersList(MinimapTracker tracker)
    {
        //removes tracker from buisy list if it was there already
        if (buisyTrackersList.Contains(tracker))
            buisyTrackersList.Remove(tracker);
        //checks if somehow tracker is not already in that list
        if(!emptyTrackersList.Contains(tracker))
            emptyTrackersList.Add(tracker);
        //switches off tracker's gameObject
        tracker.gameObject.SetActive(false);
    }
    void MoveToBuisyTrackersList(MinimapTracker tracker)
    {
        //removes tracker from 'empty' list if it was there already
        if (emptyTrackersList.Contains(tracker))
            emptyTrackersList.Remove(tracker);
        //checks if somehow tracker is not already in that list
        if (!buisyTrackersList.Contains(tracker))
            buisyTrackersList.Add(tracker);
        //switches on tracker's gameObject
        tracker.gameObject.SetActive(true);
    }

    void FindChildrenWithoutTracker()
    {
        bool isTrackerFound;
        foreach(Transform child in transform)
        {
            isTrackerFound = false;
            foreach(MinimapTracker tracker in buisyTrackersList)
            {
                if (child.Equals(tracker.Target))
                {
                    isTrackerFound = true;
                    break;
                }
            }
            if (!isTrackerFound){
                if (emptyTrackersList.Count > 0)
                    AssignTrackerFromList(emptyTrackersList[0], child);
                else
                {
                    CreateNewTracker(child);
                    return;
                }
            }
        }
    }

    void AssignTrackerFromList(MinimapTracker tracker, Transform target)
    {
        tracker.Target = target;
        tracker.gameObject.SetActive(true);
        MoveToBuisyTrackersList(tracker);
    }
    void CreateNewTracker(Transform target)
    {
        GameObject _gameObject = Instantiate(initialTracker.gameObject, minimapObjectsParent);
        MinimapTracker _tracker = _gameObject.GetComponent<MinimapTracker>();
        _tracker.Target = target;
        _tracker.enabled = true;
        MoveToBuisyTrackersList(_tracker);
    }
}
