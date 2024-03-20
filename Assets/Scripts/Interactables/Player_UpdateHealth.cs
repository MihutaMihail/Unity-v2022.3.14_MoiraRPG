using UnityEngine;

public class Player_UpdateHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private bool heal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<LifeScript>().UpdateHp(health, heal);
            Destroy(gameObject);
        }
    }
}
