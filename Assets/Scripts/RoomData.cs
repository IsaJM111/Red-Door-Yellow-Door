using UnityEngine;

public class RoomData : MonoBehaviour
{
    public int width;
    public int depth;

    // 0 = North
    // 1 = South
    // 2 = East
    // 3 = West
    public int doorWall;
    // 0 = North
    // 1 = South
    // 2 = East
    // 3 = West
    public int doorwayWall;
    public int roomID;
    public Transform doorTransform;
}