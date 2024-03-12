using System.Collections;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private GameObject _iconDamageBuff;
    [SerializeField] private GameObject _iconDamageDebuff;

    [SerializeField] private GameObject _iconAttackSpeedBuff;
    [SerializeField] private GameObject _iconAttackSpeedDebuff;
    
    [SerializeField] private GameObject _iconSpeedBuff;
    [SerializeField] private GameObject _iconSpeedDebuff;

    private PlayerController pc;

    public enum AffectedStat
    {
        Damage,
        Speed,
        AttackSpeed
    }

    public enum EffectType
    {
        Buff,
        Debuff
    }

    void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    // Apply any affect
    public void ApplyEffect(AffectedStat stat, float multiplier, EffectType type, float duration)
    {
        StartCoroutine(ApplyEffectCoroutine(stat, multiplier, type, duration));
    }
    
    private IEnumerator ApplyEffectCoroutine(AffectedStat stat, float multiplier, EffectType type, float duration)
    {
        // Start applying effects
        switch (stat)
        {
            case AffectedStat.Damage:
                pc._damage *= multiplier;
                switch (type)
                {
                    case EffectType.Buff:
                        _iconDamageBuff.SetActive(true);
                        break;
                    case EffectType.Debuff:
                        _iconDamageDebuff.SetActive(true);
                        break;
                }
                break;
            case AffectedStat.Speed:
                pc.normalSpeed *= multiplier;
                pc.sprintSpeed *= multiplier;
                switch (type)
                {
                    case EffectType.Buff:
                        _iconSpeedBuff.SetActive(true);
                        break;
                    case EffectType.Debuff:
                        _iconSpeedDebuff.SetActive(true);
                        break;
                }
                break;
            case AffectedStat.AttackSpeed:
                pc._reloadTime /= multiplier;
                switch (type)
                {
                    case EffectType.Buff:
                        _iconAttackSpeedBuff.SetActive(true);
                        break;
                    case EffectType.Debuff:
                        _iconAttackSpeedDebuff.SetActive(true);
                        break;
                }
                break;
        }

        yield return new WaitForSeconds(duration);

        // Remove added effects
        switch (stat)
        {
            case AffectedStat.Damage:
                pc._damage /= multiplier;
                switch (type)
                {
                    case EffectType.Buff:
                        _iconDamageBuff.SetActive(false);
                        break;
                    case EffectType.Debuff:
                        _iconDamageDebuff.SetActive(false);
                        break;
                }
                break;
            case AffectedStat.Speed:
                pc.normalSpeed /= multiplier;
                pc.sprintSpeed /= multiplier;
                switch (type)
                {
                    case EffectType.Buff:
                        _iconSpeedBuff.SetActive(false);
                        break;
                    case EffectType.Debuff:
                        _iconSpeedDebuff.SetActive(false);
                        break;
                }
                break;
            case AffectedStat.AttackSpeed:
                pc._reloadTime *= multiplier;
                switch (type)
                {
                    case EffectType.Buff:
                        _iconAttackSpeedBuff.SetActive(false);
                        break;
                    case EffectType.Debuff:
                        _iconAttackSpeedDebuff.SetActive(false);
                        break;
                }
                break;
        }
    }
}
