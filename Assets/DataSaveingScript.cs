using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;

//using Newtonsoft.Json;
[Serializable]
public class ObjectInfo
{
    public string name;
    public int ID;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public Color color;
    public int charge;
    public bool useGravity;
    public bool isAnchored;
    public Vector2 pointJointPosition;
    public Vector3 initialVelocity;
    public int[] connectedIDs;
    public Vector3[] linePositions;
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
        //print(JSONFileText);
    }
    public static void LoadJSON(string json)
    {
        print(json);
        LoadPrefabs();
        var N = JSON.Parse(json);
        //clear the scene
        PhysicsObjectScript[] currentObjects = FindObjectsOfType<PhysicsObjectScript>();
        foreach (var item in currentObjects)
        {
            Destroy(item.gameObject);
        }
        //get objects from json
        foreach (var item in N["objects"])
        {

        }
        Instantiate(square, Vector3.zero, Quaternion.identity);
    }

    static ObjectInfo ConvertToClass(PhysicsObjectScript obj)
    {
        ObjectInfo Class = new ObjectInfo();
        Class.name = obj.name;
        Class.ID = obj.ID;
        Class.position = obj.transform.position;
        Class.rotation = obj.transform.eulerAngles;
        Class.scale = obj.transform.localScale;
        Class.color = obj.GetComponent<Renderer>().material.color;
        Class.charge = obj.charge;
        if (obj.rb.gravityScale == 0)
        {
            Class.useGravity = false;
        }
        else
        {
            Class.useGravity = true;
        }
        Class.isAnchored = obj.isAnchored;
        if (obj.GetComponent<HingeJoint2D>() != null)
        {
            Class.pointJointPosition = obj.GetComponent<HingeJoint2D>().anchor;
        }
        Class.initialVelocity = obj.initalVelocity;
        Class.connectedIDs = obj.connectedIDS.ToArray();
        if (obj.GetComponent<LineRenderer>() != null)
        {
            Vector3[] poses = new Vector3[obj.GetComponent<LineRenderer>().positionCount];
            obj.GetComponent<LineRenderer>().GetPositions(poses);
            print(poses);
            Class.linePositions = poses;
        }
        return Class;
    }

    public static GameObject square;
    public static GameObject circle;
    public static GameObject triangle;
    public static GameObject line;
    public static void LoadPrefabs()
    {
        if (square == null)
        {
            square = (GameObject)Resources.Load("Square", typeof(GameObject));
            circle = (GameObject)Resources.Load("Circle", typeof(GameObject));
            triangle = (GameObject)Resources.Load("Triangle", typeof(GameObject));
            line = (GameObject)Resources.Load("Line", typeof(GameObject));
        }
    }
}

