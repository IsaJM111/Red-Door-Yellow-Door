using UnityEngine;

public class RoomData : MonoBehaviour
{
    public float width;
    public float depth;

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
    public Transform doorTransform;
}