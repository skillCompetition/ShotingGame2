using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Singleton<Player>
{
    [SerializeField] float speed;

    [Header("Bullet")]
    [SerializeField] GameObject[] bullets;
    public int bulletLevel;

    [Header("SuperShot")]
    [SerializeField] GameObject superBullet;
    public Slider slider;

    [Header("Boom")]
    public int boom;
    [SerializeField] GameObject[] boomImg;
    void Start()
    {
        for (int i = 0; i < boomImg.Length; i++)
        {
            boomImg[i].SetActive(false);
        }
    }

    void Update()
    {
        FireCheck();
        SuperShotCheck();
        CheckBoom();
    }

    void FixedUpdate()
    {
        Move();

    }

    /// <summary>
    /// 움직임
    /// </summary>
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(h, v, 0) * speed * Time.fixedDeltaTime);
    }

    float fireTime;
    /// <summary>
    /// 공격 체크
    /// </summary>
    void FireCheck()
    {
        fireTime += Time.deltaTime;
        if (fireTime >= 0.3f && Input.GetMouseButton(0))
            Fire();
    }

    /// <summary>
    /// 공격
    /// </summary>
    void Fire()
    {
        if (!issuperShot)
        {
            Instantiate(bullets[bulletLevel], transform.position, transform.rotation);
            fireTime = 0;
        }

    }

    void SuperShotCheck()
    {
        if (Input.GetMouseButton(1) && slider.value >= 100)
        {
            StartCoroutine(SuperShotAttack());
        }
    }

    public bool issuperShot;
    IEnumerator SuperShotAttack()
    {
        float timer = 0f;
        issuperShot = true;
        while (true)
        {
            timer += Time.deltaTime;
            slider.value -= 1;
            Instantiate(superBullet, transform.position, transform.rotation);
            if (timer >= 1f)
               break;
            yield return new WaitForEndOfFrame();
        }

        issuperShot = false; 
    }

    public void BoomPlus()
    {
        boom++;
        boomImg[boom - 1].SetActive(true);
    }


    void CheckBoom()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            UseBoom();
        }
    }

    public void UseBoom()
    {
        boomImg[boom - 1].SetActive(false);
        boom--;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if(collision.gameObject.TryGetComponent<Bullet>(out var logic))
            {
                if(logic.myBullet == Bullet.BulletType.Enemy)
                {
                    slider.value += 10;
                }
            }
        }
    }


}
