using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LineDrawerScript : MonoBehaviour
{
    public GameObject LineObject;
    public float lineQuality=0.5f;
    private GameObject Line;
    private LineRenderer LR;
    private Vector3 DownPos;
    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                var v3 = Input.mousePosition;
                v3.z = 10.0f;
                DownPos = Camera.main.ScreenToWorldPoint(v3);

                Line = Instantiate(LineObject, DownPos, Quaternion.identity);
                LR = Line.GetComponent<LineRenderer>();
                LR.positionCount++;
                LR.SetPosition(LR.positionCount-1, DownPos);
            }
            if (Input.GetMouseButton(0))
            {
                var v3 = Input.mousePosition;
                v3.z = 10.0f;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(v3);
                if ((mousePos - LR.GetPosition(LR.positionCount-1)).magnitude > lineQuality)
                {
                    LR.positionCount++;
                    LR.SetPosition(LR.positionCount - 1, mousePos);
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                //TO-DO apply changes to the save json script
                //triangle.GetComponent<Rigidbody2D>().simulated = true;
                if (LR.positionCount <= 1)
                {
                    Destroy(Line);
                }
                else
                {
                    AddMesh(LR, Line.GetComponent<PolygonCollider2D>());
                }
            }
        }
    }

    void AddMesh(LineRenderer line, PolygonCollider2D Poly)
    {

    }
}
