using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class PlayerTeleportController : MonoBehaviour {

    //teleportation target game object
    public GameObject target;

    //reticle
    Reticle reticle;

    //max tele distance
    public float maxDistance;

    //keep track of whether we are showing or not
    bool isShowing;

    // line renderer
    LineRenderer lineRend;

    //show arc
    public bool showArc;

    //number of points in arc
    public int numArcPoints;

    //points of the arc
    Vector3[] arcPoints;

    //orgin of arc
    public GameObject arcOrigin;


    void Awake()
    {
        HideTarget();

        //find reticle
        reticle = FindObjectOfType<Reticle>();

        //hide by default
        isShowing = false;

        //get the line rend
        if (showArc)
        {
            lineRend = target.GetComponent<LineRenderer>();
            if(lineRend == null)
            {
                Debug.LogError("No line Renderer");
            }
        }

        //set number of points
        lineRend.positionCount = numArcPoints;

        //points vector
        arcPoints = new Vector3[numArcPoints];
    }

    public void HideTarget()
    {
        target.SetActive(false);

        //show reticle when target is hidden
        if (reticle != null)
        {
            reticle.Show();
        }

        //update our bool
        isShowing = false;
    }

    //show target
    public void ShowTarget(Vector3 position)
    {
        //Checks if distance is too far
        if (Vector3.Distance(position, transform.position) <= maxDistance)
        {
            //if the target is not active, activate it
            if (!target.activeInHierarchy)
            {
                target.SetActive(true);
                //hide reticle when target is displayed
                if (reticle != null)
                {
                    reticle.Hide();
                }

            }
            //set the telepoort target to the position we are looking at
            target.transform.position = position;

            //update bool
            isShowing = true;

            if(showArc)
            {
                DrawRay();
            }
        }
        //if we're showing the target but it's too far, hide
        else if (isShowing)
        {
            HideTarget();
        }
    }

    //teleportation
    public void Teleport()
    {
        if(isShowing)
        {
            //player position will be equal to target position
            transform.position = target.transform.position;
            HideTarget();
        }
    }

    //draw ray
    void DrawRay()
    {
        //starting position
        Vector3 startPoint = arcOrigin.transform.position;

        //ending position
        Vector3 endPoint = target.transform.position;

        //arc effect
        float arcY;

        //create all the points
        for(int i = 0; i < numArcPoints; i++)
        {

            //arc effect
            arcY = Mathf.Sin((float)i / numArcPoints * Mathf.PI)/2;

            //create point
            arcPoints[i] = Vector3.Lerp(startPoint, endPoint, (float)i / numArcPoints);
            arcPoints[i].y += arcY;
        }

        //assign points to line renderer
        lineRend.SetPositions(arcPoints);
    }

    public bool IsSelecting()
    {
        return isShowing;
    }

}
