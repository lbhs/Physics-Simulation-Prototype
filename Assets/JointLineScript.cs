using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointLineScript : MonoBehaviour
{
    public LineRenderer LR;
    public RelativeJoint2D one;
    public RelativeJoint2D two;
    private void Start()
    {

        transform.parent = null;
    }
    // Update is called once per frame
    void Update()
    {
        if (one != null && two != null)
        {
            LR.SetPosition(0, two.transform.position);
            LR.SetPosition(1, one.transform.position);
        }
        else
        {
            kaboom();
        }
    }
    public void kaboom()
    {
        if (one != null)
        {
            Destroy(one);
        }
        if (two != null)
        {
            Destroy(two);
        }
        Destroy(gameObject);
    }
}
