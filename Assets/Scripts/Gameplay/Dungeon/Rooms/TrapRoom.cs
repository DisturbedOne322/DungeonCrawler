using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.RoomVariants;
using Gameplay.Traps;
using UnityEngine;
using Zenject;

namespace Gameplay.Dungeon.Rooms
{
    public class TrapRoom : VariantRoom<TrapRoomVariantData>
    {
        [SerializeField] private Transform _trapPoint;
        
        private TrapFactory _factory;
        
        private TrapMono _trapInstance;

        [Inject]
        private void Construct(TrapFactory factory)
        {
            _factory = factory;
        }
        
        public override void SetupRoom()
        {
            _trapInstance = _factory.CreateTrap(RoomVariantData);
            _trapInstance.transform.SetParent(_trapPoint);
            _trapInstance.transform.localPosition = Vector3.zero;
        }

        public override void ResetRoom()
        {
            if(_trapInstance)
                Destroy(_trapInstance.gameObject);
        }
        
        public override async UniTask ClearRoom()
        {
            await UniTask.WaitForSeconds(0.5f);
            _trapInstance.TriggerTrap();
            await UniTask.WaitForSeconds(1.5f);
        }
    }
}