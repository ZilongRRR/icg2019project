using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingHook : MonoBehaviour
{
    // Start is called before the first frame update
    const float MOVE_SPEED = 2f;
    const float ATTACH_DISTANCE = 4f;
    bool checkfirstconnectmoment = false;
    GameObject m_DetectedObject;
    void Start()
    {
        
    }
    Joint m_JointForObject;
    // Update is called once per frame
    void Update()
    {
        #region Key Control
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(0, 0, MOVE_SPEED * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0, 0, -MOVE_SPEED * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(MOVE_SPEED * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(-MOVE_SPEED * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Translate(0, MOVE_SPEED * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            this.transform.Translate(0, -MOVE_SPEED * Time.deltaTime, 0);
        }
        #endregion
        DetectObjects();
        /*
        if (m_JointForObject.connectedBody == null)
        {
            DetectObjects();
        }
        */
        if (Input.GetKey(KeyCode.Space))
        {
            AttachOrDetachObject();
        }

        UpdateCable();
    }
    [SerializeField] GameObject m_JointBody;
    void AttachOrDetachObject()
    {

        if (m_JointForObject == null)
        {

            if (m_DetectedObject != null)
            {

                var joint = m_JointBody.AddComponent<ConfigurableJoint>();
                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
                joint.zMotion = ConfigurableJointMotion.Limited;
                joint.angularXMotion = ConfigurableJointMotion.Free;
                joint.angularYMotion = ConfigurableJointMotion.Free;
                joint.angularZMotion = ConfigurableJointMotion.Free;

                var limit = joint.linearLimit;
                limit.limit = 4;

                joint.linearLimit = limit;

                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = new Vector3(0f, 0.5f, 0f);
                joint.anchor = new Vector3(0f, 0f, 0f);

                joint.connectedBody = m_DetectedObject.GetComponent<Rigidbody>();

                m_JointForObject = joint;

                m_DetectedObject.GetComponent<MeshRenderer>().material.color = Color.red;
                m_DetectedObject = null;
            }

        }
        else
        {

            GameObject.Destroy(m_JointForObject);
            m_JointForObject = null;
        }
    }
    void DetectObjects()
    {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, ATTACH_DISTANCE))
        {
            if (m_DetectedObject == hit.collider.gameObject)
            {
                return;
            }
            RecoverDetectedObject();
            MeshRenderer renderer = hit.collider.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.yellow;

                m_DetectedObject = hit.collider.gameObject;
            }
            else
            {
                RecoverDetectedObject();
            }

        }
    }
    void RecoverDetectedObject()
    {
        if (m_DetectedObject != null)
        {
            m_DetectedObject.GetComponent<MeshRenderer>().material.color = Color.white;
            m_DetectedObject = null;
        }
    }
    LineRenderer m_Cable;
    int[] indexes = new int[4] { 0, 0, 0, 0 };
    void UpdateCable()
    {
        LineRenderer[] lineArray = this.GetComponentsInChildren<LineRenderer>();
        if (m_JointForObject != null)
        {
            if (m_JointForObject.connectedBody != null)
            {
                foreach (LineRenderer lr in lineArray)
                    lr.enabled = true;
                Vector3[] vertices = m_JointForObject.connectedBody.GetComponentInParent<MeshFilter>().mesh.vertices;
                if (checkfirstconnectmoment == false)
                {
                    float totalZ = 0f;
                    foreach (Vector3 vec in vertices)
                        totalZ += vec.z;
                    float ave = totalZ / 8;

                    checkfirstconnectmoment = true;
                    int index = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        Debug.Log(vertices[i]);
                        if (vertices[i].z > ave)
                        {
                            indexes[index] = i;
                            index++;
                        }
                    }
                }

                for (int i = 0; i < lineArray.Length; i++)
                {
                    lineArray[i].SetPosition(0, this.transform.position);
                    lineArray[i].SetPosition(1, m_JointForObject.connectedBody.transform.position + vertices[indexes[i]]);
                }
            }
            else
            {
                checkfirstconnectmoment = false;
            }
        }

        foreach (LineRenderer lr in lineArray)
            Destroy(lr);
    }
}
