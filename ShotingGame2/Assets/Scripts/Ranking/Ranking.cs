using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    List<Rank> rankingList => GameManager.Instance.rankingList;

    [Header("UI")]
    [SerializeField] GameObject inputPanel;
    [SerializeField] InputField input;
    [SerializeField] Text[] nameText; 
    [SerializeField] Text[] scoreText; 

    void Start()
    {
        //inputPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void InputBtnClick()
    {
        string name = input.text;
        int score = 121;

        InputRank(name, score);
        inputPanel.SetActive(false);


    }

    void InputRank(string name, int score)
    {
        Rank rank = new Rank();

        rank.name = name;
        rank.score = score;

        rankingList.Add(rank);

        RankingSet();
    }

    void RankingSet()
    {
        rankingList.Sort((rank1, rank2) => rank1.score.CompareTo(rank2.score));
        rankingList.Reverse();

        if (rankingList.Count > 5)
            rankingList.RemoveAt(4);

        ShowRanking();
    }

    void ShowRanking()
    {
        for (int i = 0; i < rankingList.Count; i++)
        {
            nameText[i].text = rankingList[i].name;
            scoreText[i].text = rankingList[i].score.ToString();
        }
    }
}
