using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaveKoala.Characters
{
    public class ProjectileController : MonoBehaviour
    {
        private SpriteRenderer m_spriteRenderer;

        public Color32 Colour
        {
            get
            {
                if (IsSpriteRendererFound())
                {
                    return m_spriteRenderer.color;
                }

                return new Color32();
            }
            set
            {
                if (IsSpriteRendererFound())
                {
                    m_spriteRenderer.color = value;
                }
            }
        }

        public Vector2 Speed;
        public float ProjectileDestroyDelay;

        private float m_collisionObjectDestroyDelay = 0.15f;
        private Rigidbody2D m_rigidBody;

        /// <summary>
        /// Use this for initialization
        /// </summary>
        void Start()
        {
            m_rigidBody = GetComponent<Rigidbody2D>();
            m_rigidBody.velocity = this.Speed;

            Destroy(gameObject, this.ProjectileDestroyDelay);
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            m_rigidBody.velocity = this.Speed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                Destroy(collision.gameObject, m_collisionObjectDestroyDelay);
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Checks whether SpriteRenderer exists and attempts 
        /// to retreive the dependency when not loaded.
        /// </summary>
        /// <returns>Value is true when dependency was found.</returns>
        private bool IsSpriteRendererFound()
        {
            if (m_spriteRenderer == null)
            {
                m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

                if (m_spriteRenderer == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}