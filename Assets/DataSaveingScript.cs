using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveingScript : MonoBehaviour
{
    public static GameObject ListOfPhysicsObjects;
    public static string JSONFileText;
    public static void SaveJSON()
    {
        JSONFileText = "not yet!";
    }
    public static void LoadJSON(string JSON)
    {
        print(JSON);
    }
}
