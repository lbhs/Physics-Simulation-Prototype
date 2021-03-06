﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicsObjectScript : MonoBehaviour
{
    public int ID = -1;
    public List<int> connectedIDS = new List<int>();
    public Rigidbody2D rb;
    public GameObject AnchorPrefab;
    public GameObject NoGravityPrefab;
    public GameObject PointJointPrefab;
    private GameObject PointJoint;
    public GameObject LineJointLinePrefab;
    private List<GameObject> LineJointLinesList = new List<GameObject>();
    public GameObject VelocityLinePrefab;
    [HideInInspector] public LineRenderer VelocityLine;
    public Vector3 initalVelocity;
    private GameObject Anchor;
    private GameObject NoGravity;
    public bool isAnchored;
    public int charge;
    public Text ChargeText;
    public void InstanciateAnchor(Vector3 Pos)
    {
        if (Anchor == null)
        {
            Anchor = Instantiate(AnchorPrefab, Pos, Quaternion.identity);
        }
    }
    public void InstanciatePointJointicon(bool d)
    {
        if (d)
        {
            Destroy(PointJoint);
        }
        else
        {

            PointJoint = Instantiate(PointJointPrefab, transform.position, Quaternion.identity);
            PointJoint.transform.parent = transform;
            PointJoint.transform.localPosition = GetComponent<HingeJoint2D>().anchor;
            PointJoint.transform.parent = null;
        }
    }
    public void InstanciateLineJticon(RelativeJoint2D jointOne, RelativeJoint2D jointTwo)
    {
        GameObject Line = Instantiate(LineJointLinePrefab,Vector3.zero,Quaternion.identity);
        Line.transform.parent = transform;
        Line.GetComponent<LineRenderer>().SetPosition(0, jointOne.transform.position);
        Line.GetComponent<LineRenderer>().SetPosition(1, jointTwo.transform.position);
        LineJointLinesList.Add(Line);
        Line.GetComponent<JointLineScript>().one = jointOne;
        Line.GetComponent<JointLineScript>().two = jointTwo;
        
            List<int> list = jointOne.GetComponent<PhysicsObjectScript>().connectedIDS;
            List<int> list2 = jointOne.GetComponent<PhysicsObjectScript>().connectedIDS;
            foreach (var item in list.ToArray())
            {
                foreach (var item2 in list2.ToArray())
                {
                    if (item != item2)
                    {
                        connectedIDS.Add(item);
                    }

                }
            }
        
    }
    public void InstanciateVelocityLine(Vector3 Pos)
    {
        if (VelocityLine != null)
        {
            Destroy(VelocityLine.gameObject);
        }
        VelocityLine = Instantiate(VelocityLinePrefab, Pos, Quaternion.identity).GetComponent<LineRenderer>();
        VelocityLine.transform.parent = transform;
    }
    private void Start()
    {
        if (!DataSaveingScript.ListOfPhysicsObjects.Contains(GetComponent<PhysicsObjectScript>()))
        {
            DataSaveingScript.ListOfPhysicsObjects.Add(GetComponent<PhysicsObjectScript>());
        }
        if (ID == -1) //int cannot be null (only zero), so -1 is my pretend null
        {
            ID = DataSaveingScript.nextIDNum;
            DataSaveingScript.nextIDNum++;
        }
        if (isAnchored)
        {
            InstanciateAnchor(transform.position);
        }
        ResetVelocity();
    }
    private void Update()
    {
        if (isAnchored)
        {
            //Anchor.SetActive(true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            Destroy(Anchor);
            rb.constraints = RigidbodyConstraints2D.None;
        }
        if (VelocityLine == null)
        {
            initalVelocity = Vector3.zero;
        }
        if (Input.GetMouseButton(0))
        {
            ResetVelocity();
        }
        else if(Time.timeScale == 1)
        {
            if (VelocityLine != null)
            {
                Destroy(VelocityLine.gameObject);
            }
        }
        if (charge != 0)
        {
            if (charge > 0)
            {
                ChargeText.text = "+"+charge.ToString();
            }
            else
            {
                ChargeText.text = charge.ToString();
            }
        }
        if (Time.timeScale == 0)
        {
            if (rb.gravityScale == 1)
            {
                Destroy(NoGravity);
            }
            else
            {
                if (NoGravity == null)
                {
                    NoGravity = Instantiate(NoGravityPrefab, transform);
                }
            }
        }
        else
        {
            
                if (NoGravity != null)
                {
                    Destroy(NoGravity);
                }
            
        }
    }
    private void OnDestroy()
    {
        Destroy(Anchor);
        Destroy(NoGravity);
        Destroy(PointJoint);
        foreach (var item in connectedIDS)
        {
            foreach (var items in DataSaveingScript.ListOfPhysicsObjects)
            {
                if(item == items.ID)
                {
                    RelativeJoint2D[] j = items.GetComponents<RelativeJoint2D>();
                    foreach (var itemsj in j)
                    {
                        Destroy(itemsj);
                    }
                    List<GameObject> L = items.LineJointLinesList;
                    foreach (var itemsl in L)
                    {
                        Destroy(itemsl);
                    }
                }
            }
        }
        DataSaveingScript.ListOfPhysicsObjects.Remove(GetComponent<PhysicsObjectScript>());
    }

    public void ResetVelocity()
    {
        InstanciateVelocityLine(transform.position);
        VelocityLine.SetPosition(1, initalVelocity);
        AddCollision(VelocityLine, VelocityLine.GetComponent<EdgeCollider2D>());
    }

    public void StartSimulation()
    {
        rb.velocity = initalVelocity;
    }
    public void AddCollision(LineRenderer line, EdgeCollider2D Edge)
    {
        Vector3[] Poses = new Vector3[line.positionCount];
        line.GetPositions(Poses);
        List<Vector2> DPoses = new List<Vector2>();

        for (int i = 0; i < Poses.Length; i++)
        {
            DPoses.Add(new Vector2(Poses[i].x, Poses[i].y));
        }
        Edge.points = DPoses.ToArray();
    }
}
