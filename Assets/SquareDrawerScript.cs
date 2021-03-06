﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SquareDrawerScript : MonoBehaviour
{
    public GameObject SquarePrefab;
    public bool MaintainAspectRatio = true;
    public float scale = 10;
    public float minMagnitude = 3;
    public float maxMagnitude = 15;
    private GameObject square;
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

                square = Instantiate(SquarePrefab, DownPos, Quaternion.identity);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (square != null)
            {
                var v3 = Input.mousePosition;
                v3.z = 10.0f;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(v3);

                Vector3 value = (square.transform.position - mousePos) * scale;
                if (MaintainAspectRatio == true)
                {
                    float max = Mathf.Max(value.x, value.y, value.z);
                    value = new Vector3(max, max, max);
                }
                if (value.sqrMagnitude > maxMagnitude * maxMagnitude) //sqrMagnitude is more efficient than magnitude
                {
                    value = square.transform.localScale;
                }
                if (value.sqrMagnitude < minMagnitude * minMagnitude)
                {
                    value = square.transform.localScale;
                }
                square.transform.position = (DownPos + mousePos) / 2;
                square.transform.localScale = value;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (square != null)
            {
                //TO-DO apply changes to the save json script
                square.GetComponent<Rigidbody2D>().simulated = true;
                square = null;
            }
        }

    }
}
