using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private GameObject _iconDamageBuff;
    [SerializeField] private GameObject _iconAttackSpeedBuff;
    [SerializeField] private GameObject _iconSpeedBuff;
    private PlayerController pc;

    void Start()
    {
        pc = GetComponent<PlayerController>();    
    }
    
    //
    // Damage
    //

    public void DamageBuff(float damageMultiplier, float buffDuration)
    {
        StartCoroutine(DamageBuffCoroutine(damageMultiplier, buffDuration));
    }

    private IEnumerator DamageBuffCoroutine(float amount, float duration)
    {
        pc._damage *= amount;
        _iconDamageBuff.SetActive(true);

        yield return new WaitForSeconds(duration);
        pc._damage /= amount;
        _iconDamageBuff.SetActive(false);
    }

    //
    // Speed
    //

    public void SpeedBuff(float speedMultlipier, float buffDuration)
    {
        StartCoroutine(SpeedBuffCoroutine(speedMultlipier, buffDuration));
    }
    
    private IEnumerator SpeedBuffCoroutine(float amount, float duration)
    {
        pc.normalSpeed *= amount;
        pc.sprintSpeed *= amount;
        _iconSpeedBuff.SetActive(true);

        yield return new WaitForSeconds(duration);
        pc.normalSpeed /= amount;
        pc.sprintSpeed /= amount;
        _iconSpeedBuff.SetActive(false);
    }
}
