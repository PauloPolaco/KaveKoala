using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaveKoala.Characters
{
    public class EnemyManager : CharacterManager
    {
        /// <summary>
        /// Use this for initialization
        /// </summary>
        protected override void Start()
        {
            base.Start();

            m_isFacingRight = false;
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        protected override void Update()
        {
            base.Update();
        }

        protected override void SetPlayerState(float playerSpeed)
        {
            if (playerSpeed != 0)
            {
                m_animator.SetInteger("State", 1);
            }
            else
            {
                m_animator.SetInteger("State", 0);
            }

            base.SetPlayerState(playerSpeed);
        }

        protected override void MovePlayer()
        {
        }
    }
}