﻿using UnityEngine;

namespace Asteroids.Model
{
    public abstract class Enemy : Transformable
    {
        public Enemy(Vector2 position, float rotation) : base(position, rotation)
        {
        }

        public abstract void Update(float deltaTime);
    }
}
