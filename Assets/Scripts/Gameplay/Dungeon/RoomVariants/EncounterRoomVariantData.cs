using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    public abstract class EncounterRoomVariantData : RoomVariantData
    {
        [Range(0f, 1f)]
        [SerializeField] private float _rollChance = 0.5f;

        [SerializeField, Min(1)] private int startOffset = 1;
        [SerializeField, Min(1)] private int endOffset = 1;

        public float RollChance => _rollChance;
        public int StartOffset => startOffset;
        public int EndOffset => endOffset;
    }
}