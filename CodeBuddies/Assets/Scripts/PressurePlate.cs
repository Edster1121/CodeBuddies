using System.Collections;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject[] breakableTiles; // Assign breakable tiles in the inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // If player steps on it
        {
            Debug.Log("Pressure plate activated! Destroying tiles...");
            foreach (GameObject tile in breakableTiles)
            {
                tile.SetActive(false); // Remove the tiles
            }
        }
    }
}
