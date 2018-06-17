﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KaveKoala.Characters.EnemyStates
{
    public class PatrolState : IEnemyState
    {
        private EnemyManager m_enemy;
        private float m_patrolTimer;
        private double m_patrolDuration = 2;// 0.5f;
        
        public void Enter(EnemyManager enemy)
        {
            m_enemy = enemy;
        }

        public void Execute()
        {
            Patrol();
            m_enemy.MoveCharacter();
        }

        public void Exit()
        {
        }

        public void OnTriggerEnter(Collider2D other)
        {
        }

        private void Patrol()
        {
            m_enemy.Animator.SetFloat("speed", 1);
            m_patrolTimer += Time.deltaTime;

            if (m_patrolTimer >= m_patrolDuration)
            {
                m_enemy.ChangeState(new IdleState());
            }
        }
    }
}