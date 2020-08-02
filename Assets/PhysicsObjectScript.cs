using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicsObjectScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject AnchorPrefab;
    public GameObject NoGravityPrefab;
    public GameObject PointJointPrefab;
    private GameObject PointJoint;
    public bool hideGravitySymbolOnPlay=false;
    public GameObject VelocityLinePrefab;
    [HideInInspector]public LineRenderer VelocityLine;
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
        DataSaveingScript.ListOfPhysicsObjects.Add(GetComponent<PhysicsObjectScript>());
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
            if (hideGravitySymbolOnPlay)
            {
                if (NoGravity != null)
                {
                    Destroy(NoGravity);
                }
            }
        }
    }
    private void OnDestroy()
    {
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
    void AddCollision(LineRenderer line, EdgeCollider2D Edge)
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
