using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

[RequireComponent(typeof(VRInteractiveItem))]
public class FreeTeleportationZone : MonoBehaviour {

    VRInteractiveItem vrItem;
    VREyeRaycaster vrEyeRaycaster;
    PlayerTeleportController playerCtrl;

    void Awake()
    {
        //get interactive vr item component
        vrItem = GetComponent<VRInteractiveItem>();

        //get vr eye raycaster
        vrEyeRaycaster = FindObjectOfType<VREyeRaycaster>();

        if (vrEyeRaycaster == null)
        {
            Debug.LogError("There needs to be a Vr Eye Raycaster in the scene.");
        }

        //get player teleport controller
        playerCtrl = FindObjectOfType<PlayerTeleportController>();

        if (playerCtrl == null)
        {
            Debug.LogError("There needs to be a player teleport controller in the scene.");
        }
    }

    void OnEnable()
    {
        //subscrive to events
        vrEyeRaycaster.OnRaycasthit += HandleShowTarget;
        vrItem.OnOut += HandleOut;
        vrItem.OnClick += HandleClick;
    }

    void OnDisable()
    {
        //unsubscrive to events
        vrEyeRaycaster.OnRaycasthit -= HandleShowTarget;
        vrItem.OnOut -= HandleOut;
        vrItem.OnClick -= HandleClick;
    }

    private void HandleClick()
    {
        playerCtrl.Teleport();
    }

    private void HandleOut()
    {
        playerCtrl.HideTarget();
    }

    private void HandleShowTarget(RaycastHit hit)
    {
        //check the we are actually looking at this object
        if (!vrItem.IsOver) return;

        playerCtrl.ShowTarget(hit.point);
    }

    


}
