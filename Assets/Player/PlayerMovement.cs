using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : GameEntity, IDamageable
{
    private Vector2 spaceshipMovement, spacechipRotation;
    [SerializeField]
    private float maxSpeed = 1f;
    private float acceleration = 10f;
    private float deceleration = 3f;
    [SerializeField]
    private float rotationSpeed;

    public GameEvent PlayerDeathEvent;

    public void Move(InputAction.CallbackContext context)
    {
        spaceshipMovement = context.ReadValue<Vector2>();
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        spacechipRotation = context.ReadValue<Vector2>();
    }

    protected override void MoveEntity()
    {
        if (spaceshipMovement.y >= 1 && Speed < maxSpeed)
        {
            Speed = Speed + acceleration * Time.deltaTime;
            Direction = transform.up;
        }

        else
        {
            if (Speed > deceleration * Time.deltaTime)
            {
                Speed = Speed - deceleration * Time.deltaTime;
            }
            else
            {
                Speed = 0;
            }
        }
        //apply movement
        transform.position += new Vector3(Direction.x, Direction.y, 0f) * Speed * Time.deltaTime;
        //rotation
        transform.eulerAngles += new Vector3(0f, 0f, -spacechipRotation.x) * rotationSpeed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            //Call death
            PlayerDeathEvent.Raise();
            Destroy(gameObject);
        }
    }
}
