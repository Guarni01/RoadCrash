using UnityEngine;

// Da attaccare alla macchina del giocatore.
public class LaneMover : MonoBehaviour
{
    public float laneDistance = 2f;     // distanza tra le corsie sull'asse X
    public float laneChangeSpeed = 12f; // velocità del cambio corsia
    public float swipeThreshold = 50f;  // pixel minimi per contare come swipe

    int currentLane = 1;                // 0 = sinistra, 1 = centro, 2 = destra
    Vector2 touchStart;

    void Update()
    {
        // --- Tastiera (per testare nell'editor) ---
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            ChangeLane(-1);
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            ChangeLane(1);

        // --- Swipe su touchscreen ---
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
                touchStart = t.position;
            else if (t.phase == TouchPhase.Ended)
            {
                float dx = t.position.x - touchStart.x;
                if (Mathf.Abs(dx) > swipeThreshold)
                    ChangeLane(dx > 0 ? 1 : -1);
            }
        }

        // --- Movimento fluido verso la corsia target ---
        float targetX = (currentLane - 1) * laneDistance;
        Vector3 pos = transform.position;
        pos.x = Mathf.MoveTowards(pos.x, targetX, laneChangeSpeed * Time.deltaTime);
        transform.position = pos;
    }

    void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, 0, 2);
    }
}
