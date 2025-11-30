using Cysharp.Threading.Tasks;
using Gameplay.Combat.Services;
using Gameplay.Dungeon.Data;
using Gameplay.Enemies;
using Gameplay.Units;
using UnityEngine;
using Zenject;

namespace Gameplay.Dungeon.RoomTypes
{
    public class CombatRoom : StopRoom
    {
        [SerializeField] private Transform _enemySpawnPoint;

        private CombatSequenceController _combatSequenceController;

        private EnemyUnit _enemy;
        private EnemyFactory _enemyFactory;

        private CombatRoomVariantData _roomData;
        public override RoomVariantData RoomData => _roomData;
        
        [Inject]
        private void Construct(CombatSequenceController combatSequenceController, EnemyFactory enemyFactory)
        {
            _combatSequenceController = combatSequenceController;
            _enemyFactory = enemyFactory;
        }

        public void SetData(CombatRoomVariantData data) => _roomData = data;
        
        public override void SetupRoom()
        {
            _enemy = _enemyFactory.CreateEnemy();

            _enemy.transform.SetParent(_enemySpawnPoint, false);
            _enemy.transform.localPosition = Vector3.zero;
        }

        public override async UniTask PlayEnterSequence()
        {
            await _enemy.PlayAppearAnimation();
        }

        public override async UniTask ClearRoom()
        {
            await _combatSequenceController.StartCombat(_enemy);
            Destroy(_enemy.gameObject);
        }
    }
}