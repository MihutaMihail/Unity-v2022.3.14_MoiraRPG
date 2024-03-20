using System.Collections;
using UnityEngine;

// This spawner only spawns only one type of enemy
public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] [Min(0)] private int nbEnemy = 2;
    [SerializeField] [Min(0)] private float timeBetweenWaves = 5f;
    [SerializeField] [Min(0)] private float spawnOffsetRange = 2f;

    // FOR TESTING PURPOSES
    // IN PRACTICE, ANOTHER GAMEOBJECT WILL CALL THE FUNCTION StartSpawner()
    /*void Start()
    {
        StartSpawner();
    }*/

    public void StartSpawner()
    {
        StartCoroutine(EnemySpawnerCoroutine(nbEnemy, timeBetweenWaves, spawnOffsetRange));
    }

    // Spawn enemies indefinitely
    private IEnumerator EnemySpawnerCoroutine(int nbEnemy, float timeBetweenWaves, float spawnOffsetRange)
    {
        while (true)
        {
            for (int i = 0; i < nbEnemy; i++)
            {
                // Make the enemy spawn a little more random as to add some variety
                float randomOffsetPositionX = transform.position.x + Random.Range(-spawnOffsetRange, spawnOffsetRange);
                float randomOffsetPositionY = transform.position.y + Random.Range(-spawnOffsetRange, spawnOffsetRange);

                // Clone a new enemy from a pre-existing prefab
                Instantiate(enemy, new Vector3(randomOffsetPositionX, randomOffsetPositionY, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    public void StopSpawner()
    {
        StopCoroutine(EnemySpawnerCoroutine(nbEnemy, timeBetweenWaves, spawnOffsetRange));
    }

    public void DestroySpawner()
    {
        Destroy(gameObject);
    }
}
