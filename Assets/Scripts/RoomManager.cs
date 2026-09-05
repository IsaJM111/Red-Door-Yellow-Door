using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    [Header("Room Generator")]
    public RoomGenerator roomGenerator;

    private GameObject currentRoom;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateFirstRoom();
    }

    int numRooms = 1;

    private void GenerateFirstRoom()
    {
        currentRoom = roomGenerator.GenerateRoom();

        // use the Vector3 constructor
        currentRoom.transform.position = new Vector3(0f, 0f, 2f);

        Debug.Log("First room generated!");
    }

    public void GenerateNextRoom(DoorTrigger previousDoor)
    {
        Debug.Log("Generating next room...");

        // Get the room the old door belongs to.
        RoomData previousRoom =
            previousDoor.GetComponentInParent<RoomData>();

        if (previousRoom == null)
        {
            Debug.LogError(
                "Previous room is missing RoomData!"
            );

            return;
        }

        // Determine which wall the new door goes on.
        int newDoorWall = GetOppositeWall(previousDoor.doorWall);
        int DoorwayWall = GetDoorwayWall(previousDoor.doorWall);

        // Generate the new room.
        GameObject newRoom =
            roomGenerator.GenerateRoom(newDoorWall, DoorwayWall);

        if (newRoom == null)
        {
            Debug.LogError(
                "RoomGenerator failed to create room!"
            );

            return;
        }

        // Get the new room's dimensions.
        RoomData newRoomData =
            newRoom.GetComponent<RoomData>();

        if (newRoomData == null)
        {
            Debug.LogError(
                "New room is missing RoomData!"
            );

            return;
        }

        // Position the new room.
        PositionNewRoom(
            previousDoor,
            previousRoom,
            newRoom,
            newRoomData
        );

        Debug.Log("New room generated and positioned!");
        numRooms++;
        Debug.Log("There are " + numRooms + " rooms");
    }

    private void PositionNewRoom(DoorTrigger previousDoor,RoomData previousRoom,GameObject newRoom,RoomData newRoomData){
        Vector3 oldDoorPosition = previousDoor.transform.parent.position;
        float wallThickness = roomGenerator.wallThickness;
        switch (previousDoor.doorWall)
        {
            // ========================================
            // NORTH
            // ========================================

            case 0:
                newRoom.transform.position =
                    new Vector3(
                        oldDoorPosition.x,
                        0f,
                        oldDoorPosition.z
                        + newRoomData.depth / 2f
                        + wallThickness
                    );
                break;

            // ========================================
            // SOUTH
            // ========================================

            case 1:

                newRoom.transform.position =
                    new Vector3(
                        oldDoorPosition.x,
                        0f,
                        oldDoorPosition.z
                        - newRoomData.depth / 2f
                        - wallThickness
                    );

                break;

            // ========================================
            // EAST
            // ========================================

            case 2:

                newRoom.transform.position =
                    new Vector3(
                        oldDoorPosition.x
                        + newRoomData.width / 2f
                        + wallThickness,
                        0f,
                        oldDoorPosition.z
                    );

                break;

            // ========================================
            // WEST
            // ========================================

            case 3:

                newRoom.transform.position =
                    new Vector3(
                        oldDoorPosition.x
                        - newRoomData.width / 2f
                        - wallThickness,
                        0f,
                        oldDoorPosition.z
                    );

                break;
        }
    }

    private void RoomNumManagment()
    {
        numRooms--;
    }

    private int GetOppositeWall(int wall)
    {
        switch (wall)
        {
            case 0:
                return 0; // North → South

            case 1:
                return 1; // South → North

            case 2:
                return 2; // East → West

            case 3:
                return 3; // West → East
        }

        return 0;
    }
    private int GetDoorwayWall(int wall)
    {
        switch (wall)
        {
            case 0:
                return 1; // North → South

            case 1:
                return 0; // South → North

            case 2:
                return 3; // East → West

            case 3:
                return 2; // West → East
        }

        return 0;
    }
}
