using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    public abstract class EncounterRoomVariantData : RoomVariantData
    {
        [Range(0f, 1f)] [SerializeField] private float _rollChance = 0.5f;
        public float RollChance => _rollChance;
    }
}