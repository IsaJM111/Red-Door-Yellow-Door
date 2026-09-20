using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MDoorTrigger : MonoBehaviour
{
    [SerializeField] private FadeController fadeController;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Debug.Log("player detection problem");
            return;
        }
        if (activated)
            return;
        activated = true;
        Debug.Log("Player entered the door!");

        if (fadeController == null)
        {
            Debug.LogError("fadeController is not assigned");
            return;
        }
        fadeController.LoadNextScene("MainScene");

        RoomManager.Instance.GenerateFirstRoom();
    }
}