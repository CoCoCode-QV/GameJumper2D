using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float LerpTime;
    public float xOffset;
    bool m_canLerp;
    float m_LerpXDist;
    private void Update()
    {
        if (m_canLerp)
            MoveLerp();
    }
    void MoveLerp()
    {
        
        float xPos = transform.position.x;
        xPos = Mathf.Lerp(xPos, m_LerpXDist, LerpTime * Time.deltaTime);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        if(transform.position.x >= (m_LerpXDist - xOffset))
        {
            m_canLerp = false;
        }
    }
    public void LearpTrigger(float dist)
    {
        m_canLerp = true;
        m_LerpXDist = dist;
    }
}
