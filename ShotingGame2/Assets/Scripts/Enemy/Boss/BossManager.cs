using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : Singleton<BossManager>
{
    public bool isBossTime;
    public bool isLastMiniBoss;

    [Header("Boss")]
    [SerializeField] GameObject bossPrefabs;
    public GameObject boss;

    [Header("MiniBoss")]
    [SerializeField] GameObject miniPrefabs;
    public GameObject mini1;
    public GameObject mini2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowBoss()
    {
        isBossTime = true;
        boss =  Instantiate(bossPrefabs);
    }

    public void ShowMiniBoss()
    {
        mini1 = Instantiate(miniPrefabs);
        mini2 = Instantiate(miniPrefabs);

        mini1.GetComponent<MiniBoss>().changeVec = Vector3.right;
        mini2.GetComponent<MiniBoss>().changeVec = Vector3.left;

        UIController.Instance.MiniMaxHP = mini1.GetComponent<MiniBoss>().MaxHP + mini2.GetComponent<MiniBoss>().MaxHP;
    }                                     
}
