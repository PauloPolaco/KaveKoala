using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaveKoala.Characters.EnemyStates
{
    public class RangedState : IEnemyState
    {
        private EnemyManager m_enemy;

        private float m_projectileTimer;
        private float m_projectileCoolDown = 0.75f;
        private bool m_canFireProjectile;

        public void Enter(EnemyManager enemy)
        {
            m_enemy = enemy;
        }

        public void Execute()
        {
            if (m_enemy.Target != null)
            {
                FireProjectile();
                m_enemy.MoveCharacter();
            }
            else
            {
                m_enemy.ChangeState(new IdleState());
            }
        }

        public void Exit()
        {
        }

        public void OnTriggerEnter(Collider2D other)
        {
            if (other.tag == Tags.Edge)
            {
                m_enemy.ChangeState(new IdleState());
            }
        }

        private void FireProjectile()
        {
            m_projectileTimer += Time.deltaTime;

            if (m_projectileTimer >= m_projectileCoolDown)
            {
                m_canFireProjectile = true;
                m_projectileTimer = 0;
            }

            if (m_canFireProjectile)
            {
                m_canFireProjectile = false;
                m_enemy.Fire();
            }
        }
    }
}