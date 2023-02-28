using System;
using UnityEngine;

namespace Asteroids.Model
{
    public class Ship : Transformable
    {
        public uint LivesAmount { get; private set; }

        public Ship(Vector2 position, float rotation, uint livesAmount) : base(position, rotation)
        {
            LivesAmount = livesAmount;
        }

        public void ReduceLivesAmount()
        {
            LivesAmount--;
        }

        public void MoveLooped(Vector2 delta)
        {
            var nextPosition = Position + delta;

            nextPosition.x = Mathf.Repeat(nextPosition.x, 1);
            nextPosition.y = Mathf.Repeat(nextPosition.y, 1);

            Position = nextPosition;
        }
    }
}
