using Gameplay.Player;
using Gameplay.Services;
using UI.PlayerBattleMenu;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIFactory : MonoBehaviour
    {
        [SerializeField] private BattleMenuController _battleMenuController;
        [SerializeField] private Canvas _canvas;
        
        private ContainerFactory _factory;

        [Inject]
        private void Construct(ContainerFactory factory)
        {
            _factory = factory;
        }
        
        public BattleMenuController CreateSkillSelectPopup()
        {
            var result = _factory.Create<BattleMenuController>(_battleMenuController.gameObject);
            result.transform.SetParent(_canvas.transform, false);
            return result;
        }
    }
}