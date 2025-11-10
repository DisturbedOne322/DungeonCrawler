using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace UI.Notifications
{
    public class NotificationsPlayer
    {
        private readonly CancellationToken _destroyToken;
        private readonly Queue<NotificationData> _notificationsQueue = new();

        private bool _isPlaying;

        public NotificationsPlayer(CancellationToken destroyToken)
        {
            _destroyToken = destroyToken;
        }

        public void EnqueueNotification(NotificationData notification)
        {
            _notificationsQueue.Enqueue(notification);

            if (!_isPlaying)
                PlayNotification().Forget();
        }

        private async UniTaskVoid PlayNotification()
        {
            while (HasNotificationsPending())
            {
                _isPlaying = true;

                if (_destroyToken.IsCancellationRequested)
                    break;

                var notification = _notificationsQueue.Dequeue();
                notification.Init.Invoke();

                try
                {
                    await notification.ShowTask.Invoke(_destroyToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }

            _isPlaying = false;
        }

        private bool HasNotificationsPending()
        {
            return _notificationsQueue.Count > 0 && !_destroyToken.IsCancellationRequested;
        }
    }
}