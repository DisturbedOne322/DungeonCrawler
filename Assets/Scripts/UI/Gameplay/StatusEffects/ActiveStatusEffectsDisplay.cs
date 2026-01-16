using System.Collections.Generic;
using Gameplay.Facades;
using Gameplay.Services;
using Gameplay.StatusEffects.Core;
using Pools;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Gameplay.StatusEffects
{
    public class ActiveStatusEffectsDisplay : MonoBehaviour
    {
        [SerializeField] private RectTransform _parent;
        [SerializeField] private StatusEffectDisplay _statusEffectDisplay;

        private readonly Dictionary<BaseStatusEffectInstance, StatusEffectDisplay> _displayedEffectsDict = new();

        private readonly CompositeDisposable _subscription = new();

        private BasePool<StatusEffectDisplay> _statusEffectDisplayPool;

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }

        [Inject]
        private void Construct(IStatusEffectCarrier unit, ContainerFactory factory)
        {
            _statusEffectDisplayPool = new BasePool<StatusEffectDisplay>(factory);
            _statusEffectDisplayPool.Initialize(_statusEffectDisplay);

            unit.UnitActiveStatusEffectsContainer.All.ObserveAdd()
                .Subscribe(e => { AddStatusEffectDisplay(e.Value); }).AddTo(_subscription);

            unit.UnitActiveStatusEffectsContainer.All.ObserveRemove()
                .Subscribe(e => { RemoveStatusEffectDisplay(e.Value); }).AddTo(_subscription);
        }

        private void AddStatusEffectDisplay(BaseStatusEffectInstance statusEffect)
        {
            var display = CreateDisplay(statusEffect);
            _displayedEffectsDict.Add(statusEffect, display);
        }

        private void RemoveStatusEffectDisplay(BaseStatusEffectInstance statusEffect)
        {
            if (!_displayedEffectsDict.TryGetValue(statusEffect, out var display))
                return;

            _statusEffectDisplayPool.Return(display);
        }

        private StatusEffectDisplay CreateDisplay(BaseStatusEffectInstance statusEffect)
        {
            var display = _statusEffectDisplayPool.Get();
            display.SetData(statusEffect);
            display.transform.SetParent(_parent, false);
            display.transform.localPosition = Vector3.zero;
            return display;
        }
    }
}