using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivingState : PlayerBaseState
{
    float amulateCooldown = 5f;

    public override void EnterState(PlayerStateManager player)
    {
        player.movementController.SetObject(player._rb2d, player._boxCollider2D);
        player.hintText.text = "Press Q to leave your body behind";
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.SwitchState(player.GhostState);
        }
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {

    }
    public override void ExitState(PlayerStateManager player)
    {

    }
}
