using Gameplay.Player;
using Gameplay.Services;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIFactory : MonoBehaviour
    {
        [SerializeField] private PlayerSkillSelectPopup _prefab;
        [SerializeField] private Canvas _canvas;
        
        private ContainerFactory _factory;

        [Inject]
        private void Construct(ContainerFactory factory)
        {
            _factory = factory;
        }
        
        public PlayerSkillSelectPopup CreateSkillSelectPopup()
        {
            var result = _factory.Create<PlayerSkillSelectPopup>(_prefab.gameObject);
            result.transform.SetParent(_canvas.transform, false);
            return result;
        }
    }
}