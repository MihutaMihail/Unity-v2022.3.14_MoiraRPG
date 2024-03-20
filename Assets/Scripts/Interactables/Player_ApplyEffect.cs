using UnityEngine;

public class Player_ApplyEffect : MonoBehaviour
{
    [SerializeField] private PlayerEffects.AffectedStat affectedStat;
    [SerializeField] private float multiplier;
    [SerializeField] private PlayerEffects.EffectType effectType;
    [SerializeField] private float duration;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerEffects>().ApplyEffect(
                affectedStat,
                multiplier,
                effectType,
                duration
                );
            Destroy(gameObject);
        }
    }
}
