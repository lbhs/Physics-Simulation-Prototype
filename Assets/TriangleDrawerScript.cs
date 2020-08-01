using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriangleDrawerScript : MonoBehaviour
{
    public GameObject TrianglePrefab;
    public bool MaintainAspectRatio = false;
    public float scale = 10;
    public float minMagnitude = 3;
    public float maxMagnitude = 100;
    private GameObject triangle;
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

                triangle = Instantiate(TrianglePrefab, DownPos, Quaternion.identity);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (triangle != null)
            {
                var v3 = Input.mousePosition;
                v3.z = 10.0f;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(v3);

                Vector3 value = (triangle.transform.position - mousePos) * scale;
                if (MaintainAspectRatio == true)
                {
                    float max = Mathf.Max(value.x, value.y, value.z);
                    value = new Vector3(max, max, max);
                }
                if (value.sqrMagnitude > maxMagnitude * maxMagnitude) //sqrMagnitude is more efficient than magnitude
                {
                    value = triangle.transform.localScale;
                }
                if (value.sqrMagnitude < minMagnitude * minMagnitude)
                {
                    value = triangle.transform.localScale;
                }
                triangle.transform.position = (DownPos + mousePos) / 2;
                triangle.transform.localScale = value;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (triangle != null)
            {
                //TO-DO apply changes to the save json script
                triangle.GetComponent<Rigidbody2D>().simulated = true;
            }
        }
    }
}