using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerController))]
[RequireComponent (typeof(WeaponController))]
public class Player : LivingEntity
{
    public float moveSpeed = 5;
    PlayerController controller;
    WeaponController weaponController;

    Camera viewCamera;

    public override void Start ()
    {
        base.Start ();
        controller = GetComponent<PlayerController>();
        weaponController = GetComponent<WeaponController> ();
        viewCamera = Camera.main;
    }
    // void Start()
    // {

    // }

    // Update is called once per frame
    void Update()
    {   
        //Movement Input
        Vector3 moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal"),0, Input.GetAxisRaw ("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move (moveVelocity);

        //Look Input
        Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
        Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast (ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            controller.LookAt (point);
        }

        //Weapon Input
        if (Input.GetMouseButton (0))
        {
            weaponController.Shoot ();
        }
    }
}
