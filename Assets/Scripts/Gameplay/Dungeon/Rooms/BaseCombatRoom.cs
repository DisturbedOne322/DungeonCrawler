using Cysharp.Threading.Tasks;
using Gameplay.Combat.Services;
using Gameplay.Dungeon.RoomVariants;
using Gameplay.Enemies;
using Gameplay.Units;
using UnityEngine;
using Zenject;

namespace Gameplay.Dungeon.Rooms
{
    public abstract class BaseCombatRoom<T> : VariantRoom<T> where T : RoomVariantData, ICombatRoomData
    {
        [SerializeField] private Transform _enemySpawnPoint;

        private CombatSequenceController _combatSequenceController;

        private EnemyUnit _enemy;
        private EnemyFactory _enemyFactory;

        [Inject]
        private void Construct(CombatSequenceController combatSequenceController, EnemyFactory enemyFactory)
        {
            _combatSequenceController = combatSequenceController;
            _enemyFactory = enemyFactory;
        }

        public override void SetupRoom()
        {
            _enemy = _enemyFactory.CreateEnemy(RoomVariantData);

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