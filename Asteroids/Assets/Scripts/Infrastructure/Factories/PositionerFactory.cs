using Asteroid;
using Enemy;

namespace Infrastructure.Factories
{
    public static class PositionerFactory
    {
        public static IEnemyPositioner GetEnemyPositioner() => 
            new EnemyPositioner();

        public static IAsteroidPositioner GetAsteroidPositioner() =>
            new AsteroidPositioner();
    }
}
