using UnityEngine;
using System.Collections;

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

        activated = true;
        Debug.Log("Player entered the door!");
        RoomManager.Instance.GenerateNextRoom(this);
        canActivate = false;
        Destroy(transform.parent.gameObject);
    }
}