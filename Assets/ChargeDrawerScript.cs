using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChargeDrawerScript : MonoBehaviour
{
    public bool isPostiveDrawing;
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
            if (hit.collider != null && hit.collider.GetComponent<PhysicsObjectScript>() != null)
            {
                if (isPostiveDrawing)
                {
                    hit.collider.GetComponent<PhysicsObjectScript>().charge++;
                }
                else
                {
                    hit.collider.GetComponent<PhysicsObjectScript>().charge--;
                }
            }
        }
    }
}
