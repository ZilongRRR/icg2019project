using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightControl : MonoBehaviour
{
    // order is green->yellow->red
    [SerializeField] SpriteRenderer[] m_lightRenderers = new SpriteRenderer[3];
    Color[] m_defaultColor = new Color[3];
    Color[] m_newColor = new Color[3] { Color.green, Color.yellow, Color.red };
    public int redTime = 30, yellowTime = 3, greenTime = 30;
    int m_redTime, m_yellowTime, m_greenTime;
    // 0 for green, 1 for yellow, 2 for red
    int m_status = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_redTime = redTime;
        m_greenTime = greenTime;
        m_yellowTime = yellowTime;
        for (int i = 0; i < 3; i++) { m_defaultColor[i] = m_lightRenderers[i].color; }
        // start for green status
        m_lightRenderers[0].color = m_newColor[0];
        InvokeRepeating("lightCounting", 1, 1);
    }
    void updateColor()
    {
        m_lightRenderers[m_status].color = m_defaultColor[m_status];
        m_status = (m_status + 1) % 3;
        m_lightRenderers[m_status].color = m_newColor[m_status];

    }
    void lightCounting()
    {
        switch (m_status)
        {
            case 0:
                Debug.Log("green " + m_greenTime);
                m_greenTime -= 1;
                if (m_greenTime == 0)
                {
                    updateColor();
                    m_greenTime = greenTime;
                }
                break;
            case 1:
                Debug.Log("yellow " + m_yellowTime);
                m_yellowTime -= 1;
                if (m_yellowTime == 0)
                {
                    updateColor();
                    m_yellowTime = yellowTime;
                }
                break;
            case 2:
                Debug.Log("red " + m_redTime);
                m_redTime -= 1;
                if (m_redTime == 0)
                {
                    updateColor();
                    m_redTime = redTime;
                }
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
