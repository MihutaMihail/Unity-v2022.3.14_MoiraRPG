using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    [SerializeField]
    private float _maxHp;

    public float _currentHp;

    [SerializeField] private bool _healthRegen = false;
    
    [SerializeField] public Image _healthBar;

    private void Awake()
    {
        SetHP(_maxHp);
        UpdateHealthBar();
    }
    
    public void SetHP(float amount)
    {
        _currentHp = amount;
        UpdateHealthBar();
    }

    public void UpdateHp(float amount, bool heal = false)
    {
        _ = heal ? _currentHp += amount : _currentHp -= amount;
        if (_currentHp > 0)
        {

        }
        // Enemy death
        else if (_currentHp <= 0 && gameObject.tag != "Player")
        {
            // StartCoroutine(EnnemyDeath());

            GameManager.Instance.EnnemyKilled();
            _currentHp = 0;
            Destroy(gameObject);
        }
        // Player death
        else if (_currentHp <= 0 && gameObject.tag == "Player")
        {
            _currentHp = _maxHp;
            GetComponent<PlayerController>().PlayerDeath();
        }

        // If current HP exceeds max HP, set HP back to max HP
        if (_currentHp > _maxHp)
            _currentHp = _maxHp;

        UpdateHealthBar();
    }
    
    public void HealthRegen(float amount, float tickInterval, float duration)
    {
       if (!_healthRegen)
            StartCoroutine(HealthRegenCoroutine(amount, tickInterval, duration));
    }
    
    private IEnumerator HealthRegenCoroutine(float amount, float interval, float duration)
    {
        Debug.Log("START health regen");
        _healthRegen = true;
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            _currentHp += amount;
            if (_currentHp > _maxHp) _currentHp = _maxHp;
            UpdateHealthBar();

            yield return new WaitForSeconds(interval);
            elapsedTime += interval;
        }

        Debug.Log("STOP health regen");
        _healthRegen = false;
    }

    public void UpdateHealthBar()
    {
        _healthBar.fillAmount = _currentHp / _maxHp;
    }
}
