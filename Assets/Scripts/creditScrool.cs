using UnityEngine;

public class creditScrool : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.up * 25f * Time.deltaTime);
    }
}
