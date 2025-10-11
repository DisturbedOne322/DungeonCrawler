using UnityEngine;

namespace Gameplay.Combat
{
    public abstract class BaseGameItem : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField, TextArea] private string _description;
        
        public Sprite Icon => _icon;
        public string Name => _name;
        public string Description => _description;
    }
}