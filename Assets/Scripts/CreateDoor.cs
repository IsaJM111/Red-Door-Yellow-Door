using UnityEngine;

public class CreateDoor : MonoBehaviour
{
    private void Start()
    {
        CreateLevelDoor();
    }

    public void CreateLevelDoor()
    {
            GameObject door = GameObject.CreatePrimitive(PrimitiveType.Cube);
            door.name = "Door";
            colorBrown doorColor = door.AddComponent<colorBrown>();
            Vector3 position = Vector3.zero;
            Vector3 scale = new Vector3(1.2f, 2.5f, 0.1f);
            door.transform.localPosition = position;
            door.transform.localScale = scale;
            GameObject trigger = new GameObject("Door Trigger");
            trigger.transform.SetParent(door.transform);
            trigger.transform.localPosition = Vector3.zero;
            trigger.transform.localRotation = Quaternion.identity;
            BoxCollider triggerCollider = trigger.AddComponent<BoxCollider>();
            triggerCollider.isTrigger = true;
            triggerCollider.size = new Vector3(1.2f, 2.5f, 1.01f);
            DoorTrigger doorTrigger = trigger.AddComponent<DoorTrigger>();
    }
}
