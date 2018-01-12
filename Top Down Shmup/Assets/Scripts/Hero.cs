using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S; // Singleton

    public float gameRestartDelay = 2f;

    // These fields control the movement of the ship
    public float speed = 30;

    public bool ____________________________;

    public Bounds bounds;

    // Declare a new delegate type WeaponFireDelegate
    public delegate void WeaponFireDelegate();
    // Create a WeaponFireDelegate field named fireDelegate.
    public WeaponFireDelegate fireDelegate;

    void Awake()
    {
        S = this;  // Set the Singleton
        bounds = Utils.CombineBoundsOfChildren(this.gameObject);
    }

    void Start()
    {
    }

    void Update()
    {
        // Pull in information from the Input class
        float xAxis = Input.GetAxis("Horizontal"); // 1
        float yAxis = Input.GetAxis("Vertical"); // 1

        // Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        bounds.center = transform.position; // 1

        // Keep the ship constrained to the screen bounds
        Vector3 off = Utils.ScreenBoundsCheck(bounds, BoundsTest.onScreen);// 2
        if (off != Vector3.zero)
        { // 3
            pos -= off;
            transform.position = pos;
        }

        // Use the fireDelegate to fire Weapons
        // First, make sure the Axis("Jump") button is pressed 
        // Then ensure that fireDelegate isn't null to avoid an error
        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
        {           // 1
            fireDelegate();
        }
    }
}