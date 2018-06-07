using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float SpeedX;
    public float JumpSpeedY;

    public GameObject ProjectileLeft;
    public GameObject ProjectileRight;

    private Animator m_animator;
    private Rigidbody2D m_rigidbody;

    private bool m_isFacingRight;
    private bool m_isJumping;
    private bool m_isDoubleJumping;
    private float m_speed;

    private Transform m_firePosition;
    private DateTime m_lastFireTime = DateTime.Now;

    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();

        m_isFacingRight = true;

        m_firePosition = transform.Find("FirePosition");
	}
	
	// Update is called once per frame
	void Update ()
    {
        SetPlayerState(m_speed);
        SetPlayerOrientation();

        MovePlayer();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            m_isJumping = false;
            m_isDoubleJumping = false;

            m_animator.SetInteger("State", 0);
        }
    }

    private void SetPlayerState(float playerSpeed)
    {
        if (m_isJumping == false)
        {
            if (playerSpeed != 0)
            {
                m_animator.SetInteger("State", 1);
            }
            else
            {
                m_animator.SetInteger("State", 0);
            }
        }

        m_rigidbody.velocity = new Vector3(m_speed, m_rigidbody.velocity.y, 0);
    }

    private void SetPlayerOrientation()
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

    #region Movement

    private void MovePlayer()
    {
        // Left player movement
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_speed = -SpeedX;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            m_speed = 0;
        }

        // Right player movement
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_speed = SpeedX;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_speed = 0;
        }

        // Upward player movement
        if (Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.B))
        {
            Jump();
        }

        // Fire
        if (Input.GetKeyDown(KeyCode.LeftControl) ||
            Input.GetKeyDown(KeyCode.RightControl) ||
            Input.GetKeyDown(KeyCode.A))
        {
            Fire();
        }
    }
    
    public void MoveLeft()
    {
        m_speed = -SpeedX;
    }

    public void MoveRight()
    {
        m_speed = SpeedX;
    }

    public void StopMoving()
    {
        m_speed = 0;
    }

    public void Jump()
    {
        if (m_rigidbody.velocity.y > 0)
        {
            m_animator.SetInteger("State", 3); // jumping
        }
        else
        {
            m_animator.SetInteger("State", 2); // falling
        }

        if (m_isJumping && m_isDoubleJumping)
        {
            return;
        }

        if (m_isJumping == false)
        {
            m_isJumping = true;
        }
        else
        {
            m_isDoubleJumping = true;
        }

        m_rigidbody.AddForce(new Vector2(m_rigidbody.velocity.x, JumpSpeedY));

        
    }
    
    public void Fire()
    {
        TimeSpan ts = DateTime.Now.Subtract(m_lastFireTime);

        if (ts.TotalMilliseconds > 300)
        {
            m_lastFireTime = DateTime.Now;

            if (m_isFacingRight)
            {
                Instantiate(this.ProjectileRight, m_firePosition.position, Quaternion.identity);
            }
            else
            {
                Instantiate(this.ProjectileLeft, m_firePosition.position, Quaternion.identity);
            }
        }
    }

    #endregion Movement
}