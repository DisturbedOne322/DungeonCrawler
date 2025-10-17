using System;
using Data;
using UnityEngine;

namespace Gameplay.Skills.Core
{
    [Serializable]
    public class SkillScalingData
    {
        public StatType StatType;

        [Min(0.01f)] public float Scaling = 1f;
    }
}