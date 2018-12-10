using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectableCtrl : MonoBehaviour {

    //max distance for collection
    public float maxDistance;

    //flag of whether we are selecting or not
    bool isSelecting = false;

    //event for when an item is collected
    //1)delegate
    public delegate void OnCollectedEventHandler(GameObject item);

    //2)event
    public event OnCollectedEventHandler OnCollect;

    //Called when collecting an item
	public void Collect(GameObject item)
    {
        if(OnCollect != null) 
        {
            //trigger our event
            OnCollect(item);
        }

        //set selection to false
        isSelecting = false;
    }

    //called when selcting an item
    public void SelectionOver()
    {
        isSelecting = true;
    }

    //called when we deselect something
    public void SelectionOut()
    {
        isSelecting = false;
    }

    //return whether we are returning or not
    public bool IsSelecting()
    {
        return isSelecting;
    }
}
