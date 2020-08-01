using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainCanvasScript : MonoBehaviour
{
    public ToggleGroup TG;
    public List<MonoBehaviour> DrawerScripts = new List<MonoBehaviour>();
    private string toggle;
    private Vector3 offset;
    private Quaternion Roffset;
    private Rigidbody2D MovingObject;
    public DragCamera2D CameraDrag;
    // Update is called once per frame
    void Update()
    {
        //Gets the current toggle
        toggle = "";
        if (TG != null)
        {
            IEnumerator<Toggle> toggleEnum = TG.ActiveToggles().GetEnumerator();
            toggleEnum.MoveNext();
            toggle = toggleEnum.Current.name;
        }

        //Rotate tool
        if (toggle == "Rotate")
        {
            CameraDrag.dragEnabled = false;
            foreach (MonoBehaviour item in DrawerScripts)
            {
                item.enabled = false;
            }
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    MovingObject = hit.collider.GetComponent<Rigidbody2D>();
                    MovingObject.simulated = false;
                    Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - MovingObject.transform.position;
                    diff.Normalize();

                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                    Roffset = Quaternion.Inverse( Quaternion.Euler(0f, 0f, rot_z - 90)) * MovingObject.transform.rotation;
                }
            }
            if (Input.GetMouseButton(0))
            {
                if (MovingObject != null)
                {
                    Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - MovingObject.transform.position;
                    diff.Normalize();

                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                    MovingObject.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90) * Roffset;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (MovingObject != null)
                {
                    //move object
                    MovingObject.simulated = true;
                    MovingObject = null;
                }
            }
        }
        //move tool
        else if (toggle == "Move")
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    //move object
                    MovingObject = hit.collider.GetComponent<Rigidbody2D>();
                    MovingObject.simulated = false;
                    offset = hit.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    offset.z = 10;
                }
                else
                {
                    CameraDrag.dragEnabled = true;
                }
            }
            if (Input.GetMouseButton(0))
            {
                if (MovingObject != null)
                {
                    //move object
                    MovingObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (MovingObject != null)
                {
                    //move object
                    MovingObject.simulated = true;
                    MovingObject = null;
                }
                CameraDrag.dragEnabled = false;
            }
            foreach (MonoBehaviour item in DrawerScripts)
            {
                item.enabled = false;
            }
        }
        //enable Circle tool
        else if (toggle == "Circle")
        {
            CameraDrag.dragEnabled = false;
            foreach (MonoBehaviour item in DrawerScripts)
            {
                if (item.GetType().Name == "CircleDrawerScript")
                {
                    item.enabled = true;
                }
                else
                {
                    item.enabled = false;
                }
            }
        }
        //enable Triangle tool
        else if (toggle == "Triangle")
        {
            CameraDrag.dragEnabled = false;
            foreach (MonoBehaviour item in DrawerScripts)
            {
                if (item.GetType().Name == "TriangleDrawerScript")
                {
                    item.enabled = true;
                }
                else
                {
                    item.enabled = false;
                }
            }
        }
        //enable Square tool
        else if (toggle == "Square")
        {
            CameraDrag.dragEnabled = false;
            foreach (MonoBehaviour item in DrawerScripts)
            {
                if (item.GetType().Name == "SquareDrawerScript")
                {
                    item.enabled = true;
                }
                else
                {
                    item.enabled = false;
                }
            }
        }
        //enable Line tool
        else if (toggle == "Line")
        {
            CameraDrag.dragEnabled = false;
            foreach (MonoBehaviour item in DrawerScripts)
            {
                if (item.GetType().Name == "LineDrawerScript")
                {
                    item.enabled = true;
                }
                else
                {
                    item.enabled = false;
                }
            }
        }
        //Anchor tool
        else if (toggle == "Anchor")
        {
            CameraDrag.dragEnabled = false;
            foreach (MonoBehaviour item in DrawerScripts)
            {
                item.enabled = false;
            }
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<PhysicsObjectScript>() != null)
                    {
                        if (hit.collider.GetComponent<PhysicsObjectScript>().isAnchored == false)
                        {
                            hit.collider.GetComponent<PhysicsObjectScript>().InstanciateAnchor(hit.point);
                            hit.collider.GetComponent<PhysicsObjectScript>().isAnchored = true;
                        }
                        else
                        {
                            hit.collider.GetComponent<PhysicsObjectScript>().isAnchored = false;
                        }
                    }
                }
            }
        }
        //joint tool (pivot point)
        else if (toggle == "Point_Joint")
        {
            CameraDrag.dragEnabled = false;
            foreach (MonoBehaviour item in DrawerScripts)
            {
                if (item.GetType().Name == "PointJointScript")
                {
                    item.enabled = true;
                }
                else
                {
                    item.enabled = false;
                }
            }
        }
        //Delete tool
        else if (toggle == "Delete")
        {
            CameraDrag.dragEnabled = false;
            foreach (MonoBehaviour item in DrawerScripts)
            {
                item.enabled = false;
            }
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<PhysicsObjectScript>() != null)
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }
            }

        }
        //line joint tool (between two objects
        else if (toggle == "Line_Joint")
        {
            CameraDrag.dragEnabled = false;
            foreach (MonoBehaviour item in DrawerScripts)
            {
                if (item.GetType().Name == "LineJointScript")
                {
                    item.enabled = true;
                }
                else
                {
                    item.enabled = false;
                }
            }
        }
        //line joint tool (between two objects
        else if (toggle == "Velocity")
        {
            CameraDrag.dragEnabled = false;
            foreach (MonoBehaviour item in DrawerScripts)
            {
                if (item.GetType().Name == "VelocityArrowScript")
                {
                    item.enabled = true;
                }
                else
                {
                    item.enabled = false;
                }
            }
        }
        //Color tool
        else if (toggle == "Color")
        {
            CameraDrag.dragEnabled = false;
            foreach (MonoBehaviour item in DrawerScripts)
            {
                if (item.GetType().Name == "ColorScript")
                {
                    item.enabled = true;
                }
                else
                {
                    item.enabled = false;
                }
            }
        }
        //Charge tool
        else if (toggle == "PlusMinus")
        {
            CameraDrag.dragEnabled = false;
            foreach (MonoBehaviour item in DrawerScripts)
            {
                item.enabled = false;
            }
        }
        //Charge tool
        else if (toggle == "ChargeMinus")
        {
            CameraDrag.dragEnabled = false;
            foreach (MonoBehaviour item in DrawerScripts)
            {
                item.enabled = false;
            }
        }
    }
}
