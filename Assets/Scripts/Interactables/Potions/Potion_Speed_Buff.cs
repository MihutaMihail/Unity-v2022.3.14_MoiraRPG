using UnityEngine;

public class Potion_Speed_Buff : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float buffDuration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // collision.gameObject.GetComponent<PlayerEffects>().ApplyBuff(speedMultiplier, buffDuration);
            Destroy(gameObject);
        }
    }
}
