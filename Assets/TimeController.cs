using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float PlayingTimeScale = 1;
    public float G;
    public float k;
    // Start is called before the first frame update
    void Start()
    {
        StopTime();
    }

    public void startTime()
    {
        foreach (PhysicsObjectScript item in DataSaveingScript.ListOfPhysicsObjects)
        {
            item.StartSimulation();
        }
        Time.timeScale = PlayingTimeScale;
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    //Benny's code
    //Calculates electrostatic and gravitational forces on all objects in gameobjects list every frame
    void FixedUpdate()
    {
        //Ensures that forces do not get caculated while paused
        if (Time.timeScale != 0)
        {
            //Nested for loops + if statement to calculate force that each object exerts on every other object
            foreach (var item in DataSaveingScript.ListOfPhysicsObjects)
            {
                GameObject a = item.gameObject;
                foreach (var itemTwo in DataSaveingScript.ListOfPhysicsObjects)
                {
                    GameObject b = itemTwo.gameObject;
                    if (a != b && a.GetComponent<Rigidbody2D>() != null && b.GetComponent<Rigidbody2D>() != null)
                    {
                        //all variable retrieval necessary for force math                   
                        float m1 = a.GetComponent<Rigidbody2D>().mass;
                        float m2 = b.GetComponent<Rigidbody2D>().mass;
                        float q1 = item.charge;
                        float q2 = itemTwo.charge;
                        Vector3 dir = Vector3.Normalize(b.transform.position - a.transform.position);
                        float r = Vector3.Distance(a.transform.position, b.transform.position);

                        //individually calculates force of gravity and electrostatics
                        float Fg = (m1 * m2 * G) / Mathf.Pow(r, 2);
                        float Fe = (k * q1 * q2) / Mathf.Pow(r, 2);

                        //applies force vector
                        //must use time.fixeddeltatime here to keep constant framerate with different timescales
                        a.GetComponent<Rigidbody2D>().AddForce(dir * (Fg - Fe) * Time.fixedDeltaTime);
                    }
                }
            }
        }
    }
}
