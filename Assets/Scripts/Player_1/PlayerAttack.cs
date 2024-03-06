using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Hit");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<LifeScript>().UpdateHp(GameManager.Instance._player.GetComponent<PlayerController>()._damage);
        }
    }
}
