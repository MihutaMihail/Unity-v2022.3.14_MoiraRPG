using UnityEngine;

public class UpdatePlayerHealth : MonoBehaviour
{
    [SerializeField] private int health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<LifeScript>().UpdateHp(health, true);
            Destroy(gameObject);
        }
    }
}
