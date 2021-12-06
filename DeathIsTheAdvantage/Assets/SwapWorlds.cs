using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwapWorlds : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private Transform livingWorldOnly;
    private List<GameObject> livingWorldOnlyObjects = new List<GameObject>();
    private bool isLiving = true;
    [SerializeField] private float ghostWorldCooldown;
    private float cooldownTime;
    private bool livingWorldObjectsActive = true;
    [SerializeField] TMP_Text ui_CooldownText;

    private void Start()
    {
        foreach (Transform Child in livingWorldOnly)
        {
            livingWorldOnlyObjects.Add(Child.gameObject);
        }
    }

    private void Update()
    {
        if (cooldownTime <= 0)
        {
            if (isLiving)
                ui_CooldownText.text = "Press E to end it all and hope the Reaper Amulet you stole protects you (Die)";
            else
                ui_CooldownText.text = "Press E to use the Reaper Amulet and return to the world of the living";
        }
        else
        {
            ui_CooldownText.text = "Cooldown On Reaper Amulet: " + Mathf.RoundToInt(cooldownTime).ToString();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (cooldownTime <= 0)
            {
                if (!isLiving)
                {
                    // add ragdoll to drop body to the floor
                    cooldownTime = ghostWorldCooldown;
                }
                isLiving = !isLiving;
                if (!isLiving && livingWorldObjectsActive)
                {
                    RemoveLivingWorldOnly();
                }
                else
                {
                    AddLivingWorldOnly();
                }
            }
        }
        if (isLiving)
        {
            // remove ragdoll
            body.position = transform.position;
            body.rotation = transform.rotation;
            if (cooldownTime > 0)
            {
                cooldownTime -= Time.deltaTime;
            }
        }
    }

    private void RemoveLivingWorldOnly()
    {
        foreach (GameObject item in livingWorldOnlyObjects)
        {
            item.GetComponent<BoxCollider2D>().enabled = false;
        }
        livingWorldObjectsActive = false;
    }

    private void AddLivingWorldOnly()
    {
        foreach (GameObject item in livingWorldOnlyObjects)
        {
            item.GetComponent<BoxCollider2D>().enabled = true;
        }
        livingWorldObjectsActive = true;
    }
}
