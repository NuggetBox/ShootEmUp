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

        public string
            myLabel;

        public int
            myIndex,
            myNumEnemies,
            myNumWaves,
            mySpawnedEnemies;

        public int
            myCrabFactor,
            myOctopusFactor,
            myClamFactor;

        public float
            myEnemyDelay,
            myLevelDelay,
            myWaveDelay,
            myDamageMod;

        public bool 
            myIsBoss,
            myComplete;

        public Level(int anIndex, int aNumEnemies, int aNumWaves, int aCrabFactor, int anOctopusFactor, int aClamFactor, float anEnemyDelay, float aLevelDelay,  int aWaveDelay, float aDamageMod, bool anIsBossBool)
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
            myDamageMod = aDamageMod;
            myIsBoss = anIsBossBool;
        }

        //public Enemy GetNextEnemy()
        //{
        //    int tempNumber = myRandom.Next(1, GetTotalFactor + 1);

        //}
    }
}
