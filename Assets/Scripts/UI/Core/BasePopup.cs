using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Core
{
    public abstract class BasePopup : MonoBehaviour
    {
        public abstract void ShowPopup();
        public abstract void HidePopup();

        public void DestroyPopup() => Destroy(gameObject);
    }
}