using System.Collections;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private GameObject _iconDamageBuff;
    [SerializeField] private GameObject _iconAttackSpeedBuff;
    [SerializeField] private GameObject _iconSpeedBuff;
    private PlayerController pc;

    public enum BuffType
    {
        Damage,
        Speed,
        AttackSpeed
    }

    void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    // Apply any buff
    public void ApplyBuff(BuffType type, float multiplier, float buffDuration)
    {
        StartCoroutine(BuffCoroutine(type, multiplier, buffDuration));
    }
    
    private IEnumerator BuffCoroutine(BuffType type, float multiplier, float duration)
    {
        // Start buffing
        switch (type)
        {
            case BuffType.Damage:
                pc._damage *= multiplier;
                _iconDamageBuff.SetActive(true);
                break;
            case BuffType.Speed:
                pc.normalSpeed *= multiplier;
                pc.sprintSpeed *= multiplier;
                _iconSpeedBuff.SetActive(true);
                break;
            case BuffType.AttackSpeed:
                pc._reloadTime /= multiplier;
                _iconAttackSpeedBuff.SetActive(true);
                break;
        }

        yield return new WaitForSeconds(duration);

        // Stop buffing
        switch (type)
        {
            case BuffType.Damage:
                pc._damage /= multiplier;
                _iconDamageBuff.SetActive(false);
                break;
            case BuffType.Speed:
                pc.normalSpeed /= multiplier;
                pc.sprintSpeed /= multiplier;
                _iconSpeedBuff.SetActive(false);
                break;
            case BuffType.AttackSpeed:
                pc._reloadTime *= multiplier;
                _iconAttackSpeedBuff.SetActive(false);
                break;
        }
    }
}
