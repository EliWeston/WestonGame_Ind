using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

[RequireComponent(typeof(VRInteractiveItem))]
public class CollectableItem : MonoBehaviour
{
    //serializable class for any items
    [Serializable]
    public class ItemStat
    {
        public string label;
        public float amount;
    }

    //flexible array of properties
    public ItemStat[] properties;

    //vr interactive item
    VRInteractiveItem vrItem;

    //player controller
    PlayerCollectableCtrl playerCollect;

    void Awake()
    {
        //get vr interactive item component
        vrItem = GetComponent<VRInteractiveItem>();

        //get the playercollectctrl
        playerCollect = FindObjectOfType<PlayerCollectableCtrl>();
        if (playerCollect == null)
        {
            Debug.LogError("nneds to be player collect ctrl.");
        }
    }

    void OnEnable()
    {
        // subscribe Events
        vrItem.OnOver += HandleOver;
        vrItem.OnOut += HandleOut;
        vrItem.OnClick += HandleClick;
    }

    void OnDisable()
    {
        // subscribe Events
        vrItem.OnOver -= HandleOver;
        vrItem.OnOut -= HandleOut;
        vrItem.OnClick -= HandleClick;
    }

    private void HandleClick()
    {
        //collect item
        playerCollect.Collect(gameObject);

        //destroy item
        Destroy(gameObject);
    }

    private void HandleOut()
    {
        //the okayer collectable ctrl knows we are not selecting
        playerCollect.SelectionOut();

        //unselect the item
        Highlight(false);
    }

    private void HandleOver()
    {
        if (Vector3.Distance(transform.position, playerCollect.gameObject.transform.position) <= playerCollect.maxDistance)
        {
            //the player collectable controllers knows we are selecting
            playerCollect.SelectionOver();

            //Highlight the item
            Highlight(true);
        }
    }

    void Highlight(bool flag)
    {
        //highlight element
        GetComponent<Renderer>().material.SetFloat("_Outline", flag ? 0.003f : 0f);
    }

    //get the amount of a property with a label
    public float GetProperty(string label)
    {
        //searching for label in array
        for (int i = 0; i < properties.Length; i++)
        {
            if (properties[0].label == label)
            {
                // return
                return properties[i].amount;
            }
        }

        return 0;
    }
}