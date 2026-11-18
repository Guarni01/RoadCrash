using UnityEngine;
using UnityEngine.SceneManagement;

// Da attaccare alla macchina del giocatore.
public class CarCollision : MonoBehaviour
{
    public RoadScroller road;      // trascina qui il RoadManager
    public LaneMover laneMover;    // trascina qui la macchina stessa (o lascia vuoto)

    bool isDead = false;

    void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isDead = true;
        Debug.Log("Game Over!");

        // Fermo la strada e i controlli
        if (road != null) road.enabled = false;
        if (laneMover != null) laneMover.enabled = false;

        // Riavvia la scena dopo 1.5 secondi
        Invoke(nameof(Restart), 1.5f);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
