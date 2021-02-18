using UnityEngine;
using System.Collections;
using System;

namespace RpgAdventure
{
    public class PlayerStats : MonoBehaviour
    {
        public int maxLevel;
        public int currentLevel;
        public int currentExp;
        public int[] availableLevels;

        private void Awake()
        {
            availableLevels = new int[maxLevel];
            ComputeLevels(maxLevel);
        }

        private void ComputeLevels(int levelCount)
        {
            for (int i = 0; i < levelCount; i++)
            {
                var level = i + 1;
                var levelPow = Mathf.Pow(level, 2);
                var expToLevel = Convert.ToInt32(levelPow * levelCount);
                availableLevels[i] = expToLevel;
            }
        }
    }
}
