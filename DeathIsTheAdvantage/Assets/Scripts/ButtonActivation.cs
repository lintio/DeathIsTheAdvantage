using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivation : MonoBehaviour
{
    Vector2 initalPos;
    Vector2 pressedPos;
    bool buttonStillPressed;

    [SerializeField] List<DoorController> LinkedDoors = new List<DoorController>();
    [SerializeField] private LayerMask ButtonActivatingLayers;

    private void Start()
    {
        initalPos = transform.position;
        pressedPos = new Vector2(initalPos.x, initalPos.y - 0.15f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Solid"))
        {
            transform.position = pressedPos;
            foreach (DoorController door in LinkedDoors)
            {
                door.OpenDoor();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Solid"))
        {
            transform.position = initalPos;
            foreach (DoorController door in LinkedDoors)
            {
                door.CloseDoor();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Solid"))
        {
            transform.position = pressedPos;
            foreach (DoorController door in LinkedDoors)
            {
                door.OpenDoor();
            }
        }
    }
}
