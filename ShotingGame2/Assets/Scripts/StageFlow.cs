using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageFlow : Singleton<StageFlow>
{
    SpawnData spawnData => SpawnData.Instance;
    UIController uIController => UIController.Instance;

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
        uIController.bossHPGaugeController(false);
        StageAnim(stage);
        uIController.ChangeBackground(stage);
        spawnData.SpawnDataRead(stage);
    }

    public void EndStage()
    {
        stage++;
        if(stage > 2)
        {
            Debug.Log("∞‘¿” ≥°");
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
