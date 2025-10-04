using UnityEngine;
using UnityEngine.Localization;

namespace Gameplay.Combat
{
    public abstract class BaseGameItem : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField, TextArea] private string _description;
        
        public Sprite Icon => _icon;
        public virtual string Name => _name;
        public string Description => _description;
    }
}