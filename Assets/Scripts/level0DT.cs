
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level0MDoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Debug.Log("player detection problem");
            return;
        }
        Debug.Log("Player entered the door!");
        SceneManager.LoadSceneAsync("MeadowLevel");
    }
}