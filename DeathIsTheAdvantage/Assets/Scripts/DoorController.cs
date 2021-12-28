using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Vector2 initalPos;
    [SerializeField] Vector2 openPos;
    float closeTime = .25f;
    float closeTimer;
    bool doorActive;

    private void Start()
    {
        initalPos = transform.position;
        openPos = new Vector2(initalPos.x, initalPos.y + 2.5f);
    }

    private void Update()
    {
        ActivateDoor();
    }

    public void OpenDoor()
    {
        doorActive = true;
    }

    public void CloseDoor()
    {
        doorActive = false;
    }

    void ActivateDoor()
    {
        if (doorActive)
        {
            transform.position = openPos;
            closeTimer = 0;
        }
        else
        {
            closeTimer += Time.deltaTime;
            if (closeTimer > closeTime)
            {
                transform.position = initalPos;
                closeTimer = 0;
            }
        }
    }
}
