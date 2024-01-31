using UnityEngine;

// This script will be added to any NPC that has something to say to the Player
// It serves the purpose of starting the conversation

[RequireComponent(typeof(Dialogue_Trigger))]
public class NPC_Dialogue : MonoBehaviour
{
    private bool playerInZone = false;
    private bool canInitiateDialogue = false;

    void Update()
    {
        if (playerInZone && canInitiateDialogue && Input.GetKeyDown(KeyCode.E))
        {
            canInitiateDialogue = false;
            GetComponent<Dialogue_Trigger>().TriggerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInZone = true;
            canInitiateDialogue = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInZone = false;
            canInitiateDialogue = false;
        }
    }
}
