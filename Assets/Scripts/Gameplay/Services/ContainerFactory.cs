using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class ContainerFactory
    {
        private readonly DiContainer _container;

        public ContainerFactory(DiContainer container)
        {
            _container = container;
        }

        public T Create<T>(GameObject prefab)
        {
            return _container.InstantiatePrefabForComponent<T>(prefab);
        }

        public T Create<T>(T prefab) where T : Component
        {
            return _container.InstantiatePrefabForComponent<T>(prefab);
        }

        public GameObject Create(GameObject prefab)
        {
            return _container.InstantiatePrefab(prefab);
        }
    }
}