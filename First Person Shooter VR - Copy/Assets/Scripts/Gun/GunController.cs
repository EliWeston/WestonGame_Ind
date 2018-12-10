using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenvaVR;

[RequireComponent(typeof(ObjectPool))]
public class GunController : MonoBehaviour {

    //ammo
    public float ammo = 10;

    //max ammo
    public float maxAmmo = 10;

    //ammo panel
    public RectTransform ammoIndicator;

    //pool
    ObjectPool bulletPool;

    //bullet speed
    public float bulletSpeed = 50;

    void Awake()
    {
        //get the object pool for our bullets
        bulletPool = GetComponent<ObjectPool>();

        //make sure pool is initialized
        bulletPool.InitPool();

        //refreshUI
        RefreshUI();
    }

    public bool CanShoot()
    {
        return ammo > 0;
    }

    //shoot a bullet
    public void Shoot()
    {
        //get a bullet from a pool
        GameObject newBullet = bulletPool.GetObj();

        //position
        newBullet.transform.position = transform.position;

        //get rigidbod
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();

        //give velocity
        rb.velocity = transform.forward * bulletSpeed;

        //decrease ammo
        ammo--;

        //refresh UI
        RefreshUI();
    }

    void RefreshUI()
    {
        ammoIndicator.localScale = new Vector3( (float)ammo / maxAmmo, 1, 1);
    }

    //add more ammo
    public void Recharge(float amount)
    {
        //new ammo value is minimum between: ammo + amount, max possible ammo
        ammo = Mathf.Min(ammo + amount, maxAmmo);

        RefreshUI();
    }
}
