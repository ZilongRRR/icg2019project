using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossDetec : MonoBehaviour
{
    AudioSource m_alarm;
    public SpriteRenderer redLight;
    int count = 0;
    void OnTriggerEnter2D(Collider2D other)
    {
        count += 1;
        if (redLight.color == Color.red)
        {
            if (!m_alarm.isPlaying) { m_alarm.Play(); }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (redLight.color == Color.red)
        {
            if (!m_alarm.isPlaying) { m_alarm.Play(); }
        }
        else { m_alarm.Stop(); }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        count -= 1;
        if (count == 0) { m_alarm.Stop(); }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_alarm = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (count != 0)
        {
            if (redLight.color == Color.red)
            {
                if (!m_alarm.isPlaying) { m_alarm.Play(); }
            }
        }
    }
}
