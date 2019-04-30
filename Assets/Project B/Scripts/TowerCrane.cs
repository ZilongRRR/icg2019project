using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCrane : MonoBehaviour
{
    [SerializeField] GameObject m_Jib;
    [SerializeField] GameObject m_Trolley;
    [SerializeField] GameObject m_Cable;

    const float MOVE_SPEED = 8f;
    float LONG_LLIMIT = 0f;
    const float LONG_HLIMIT = -17f;

    List<GameObject> gameObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        LONG_LLIMIT = m_Trolley.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        #region Key Control
        if (Input.GetKey(KeyCode.A))
        {
            var nowRotate = m_Jib.transform.localRotation.eulerAngles;
            nowRotate.z -= MOVE_SPEED * Time.deltaTime;
            m_Jib.transform.localRotation = Quaternion.Euler(nowRotate);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            var nowRotate = m_Jib.transform.localRotation.eulerAngles;
            nowRotate.z += MOVE_SPEED * Time.deltaTime;
            m_Jib.transform.localRotation = Quaternion.Euler(nowRotate);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            var nowPos = m_Trolley.transform.localPosition;
            m_Trolley.transform.localPosition = new Vector3(nowPos.x, Mathf.Max(nowPos.y -= MOVE_SPEED / 2 * Time.deltaTime, LONG_HLIMIT), nowPos.z);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            var nowPos = m_Trolley.transform.localPosition;
            m_Trolley.transform.localPosition = new Vector3(nowPos.x, Mathf.Min(nowPos.y += MOVE_SPEED / 2 * Time.deltaTime, LONG_LLIMIT), nowPos.z);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            var lineRenderer = m_Cable.GetComponent<LineRenderer>();
            var oldPos = lineRenderer.GetPosition(1);
            lineRenderer.SetPosition(1, new Vector3(oldPos.x, oldPos.y, Mathf.Min(-1, oldPos.z += MOVE_SPEED / 2 * Time.deltaTime)));
            m_Cable.transform.GetChild(0).transform.localPosition = new Vector3(oldPos.x, oldPos.y, Mathf.Min(-1, oldPos.z += MOVE_SPEED / 2 * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.E))
        {
            var lineRenderer = m_Cable.GetComponent<LineRenderer>();
            var oldPos = lineRenderer.GetPosition(1);
            lineRenderer.SetPosition(1, new Vector3(oldPos.x, oldPos.y, oldPos.z -= MOVE_SPEED / 2 * Time.deltaTime));
            m_Cable.transform.GetChild(0).transform.localPosition = new Vector3(oldPos.x, oldPos.y, oldPos.z -= MOVE_SPEED / 2 * Time.deltaTime);
        }
        #endregion
    }
}
