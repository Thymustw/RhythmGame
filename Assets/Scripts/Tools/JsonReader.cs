using System.IO;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
// public class Change
// {
//     public double beat;
//     public double timeScale;
// }

[System.Serializable]
public class Object
{
    public float beat;
    public float bpm;
    public string type;
    //public List<Change>? changes;
    public bool critical;
    public float lane;
    public float size;
    public int timeScaleGroup;
    public bool trace;
    public int time;
    public int meter;
}

[System.Serializable]
public class Root
{
    public List<Object> objects;
    public double offset;
}


public class JsonReader
{
    private static JsonReader instance;
    public static JsonReader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new JsonReader();
            }
            return instance;
        }
    }

    public void PrintAll()
    {
        string jsonData = File.ReadAllText(Application.dataPath + "/Data/test.json");
        Root root = JsonUtility.FromJson<Root>(jsonData);

        foreach (Object temp in root.objects)
        {
            if(temp.type == "bpm")
            {
                Debug.Log("beat: " + temp.beat + " , bpm = " + temp.bpm);
                Debug.Log("======================================================");
            }
            else if(temp.type == "single")
            {
                Debug.Log("beat: " + temp.beat + " , bpm = " + temp.bpm + " , critical = " + temp.critical
                 + " , lane = " + temp.lane + " , size = " + temp.size + " , timeScaleGroup = " + temp.timeScaleGroup
                 + " , trace = " + temp.trace);
                Debug.Log("======================================================");
            }
        }
    }

    // Get Map.
    public Root GetTheChart(string chartName)
    {
        TextAsset file = Resources.Load("Maps/" + chartName) as TextAsset;

        string jsonData = file.text;
        Root root = JsonUtility.FromJson<Root>(jsonData);

        return root;
    }
}
