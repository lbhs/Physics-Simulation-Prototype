﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float PlayingTimeScale = 1;
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
}
