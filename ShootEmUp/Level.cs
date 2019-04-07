using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Level
    {
        public static Random myRandom = new Random();

        public Enemy myBoss;

        public int GetLevelNumber => myIndex + 1;

        public int GetTotalFactor => myCrabFactor + myOctopusFactor + myClamFactor;

        //public Vector2
        //    my1Spawn1 = new Vector2(0, 0),
        //    my1Spawn2 = new Vector2(1280, 720),
        //    my2Spawn1 = new Vector2(0, 720),
        //    my2Spawn2 = new Vector2(1280, 720);

        public string GetLevelLabel => "Level: " + GetLevelNumber;

        public int
            myIndex,
            myNumEnemies,
            mySpawnedEnemies,
            myNumWaves,
            mySpawnChange,
            myCompletedWaves;

        public int
            myCrabFactor,
            myOctopusFactor,
            myClamFactor;

        public float
            myMinEnemyDelay,
            myEnemyDelay,
            myLevelDelay,
            myWaveDelay,
            myEnemyDelayFactor,
            myIncreaseEnemyFactor,
            myDamageMod;

        public bool
            myIsBoss,
            myIsEndless,
            myComplete;

        public Level(int anIndex, int aNumEnemies, int aNumWaves, int aCrabFactor, int anOctopusFactor, int aClamFactor, float anEnemyDelay, float aLevelDelay, int aWaveDelay, float anIncreaseEnemyFactor, float aDamageMod, bool anIsBossBool)
        {
            myIndex = anIndex;
            myNumEnemies = aNumEnemies;
            myNumWaves = aNumWaves;

            myCrabFactor = aCrabFactor;
            myOctopusFactor = anOctopusFactor;
            myClamFactor = aClamFactor;

            myEnemyDelay = anEnemyDelay;
            myLevelDelay = aLevelDelay;
            myWaveDelay = aWaveDelay;
            myIncreaseEnemyFactor = anIncreaseEnemyFactor;
            myDamageMod = aDamageMod;
            myIsBoss = anIsBossBool;
        }

        // For creating an endless level
        public Level(int aCrabFactor, int anOctopusFactor, int aClamFactor, int someSpawnChange, float anEnemyDelay, float anEnemyDelayFactor, float aMinEnemyDelay, int aWaveDelay, float anIncreaseEnemyFactor)
        {
            myIsEndless = true;

            myCrabFactor = aCrabFactor;
            myOctopusFactor = anOctopusFactor;
            myClamFactor = aClamFactor;
            mySpawnChange = someSpawnChange;
            myEnemyDelay = anEnemyDelay;
            myEnemyDelayFactor = anEnemyDelayFactor;
            myMinEnemyDelay = aMinEnemyDelay;
            myWaveDelay = aWaveDelay;
            myIncreaseEnemyFactor = anIncreaseEnemyFactor;
        }

        public Enemy GetNextEnemy(int x, int y)
        {
            int tempNumber = myRandom.Next(1, GetTotalFactor + 1);

            if (tempNumber <= myCrabFactor)
            {
                return new Crab(x, y);
            }
            else if (tempNumber <= myCrabFactor + myOctopusFactor)
            {
                return new Octopus(x, y);
            }
            else
            {
                return new Clam(x, y);
            }
        }
    }
}
