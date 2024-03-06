using UnityEngine.UI;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    [SerializeField]
    private float _maxHp;

    public float _currentHp;

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

    public void UpdateHp(float amount)
    {
        _currentHp -= amount;
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

    public void UpdateHealthBar()
    {
        _healthBar.fillAmount = _currentHp / _maxHp;
    }
}
