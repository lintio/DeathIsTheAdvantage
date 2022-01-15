using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public SwitchWorlds switchWorlds;

    // UI
    public TMP_Text hintText;

    // list of possetion particals
    public List<ParticleSystem> possesionParticals = new List<ParticleSystem>();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
        if (switchWorlds.isLiving)
        {
            foreach (ParticleSystem possesionPartical in possesionParticals)
            {
                possesionPartical.Stop();
            }
        }
        else
        {
            foreach (ParticleSystem possesionPartical in possesionParticals)
            {
                possesionPartical.Play();
            }
        }
    }

    void UpdateText()
    {
        if (switchWorlds.isLiving)
        {
            hintText.text = "Press Q to leave your body behind";
        }
        else
        {
            if (switchWorlds.inRangeR)
            {
                hintText.text = "Press Q to return to your body";
            }
            else if (!switchWorlds.inRangeR && switchWorlds.inRangeP)
            {
                hintText.text = "Press E to Possess this item";
            }
            else if (switchWorlds.isPossesing)
            {
                hintText.text = "Press E leave this item and return to your spirit form";
            }
            else
            {
                hintText.text = "Out of range";
            }
        }
    }
}
