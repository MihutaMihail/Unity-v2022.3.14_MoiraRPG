using UnityEngine;

public class Potion_Health : MonoBehaviour
{
    [SerializeField] private int healthToRecover;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<LifeScript>().UpdateHp(healthToRecover, true);
            Destroy(gameObject);
        }
    }
}
