using UnityEngine;

// This script will be added to any NPC that has something to say to the Player
// It serves the purpose of starting the conversation

[RequireComponent(typeof(Dialogue_Trigger))]
public class NPC_Dialogue : MonoBehaviour
{
    private bool playerInZone = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerInZone && Input.GetKeyDown(KeyCode.E)) 
        {
            GetComponent<Dialogue_Trigger>().TriggerDialogue();  
        }
    }
}
