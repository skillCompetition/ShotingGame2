using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    protected BossManager bossManager => BossManager.Instance;
    protected UIController uIController => UIController.Instance;

    public int MaxHP;
    [SerializeField] GameObject[] enemies;
    [SerializeField] Transform[] poses;

    public enum BossType
    {
        Boss,
        BossPlus
    }

    public BossType myBoss;

    protected override void Start()
    {
        StartCoroutine(Stop());
        base.Start();
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(2f);
        speed = 0;
        ChooseAttack();

    }

    int attackIndex = 0;
    void ChooseAttack()
    {
        if (attackIndex >= 4)
            attackIndex = 0;
        switch (attackIndex)
        {
            case 0:
                StartCoroutine(CircleAttack());
                break;
            case 1:
                StartCoroutine(SnakeAttack());
                break;
            case 2:
                StartCoroutine(SpawnEnemy());
                break;
            case 3:
                StartCoroutine(GotoCircle());
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Åº¸· ÆÐÅÏ
    /// </summary>
    /// <returns></returns>
    IEnumerator CircleAttack()
    {
        int repeat;
        if (myBoss == BossType.Boss)
            repeat = 3;
        else
            repeat = 5;

        for (int i = 0; i < repeat; i++)
        {
            for (int j = 0; j <= 360; j+= 13)
            {
                GameObject bullet = Instantiate(base.bullet, transform.position, transform.rotation);
                bullet.GetComponent<Bullet>().power = power;

                bullet.transform.rotation = Quaternion.Euler(0, 0, j);
            }

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1f);
        ++attackIndex;
        ChooseAttack();
    }

    /// <summary>
    /// ¹ì²¿¸® °ø°Ý
    /// </summary>
    /// <returns></returns>
    IEnumerator SnakeAttack()
    {
        int bulletNum = 51;

        for (int i = 0; i < bulletNum; i++)
        {
            Vector2 vec = new Vector2(Mathf.Sin(Mathf.PI * i * 10 / bulletNum), -1);
            GameObject bullet = Instantiate(base.bullet, transform.position, transform.rotation);
            Bullet bulletLogic = bullet.GetComponent<Bullet>();
            bulletLogic.power = power;
            bulletLogic.changeVec = vec;

            yield return new WaitForSeconds(0.2f);

        }

        yield return new WaitForSeconds(1f);
        ++attackIndex;
        ChooseAttack();

    }

    /// <summary>
    /// Àû±º ¼ÒÈ¯
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemy()
    {
        int num;
        if (myBoss == BossType.Boss)
            num = 2;
        else
            num = 3;

        for (int i = 0; i < num; i++)
        {
            if (myBoss == BossType.Boss)
            {
                Transform pos = poses[Random.Range(0, poses.Length)];
                Instantiate(enemies[Random.Range(0, enemies.Length)], pos.position, pos.rotation);
            }
            else
            {
                Instantiate(enemies[Random.Range(0, enemies.Length)], poses[0].position, poses[0].rotation);
                Instantiate(enemies[Random.Range(0, enemies.Length)], poses[1].position, poses[1].rotation);
            }

            yield return new WaitForSeconds(0.5f);
        }


        yield return new WaitForSeconds(1f);
        ++attackIndex;
        ChooseAttack();
    }

    IEnumerator GotoCircle()
    {
        if(myBoss == BossType.Boss)
        {
            ++attackIndex;
            ChooseAttack();
            yield break;
        }

        for (int i = 0; i < 3; i++)
        {
            List<Transform> b1 = new List<Transform>();

            for (int j = 0; j < 360; j+= 13)
            {

                GameObject bullet = Instantiate(base.bullet, transform.position, transform.rotation);
                bullet.GetComponent<Bullet>().power = power;
                bullet.transform.rotation = Quaternion.Euler(0, 0, j);


                b1.Add(bullet.transform);
            }

            StartCoroutine(Go(b1));

            yield return new WaitForSeconds(0.5f);
        }

        ++attackIndex;
        ChooseAttack();


    }

    IEnumerator Go(List<Transform> b1)
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < b1.Count; i++)
        {
            if (b1[i] == null)
                continue;

            Vector2 vec = player.transform.position - b1[i].transform.position;

            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

            b1[i].rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }


        b1.Clear();
    }

    public override void Dead()
    {
        if (TryGetComponent<MiniBoss>(out var mini))
        {
            Debug.Log("¹Ì´Ï º¸½º Á×À½");
        }
        else
        {
            uIController.bossHPGaugeController(false);
            bossManager.ShowMiniBoss();
        }
        base.Dead();
    }

}
