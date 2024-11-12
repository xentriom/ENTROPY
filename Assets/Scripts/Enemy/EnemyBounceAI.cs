using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBounceAI : MonoBehaviour
{
    public GameObject trackerObject; // Invisible tracking object
    public GameObject bouncingObject; // Sphere that bounces
    public float followDistance = 5.0f; // Distance the bouncing object follows behind
    public float bounceSpeed = 5.0f; // Speed of the bouncing object
    public LayerMask barrierLayer; // Layer for walls/barriers

    private Rigidbody bouncingRb;

    // Start is called before the first frame update
    void Start()
    {
        bouncingRb = bouncingObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
