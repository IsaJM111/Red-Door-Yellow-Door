using UnityEngine;

public class colorBrown : MonoBehaviour
{
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = Color.saddleBrown;
    }
}
