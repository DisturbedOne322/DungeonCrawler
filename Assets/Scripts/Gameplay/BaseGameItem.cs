using Gameplay.Rewards;
using UnityEngine;

namespace Gameplay
{
    public abstract class BaseGameItem : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private ItemRarity _itemRarity;
        [SerializeField] [TextArea] private string _description;

        public Sprite Icon => _icon;
        public string Name => _name;
        public ItemRarity Rarity => _itemRarity;
        public string Description => _description;
    }
}