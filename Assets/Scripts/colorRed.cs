using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = Color.red;
    }
}