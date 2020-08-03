using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class DataSaveingScript : MonoBehaviour
{
    public static List<PhysicsObjectScript> ListOfPhysicsObjects = new List<PhysicsObjectScript>();
    public static int nextIDNum;
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
