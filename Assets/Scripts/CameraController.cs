using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaveKoala
{
    public class CameraController : MonoBehaviour
    {
        public Transform Player;

        /// <summary>
        /// Use this for initialization
        /// </summary>
        void Start()
        {
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {
            if (!(transform == null || this.Player == null))
            {
                transform.position = new Vector3(
                    this.Player.position.x,
                    this.Player.position.y,
                    transform.position.z);
            }
        }
    }
}