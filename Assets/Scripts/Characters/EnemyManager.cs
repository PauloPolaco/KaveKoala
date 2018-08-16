using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaveKoala.Characters.EnemyStates;

namespace KaveKoala.Characters
{
    public sealed class EnemyManager : CharacterManager
    {
        private IEnemyState m_currentState;
        private System.Random m_rand = new System.Random();

        public GameObject Target { get; set; }
        public bool HasCollidedWithEdge { get; set; }
        private bool m_forceAboutFace;

        /// <summary>
        /// Use this for initialization
        /// </summary>
        protected override void Start()
        {
            base.Start();

            ChangeState(new IdleState());
            m_isFacingRight = false;
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
            if (other.tag == Tags.Edge)
            {
                this.HasCollidedWithEdge = true;
            }
            else
            {
                if (this.HasCollidedWithEdge == true)
                {
                    this.HasCollidedWithEdge = false;

                    Type type = m_currentState.GetType();
                    if (m_currentState.GetType() == typeof(RangedState))
                    {
                        m_forceAboutFace = true;
                    }
                }
            }

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
            else if (m_currentState is RangedState)
            {
                if (this.HasCollidedWithEdge)
                {
                    m_currentSpeed = 0;
                }
                else
                {
                    if (m_forceAboutFace == true)
                    {
                        FlipCharacter();
                        m_forceAboutFace = false;
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
            }
            else
            {
                m_currentSpeed = 0;
            }

            SetPlayerState(m_currentSpeed);
        }

        

        public void Fire()
        {
            TimeSpan ts = DateTime.Now.Subtract(m_lastFireTime);

            if (ts.TotalMilliseconds > 300)
            {
                m_lastFireTime = DateTime.Now;

                if (m_isFacingRight)
                {
                    Instantiate(this.ProjectileRight, m_firePosition.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(this.ProjectileLeft, m_firePosition.position, Quaternion.identity);
                }
            }
        }

        public void LookAtTarget()
        {
            if (this.Target != null)
            {
                float directionX =
                    this.Target.transform.position.x
                    - transform.position.x;

                if (directionX < 0 && m_isFacingRight ||
                    directionX > 0 && !m_isFacingRight)
                {
                    FlipCharacter();
                }
            }
        }

        public Vector2 GetDirection()
        {
            return m_isFacingRight ? Vector2.right : Vector2.left;
        }

        public Type GetCurrentState()
        {
            return m_currentState.GetType();
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