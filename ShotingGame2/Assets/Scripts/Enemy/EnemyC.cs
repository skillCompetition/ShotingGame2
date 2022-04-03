using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : Enemy
{
    protected override void Start()
    {
        StartCoroutine(Attack());
        base.Start();
    }

    IEnumerator Attack()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 vec = player.transform.position - transform.position;

            GameObject bullet =  Instantiate(base.bullet, transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().power = power;

            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

            bullet.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

            yield return new WaitForSeconds(0.5f);
        }
    }

}
