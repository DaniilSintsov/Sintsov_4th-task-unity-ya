using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Model
{
    public class CruisingNlo : Enemy
    {
        private readonly float _speed;

        public CruisingNlo(Vector2 position, float speed) : base(position, 0)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            var newPosition = GetDirectionThroughtScreen();
            Position = Vector2.MoveTowards(Position, newPosition, _speed * deltaTime);
            LookAt(newPosition);
        }

        private void LookAt(Vector2 point)
        {
            Rotate(Vector2.SignedAngle(Quaternion.Euler(0, 0, Rotation) * Vector3.up, (Position - point)));
        }

        private static Vector2 GetDirectionThroughtScreen()
        {
            return new Vector2(Random.value, Random.value).normalized;
        }
    }
}
