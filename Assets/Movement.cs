using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float PlayerSpeed;
    public float JumpForce;

    float horizontalmovement;

    public Rigidbody2D Rb;

    private Vector3 velocity = Vector3.zero;

    public Transform EarthLeft;
    public Transform EarthRight;

    private bool isJumping = false;
    private bool isEarth = false;
    void FixedUpdate()
    {
        isEarth = Physics2D.OverlapArea(EarthLeft.position, EarthRight.position);

        if (isEarth == true)
        {
            if (isJumping == true)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
            }
        }
        Vector3 targetVelocity = new Vector2(horizontalmovement, Rb.velocity.y);
        Rb.velocity = Vector3.SmoothDamp(Rb.velocity, targetVelocity, ref velocity, .05f);
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        horizontalmovement = context.ReadValue<Vector2>().x*PlayerSpeed;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isJumping = true;
        }
        if (context.canceled)
        {
            isJumping = false;
        }
    }

}
