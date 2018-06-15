using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaveKoala.Characters
{
    public class PlayerManager : CharacterManager
    {
        public float JumpSpeedY;

        private bool m_isJumping;
        private bool m_isDoubleJumping;
        
        /// <summary>
        /// Use this for initialization
        /// </summary>
        protected override void Start()
        {
            base.Start();

            m_isFacingRight = true;
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        protected override void Update()
        {
            base.Update();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Ground")
            {
                m_isJumping = false;
                m_isDoubleJumping = false;

                m_animator.SetInteger("State", 0);
            }
        }

        protected void SetPlayerState(float playerSpeed)
        {
            if (m_isJumping == false)
            {
                if (playerSpeed != 0)
                {
                    m_animator.SetInteger("State", 1);
                }
                else
                {
                    m_animator.SetInteger("State", 0);
                }
            }

            base.SetPlayerState(playerSpeed);
        }

        #region Movement

        protected override void MovePlayer()
        {
            // Left player movement
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                m_speed = -SpeedX;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                m_speed = 0;
            }

            // Right player movement
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                m_speed = SpeedX;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                m_speed = 0;
            }

            // Upward player movement
            if (Input.GetKeyDown(KeyCode.UpArrow) ||
                Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown(KeyCode.B))
            {
                Jump();
            }

            // Fire
            if (Input.GetKeyDown(KeyCode.LeftControl) ||
                Input.GetKeyDown(KeyCode.RightControl) ||
                Input.GetKeyDown(KeyCode.A))
            {
                Fire();
            }
        }

        public void MoveLeft()
        {
            m_speed = -SpeedX;
        }

        public void MoveRight()
        {
            m_speed = SpeedX;
        }

        public void StopMoving()
        {
            m_speed = 0;
        }

        public void Jump()
        {
            if (m_rigidbody.velocity.y > 0)
            {
                m_animator.SetInteger("State", 3); // jumping
            }
            else
            {
                m_animator.SetInteger("State", 2); // falling
            }

            if (m_isJumping && m_isDoubleJumping)
            {
                return;
            }

            if (m_isJumping == false)
            {
                m_isJumping = true;
            }
            else
            {
                m_isDoubleJumping = true;
            }

            m_rigidbody.AddForce(new Vector2(m_rigidbody.velocity.x, JumpSpeedY));


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

        #endregion Movement
    }
}