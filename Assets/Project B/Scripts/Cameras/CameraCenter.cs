using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour
{
    [SerializeField] Camera[] m_Cameras = new Camera[2];
    int m_SelectedIndex = 0;
    // Use this for initialization
    void Start()
    {
        SelectCamera(m_SelectedIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            NextCamera();
        }
    }
    void NextCamera()
    {
        m_SelectedIndex++;
        if (m_SelectedIndex >= m_Cameras.Length)
        {
            m_SelectedIndex = 0;
        }
        SelectCamera(m_SelectedIndex);
    }
    void SelectCamera(int index)
    {
        for (int i = 0; i < m_Cameras.Length; i++)
        {
            m_Cameras[i].enabled = i == index;
        }
    }
}
