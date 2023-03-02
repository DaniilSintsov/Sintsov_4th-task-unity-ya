using System;
using System.Collections;
using Codice.Client.BaseCommands;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Model
{
    public class EnemiesSpawner
    {
        private readonly EnemiesSimulation _simulation;
        private readonly Transformable _player;
        private readonly CruisingNlo _cruisingNlo;

        private readonly Func<Enemy>[] _variants;
        private readonly Timers<Func<Enemy>> _queue = new Timers<Func<Enemy>>();

        public EnemiesSpawner(EnemiesSimulation simulation, Transformable player)
        {
            _simulation = simulation;
            _player = player;
            _cruisingNlo = new CruisingNlo(GetRandomPositionOutsideScreen(), Config.NloSpeed);

            _variants = new Func<Enemy>[]
            {
                CreateAsteroid,
                CreateNloForPlayer,
                CreateNloForAnotherOne
            };
        }

        public void FillTestQueue()
        {
            for (int stacks = 0; stacks < 100; stacks++)
            {
                int countInStack = Random.Range(0, 2);

                while (countInStack-- > 0)
                {
                    _queue.Start(_variants[0], stacks, (factory) => _simulation.Simulate(factory.Invoke()));

                    _queue.Start(_variants[1], stacks * 2, (factory) => _simulation.Simulate(factory.Invoke()));
                    _queue.Start(_variants[2], stacks, (factory) => _simulation.Simulate(factory.Invoke()));
                }
            }
        }

        public void Update(float deltaTime)
        {
            _queue.Tick(deltaTime);
        }

        private Vector2 GetRandomPositionOutsideScreen()
        {
            return Random.insideUnitCircle.normalized + new Vector2(0.5F, 0.5F);
        }

        private Nlo CreateNloForPlayer()
        {
            return new Nlo(_player, GetRandomPositionOutsideScreen(), Config.NloSpeed);
        }

        private Nlo CreateNloForAnotherOne()
        {
            return new Nlo(_cruisingNlo, GetRandomPositionOutsideScreen(), Config.NloSpeed);
        }

        private Asteroid CreateAsteroid()
        {
            Vector2 position = GetRandomPositionOutsideScreen();
            Vector2 direction = GetDirectionThroughtScreen(position);

            return new Asteroid(position, direction, Config.AsteroidSpeed);
        }

        private static Vector2 GetDirectionThroughtScreen(Vector2 position)
        {
            return (new Vector2(Random.value, Random.value) - position).normalized;
        }
    }
}
