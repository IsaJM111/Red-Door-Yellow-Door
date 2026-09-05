using UnityEngine;

public class ChangeColor4 : MonoBehaviour
{
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = Color.antiqueWhite;
    }
}