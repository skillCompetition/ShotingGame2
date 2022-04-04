using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Boss
{
     StageFlow stageFlow => StageFlow.Instance;

    protected override void Start()
    {
        base.Start();
        
    }

    public override void Dead()
    {
        base.Dead();

        if (!bossManager.isLastMiniBoss)
            bossManager.isLastMiniBoss = true;
        else
        {
            uIController.bossHPGaugeController(false);
            stageFlow.EndStage();
        }

    }
}
