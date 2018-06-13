using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaveKoala.Characters
{
    public class ProjectileController : MonoBehaviour
    {
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
    }
}