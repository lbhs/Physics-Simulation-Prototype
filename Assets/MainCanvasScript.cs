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
                }
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - MovingObject.transform.position;
                diff.Normalize();

                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                MovingObject.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
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
                    if (hit.collider.GetComponent<PhysicsObjectScript>() != null && hit.collider.GetComponent<Rigidbody2D>() != null)
                    {
                        Rigidbody2D rb = hit.collider.GetComponent<Rigidbody2D>();
                        if (rb.constraints == RigidbodyConstraints2D.FreezeAll)
                        {
                            hit.collider.GetComponent<PhysicsObjectScript>().Anchor.SetActive(false);
                            rb.constraints = RigidbodyConstraints2D.None;
                        }
                        else
                        {
                            hit.collider.GetComponent<PhysicsObjectScript>().Anchor.SetActive(true);
                            rb.constraints = RigidbodyConstraints2D.FreezeAll;
                        }
                    }
                }
            }
        }
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
    }
}
