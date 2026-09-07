using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [Header("Room Size")]
    public int minWidth = 20;
    public int maxWidth = 40;

    public int minDepth = 20;
    public int maxDepth = 40;

    public float roomHeight = 4f;

    [Header("Wall Settings")]
    public float wallThickness = 0.25f;

    [Header("Door Settings")]
    public float doorWidth = 1.2f;
    public float doorHeight = 2.5f;

    [Header("Generation")]
    public bool generateOnStart = true;

    private void Start()
    {
        if (generateOnStart)
        {
            GenerateRoom();
        }
    }

    public GameObject GenerateRoom()
    {
        int width = (int)Random.Range(minWidth, maxWidth);
        int depth = (int)Random.Range(minDepth, maxDepth);

        // Pick a random wall.
        int doorWall = Random.Range(0, 4);
        int doorwayWall = 5;

        return GenerateRoom(width, depth, doorWall, doorwayWall);
    }
    public GameObject GenerateRoom(int doorWall, int doorwayWall)
    {
        int width = (int)Random.Range(minWidth, maxWidth);
        int depth = (int)Random.Range(minDepth, maxDepth);
        return GenerateRoom(width, depth, doorWall, doorwayWall);
    }

    public GameObject GenerateRoom(int width, int depth ,int doorWall, int doorwayWall)
    {
        GameObject room = new GameObject("Room");
        RoomData roomData = room.AddComponent<RoomData>();
        roomData.width = width;
        roomData.depth = depth;
        roomData.doorWall = doorWall;
        roomData.doorwayWall = doorwayWall;

        // Floor
        CreatePart(
            "Floor",
            new Vector3(width + 0.25f, 0.1f, depth + 0.25f),
            new Vector3(0f, 0f, 0f),
            room.transform
            
        );
        colorWhite roomColor = room.AddComponent<colorWhite>();
        // Ceiling
        CreatePart(
            "Ceiling",
            new Vector3(width + 0.25f, 0.1f, depth + 0.25f),
            new Vector3(0f, roomHeight, 0f),
            room.transform
        );

        // Generate walls.
        GenerateNorthWall(room.transform, width, depth, doorWall == 0, doorwayWall == 0);
        GenerateSouthWall(room.transform, width, depth, doorWall == 1, doorwayWall == 1);
        GenerateEastWall(room.transform, width, depth, doorWall == 2, doorwayWall == 2);
        GenerateWestWall(room.transform, width, depth, doorWall == 3, doorwayWall == 3);

        // Create the actual door.
        CreateDoor(room.transform, width, depth, doorWall);
        // Generate Furniture.
        CreateFurniture(room.transform, width, depth);
        GenerateRoomLight(room.transform);
        Debug.Log("Room -> Width: " + width + " Depth: " + depth);
        return room;
    }
    private void GenerateRoomLight(Transform parent)
    {
        GameObject light = GameObject.CreatePrimitive(
            PrimitiveType.Cylinder
        );
        light.name = "Light";
        light.transform.SetParent(parent.transform);
        light.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
        light.transform.localPosition = new Vector3(0f,roomHeight-0.05f,0f);
        light.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
        colorBrown lightingColor = light.AddComponent<colorBrown>();
        Light lighting = light.AddComponent<Light>();

        lighting.type = LightType.Directional; // You can choose other types like Directional, Spot, etc.
        lighting.intensity = 0.5f;
        lighting.range = 20.0f;
        lighting.color = Random.ColorHSV();

    }

    private void CreateFurniture(Transform parent, int width, int depth)
    {

    }
    private void GenerateNorthWall(Transform parent,float width, float depth,bool hasDoor, bool doorway)
    {
        if (!hasDoor && !doorway)
        {
            CreatePart(
                "North Wall",
                new Vector3(width, roomHeight, wallThickness),
                new Vector3(0f, roomHeight / 2f, depth / 2f),
                parent
            );

            return;
        }

        CreateDoorWall(
            parent,
            "North Wall",
            width,
            new Vector3(0f, roomHeight / 2f, depth / 2f),
            true
        );
    }

    private void GenerateSouthWall(
        Transform parent,
        float width,
        float depth,
        bool hasDoor,
        bool doorway
    )
    {
        if (!hasDoor && !doorway)
        {
            CreatePart(
                "South Wall",
                new Vector3(width, roomHeight, wallThickness),
                new Vector3(0f, roomHeight / 2f, -depth / 2f),
                parent
            );

            return;
        }

        CreateDoorWall(
            parent,
            "South Wall",
            width,
            new Vector3(0f, roomHeight / 2f, -depth / 2f),
            true
        );
    }

    private void GenerateEastWall(
        Transform parent,
        float width,
        float depth,
        bool hasDoor,
        bool doorway
    )
    {
        if (!hasDoor && !doorway)
        {
            CreatePart(
                "East Wall",
                new Vector3(wallThickness, roomHeight, depth),
                new Vector3(width / 2f, roomHeight / 2f, 0f),
                parent
            );

            return;
        }

        CreateDoorWall(
            parent,
            "East Wall",
            depth,
            new Vector3(width / 2f, roomHeight / 2f, 0f),
            false
        );
    }

    private void GenerateWestWall(
        Transform parent,
        float width,
        float depth,
        bool hasDoor,
        bool doorway
    )
    {
        if (!hasDoor && !doorway)
        {
            CreatePart(
                "West Wall",
                new Vector3(wallThickness, roomHeight, depth),
                new Vector3(-width / 2f, roomHeight / 2f, 0f),
                parent
            );

            return;
        }

        CreateDoorWall(
            parent,
            "West Wall",
            depth,
            new Vector3(-width / 2f, roomHeight / 2f, 0f),
            false
        );
    }

    private void CreateDoorWall(
        Transform parent,
        string wallName,
        float wallLength,
        Vector3 wallPosition,
        bool horizontal
    )
    {
        float sideLength = (wallLength - doorWidth) / 2f;

        if (horizontal)
        {
            // Left section
            CreatePart(
                wallName + " Left",
                new Vector3(
                    sideLength,
                    roomHeight,
                    wallThickness
                ),
                wallPosition + new Vector3(
                    -(doorWidth / 2f + sideLength / 2f),
                    0f,
                    0f
                ),
                parent
            );

            // Right section
            CreatePart(
                wallName + " Right",
                new Vector3(
                    sideLength,
                    roomHeight,
                    wallThickness
                ),
                wallPosition + new Vector3(
                    doorWidth / 2f + sideLength / 2f,
                    0f,
                    0f
                ),
                parent
            );
        }
        else
        {
            // Back section
            CreatePart(
                wallName + " Back",
                new Vector3(
                    wallThickness,
                    roomHeight,
                    sideLength
                ),
                wallPosition + new Vector3(
                    0f,
                    0f,
                    -(doorWidth / 2f + sideLength / 2f)
                ),
                parent
            );

            // Front section
            CreatePart(
                wallName + " Front",
                new Vector3(
                    wallThickness,
                    roomHeight,
                    sideLength
                ),
                wallPosition + new Vector3(
                    0f,
                    0f,
                    doorWidth / 2f + sideLength / 2f
                ),
                parent
            );
        }

        // Fill the area above the door.
        Vector3 topPosition = wallPosition;

        topPosition.y =
            doorHeight + (roomHeight - doorHeight) / 2f;

        if (horizontal)
        {
            CreatePart(
                wallName + " Above Door",
                new Vector3(
                    doorWidth,
                    roomHeight - doorHeight,
                    wallThickness
                ),
                topPosition,
                parent
            );
        }
        else
        {
            CreatePart(
                wallName + " Above Door",
                new Vector3(
                    wallThickness,
                    roomHeight - doorHeight,
                    doorWidth
                ),
                topPosition,
                parent
            );
        }
    }

    private void CreateDoor(
        Transform parent,
        float width,
        float depth,
        int doorWall
    ){
        GameObject door = GameObject.CreatePrimitive(
            PrimitiveType.Cube
        );

        door.name = "Door";
        door.transform.SetParent(parent);
        colorBrown doorColor = door.AddComponent<colorBrown>();
        Vector3 position = Vector3.zero;
        Vector3 scale = new Vector3(
            doorWidth,
            doorHeight,
            0.1f
        );

        switch (doorWall)
        {
            // North
            case 0:
                position = new Vector3(
                    0f,
                    doorHeight / 2f,
                    depth / 2f
                );
                break;

            // South
            case 1:
                position = new Vector3(
                    0f,
                    doorHeight / 2f,
                    -depth / 2f
                );
                break;

            // East
            case 2:
                position = new Vector3(
                    width / 2f,
                    doorHeight / 2f,
                    0f
                );

                scale = new Vector3(
                    0.1f,
                    doorHeight,
                    doorWidth
                );
                break;
            // West
            case 3:
                position = new Vector3(
                    -width / 2f,
                    doorHeight / 2f,
                    0f
                );
                scale = new Vector3(
                    0.1f,
                    doorHeight,
                    doorWidth
                );
                break;
        }
        door.transform.localPosition = position;
        door.transform.localScale = scale;
        GameObject trigger = new GameObject("Door Trigger");
        trigger.transform.SetParent(door.transform);
        trigger.transform.localPosition = Vector3.zero;
        trigger.transform.localRotation = Quaternion.identity;
        BoxCollider triggerCollider =
            trigger.AddComponent<BoxCollider>();
        triggerCollider.isTrigger = true;
        triggerCollider.size = new Vector3(
            doorWidth,
            doorHeight,
            1.01f
        );
        if (doorWall == 2 || doorWall == 3)
        {
            triggerCollider.size = new Vector3(
                1.5f,
                doorHeight,
                doorWidth
            );
        }
        DoorTrigger doorTrigger = trigger.AddComponent<DoorTrigger>();
        doorTrigger.doorWall = doorWall;
    }
    private GameObject CreatePart(
        string partName,
        Vector3 scale,
        Vector3 position,
        Transform parent
    )
    {
        GameObject part = GameObject.CreatePrimitive(
            PrimitiveType.Cube
        );
        part.name = partName;
        part.transform.SetParent(parent);
        part.transform.localPosition = position;
        part.transform.localScale = scale;
        return part;
    }
}