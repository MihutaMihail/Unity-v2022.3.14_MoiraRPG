using UnityEngine.UI;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    [SerializeField]
    private float _maxHp;

    public float _currentHp;

    [SerializeField] public Image _healthBar;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        SetHP(_maxHp);
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {

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
        else if (_currentHp <= 0 && gameObject.tag != "Player")
        {
            // StartCoroutine(EnnemyDeath());

            GameManager.Instance.EnnemyKilled();
            _currentHp = 0;
            Destroy(gameObject);
        }
        else if (_currentHp <= 0 && gameObject.tag == "Player")
        {
            _currentHp = 0;
            // PlayerDeath();
        }
        if (_currentHp > _maxHp)
            _currentHp = _maxHp;

        UpdateHealthBar();
    }
    public void UpdateHealthBar()
    {
        _healthBar.fillAmount = _currentHp / _maxHp;
    }
}
