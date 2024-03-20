using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    List<Collider2D> _ColliderHit = new();
    ContactFilter2D contactFilter2D = new ContactFilter2D();

    [SerializeField] PolygonCollider2D _collider;


    public void Activated()
    {
        gameObject.SetActive(true);

        Physics2D.OverlapCollider(_collider, contactFilter2D, _ColliderHit);

        foreach (Collider2D collision in _ColliderHit)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<LifeScript>().UpdateHp(GameManager.Instance._player.GetComponent<PlayerController>()._damage);
            }
        }
    }

    public void Desactivated()
    {
        gameObject.SetActive(false);
    }
}