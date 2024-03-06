using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform checkpointTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && checkpointTransform != null)
            collision.gameObject.GetComponent<PlayerController>().SetCheckpoint(checkpointTransform);
    }
}
