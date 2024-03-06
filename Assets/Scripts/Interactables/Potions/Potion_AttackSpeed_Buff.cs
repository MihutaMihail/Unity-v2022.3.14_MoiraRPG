using UnityEngine;

public class Potion_AttackSpeed_Buff : MonoBehaviour
{
    [SerializeField] float attackSpeedMultiplier;
    [SerializeField] float buffDuration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerEffects>().ApplyBuff(PlayerEffects.BuffType.AttackSpeed, attackSpeedMultiplier, buffDuration);
            Destroy(gameObject);
        }
    }
}
