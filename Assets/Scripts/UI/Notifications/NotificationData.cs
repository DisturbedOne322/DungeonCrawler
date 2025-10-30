using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace UI.Notifications
{
    public readonly struct NotificationData
    {
        public readonly Action Init;
        public readonly Func<CancellationToken, UniTask> ShowTask;

        public NotificationData(Action init, Func<CancellationToken, UniTask> showTask)
        {
            Init = init ?? throw new ArgumentNullException(nameof(init));
            ShowTask = showTask ?? throw new ArgumentNullException(nameof(showTask));
        }
    }
}