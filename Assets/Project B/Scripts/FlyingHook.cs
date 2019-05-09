using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingHook : MonoBehaviour
{
    // Start is called before the first frame update
    const float MOVE_SPEED = 2f;
    const float ATTACH_DISTANCE = 1f;
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
        /*
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
        */
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
                var angXLimit = joint.highAngularXLimit;
                angXLimit.limit = 5f;
                joint.highAngularXLimit = angXLimit;

                var angYLimit = joint.angularYLimit;
                angYLimit.limit = 5f;
                joint.angularYLimit = angYLimit;

                var angZLimit = joint.angularZLimit;
                angZLimit.limit = 5f;
                joint.angularZLimit = angZLimit;

                var limit = joint.linearLimit;
                limit.limit = 1f;

                joint.linearLimit = limit;

                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = new Vector3(0f, 0.5f, 0f);
                joint.anchor = new Vector3(0f, 0f, 0f);

                joint.connectedBody = m_DetectedObject.GetComponent<Rigidbody>();
                joint.connectedBody.velocity = Vector3.zero;

                m_JointForObject = joint;

                //m_DetectedObject.GetComponent<MeshRenderer>().material.color = Color.red;
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
        Ray ray = new Ray(this.transform.position - new Vector3(0, 0, 0.4f), Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, ATTACH_DISTANCE))
        {
            if (m_DetectedObject == hit.collider.gameObject)
            {
                return;
            }
            RecoverDetectedObject();
            MeshRenderer renderer = hit.collider.GetComponent<MeshRenderer>();
            //Debug.Log(renderer.gameObject.GetComponent<MeshFilter>().mesh.name);
            if (renderer != null && renderer.gameObject.GetComponent<MeshFilter>().mesh.name == "PlayingBaby Instance")
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
    void UpdateCable()
    {
        LineRenderer[] lineArray = this.GetComponentsInChildren<LineRenderer>();
        if (m_JointForObject != null)
        {
            if (m_JointForObject.connectedBody != null)
            {
                foreach (LineRenderer lr in lineArray)
                    lr.enabled = true;
                List<Vector3> vertices = new List<Vector3>();
                foreach(Transform tr in m_JointForObject.connectedBody.GetComponentsInChildren<Transform>())
                {
                    vertices.Add(tr.position);
                }

                for (int i = 0; i < lineArray.Length; i++)
                {
                    lineArray[i].SetPosition(0, this.transform.position);
                    lineArray[i].SetPosition(1, vertices[i + 1]);
                }
            }
            
        }
        else
        {
            checkfirstconnectmoment = false;
            foreach (LineRenderer lr in lineArray)
            {
                lr.SetPosition(0, this.transform.position);
                lr.SetPosition(1, this.transform.position);
            }
        }
    }
}
