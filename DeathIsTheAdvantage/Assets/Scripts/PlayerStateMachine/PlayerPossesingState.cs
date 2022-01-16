using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPossesingState : PlayerBaseState
{
    GameObject possesableObject;
    Rigidbody2D possesedRB2D;
    BoxCollider2D possesedBoxCollider2D;
    public override void EnterState(PlayerStateManager player)
    {
        possesableObject = player.FindPossesableObjectInRange();
        if (possesableObject == null)
        {
            player.SwitchState(player.GhostState);
        }
        else
        {
            possesedRB2D = possesableObject.GetComponent<Rigidbody2D>();
            possesedBoxCollider2D = possesableObject.GetComponent<BoxCollider2D>();

            // Swaps to moving Possesed object
            player.movementController.SetObject(possesedRB2D, possesedBoxCollider2D);
            player.hintText.text = "Press E leave this item and return to your spirit form";
        }
    }

    public override void UpdateState(PlayerStateManager player)
    {

    }
    
    public override void FixedUpdateState(PlayerStateManager player)
    {

    }

    public override void ExitState(PlayerStateManager player)
    {
        player.movementController.SetObject(player._rb2d, player._boxCollider2D);
    }

    

}
