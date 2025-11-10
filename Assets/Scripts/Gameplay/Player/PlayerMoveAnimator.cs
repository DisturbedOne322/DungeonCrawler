using System;
using Cysharp.Threading.Tasks;
using Data;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerMoveAnimator : MonoBehaviour
    {
        private bool _firstAppend;

        private Vector3 _prevPos;
        private Sequence _sequence;

        public void CreateSequence()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _firstAppend = true;
        }

        public void AppendSequence(MovementData movementData, Action callback, bool isLast)
        {
            if (MoveHorizontally(movementData, out var moveTween))
                _sequence.Append(moveTween);

            if (_firstAppend && isLast)
            {
                _sequence.Append(Move(movementData, callback, Ease.InOutQuad));
                return;
            }

            if (_firstAppend)
            {
                _sequence.Append(Move(movementData, callback, Ease.InQuad));
                _firstAppend = false;
                return;
            }

            if (isLast)
            {
                _sequence.Append(Move(movementData, callback, Ease.OutQuad));
                return;
            }

            _sequence.Append(Move(movementData, callback, Ease.Linear));
        }

        public async UniTask ExecuteSequence()
        {
            await _sequence.Play().AsyncWaitForCompletion().AsUniTask();
        }

        private bool MoveHorizontally(MovementData movementData, out Tween tween)
        {
            var currentPos = transform.position;
            var targetPos = movementData.TargetPos;

            if (!Mathf.Approximately(currentPos.x, targetPos.x))
            {
                var horizontalMovePos = new Vector3(targetPos.x, currentPos.y, currentPos.z);

                var moveData = new MovementData
                {
                    TargetPos = horizontalMovePos,
                    MoveTimePerMeter = movementData.MoveTimePerMeter
                };

                tween = transform.DOMove(horizontalMovePos, GetMoveTime(moveData))
                    .SetEase(Ease.InOutQuad)
                    .SetLink(gameObject);

                return true;
            }

            tween = null;
            return false;
        }

        private Tween Move(MovementData data, Action callback, Ease ease)
        {
            Tween tween = transform.DOMove(data.TargetPos, GetMoveTime(data))
                .SetEase(ease)
                .OnStart(callback.Invoke)
                .SetLink(gameObject);

            return tween;
        }

        private float GetMoveTime(MovementData data)
        {
            if (_firstAppend)
                _prevPos = transform.position;

            var moveTime = Vector3.Distance(_prevPos, data.TargetPos) * data.MoveTimePerMeter;
            _prevPos = data.TargetPos;

            return moveTime;
        }
    }
}