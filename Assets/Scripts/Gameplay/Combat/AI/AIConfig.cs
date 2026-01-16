using Data.Constants;
using UnityEngine;

namespace Gameplay.Combat.AI
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayAI + "EnemyAI")]
    public class AIConfig : ScriptableObject
    {
        [SerializeField, Range(0.1f, 1f)] private float _intelligence = 0.5f;
        [SerializeField, Range(0.1f, 1f)] private float _aggression = 0.5f;
        [SerializeField, Range(0.1f, 1f)] private float _defense = 0.5f;
        [SerializeField, Range(0.1f, 1f)] private float _randomness = 0.1f;
        
        public float Intelligence => _intelligence;
        public float Aggression => _aggression;
        public float Defense => _defense;
        public float Randomness => _randomness;

        public bool Debug = false;
    }
}