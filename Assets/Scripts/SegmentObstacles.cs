using UnityEngine;

// Da attaccare a OGNI segmento di strada.
// Crea nel segmento un pool di ostacoli e li riposiziona a caso
// ogni volta che il segmento viene riciclato.
public class SegmentObstacles : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;   // prefab degli ostacoli
    public int obstaclesPerSegment = 3;
    public float laneDistance = 2f;        // deve combaciare con LaneMover
    public float segmentLength = 30f;
    public float firstObstacleOffset = 5f; // evita ostacoli a inizio segmento

    GameObject[] pool;

    void Awake()
    {
        // Instanzio una volta sola: poi li riuso
        pool = new GameObject[obstaclesPerSegment];
        for (int i = 0; i < pool.Length; i++)
        {
            var prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            pool[i] = Instantiate(prefab, transform);
        }
        Randomize();
    }

    // Chiamala da RoadScroller ogni volta che il segmento viene riciclato
    public void Randomize()
    {
        // Divido il segmento in "fette" così gli ostacoli non si sovrappongono
        float slice = (segmentLength - firstObstacleOffset) / pool.Length;

        // Lascio sempre almeno una corsia libera per fetta
        for (int i = 0; i < pool.Length; i++)
        {
            int lane = Random.Range(0, 3);
            float x = (lane - 1) * laneDistance;
            float z = firstObstacleOffset + slice * i + Random.Range(0f, slice * 0.5f);

            pool[i].transform.localPosition = new Vector3(x, 0f, z);
            pool[i].SetActive(Random.value > 0.2f); // 20% di fette vuote
        }
    }
}
