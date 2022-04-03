using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    [Header("Ranking")]
    public List<Rank> rankingList = new List<Rank>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GameOver()
    {

    }
}
