using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageFlow : Singleton<StageFlow>
{
    SpawnData spawnData => SpawnData.Instance;
    UIController uIController => UIController.Instance;
    GameManager gameManager => GameManager.Instance;

    public int stage = 1;

    [Header("Animation")]
    [SerializeField] Animator anim;
    [SerializeField] Text stageText;

    void Start()
    {
        StartStage();
    }

    void Update()
    {
        
    }

    void StartStage()
    {
        gameManager.Init(stage);
        uIController.bossHPGaugeController(false);
        StageAnim(stage);
        uIController.ChangeBackground(stage);
        spawnData.SpawnDataRead(stage);
    }

    public void EndStage()
    {
        gameManager.stageScore += (int)(gameManager.HP + (GameManager.MaxPain - gameManager.Pain));
        stage++;
        if(stage > 2)
        {
            gameManager.GameClear();
        }
        else
        {
            StartStage();
        }
    }

    void StageAnim(int stage)
    {
        anim.SetTrigger("isShowStage");
        stageText.text =  "stage" + stage.ToString();
    }
}
