using UnityEngine;

public class Potion_Damage_Buff : MonoBehaviour
{
    [SerializeField] private float damageMultiplier;
    [SerializeField] private float buffDuration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerEffects>().DamageBuff(damageMultiplier, buffDuration);
            Destroy(gameObject);
        }
    }
}
