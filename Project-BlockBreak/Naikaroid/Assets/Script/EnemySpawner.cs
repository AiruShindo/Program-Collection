using JetBrains.Annotations;
using System;
using UnityEngine;


//“G‚ð¶¬‚·‚éƒXƒNƒŠƒvƒg
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;                  //“G‚ÌƒvƒŒƒnƒu
    public Transform spawnPoint;                    //ƒXƒ|[ƒ“‚·‚éˆÊ’u
    private float spawnTimer = 0.0f;                //ŽžŠÔ‚ðŠi”[‚·‚é•Ï”
    private float spawnTime = 5.0f;                 //¶¬‚·‚é‚Ü‚Å‚ÌŽžŠÔ‚ªŠi”[‚³‚ê‚½•Ï”

    [SerializeField] private int maxEnemyCount = 4;     //Å‘å‚Å¶¬‚·‚é“G‚Ì”
    [SerializeField] private int currentEnemyCount = 0; //Œ»Ý¶¬‚³‚ê‚Ä‚¢‚é“G‚Ì”

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        //ˆê’èŽžŠÔ’´‚¦‚é‚Æˆê’è”‚Ü‚Å“G‚Ì¶¬‚ðs‚¤
        if (spawnTimer > spawnTime)
        {
            if (currentEnemyCount < maxEnemyCount)
            {
                SpawnEnemy();
            }

            spawnTimer = 0.0f;      //ƒ^ƒCƒ}[ƒŠƒZƒbƒg
        }
    }

    //“G‚ð¶¬‚·‚éƒƒ\ƒbƒh
    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        currentEnemyCount++;
    }

    // “G‚ª”j‰ó‚³‚ê‚½‚Æ‚«‚ÉŒÄ‚Ño‚·ƒƒ\ƒbƒhi—áF“G‚ª“|‚³‚ê‚½Žžj
    public void EnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
