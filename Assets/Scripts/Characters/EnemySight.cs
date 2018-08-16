using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaveKoala.Characters
{
    public class EnemySight : MonoBehaviour
    {
        [SerializeField]
        public EnemyManager Enemy;

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        void Start()
        {

        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D otherObject)
        {
            if (otherObject.tag == Tags.Player)
            {
                this.Enemy.Target = otherObject.gameObject;
                this.Enemy.HasCollidedWithEdge = true;
            }
        }

        private void OnTriggerExit2D(Collider2D otherObject)
        {
            if (otherObject.tag == Tags.Player)
            {
                this.Enemy.LookAtTarget();
                this.Enemy.Target = null;
            }
        }
    }
}