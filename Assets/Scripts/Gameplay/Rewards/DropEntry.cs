using System;
using UnityEngine;

namespace Gameplay.Rewards
{
    [Serializable]
    public class DropEntry
    {
        [SerializeField] private BaseGameItem _item;
        [SerializeField] private int _weight;
        [SerializeField] [Min(1)] private int _amount = 1;

        public BaseGameItem Item => _item;
        public int Weight => _weight;
        public int Amount => _amount;
    }
}