using UnityEngine;

public class colorWhite : MonoBehaviour
{
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = Color.antiqueWhite;
    }
}