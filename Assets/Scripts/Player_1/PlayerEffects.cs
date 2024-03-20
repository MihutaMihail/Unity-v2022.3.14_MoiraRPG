using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private GameObject _iconDamageBuff;
    [SerializeField] private GameObject _iconDamageDebuff;

    [SerializeField] private GameObject _iconSpeedBuff;
    [SerializeField] private GameObject _iconSpeedDebuff;

    [SerializeField] private GameObject _iconAttackSpeedBuff;
    [SerializeField] private GameObject _iconAttackSpeedDebuff;

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

    private Dictionary<AffectedStat, Dictionary<EffectType, GameObject>> iconMap;
    private Dictionary<(AffectedStat, EffectType), Coroutine> activeCoroutines;
    
    void Start()
    {
        pc = GetComponent<PlayerController>();

        // Initialize iconMap
        iconMap = new Dictionary<AffectedStat, Dictionary<EffectType, GameObject>>()
        {
            {AffectedStat.Damage, new Dictionary<EffectType, GameObject>() { {EffectType.Buff, _iconDamageBuff}, {EffectType.Debuff, _iconDamageDebuff}}},
            {AffectedStat.Speed, new Dictionary<EffectType, GameObject>() { {EffectType.Buff, _iconSpeedBuff}, {EffectType.Debuff, _iconSpeedDebuff}}},
            {AffectedStat.AttackSpeed, new Dictionary<EffectType, GameObject>() { {EffectType.Buff, _iconAttackSpeedBuff}, {EffectType.Debuff, _iconAttackSpeedDebuff}}}
        };

        // Initialize activeCoroutines
        activeCoroutines = new Dictionary<(AffectedStat, EffectType), Coroutine>();
    }
    
    // Apply effect to the player
    public void ApplyEffect(AffectedStat stat, float multiplier, EffectType type, float duration)
    {
        // Check if effect is already active
        var key = (stat, type);
        if (activeCoroutines.ContainsKey(key))
        {
            StopCoroutine(activeCoroutines[key]);
            RemoveEffectChanges(stat, multiplier, type);
        }
        
        Coroutine coroutine = StartCoroutine(ApplyEffectCoroutine(stat, multiplier, type, duration));
        activeCoroutines[key] = coroutine; // Store the coroutine instance
    }

    private IEnumerator ApplyEffectCoroutine(AffectedStat stat, float multiplier, EffectType type, float duration)
    {
        // Apply effects
        ApplyEffectChanges(stat, multiplier, type);

        yield return new WaitForSeconds(duration);
        
        // Removes effects
        RemoveEffectChanges(stat, multiplier, type);
        activeCoroutines.Remove((stat,type)); // Remove the coroutine instance
    }

    private void ApplyEffectChanges(AffectedStat stat, float multiplier, EffectType type)
    {
        switch (stat)
        {
            case AffectedStat.Damage:
                pc._damage *= multiplier;
                break;
            case AffectedStat.Speed:
                pc.normalSpeed *= multiplier;
                pc.sprintSpeed *= multiplier;
                break;
            case AffectedStat.AttackSpeed:
                pc._reloadTime /= multiplier;
                break;
        }
        iconMap[stat][type].SetActive(true);
    }

    private void RemoveEffectChanges(AffectedStat stat, float multiplier, EffectType type)
    {
        switch (stat)
        {
            case AffectedStat.Damage:
                pc._damage /= multiplier;
                break;
            case AffectedStat.Speed:
                pc.normalSpeed /= multiplier;
                pc.sprintSpeed /= multiplier;
                break;
            case AffectedStat.AttackSpeed:
                pc._reloadTime *= multiplier;
                break;
        }
        iconMap[stat][type].SetActive(false);
    }
}
