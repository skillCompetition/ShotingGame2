using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyV : Enemy
{
    float dashSpeed = 5f;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Dash());
        StartCoroutine(Attack());

    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i <= 360; i += 30)
        {            
            GameObject bullet =  Instantiate(base.bullet, transform.position, transform.rotation);
            bullet.transform.rotation = Quaternion.Euler(0, 0, i);
            bullet.GetComponent<Bullet>().power = power;
        }
    }

    IEnumerator Dash()
    {
        float temp = speed;

        yield return new WaitForSeconds(0.2f);

        speed = dashSpeed;

        yield return new WaitForSeconds(0.5f);
        speed = temp;
    }
}
