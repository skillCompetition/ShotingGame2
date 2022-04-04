using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : Enemy
{
    protected override void Start()
    {
        Go_Player();
        base.Start();
    }

    void Go_Player()
    {
        Vector3 vec = player.transform.position - transform.position;

        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

}
