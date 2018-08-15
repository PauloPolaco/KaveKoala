﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaveKoala.Characters
{
    public class ProjectileController : MonoBehaviour
    {
        private const string c_tagGround = @"Ground";
        private const string c_tagEnemy = @"Enemy";
        private const string c_tagPlayer = @"Player";

        private SpriteRenderer m_spriteRenderer;

        public CharacterType CharacterType;
        
        private string GetCharacterTag()
        {
            if (this.CharacterType == CharacterType.Enemy)
            {
                // Sets collision object tag to Enemy.
                return c_tagPlayer;
            }

            // Sets collision object tag to Enemy.
            return c_tagEnemy;
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
            if (collision.gameObject.CompareTag(c_tagGround))
            {
                Destroy(collision.gameObject, m_collisionObjectDestroyDelay);
                Destroy(gameObject);
            }
            else if (collision.gameObject.CompareTag(GetCharacterTag()))
            {
                int additionalDelay = 0;

                Destroy(collision.gameObject, m_collisionObjectDestroyDelay);

                if (this.CharacterType == CharacterType.Enemy)
                {
                    /* Player object must still exist when
                     * End Level Action is invoked, so we
                     * need to delay the Player objects
                     * destruction after until post invoke.*/
                    gameObject.transform.position = new Vector3(
                        gameObject.transform.position.x,
                        gameObject.transform.position.y,
                        -1000);
                    additionalDelay += 2;
                }

                Destroy(gameObject, m_collisionObjectDestroyDelay + additionalDelay);
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