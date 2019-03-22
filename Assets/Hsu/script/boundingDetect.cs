using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundingDetect : MonoBehaviour
{
    AudioSource m_alarm;
    int count = 0;
    void OnTriggerEnter2D(Collider2D other)
    {
        count += 1;
        if (!m_alarm.isPlaying) { m_alarm.Play(); }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (!m_alarm.isPlaying) { m_alarm.Play(); }
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

    }
}
