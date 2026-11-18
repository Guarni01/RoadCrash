using UnityEngine;
public class RoadScroller : MonoBehaviour
{
    public Transform[] segments;      // 3-4 pezzi di strada in fila
    public float segmentLength = 30f;
    public float speed = 15f;

    void Update()
    {
        foreach (var s in segments)
        {
            s.position += Vector3.back * speed * Time.deltaTime;

            if (s.position.z < -segmentLength)
            {
                // lo mando in fondo alla fila
                s.position += Vector3.forward * segmentLength * segments.Length;
                s.GetComponent<SegmentObstacles>().Randomize();
            }
        }
    }
}