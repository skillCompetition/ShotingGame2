using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : Singleton<BossManager>
{
    UIController uIController => UIController.Instance;
    StageFlow stageFlow => StageFlow.Instance;

    public bool isBossesTime;
    public bool isBossTime;
    public bool isMiniBossTime;

    public GameObject[] bossPrefabs;

    [Header("Boss")]
    public GameObject boss;

    [Header("MiniBoss")]
    public GameObject mini1;
    public GameObject mini2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NextLevelCheck();
        SpawnMiniBossCheck();
    }

    public void ShowBoss()
    {
        isBossesTime = true;
        isBossTime = true;
        boss =  Instantiate(bossPrefabs[stageFlow.stage - 1]);
    }

    public void ShowMiniBoss()
    {
        isMiniBossTime = true;

        mini1 = Instantiate(bossPrefabs[stageFlow.stage + 1]);
        mini2 = Instantiate(bossPrefabs[stageFlow.stage + 1]);

        mini1.GetComponent<MiniBoss>().changeVec = Vector3.right;
        mini2.GetComponent<MiniBoss>().changeVec = Vector3.left;

        UIController.Instance.MiniMaxHP = mini1.GetComponent<MiniBoss>().MaxHP + mini2.GetComponent<MiniBoss>().MaxHP;
    }  

    void SpawnMiniBossCheck()
    {
        if (boss == null && isBossTime)
        {
            isBossTime = false;
            uIController.bossHPGaugeController(false);
            ShowMiniBoss();

        }
    }
    
    void NextLevelCheck()
    {
        if ((mini1 == null && mini2 == null) && isMiniBossTime)
        {
            isBossesTime = false;
            isMiniBossTime = false;
            uIController.bossHPGaugeController(false);
            stageFlow.EndStage();
        }
    }
}
