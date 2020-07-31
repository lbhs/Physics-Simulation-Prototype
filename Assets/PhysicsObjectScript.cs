using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjectScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject Anchor;
    public bool isAnchored;

    private void Update()
    {
        if (isAnchored)
        {
            Anchor.SetActive(true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            Anchor.SetActive(false);
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
}
