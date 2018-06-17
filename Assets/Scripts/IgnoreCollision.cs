using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField]
    private Collider2D m_otherObject;

    private void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), m_otherObject, true);
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start ()
    {
	}

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update ()
    {	
	}
}