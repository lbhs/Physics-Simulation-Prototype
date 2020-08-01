using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VelocityArrowScript : MonoBehaviour
{
    private PhysicsObjectScript CurrentObject;
    private Vector3 hitPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                hitPos = hit.point;
                CurrentObject = hit.transform.GetComponent<PhysicsObjectScript>();
                if (hit.transform.name == "Line(Clone)")
                {
                    CurrentObject.InstanciateVelocityLine(hit.point);
                }
                else
                {
                    CurrentObject.InstanciateVelocityLine(CurrentObject.transform.position);
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (CurrentObject != null)
            {
                Vector3 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Pos = new Vector3(Pos.x, Pos.y, 0);
                if (CurrentObject.name != "Line(Clone)")
                {
                    Pos = Pos - CurrentObject.transform.position;
                }
                else
                {
                    Pos = Pos - hitPos;
                }
                CurrentObject.VelocityLine.SetPosition(1, Pos);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (CurrentObject != null)
            {
                CurrentObject.initalVelocity = CurrentObject.VelocityLine.GetPosition(1);
                CurrentObject = null;
            }
        }
    }

    
}
