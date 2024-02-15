using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] CircleCollider2D _CircleCollider;
    [SerializeField] float _AttackRange = 0.3f;
    string _TargetTag = "Player";
    [SerializeField] float _ReloadAttack = 0.5f;
    [SerializeField] int _damage = 4;

    bool _bCanAttack = true;
    bool _PlayerIsInRange = false;


    // Start is called before the first frame update
    void Start()
    {
        _CircleCollider.radius = _AttackRange;
        _CircleCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_PlayerIsInRange && _bCanAttack)
        {
            GameManager.Instance._player.GetComponent<LifeScript>().UpdateHp(_damage);
            _bCanAttack = false;
            StartCoroutine("ReloadAttack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_TargetTag))
        {
            _PlayerIsInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_TargetTag))
        {
            _PlayerIsInRange = false;
        }
    }


    public IEnumerator ReloadAttack()
    {
        yield return new WaitForSeconds(_ReloadAttack);
        _bCanAttack = true;
    }
}
