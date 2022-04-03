using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] float speed;

    [Header("Bullet")]
    [SerializeField] GameObject[] bullets;
    public int bulletLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FireCheck();
    }

    void FixedUpdate()
    {
        Move();

    }

    /// <summary>
    /// ������
    /// </summary>
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(h, v, 0) * speed * Time.fixedDeltaTime);
    }

    float fireTime;
    /// <summary>
    /// ���� üũ
    /// </summary>
    void FireCheck()
    {
        fireTime += Time.deltaTime;
        if (fireTime >= 0.3f && Input.GetMouseButton(0))
            Fire();
    }

    /// <summary>
    /// ����
    /// </summary>
    void Fire()
    {
        Instantiate(bullets[bulletLevel], transform.position, transform.rotation);
        fireTime = 0;
    }
}
