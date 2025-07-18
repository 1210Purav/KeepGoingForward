using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardSpeed = 3f;
    public float sidewaysSpeed = 3f;

    void FixedUpdate()
    {
        float zVelocity = 0f;
        float xVelocity = 0f;

        // Forward and backward
        if (Input.GetKey("w"))
        {
            zVelocity = forwardSpeed;
        }
        else if (Input.GetKey("s"))
        {
            zVelocity = -forwardSpeed;  // ⬅ Move backward
        }

        // Left & Right movement
        if (Input.GetKey("a"))
        {
            xVelocity = -sidewaysSpeed;
        }
        else if (Input.GetKey("d"))
        {
            xVelocity = sidewaysSpeed;
        }

        // Apply velocity
        rb.linearVelocity = new Vector3(xVelocity, rb.linearVelocity.y, zVelocity);
    }
}

