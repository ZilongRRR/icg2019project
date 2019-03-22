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
    public TMPro.TextMeshPro redNum, greenNum;
    int m_redTime = 0, m_yellowTime = 0, m_greenTime = 0;
    // 0 for green, 1 for yellow, 2 for red
    int m_status = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++) { m_defaultColor[i] = m_lightRenderers[i].color; }
        // start for green status
        m_lightRenderers[0].color = m_newColor[0];
        m_greenTime = greenTime;
        greenNum.text = "";
        redNum.text = "";
        InvokeRepeating("lightCounting", 1, 1);
    }
    void updateColor()
    {
        int prev = (m_status + 2) % 3;
        m_lightRenderers[prev].color = m_defaultColor[prev];
        m_lightRenderers[m_status].color = m_newColor[m_status];
    }
    void lightCounting()
    {
        switch (m_status)
        {
            case 0:
                // Debug.Log("green " + m_greenTime);
                if (m_greenTime == greenTime) { updateColor(); }
                greenNum.text = "" + m_greenTime;
                m_greenTime -= 1;
                if (m_greenTime == -1)
                {
                    m_status = (m_status + 1) % 3;
                    m_greenTime = greenTime;
                    greenNum.text = "";
                    m_yellowTime = yellowTime;
                }
                break;
            case 1:
                // Debug.Log("yellow " + m_yellowTime);
                if (m_yellowTime == yellowTime) { updateColor(); }
                m_yellowTime -= 1;
                if (m_yellowTime == -1)
                {
                    m_status = (m_status + 1) % 3;
                    m_yellowTime = yellowTime;
                    m_redTime = redTime;
                }
                break;
            case 2:
                // Debug.Log("red " + m_redTime);
                if (m_redTime == redTime) { updateColor(); }
                redNum.text = "" + m_redTime;
                m_redTime -= 1;
                if (m_redTime == -1)
                {
                    m_status = (m_status + 1) % 3;
                    m_redTime = redTime;
                    redNum.text = "";
                    m_greenTime = greenTime;
                }
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
