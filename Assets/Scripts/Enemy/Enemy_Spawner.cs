using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    public GameObject enemy;

    private void StartSpawner()
    {
        // NEED TO MAKE IT SO THAT IT SPAWNS INDEFINITELY AND ONE AT A TIME
        // EVERY 3 SECONDS FOR EXAMPLE, THE SPAWNER WILL BE DESTROYED ONCE THE 
        // BOSS INSIDE THE ROOM WHERE THE SPAWNER IS DIES
        for (int y = 0; y < 5; y++)
        {
            float newPositionX = transform.position.x + Random.Range(-2, 2);
            float newPositionY = transform.position.y + Random.Range(-2, 2);
            Instantiate(enemy, new Vector3(newPositionX, newPositionY, 0), Quaternion.identity);
        }
    }

    public void DestroySpawner()
    {
        Destroy(gameObject);
    }
}
