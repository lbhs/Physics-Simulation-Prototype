using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorScript : MonoBehaviour
{
    public Image PaintBrushIcon;
    public Color CurrentColor;
    public Color[] Colors;
    private int counter = 0;
    public void UpdateColor()
    {
        CurrentColor = Colors[counter];
        PaintBrushIcon.color = CurrentColor;
        if (Colors.Length-1 > counter)
        {
            counter++;
        }
        else
        {
            counter = 0;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null && hit.collider.GetComponent<PhysicsObjectScript>() != null)
            {
                if (hit.collider.GetComponent<LineRenderer>() == null)
                {
                    hit.transform.GetComponent<SpriteRenderer>().color = CurrentColor;
                }
                else
                {
                    hit.transform.GetComponent<LineRenderer>().material.color = CurrentColor;
                }
            }
        }
    }
}
