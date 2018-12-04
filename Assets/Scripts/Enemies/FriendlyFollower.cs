using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyFollower : EscapeShooterEnemy
{
    //follow the player if no enemy is near
    protected override void FightingUpdate()
    {
        base.FightingUpdate();
        if (target == null)
        {
            if (GameController.Instance.player)
            {
                agent.SetDestination(GameController.Instance.player.transform.position);
                agent.updateRotation = true;
            }
            
        }
    }
}
