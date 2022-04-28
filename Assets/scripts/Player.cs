using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 jumpForce;
    public Vector2 jumpForceUp;

    public float minForceX;
    public float maxForceX;
    public float minForceY;
    public float maxForceY;

    [HideInInspector]
    public int lastPlatFormID;

    bool m_didJump;
    bool m_PowerSetted;

    Rigidbody2D m_rb;
    Animator m_anim;

    float m_curPowerBarValue = 0;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
         
    }
    private void Update()
    {
        if (GameManager.Ins.IsGameStarted)
        {
            setPower();
            if (Input.GetMouseButtonDown(0))
            {
                setPower(true);
            }
            if (Input.GetMouseButtonUp(0))
            {
                setPower(false);
            }
        }
    }

    void setPower()
    {
        if(m_PowerSetted && !m_didJump)
        {
            jumpForce.x += jumpForceUp.x * Time.deltaTime;
            jumpForce.y += jumpForceUp.y * Time.deltaTime;

            jumpForce.x = Mathf.Clamp(jumpForce.x, minForceX, maxForceX);
            jumpForce.y = Mathf.Clamp(jumpForce.y, minForceY, maxForceY);

            m_curPowerBarValue += GameManager.Ins.PowerBarUp * Time.deltaTime;
            GameGuiManager.Ins.UpdatePowerBar(m_curPowerBarValue, 1);


        }
    }
    public void setPower(bool value)
    {
        m_PowerSetted = value;
        
        if(!m_PowerSetted && !m_didJump)
        {
            Jump();
        }
    }
    void Jump()
    {
        if (!m_rb || jumpForce.x <= 0 || jumpForce.y <= 0) return;

        m_rb.velocity = jumpForce;
        m_didJump = true;
        if (m_anim)
        {
            m_anim.SetBool("change", true);
        }
        AudioController.Ins.PlaySound(AudioController.Ins.jump);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConsts.Ground))
        {
            PlatformID p = collision.transform.root.GetComponent<PlatformID>();
            if (m_didJump)
            {
                m_didJump = false;
                if (m_anim)
                {
                    m_anim.SetBool("change", false);
                }
                if (m_rb)
                {
                    m_rb.velocity = Vector2.zero;
                }
                jumpForce = Vector2.zero;
                m_curPowerBarValue = 0;
                GameGuiManager.Ins.UpdatePowerBar(m_curPowerBarValue, 1);

            }
            if(p && p.ID != lastPlatFormID)
            {
                GameManager.Ins.CreatePlatformAndLerp(transform.position.x);
                GameManager.Ins.AddScore();
                lastPlatFormID = p.ID;
            }
            
        }
        if (collision.CompareTag(TagConsts.Dead_Zone))
        {
            GameGuiManager.Ins.ShowGameOverDialog();
            Destroy(gameObject);
            AudioController.Ins.PlaySound(AudioController.Ins.gameover);
        }

    }
}
