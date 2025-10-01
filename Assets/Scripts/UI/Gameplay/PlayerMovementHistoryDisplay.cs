using System.Collections.Generic;
using Data;
using DG.Tweening;
using Gameplay.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Gameplay
{
    public class PlayerMovementHistoryDisplay : MonoBehaviour
    {
        [SerializeField] private RectTransform _parent;
        [SerializeField] private RoomIconDisplay _roomIconDisplay;
        [SerializeField] [Min(2)] private int _maximumRoomsHistory;
        [SerializeField] private float _scaleUpDuration = 0.2f;
        [SerializeField] private float _scaleDownDuration = 0.15f;

        private DungeonFactory _factory;

        private readonly LinkedList<RoomIconDisplay> _activeDisplays = new();

        [Inject]
        private void Construct(PlayerMovementHistory history, DungeonFactory factory)
        {
            _factory = factory;

            for (var i = 0; i < _maximumRoomsHistory; i++)
            {
                var display = Instantiate(_roomIconDisplay, _parent);
                _activeDisplays.AddLast(display);
            }

            history.RoomsHistory.ObserveAdd().Subscribe(newRoom => { ShowRoomDisplay(newRoom.Value); })
                .AddTo(gameObject);
        }

        private void ShowRoomDisplay(RoomType roomType)
        {
            RoomIconDisplay display = _activeDisplays.Last.Value;
            
            _activeDisplays.RemoveLast();

            display.transform.DOScale(Vector3.zero, _scaleDownDuration).OnComplete(() =>
            {
                display.SetIcon(_factory.GetRoomData(roomType).Icon);
                display.transform.localScale = Vector3.zero;
                display.transform.SetAsFirstSibling();
                AnimateScaleUp(display);
            });
            
            _activeDisplays.AddFirst(display);
        }

        private void AnimateScaleUp(RoomIconDisplay display)
        {
            display.transform.DOScale(Vector3.one, _scaleUpDuration).SetEase(Ease.OutBack);
        }
    }
}