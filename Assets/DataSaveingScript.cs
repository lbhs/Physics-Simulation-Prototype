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
    public bool usePointJoint;
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
        //print(json);
        LoadPrefabs();
        var N = JSON.Parse(json);
        //clear the scene
        PhysicsObjectScript[] currentObjects = FindObjectsOfType<PhysicsObjectScript>();
        foreach (var item in currentObjects)
        {
            Destroy(item.gameObject);
        }
        //get objects from json
        for (int i = 0; i < N["objects"].Count; i++)
        {
            GameObject g = null;
            if (N["objects"][i]["name"] == "Square(Clone)")
            {
                g = Instantiate(square, new Vector3(N["objects"][i]["position"]["x"].AsFloat, N["objects"][i]["position"]["y"].AsFloat, N["objects"][i]["position"]["z"].AsFloat), Quaternion.Euler(new Vector3(N["objects"][i]["rotation"]["x"].AsFloat, N["objects"][i]["rotation"]["y"].AsFloat, N["objects"][i]["rotation"]["z"].AsFloat))) as GameObject;
                g.name = N["objects"][i]["name"];
                g.GetComponent<SpriteRenderer>().color = new Color(N["objects"][i]["color"]["r"].AsFloat, N["objects"][i]["color"]["g"].AsFloat, N["objects"][i]["color"]["b"].AsFloat, N["objects"][i]["color"]["a"].AsFloat);
            }
            else if (N["objects"][i]["name"] == "Circle(Clone)")
            {
                g = Instantiate(circle, new Vector3(N["objects"][i]["position"]["x"].AsFloat, N["objects"][i]["position"]["y"].AsFloat, N["objects"][i]["position"]["z"].AsFloat), Quaternion.Euler(new Vector3(N["objects"][i]["rotation"]["x"].AsFloat, N["objects"][i]["rotation"]["y"].AsFloat, N["objects"][i]["rotation"]["z"].AsFloat))) as GameObject;
                g.name = N["objects"][i]["name"];
                g.GetComponent<SpriteRenderer>().color = new Color(N["objects"][i]["color"]["r"].AsFloat, N["objects"][i]["color"]["g"].AsFloat, N["objects"][i]["color"]["b"].AsFloat, N["objects"][i]["color"]["a"].AsFloat);
            }
            else if (N["objects"][i]["name"] == "Triangle(Clone)")
            {
                g = Instantiate(triangle, new Vector3(N["objects"][i]["position"]["x"].AsFloat, N["objects"][i]["position"]["y"].AsFloat, N["objects"][i]["position"]["z"].AsFloat), Quaternion.Euler(new Vector3(N["objects"][i]["rotation"]["x"].AsFloat, N["objects"][i]["rotation"]["y"].AsFloat, N["objects"][i]["rotation"]["z"].AsFloat))) as GameObject;
                g.name = N["objects"][i]["name"];
                g.GetComponent<SpriteRenderer>().color = new Color(N["objects"][i]["color"]["r"].AsFloat, N["objects"][i]["color"]["g"].AsFloat, N["objects"][i]["color"]["b"].AsFloat, N["objects"][i]["color"]["a"].AsFloat);
            }
            else if (N["objects"][i]["name"] == "Line(Clone)")
            {
                g = Instantiate(line, new Vector3(N["objects"][i]["position"]["x"].AsFloat, N["objects"][i]["position"]["y"].AsFloat, N["objects"][i]["position"]["z"].AsFloat), Quaternion.Euler(new Vector3(N["objects"][i]["rotation"]["x"].AsFloat, N["objects"][i]["rotation"]["y"].AsFloat, N["objects"][i]["rotation"]["z"].AsFloat))) as GameObject;
                g.name = N["objects"][i]["name"];
                g.GetComponent<LineRenderer>().material.color = new Color(N["objects"][i]["color"]["r"].AsFloat, N["objects"][i]["color"]["g"].AsFloat, N["objects"][i]["color"]["b"].AsFloat, N["objects"][i]["color"]["a"].AsFloat);
                List<Vector3> LPoses = new List<Vector3>();
                for (int l = 0; l < N["objects"][i]["linePositions"].Count; l++)
                {
                    LPoses.Add(new Vector3(N["objects"][i]["linePositions"][l]["x"].AsFloat, N["objects"][i]["linePositions"][l]["y"].AsFloat, N["objects"][i]["linePositions"][l]["z"].AsFloat));
                }
                g.GetComponent<LineRenderer>().positionCount = LPoses.Count;
                g.GetComponent<LineRenderer>().SetPositions(LPoses.ToArray());
                g.GetComponent<PhysicsObjectScript>().AddCollision(g.GetComponent<LineRenderer>(), g.GetComponent<EdgeCollider2D>());
            }
            g.transform.localScale = new Vector3(N["objects"][i]["scale"]["x"].AsFloat, N["objects"][i]["scale"]["y"].AsFloat, N["objects"][i]["scale"]["z"].AsFloat);
            
            PhysicsObjectScript obj = g.GetComponent<PhysicsObjectScript>();
            obj.ID = N["objects"][i]["ID"].AsInt;
            nextIDNum = N["objects"][i]["ID"].AsInt;
            nextIDNum++;
            obj.charge = N["objects"][i]["charge"];
            if (N["objects"][i]["useGravity"].AsBool == true)
            {
                g.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
            else
            {
                g.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            obj.isAnchored = N["objects"][i]["isAnchored"].AsBool;
            obj.initalVelocity = new Vector3(N["objects"][i]["initialVelocity"]["x"].AsFloat, N["objects"][i]["initialVelocity"]["y"].AsFloat, N["objects"][i]["initialVelocity"]["z"].AsFloat);
            if(N["objects"][i]["usePointJoint"].AsBool == true)
            {
                g.AddComponent<HingeJoint2D>();
                g.GetComponent<HingeJoint2D>().anchor = new Vector2(N["objects"][i]["pointJointPosition"]["x"].AsFloat, N["objects"][i]["pointJointPosition"]["y"].AsFloat);
                obj.InstanciatePointJointicon(false);
            }
            g.GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    static ObjectInfo ConvertToClass(PhysicsObjectScript obj)
    {
        ObjectInfo Class = new ObjectInfo();
        Class.name = obj.name;
        Class.ID = obj.ID;
        Class.position = obj.transform.position;
        Class.rotation = obj.transform.eulerAngles;
        Class.scale = obj.transform.localScale;
        if (obj.name == "Line(Clone)")
        {
            Class.color = obj.GetComponent<LineRenderer>().material.color;
        }
        else
        {
            Class.color = obj.GetComponent<SpriteRenderer>().color;
        }
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
            Class.usePointJoint = true;
            Class.pointJointPosition = obj.GetComponent<HingeJoint2D>().anchor;
        }
        else
        {
            Class.usePointJoint = false;
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

