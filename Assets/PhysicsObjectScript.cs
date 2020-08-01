using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjectScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject AnchorPrefab;
    private GameObject Anchor;
    public bool isAnchored;
    public void InstanciateAnchor(Vector3 Pos)
    {
        if (Anchor == null)
        {
            Anchor = Instantiate(AnchorPrefab, Pos, Quaternion.identity);
        }
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
    }
}
