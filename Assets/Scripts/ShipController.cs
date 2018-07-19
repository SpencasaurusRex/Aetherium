using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    Rigidbody2D rb;

    public float backPenalty = .3f;
    public float forwardVelocity = 3f;
    public float acceleration = 10f;

    public float turningSpeed = 2f;
    public float turningAcceleration = .1f;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update ()
    {
        // Forward/Back
        float targetVelocityAmount = Input.GetAxis("Vertical") * forwardVelocity;
        if (targetVelocityAmount < 0)
        {
            targetVelocityAmount *= backPenalty;
        }
        Vector2 targetVelocity = transform.up * targetVelocityAmount;

        Vector2 currentVelocity = rb.velocity;
        Vector2 forceAmount = targetVelocity - currentVelocity;
        Vector2 force = forceAmount * acceleration;
        rb.AddForce(force);
        Debug.Log("Target vel: " + targetVelocity + " Current vel: " + rb.velocity + " Force amount: " + forceAmount);

        // Turning
        float targetTurn = -Input.GetAxis("Horizontal") * turningSpeed;
        float currentTurning = rb.angularVelocity;
        float turnAmount = targetTurn - currentTurning;
        float torque = turnAmount * turningAcceleration;
        rb.AddTorque(torque);
        Debug.Log("Target avel: " + targetTurn + " Current avel: " + rb.angularVelocity + " Torque: " + torque);
    }
}
