using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KaveKoala.Characters.EnemyStates
{
    public class IdleState : IEnemyState
    {
        private EnemyManager m_enemy;
        private float m_idleTimer;
        private float m_idleDuration = 2;
        
        public void Enter(EnemyManager enemy)
        {
            m_enemy = enemy;
        }

        public void Execute()
        {
            Idle();
        }

        public void Exit()
        {
        }

        public void OnTriggerEnter(Collider2D other)
        {
        }

        private void Idle()
        {
            m_enemy.CurrentSpeed = 0;
            m_idleTimer += Time.deltaTime;

            if (m_idleTimer >= m_idleDuration)
            {
                m_enemy.ChangeState(new PatrolState());
            }
        }
    }
}