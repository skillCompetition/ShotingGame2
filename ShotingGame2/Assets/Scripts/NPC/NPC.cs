using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed;

    public enum NPCType
    {
        Red,
        White
    }
    public NPCType myNPC;

    void Start()
    {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Move()
    {
        float timer = 0f;
        while (true)
        {
            timer = 0f;
            int dir = Random.Range(-1, 2);
            Vector3 moveVec = new Vector3(dir, -1, 0);
            while (true)
            {
                timer += Time.deltaTime;
                transform.Translate(moveVec * speed * Time.deltaTime);

                if (timer >= 1f)
                    break;
                yield return new WaitForEndOfFrame();
            }

        }
    }

    void Use()
    {
        if (myNPC == NPCType.Red)
        {
            //��������� ����
        }
        else
        {
            //������ ��ȯ
        }

        Dead();
    }

    void Dead()
    {
        Destroy(gameObject);
    }


}
