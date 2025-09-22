using Gameplay.Services;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIFactory : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        
        private ContainerFactory _factory;

        [Inject]
        private void Construct(ContainerFactory factory)
        {
            _factory = factory;
        }
    }
}