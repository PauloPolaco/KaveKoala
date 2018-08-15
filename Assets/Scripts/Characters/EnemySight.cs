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
            if (otherObject.tag == "Player")
            {
                this.Enemy.Target = otherObject.gameObject;
                this.Enemy.HasCollidedWithEdge = true;
            }
            else
            {
                //if (otherObject.tag == "Edge")
                //{
                //    this.Enemy.HasCollidedWithEdge = true;
                //}
                //    this.Enemy.Target = null;
                //    this.Enemy.ChangeState(new EnemyStates.IdleState());
                //    //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), otherObject, false);
                //}
                //else
                //{
                //    //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), otherObject);
                //}

                //if (!(otherObject.tag == "Enemy" ||
                //    otherObject.tag == "Edge"))
                //{
                //    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), otherObject);
                //}
                //else
                //{
                //    Debug.Log("Current State: " + this.Enemy.GetCurrentState().ToString());
                //}
            }
        }

        private void OnTriggerExit2D(Collider2D otherObject)
        {
            if (otherObject.tag == "Player")
            {
                this.Enemy.LookAtTarget();
                this.Enemy.Target = null;
                
                //if(this.Enemy)
                //this.Enemy.ForceTurn = true;
            }
        }
    }
}