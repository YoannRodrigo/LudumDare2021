using UnityEngine;

namespace player
{
    public class PlayerData
    {
        public Vector3 Position;
        public Vector2 Velocity;
        public bool IsOnGround;

        public PlayerData(Vector3 Position,Vector2 velocity, bool isOnGround)
        {
            Velocity = velocity;
            IsOnGround = isOnGround;
        }
    }
}