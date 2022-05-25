using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class ScreenBounds : MonoBehaviour
{

    public Camera mainCamera;
    private BoxCollider2D boxCollider;

    [SerializeField]
    private float teleportOffset = 0.2f;

    [SerializeField]
    private float cornerOffset = 1;

    [SerializeField]
    private AsteroidSpawner spawner;

    private void Awake()
    {
        this.mainCamera.transform.localScale = Vector3.one;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        UpdateBoundsSize();
    }

    public void UpdateBoundsSize()
    {
        float y = mainCamera.orthographicSize * 2;
        //camera width depending on the aspect ratio and height
        Vector2 boxColliderSize = new Vector2(y * mainCamera.aspect + 1.5f, y + 1.5f);
        boxCollider.size = boxColliderSize;
        spawner.SetBoundsSize(boxCollider.size);
    }

    public bool ObjectOutOfBounds(Vector3 worldPosition)
    {
        return Mathf.Abs(worldPosition.x) > Mathf.Abs(boxCollider.bounds.min.x) || Mathf.Abs(worldPosition.y) > Mathf.Abs(boxCollider.bounds.min.y);
    }

    public Vector2 CalculateWrappedPosition(Vector2 worldPosition)
    {
        bool xBoundResult =
            Mathf.Abs(worldPosition.x) > (Mathf.Abs(boxCollider.bounds.min.x) - cornerOffset);
        bool yBoundResult =
            Mathf.Abs(worldPosition.y) > (Mathf.Abs(boxCollider.bounds.min.y) - cornerOffset);

        Vector2 signWorldPosition =
            new Vector2(Mathf.Sign(worldPosition.x), Mathf.Sign(worldPosition.y));

        if (xBoundResult && yBoundResult)
        {
            return Vector2.Scale(worldPosition, Vector2.one * -1)
                + Vector2.Scale(new Vector2(teleportOffset, teleportOffset),
                signWorldPosition);
        }
        else if (xBoundResult)
        {
            return new Vector2(worldPosition.x * -1, worldPosition.y)
                + new Vector2(teleportOffset * signWorldPosition.x, teleportOffset);
        }
        else if (yBoundResult)
        {
            return new Vector2(worldPosition.x, worldPosition.y * -1)
                + new Vector2(teleportOffset, teleportOffset * signWorldPosition.y);
        }
        else
        {
            return worldPosition;
        }
    }
}
