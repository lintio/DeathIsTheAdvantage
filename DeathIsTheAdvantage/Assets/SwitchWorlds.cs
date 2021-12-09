using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWorlds : MonoBehaviour
{
    public GameObject pf_GhostPlayer;
    public GameObject pf_LivingPlayer;
    public GameObject pf_PlayerBody;

    GameObject livingPlayer;
    GameObject ghostPlayer;
    GameObject playerBody;

    public float reviveRange;

    bool isLiving = true;
    
    // Start is called before the first frame update
    void Start()
    {
        livingPlayer = Instantiate(pf_LivingPlayer, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchPlayer();
        }
    }

    void SwitchPlayer()
    {
        if (!isLiving && CanRevive())
        {
            livingPlayer = Instantiate(pf_LivingPlayer, playerBody.transform.position, Quaternion.identity);
            Destroy(playerBody);
            Destroy(ghostPlayer);
            isLiving = true;
        }
        else if (isLiving)
        {
            ghostPlayer = Instantiate(pf_GhostPlayer, livingPlayer.transform.position, Quaternion.identity);
            playerBody = Instantiate(pf_PlayerBody, livingPlayer.transform.position, Quaternion.identity);
            Destroy(livingPlayer);
            isLiving = false;
        }
        else
        {
            Debug.Log("Can't do that right now");
        }
    }

    bool CanRevive()
    {
        float distance = Vector3.Distance(ghostPlayer.transform.position, playerBody.transform.position);
        if (distance <= reviveRange)
        {
            return true;
        }
        return false;
    }
}
