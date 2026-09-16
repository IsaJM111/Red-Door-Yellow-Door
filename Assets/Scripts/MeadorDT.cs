using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MDoorTrigger : MonoBehaviour
{
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
        SceneManager.LoadSceneAsync("MainScene");
        RoomManager.Instance.GenerateFirstRoom();
    }
}