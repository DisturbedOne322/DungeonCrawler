using Gameplay.Player;
using Gameplay.Services;
using UI.PlayerBattleMenu;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace UI
{
    public class UIFactory : MonoBehaviour
    {
        [FormerlySerializedAs("_battleMenuController")] [SerializeField] private BattleMenuView _battleMenuView;
        [SerializeField] private Canvas _canvas;
        
        private ContainerFactory _factory;

        [Inject]
        private void Construct(ContainerFactory factory)
        {
            _factory = factory;
        }
        
        public BattleMenuView CreateSkillSelectPopup()
        {
            var result = _factory.Create<BattleMenuView>(_battleMenuView.gameObject);
            result.transform.SetParent(_canvas.transform, false);
            return result;
        }
    }
}