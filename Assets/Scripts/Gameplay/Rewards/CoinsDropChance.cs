using System;
using UnityEngine;

namespace Gameplay.Rewards
{
    [Serializable]
    public class CoinsDropChance
    {
        [Range(0, 1f)] public float MinRange;
        [Range(0, 1f)] public float MaxRange;
    }
}