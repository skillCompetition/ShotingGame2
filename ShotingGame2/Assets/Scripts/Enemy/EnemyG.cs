using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyG : Enemy
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(base.bullet, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.5f);
        }

    }

}
