using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaveKoala.Characters.EnemyStates;

namespace KaveKoala.Characters
{
    public class EnemyManager : CharacterManager
    {
        private IEnemyState m_currentState;
        private System.Random m_rand = new System.Random();
        
        /// <summary>
        /// Use this for initialization
        /// </summary>
        protected override void Start()
        {
            base.Start();

            ChangeState(new IdleState());
            m_isFacingRight = false;
            //FlipCharacter();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        protected override void Update()
        {
            base.Update();

            m_currentState.Execute();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            m_currentState.OnTriggerEnter(other);
        }

        protected override void SetPlayerState(float playerSpeed)
        {
            if (playerSpeed != 0)
            {
                Animator.SetInteger("State", 1);
            }
            else
            {
                Animator.SetInteger("State", 0);
            }

            base.SetPlayerState(playerSpeed);
        }

        public override void MoveCharacter()
        {
            if (m_currentState is PatrolState)
            {
                int rotate = m_rand.Next(0, 100);

                if (rotate == 1)
                {
                    FlipCharacter();
                }

                if (m_isFacingRight)
                {
                    m_currentSpeed = this.SpeedX;
                }
                else
                {
                    m_currentSpeed = -this.SpeedX;
                }
            }
            else
            {
                m_currentSpeed = 0;
            }

            SetPlayerState(m_currentSpeed);
        }

        public Vector2 GetDirection()
        {
            return m_isFacingRight ? Vector2.right : Vector2.left;
        }

        public void ChangeState(IEnemyState newState)
        {
            if (m_currentState != null)
            {
                m_currentState.Exit();
            }

            m_currentState = newState;
            m_currentState.Enter(this);
        }

        public override void FlipCharacter()
        {
            m_isFacingRight = !m_isFacingRight;
            base.FlipCharacter();
        }
    }
}