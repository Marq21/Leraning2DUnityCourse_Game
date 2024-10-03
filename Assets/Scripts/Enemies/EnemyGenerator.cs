using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public GameObject enemy;
    public GameObject bonusEnemy;
    public GameObject armoredEnemy;
    public Player player;
    public Sprite[] asteroidSprites;
    private float playerScore;
    private Vector3 enemyPositionSpawn = new Vector3(0, 5, 0);
    private float timeBetweenWaves = 10f;
    private Vector2 posBonusEnemy;
    private GameObject upcastingBonusEnemy;
    private List<GameObject> enemiesPool;
    private int poolSize = 40;

    void Start()
    {
        posBonusEnemy = new Vector2(Random.Range(-8f, 8f), 5);
        StartCoroutine(GenerateAnotherWave());
        StartCoroutine(WaitBonusEvent(Random.Range(12f, 40f)));
        StartCoroutine(GenerateArmoredEnemy());
        enemiesPool = new List<GameObject>();
    }

    void Update()
    {
        mAllocEnemyPool();
    }

    IEnumerator GenerateAnotherWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        GenerateEnemyLine();
        StartCoroutine(GenerateAnotherWave());
    }

    IEnumerator WaitBonusEvent(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (upcastingBonusEnemy != null && !upcastingBonusEnemy.activeSelf)
        {
            Destroy(upcastingBonusEnemy);
        }

        if (!bonusEnemy.activeSelf)
        {
            bonusEnemy.SetActive(true);
        }
        upcastingBonusEnemy = Instantiate(bonusEnemy, posBonusEnemy, Quaternion.identity);
        upcastingBonusEnemy.name = "BonusAsteroid" + Random.Range(0, 100);
        posBonusEnemy = new Vector2(Random.Range(-8f, 8f), 5);
        StartCoroutine(WaitBonusEvent(Random.Range(12f, 40f)));
    }

    IEnumerator GenerateArmoredEnemy()
    {
        float timeBetweenSpwan = Random.Range(10f, 40f);
        yield return new WaitForSeconds(timeBetweenSpwan);
        enemyPositionSpawn.x = Random.Range(-7f, 8f);
        GameObject tempEnemy = Instantiate(armoredEnemy, enemyPositionSpawn, Quaternion.identity);
        enemiesPool.Add(tempEnemy);
        enemyPositionSpawn.Set(0, 5, 0);
        StartCoroutine(GenerateArmoredEnemy());
    }


    private void GenerateEnemyLine()
    {

        for (float y = 0; y <= 3;)
        {
            enemyPositionSpawn.y += y;
            for (float x = 0; x <= 9;)
            {
                if (GenerateBoolean())
                {
                    enemyPositionSpawn.x = x - 5;
                    GameObject tempEnemy = Instantiate(enemy, enemyPositionSpawn, Quaternion.identity);
                    tempEnemy.name = "Asteroid" + Random.Range(0, 100);
                    enemy.GetComponent<SpriteRenderer>().sprite = GenRandomSprite();
                    enemiesPool.Add(tempEnemy);
                }
                x += 0.5f;
            }
            y += 0.5f;
        }
        // restore defalut position value for spwn enemies
        enemyPositionSpawn.Set(0, 5, 0);
    }

    private void mAllocEnemyPool()
    {
        if (enemiesPool.Count >= poolSize)
        {
            List<GameObject> localList = new List<GameObject>();
            foreach (var enemy in enemiesPool)
            {
                if (!enemy.activeSelf)
                {
                    localList.Add(enemy);
                    Destroy(enemy);
                }
            }
            enemiesPool.RemoveAll(x => localList.Contains(x));
        }
    }

    private bool GenerateBoolean()
    {
        return Random.Range(0, 2) == 1;
    }

    private Sprite GenRandomSprite()
    {
        return asteroidSprites[Random.Range(0, asteroidSprites.Length)];
    }
}
