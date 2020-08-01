using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CircleDrawerScript : MonoBehaviour
{
    public bool DrawFromCenter;
    public GameObject CirclePrefab;
    public bool MaintainAspectRatio = true;
    public float scale = 10;
    public float minMagnitude = 3;
    public float maxMagnitude = 15;
    private GameObject circle;
    private Vector3 DownPos;
    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                var v3 = Input.mousePosition;
                v3.z = 10.0f;
                DownPos = Camera.main.ScreenToWorldPoint(v3);

                circle = Instantiate(CirclePrefab, DownPos, Quaternion.identity);
            }
        }
            if (Input.GetMouseButton(0))
            {
                var v3 = Input.mousePosition;
                v3.z = 10.0f;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(v3);

                Vector3 value = (circle.transform.position - mousePos) * scale;
                if (MaintainAspectRatio == true)
                {
                    float max = Mathf.Max(value.x, value.y, value.z);
                    value = new Vector3(max, max, max);
                }
                if (value.sqrMagnitude > maxMagnitude * maxMagnitude) //sqrMagnitude is more efficient than magnitude
                {
                    //value = new Vector3(maxMagnitude, maxMagnitude, maxMagnitude);
                    value = circle.transform.localScale;
                }
                if (value.sqrMagnitude < minMagnitude * minMagnitude)
                {
                    //value = new Vector3(minMagnitude, minMagnitude, minMagnitude);
                    value = circle.transform.localScale;
                }
                if (DrawFromCenter == false)
                {
                    circle.transform.position = (DownPos + mousePos) / 2;
                }
                circle.transform.localScale = value;
            }
            if (Input.GetMouseButtonUp(0))
            {
                //TO-DO apply changes to the save json script
                circle.GetComponent<Rigidbody2D>().simulated = true;
            }
        
    }
}
