using UnityEngine;

public class Potion_DamageBuff : MonoBehaviour
{
    [SerializeField] private float damageMultiplier;
    [SerializeField] private float buffDuration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().DamageBuff(damageMultiplier, buffDuration);
            Destroy(gameObject);
        }
    }
}
