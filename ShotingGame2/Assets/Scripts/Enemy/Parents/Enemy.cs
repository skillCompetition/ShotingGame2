using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hp;
    public int HP {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                hp = 0;
                Dead();
            }


        }
    }

    public Vector3 moveVec;
    public Vector3 changeVec = Vector3.zero;
    public float speed;
    public int power;
    public int score;

    public bool isBoss;

    [SerializeField] protected GameObject bullet;

    protected Player player => Player.Instance;
    GameManager gameManager => GameManager.Instance;
    Animator anim;
    protected Collider2D col;

    protected virtual void Awake()
    {
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        moveVec = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeVec == Vector3.zero)
            transform.Translate(moveVec * speed * Time.deltaTime);
        else
            transform.Translate(changeVec * speed * Time.deltaTime);
    }

    public void OnHit(int damage)
    {
        anim.SetTrigger("isHit");
        HP -= damage;
    }

    public virtual void Dead()
    {
        col.enabled = false;
        gameManager.enemyScore += score;
        anim.SetTrigger("isDead");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (isBoss)
                return;
            Destroy();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            //몬스터의 공격력의 절반만큼 체력이 감소
            
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet.myBullet == Bullet.BulletType.Player)
                OnHit(bullet.power);
        }
    }
}
