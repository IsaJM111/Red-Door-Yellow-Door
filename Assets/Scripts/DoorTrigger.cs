using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public int doorWall;
    private bool canActivate = true;
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

        int levelChance = (int)Random.Range(1, 11);
        Debug.Log(levelChance);
        if (levelChance >= 9)
        {
            activated = true;
            Debug.Log("Player entered the door!");
            RoomManager.Instance.GenerateNextRoom(this);
            canActivate = false;
            Destroy(transform.parent.gameObject);
        }
        else if (levelChance >= 5)
        {
            SceneManager.LoadSceneAsync("MeadowLevel");
        }
        else
        {
            SceneManager.LoadSceneAsync("ClockLevel");
        }


    }
}