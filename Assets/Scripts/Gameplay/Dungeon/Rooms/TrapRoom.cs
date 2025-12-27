using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.RoomVariants;
using Gameplay.Traps;
using UnityEngine;
using Zenject;

namespace Gameplay.Dungeon.Rooms
{
    public class TrapRoom : StopRoom
    {
        [SerializeField] private Transform _trapPoint;
        
        private TrapFactory _factory;
        
        private TrapMono _trapInstance;

        private TrapRoomVariantData _roomData;
        public override RoomVariantData RoomData => _roomData;

        [Inject]
        private void Construct(TrapFactory factory)
        {
            _factory = factory;
        }
        
        public override void SetupRoom()
        {
            _trapInstance = _factory.CreateTrap(_roomData);
            _trapInstance.transform.SetParent(_trapPoint);
            _trapInstance.transform.localPosition = Vector3.zero;
        }

        public override void ResetRoom()
        {
            if(_trapInstance)
                Destroy(_trapInstance.gameObject);
        }

        public void SetData(TrapRoomVariantData data) => _roomData = data;

        public override async UniTask ClearRoom()
        {
            await UniTask.WaitForSeconds(0.5f);
            _trapInstance.TriggerTrap();
            await UniTask.WaitForSeconds(1.5f);
        }
    }
}