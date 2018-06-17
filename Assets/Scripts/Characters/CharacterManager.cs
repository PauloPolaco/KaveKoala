using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaveKoala.Characters
{
    public abstract class CharacterManager : MonoBehaviour
    {
        public float SpeedX;

        public Animator Animator { get; private set; }
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

            this.Animator = GetComponent<Animator>();
            m_rigidbody = GetComponent<Rigidbody2D>();

            m_firePosition = transform.Find("FirePosition");
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        protected virtual void Update()
        {
            SetPlayerState(m_speed);
            SetOrientation();
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

        protected virtual void SetOrientation()
        {
            if (m_speed > 0 & m_isFacingRight == false ||
                m_speed < 0 & m_isFacingRight == true)
            {
                m_isFacingRight = !m_isFacingRight;
                FlipCharacter();
            }
        }

        public abstract void MoveCharacter();

        protected virtual void FlipCharacter()
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}