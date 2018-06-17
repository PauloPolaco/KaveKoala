using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KaveKoala.Characters;

namespace KaveKoala.Characters.EnemyStates
{
    public interface IEnemyState
    {
        void Execute();
        void Enter(EnemyManager enemy);
        void Exit();
        void OnTriggerEnter(Collider2D other);
    }
}