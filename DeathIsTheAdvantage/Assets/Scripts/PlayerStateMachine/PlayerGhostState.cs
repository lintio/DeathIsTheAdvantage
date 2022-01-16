using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostState : PlayerBaseState
{
    GameObject playerTempBody;
    public override void EnterState(PlayerStateManager player)
    {
        // instantiate body object
        player.AddBody();
        // switch players Layer to ghost layer??
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.FindPossesableObjectInRange() != null)
        {
            player.hintText.text = "Press E to Possess this item";
        }
        if (player.FindPossesableObjectInRange() != null && Input.GetKeyDown(KeyCode.E))
        {
            player.SwitchState(player.PossesingState);
        }
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {

    }

    public override void ExitState(PlayerStateManager player)
    {

    }
}
