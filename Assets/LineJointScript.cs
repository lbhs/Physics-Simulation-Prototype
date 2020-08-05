using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class LineJointScript : MonoBehaviour
{
    public LineRenderer LR;
    private Rigidbody2D objectOne;
    private Rigidbody2D objectTwo;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform != null)
            {
                Vector3 pos = hit.point;
                pos.z = -1;
                LR.SetPosition(0, pos);
                objectOne = hit.transform.GetComponent<Rigidbody2D>();
            }   
        }
        if (Input.GetMouseButton(0))
        {
            if (objectOne != null)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = -1;
                LR.SetPosition(1, pos);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (objectOne != null)
            {
                LR.SetPosition(0, Vector3.zero);
                LR.SetPosition(1, Vector3.zero);

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.transform != null)
                {
                    objectTwo = hit.transform.GetComponent<Rigidbody2D>();
                    BondObjects(objectOne, objectTwo);
                }
            }
            objectOne = null;
            objectTwo = null;
        }
    }

    public static void BondObjects(Rigidbody2D One, Rigidbody2D Two)
    {
        RelativeJoint2D O = One.gameObject.AddComponent<RelativeJoint2D>();
        RelativeJoint2D T = Two.gameObject.AddComponent<RelativeJoint2D>();
        O.connectedBody = Two;
        T.connectedBody = One;
        One.GetComponent<PhysicsObjectScript>().connectedIDS.Add(Two.GetComponent<PhysicsObjectScript>().ID);
        Two.GetComponent<PhysicsObjectScript>().connectedIDS.Add(One.GetComponent<PhysicsObjectScript>().ID);
        //graphics
        One.GetComponent<PhysicsObjectScript>().InstanciateLineJticon(O,T);
    }
}
