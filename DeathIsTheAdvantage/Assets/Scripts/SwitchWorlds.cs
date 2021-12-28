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

    bool isLiving = true;
    bool isPossesing = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // add cooldown check for switching to ghost mode
            
            SwitchPlayer();
        }
        if (Input.GetKeyDown(KeyCode.E) && !isLiving && !isPossesing)
        {
            // add cooldown check for switching to ghost mode
            PossesItem();
        }
        else if (Input.GetKeyDown(KeyCode.E) && isPossesing)
        {
            ReturnToBody();
        }
    }

    private void ReturnToBody()
    {
        possesionTarget.GetComponent<MovementController>().active = false;
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
