using System;
using System.Collections.Generic;
using Gameplay.Services;
using Gameplay.StatusEffects.Core;
using Gameplay.Units;
using Pools;
using UnityEngine;
using Zenject;
using UniRx;

namespace UI.Gameplay
{
    public class ActiveStatusEffectsDisplay : MonoBehaviour
    {
        [SerializeField] private RectTransform _parent;
        [SerializeField] private StatusEffectDisplay _statusEffectDisplay;

        private readonly Dictionary<BaseStatusEffectInstance, StatusEffectDisplay> _displayedEffectsDict = new();
        
        private readonly CompositeDisposable _subscription = new();
        
        private BasePool<StatusEffectDisplay> _statusEffectDisplayPool;
        
        [Inject]
        private void Construct(PlayerUnit playerUnit, ContainerFactory factory)
        {
            _statusEffectDisplayPool = new(factory);
            _statusEffectDisplayPool.Initialize(_statusEffectDisplay);
            
            playerUnit.UnitActiveStatusEffectsData.AllActiveStatusEffects.
                ObserveAdd().
                Subscribe(e =>
                {
                    AddStatusEffectDisplay(e.Value);
                }).AddTo(_subscription);
            
            playerUnit.UnitActiveStatusEffectsData.AllActiveStatusEffects.
                ObserveRemove().
                Subscribe(e =>
                {
                    RemoveStatusEffectDisplay(e.Value);
                }).AddTo(_subscription);
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }

        private void AddStatusEffectDisplay(BaseStatusEffectInstance statusEffect)
        {
            var display = CreateDisplay(statusEffect);
            _displayedEffectsDict.Add(statusEffect, display);
        }
        
        private void RemoveStatusEffectDisplay(BaseStatusEffectInstance statusEffect)
        {
            if(!_displayedEffectsDict.TryGetValue(statusEffect, out var display))
                return;
            
            _statusEffectDisplayPool.Return(display);
        }

        private StatusEffectDisplay CreateDisplay(BaseStatusEffectInstance statusEffect)
        {
            var display = _statusEffectDisplayPool.Get();
            display.SetData(statusEffect);
            display.transform.SetParent(_parent, false);
            return display;
        }
    }
}