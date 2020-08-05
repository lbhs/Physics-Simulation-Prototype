using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;

//using Newtonsoft.Json;
[Serializable]
public class ObjectInfo
{
    public string Name;
    public Vector3 pos;
    public Vector3 rot;
    public Vector3 scale;
    public Color color;
}

[Serializable]
public class Wrapper
{
    public List<ObjectInfo> objects = new List<ObjectInfo>();
}

public class DataSaveingScript : MonoBehaviour
{
    public static List<PhysicsObjectScript> ListOfPhysicsObjects = new List<PhysicsObjectScript>();
    public static int nextIDNum;
    public static string JSONFileText;
    public static void SaveJSON()
    {
        List<ObjectInfo> objs = new List<ObjectInfo>();

        foreach (var item in ListOfPhysicsObjects)
        {
            objs.Add(ConvertToClass(item));
        }
        Wrapper w = new Wrapper();
        foreach (var item in objs)
        {
            w.objects.Add(item);

        }
        JSONFileText = JsonUtility.ToJson(w);
        print(JSONFileText);
    }
    public static void LoadJSON(string JSON)
    {
        print(JSON);
    }

    static ObjectInfo ConvertToClass(PhysicsObjectScript obj)
    {
        ObjectInfo Class = new ObjectInfo();
        Class.Name = obj.name;
        Class.pos = obj.transform.position;
        Class.rot = obj.transform.eulerAngles;
        Class.scale = obj.transform.localScale;
        Class.color = obj.GetComponent<Renderer>().material.color;

        return Class;
    }

}

