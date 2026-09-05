using UnityEngine;

public class ChangeColor2 : MonoBehaviour
{
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = Color.yellow;
    }
}