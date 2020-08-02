using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PointJointScript : MonoBehaviour
{
    public GameObject EmptyPrefab;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.GetComponent<PhysicsObjectScript>() != null)
            {
                GameObject go = Instantiate(EmptyPrefab, hit.transform);
                go.transform.position = hit.point;
                Vector3 pos = go.transform.localPosition;
                Destroy(go);
                if (hit.collider.GetComponent<HingeJoint2D>() == null)
                {
                    hit.collider.gameObject.AddComponent<HingeJoint2D>();
                    hit.collider.gameObject.GetComponent<HingeJoint2D>().anchor = pos;
                    hit.collider.GetComponent<PhysicsObjectScript>().InstanciatePointJointicon(false);
                }
                else
                {
                    hit.collider.GetComponent<PhysicsObjectScript>().InstanciatePointJointicon(true);
                    Destroy(hit.collider.GetComponent<HingeJoint2D>());
                }
            }
        }
    }
}
