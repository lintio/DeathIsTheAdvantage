using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWorlds : MonoBehaviour
{
    public GameObject pf_PlayerBody;

    [SerializeField] GameObject player;
    GameObject playerBody;

    GameObject possesionTarget;

    [SerializeField] List<GameObject> possesableObject =  new List<GameObject>();

    public List<LayerMask> layers = new List<LayerMask>();

    public float interactRange;

    public bool isLiving = true;
    public bool isPossesing = false;

    public bool inRangeP = false;
    public bool inRangeR = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // switch from Living to ghost
        {
            // add cooldown check for switching to ghost mode 
            
            SwitchPlayer(); // switching from living to dead will send out a weak pulse that would alert the Reapers to your location to hamper the player if they try and use it to often in the same location
        }
        if (Input.GetKeyDown(KeyCode.E) && !isLiving && !isPossesing) // Possess object
        {
            PossesItem(); // Possesion should have some tole on your life force maybe send out a pulse that would alert the Reapers to your location
        }
        else if (Input.GetKeyDown(KeyCode.E) && isPossesing) // Stop possesion of object
        {
            ReturnToBody();
        }

        if (!isLiving && canPosses() != null)
        {
            inRangeP = true;
        }
        else
        {
            inRangeP = false;
        }

        if (!isPossesing && !isLiving && CanRevive())
        {
            inRangeR = true;
        }
        else
        {
            inRangeR = false;
        }

    }

    private void ReturnToBody()
    {
        possesionTarget.GetComponent<MovementController>().active = false;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<MovementController>().active = true;
        isPossesing = false;
    }

    void PossesItem()
    {
        possesionTarget = canPosses();
        if (possesionTarget != null)
        {
            isPossesing = true;
            player.GetComponent<MovementController>().active = false;
            player.GetComponent<SpriteRenderer>().enabled = false;
            possesionTarget.GetComponent<MovementController>().active = true;
        }
        else
        {
            Debug.Log("Nothing to Posses");
        }
    }

    private GameObject canPosses()
    {
        foreach (GameObject PossesableObject in possesableObject)
        {
            float distance = Vector3.Distance(player.transform.position, PossesableObject.transform.position);
            if (distance <= interactRange)
            {
                return PossesableObject;
            }
        }
        return null;
    }

    void SwitchPlayer()
    {
        if (!isLiving && CanRevive())
        {
            Vector3 bodyPos = playerBody.transform.position;
            Destroy(playerBody);
            transform.position = bodyPos;
            gameObject.layer = LayerMask.NameToLayer("LivingPlayer");
            isLiving = true;
        }
        else if (isLiving)
        {
            gameObject.layer = LayerMask.NameToLayer("GhostPlayer");
            playerBody = Instantiate(pf_PlayerBody, player.transform.position, Quaternion.identity);
            isLiving = false;
        }
        else
        {
            Debug.Log("Can't do that right now");
        }
    }

    bool CanRevive()
    {
        float distance = Vector3.Distance(player.transform.position, playerBody.transform.position);
        if (distance <= interactRange)
        {
            return true;
        }
        return false;
    }
}
