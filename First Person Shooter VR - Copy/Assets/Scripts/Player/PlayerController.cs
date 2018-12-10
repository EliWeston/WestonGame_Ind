using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerCollectableCtrl))]
[RequireComponent(typeof(PlayerTeleportController))]
public class PlayerController : MonoBehaviour
{

    PlayerCollectableCtrl playerCollect;

    //playe tele controller
    PlayerTeleportController playerTele;

    //gun
    public GunController gun;

    //spawn
    public GameObject spawn;

    //turret
    public GameObject turret;

    void Awake()
    {
        playerCollect = GetComponent<PlayerCollectableCtrl>();
        playerTele = GetComponent<PlayerTeleportController>();
    }

    void OnEnable()
    {
        playerCollect.OnCollect += HandleCollection;
    }

    void OnDisable()
    {
        playerCollect.OnCollect -= HandleCollection;
    }

    void HandleCollection(GameObject item)
    {
        gun.Recharge(item.GetComponent<CollectableItem>().GetProperty("ammo"));
    }

    void Update()
    {
        //not selecting
        if (playerCollect.IsSelecting() || playerTele.IsSelecting()) return; 
        //check for button press
        if (Input.GetButtonDown("Fire1"))
        {
            if (gun.CanShoot())
                gun.Shoot();
        }
        if (Input.GetButtonDown("Fire3"))
        {
            CreateSpawn();
        }
        if (Input.GetButtonDown("Jump"))
        {
            CreateTurret();
        }
       
    }

    void CreateSpawn()
    {
        Instantiate(spawn, transform.position + (transform.forward * 4), transform.rotation);
    }

    void CreateTurret()
    {
        Instantiate(turret, transform.position + (transform.forward * 4), transform.rotation);
    }

}
