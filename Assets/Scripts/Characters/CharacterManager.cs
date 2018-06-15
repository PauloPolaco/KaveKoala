using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaveKoala.Characters
{
    public abstract class CharacterManager : MonoBehaviour
    {
        public float SpeedX;

        protected Animator m_animator;
        protected Rigidbody2D m_rigidbody;

        protected bool m_isFacingRight;
        protected float m_speed;

        public Color32 ProjectileColour;
        public GameObject ProjectileLeft;
        public GameObject ProjectileRight;

        protected Transform m_firePosition;
        protected DateTime m_lastFireTime = DateTime.Now;

        /// <summary>
        /// Use this for initialization
        /// </summary>
        protected virtual void Start()
        {
            SetProjectileColour();

            m_animator = GetComponent<Animator>();
            m_rigidbody = GetComponent<Rigidbody2D>();

            m_firePosition = transform.Find("FirePosition");
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        protected virtual void Update()
        {
            SetPlayerState(m_speed);
            SetPlayerOrientation();

            MovePlayer();
        }

        private void SetProjectileColour()
        {
            ProjectileController pcLeft = ProjectileLeft.GetComponent<ProjectileController>();
            ProjectileController pcRight = ProjectileRight.GetComponent<ProjectileController>();
            pcLeft.Colour = this.ProjectileColour;
            pcRight.Colour = this.ProjectileColour;
        }

        protected virtual void SetPlayerState(float playerSpeed)
        {
            m_rigidbody.velocity = new Vector3(m_speed, m_rigidbody.velocity.y, 0);
        }

        protected void SetPlayerOrientation()
        {
            if (m_speed > 0 & m_isFacingRight == false ||
                m_speed < 0 & m_isFacingRight == true)
            {
                m_isFacingRight = !m_isFacingRight;

                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
        }

        protected abstract void MovePlayer();
    }
}