using System;
using UnityEngine;

namespace Gameplay.Rewards
{
    [Serializable]
    public class CoinsDropChance
    {
        [Range(0, 1f)] public float MinRange = 0;
        [Range(0, 1f)] public float MaxRange = 0;
    }
}