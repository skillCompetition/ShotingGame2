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

    [Header("SpawnPrefabs")]
    [SerializeField] GameObject[] enemies;
    void Start()
    {
        
    }

    void Update()
    {
        
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
}
