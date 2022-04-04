using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] float hp;
    public float HP{
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
                GameOver();
            else if (hp >= MaxHP)
                hp = MaxHP;
        }
    
    }
    public const float MaxHP = 100f;

    [SerializeField] float pain;
    public float Pain
    {
        get => pain;
        set
        {
            pain = value;
            if (pain >= MaxHP)
                GameOver();
            else if (pain <= 0)
                pain = 0;
        }
    }
    public const float MaxPain = 100f;

    bool isGameOver;

    [Header("Ranking")]
    public List<Rank> rankingList = new List<Rank>();

    [Header("Score")]
    public int totalscore;
    public int enemyScore;
    public int itemScore;
    public int stageScore;

    Player player => Player.Instance;

    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GameOver()
    {
        totalscore = enemyScore + itemScore + stageScore;
        isGameOver = true;
        SceneManager.LoadScene("EndScene");
    }

    public void GameClear()
    {
        UIController.Instance.ClearPanel();
        Invoke("GameOver", 2f);
    }

    public void Init(int stage)
    {
        switch (stage)
        {
            case 1:
                totalscore = 0;
                itemScore = 0;
                enemyScore = 0;
                stageScore = 0;
                Pain = (int)(MaxHP * 0.1f);
                break;
            case 2:
                
                Pain = (int)(MaxHP * 0.3f);
                player.transform.position = new Vector3(0, -1, 0);

                break;

            default:
                break;
        }



        HP = MaxHP;
    }
}
