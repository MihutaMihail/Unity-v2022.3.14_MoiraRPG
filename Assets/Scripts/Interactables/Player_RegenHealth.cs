using UnityEngine;

public class Player_RegenHealth : MonoBehaviour
{
    [SerializeField] private float hpPerTick = 2f;
    [SerializeField] private float tickInterval = 1f;
    [SerializeField] private float duration = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<LifeScript>().HealthRegen(hpPerTick, tickInterval, duration);
            Destroy(gameObject);
        }
    }
}
