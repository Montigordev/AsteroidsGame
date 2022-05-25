using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEntity : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public float Speed 
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
    private Vector2 direction;
    [HideInInspector]
    public Vector2 Direction 
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
        }
    }

    //Screensize
    [HideInInspector]
    public ScreenBounds ScreenBounds { get; set; }

    [SerializeField]
    private bool canWrap = true;

    protected virtual void Start()
    {
        GetScreenBoundsRef();
    }

    protected virtual void Update()
    {
        MoveEntity();
        CheckBounds();
    }

    protected void CheckBounds()
    {
        if (canWrap)
        {
            if (ScreenBounds.ObjectOutOfBounds(transform.position))
            {
                Vector2 newPosition = ScreenBounds.CalculateWrappedPosition(transform.position);
                transform.position = newPosition;
            }
        }
    }

    protected virtual void MoveEntity()
    {
        transform.Translate(Direction.normalized * Speed * Time.deltaTime);
    }

    protected void GetScreenBoundsRef()
    {
        ScreenBounds = GameObject.FindGameObjectWithTag("ScreenBoundsTag").GetComponent<ScreenBounds>();
    }

    public virtual void DestroyEntity()
    {
        Destroy(gameObject);
    }
    

}
