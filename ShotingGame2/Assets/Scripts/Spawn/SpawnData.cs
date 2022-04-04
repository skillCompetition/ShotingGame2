using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpawnData : Singleton<SpawnData>
{
    BossManager bossManager => BossManager.Instance;

    List<Spawn> spawnList = new List<Spawn>();

    [Header("SpawnPos")]
    [SerializeField] Transform[] spawnPoses;
    [SerializeField] Transform[] redPoses;
    [SerializeField] Transform[] whitePoses;

    [Header("SpawnPrefabs")]
    [SerializeField] GameObject[] enemies;

    [Header("Red")]
    [SerializeField] GameObject redPrefab;
    float redTimer = 0;
    [SerializeField] float redDelay;
    [SerializeField] int redRan;

    [Header("White")]
    [SerializeField] GameObject whitePrefab;
    float whiteTimer = 0;
    [SerializeField] float whiteDelay;
    [SerializeField] int whiteRan;

    void Start()
    {
        
    }

    void Update()
    {
        if (!bossManager.isBossesTime)
        {
            SpawnRedCheck();
            SpawnWhiteCheck();
        }

    }

    public void SpawnDataRead(int stage)
    {
        string name = "stage" + stage;
        TextAsset textAsset = Resources.Load(name) as TextAsset;
        StringReader stringReader = new StringReader(textAsset.ToString());

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();

            if (line == null)
                break;

            Spawn spawn = new Spawn();
            spawn.type = line.Split(',')[0];
            spawn.pos = int.Parse(line.Split(',')[1]);
            spawn.delay = float.Parse(line.Split(',')[2]);

            spawnList.Add(spawn);
        }

        StartCoroutine(SpawnEnemies(spawnList));

    }

    IEnumerator SpawnEnemies(List<Spawn> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Instantiate(ReturnEnemy(list[i].type), spawnPoses[list[i].pos].position, spawnPoses[list[i].pos].rotation);
            yield return new WaitForSeconds(list[i].delay);
        }
        spawnList.Clear();
        yield return new WaitForSeconds(2f);
        bossManager.ShowBoss();
    }

    GameObject ReturnEnemy(string name)
    {
        GameObject enemy = null;
        switch (name)
        {
            case "B":
                enemy = enemies[0];
                break;
            case "G":
                enemy = enemies[1];
                break;
            case "V":
                enemy = enemies[2];
                break;
            case "C":
                enemy = enemies[3];
                break;

            default:
                break;
        }

        return enemy;
    }


    void SpawnRedCheck()
    {
        redTimer += Time.deltaTime;
        if(redTimer >= redDelay)
        {
            if (Random.Range(0,redRan) == 0)
            {
                RedSpawn();
            }
            redTimer = 0;
        }
    }

    void RedSpawn()
    {
        Transform t = redPoses[Random.Range(0, redPoses.Length)];
        Instantiate(redPrefab,t.position,t.rotation);
    }

    void SpawnWhiteCheck()
    {
        whiteTimer += Time.deltaTime;
        if (whiteTimer >= whiteDelay)
        {
            if (Random.Range(0, whiteRan) == 0)
            {
                WhiteSpawn();
            }
            whiteTimer = 0;
        }
    }

    void WhiteSpawn()
    {
        Transform t = whitePoses[Random.Range(0, whitePoses.Length)];
        Instantiate(whitePrefab, t.position, t.rotation);
    }


}
