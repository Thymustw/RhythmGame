using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Runtime.InteropServices;
using System;

class BPM
{
    public BPM(float beat, float tempo, int time, int meter, float nextBeat)
    {
        Beat = beat;
        Tempo = tempo;
        Time = time;
        Meter = meter;
        NextBeat = nextBeat;
    }
    public float Beat;
    public float Tempo;
    public int Time;
    public int Meter;
    public float NextBeat;
}

class Single
{
    public Single(float beat, bool critical, float lane, float size, int timeScaleGroup
        , bool trace)
    {
        Beat = beat;
        Critical = critical;
        Lane = lane;
        Size = size;
        TimeScaleGroup = timeScaleGroup;
        Trace = trace;
    }
    public float Beat;
    public bool Critical;
    public float Lane;
    public float Size;
    public int TimeScaleGroup;
    public bool Trace;
}

public class NoteGenerator : MonoBehaviour
{
    // public float tempo;
    // int meter;
    // public int beat;
    public GameObject periodLine;
    public GameObject single;
    public Text textBPM;
    public Button button;

    List<BPM> BPMs = new List<BPM>();
    List<Single> Singles = new List<Single>();

    void Awake()
    {
        GetBPMLineStatus();
        GetSingleStatus();
    }


    void FixedUpdate()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        if(GameManager.Instance.GetIsEnterGameScene())
        {
            StartCoroutine(BPMLine());
            StartCoroutine(Single());
            GameManager.Instance.SetIsEnterGameScene(false);
        }
    }
    #region "BPM"
    public IEnumerator BPMLine()
    {
        float beat, generateTime, currentBeat, loopMaxBeat;
        // beat = 4f / BPMs[0].Meter * BPMs[0].Time;
        // generateTime = 60f / BPMs[0].Tempo * beat;

        // GameObject tempFirstNote = Instantiate(periodLine, transform);
        // yield return new WaitForSeconds(generateTime);

        foreach(BPM bpmTemp in BPMs)
        {
            // Get the time to generate.
            beat = 4f / bpmTemp.Meter * bpmTemp.Time;
            generateTime = 60f / bpmTemp.Tempo * beat;

            // Get the range to generate.
            currentBeat = bpmTemp.Beat;
            loopMaxBeat = bpmTemp.NextBeat;

            textBPM.text = "";
            textBPM.text += "BPM: " + bpmTemp.Tempo + ", " + bpmTemp.Time + "/" + bpmTemp.Meter;
            
            // Generate periodLineNote.
            while(currentBeat < loopMaxBeat)
            {
                if(currentBeat + beat > loopMaxBeat)
                {
                    beat = loopMaxBeat - currentBeat;
                    generateTime = 60f / bpmTemp.Tempo * beat;
                }

                GameObject tempNote = Instantiate(periodLine, transform);
                //print(currentBeat + "/" + bigistBeat);
                currentBeat += beat;
                yield return new WaitForSeconds(generateTime);
            }
        }

        GameObject tempLastNote = Instantiate(periodLine, transform);

        yield return new WaitForSeconds(2);
        GameManager.Instance.SetIsEnterGameScene(false);
        button.interactable = true;
    }

    void GetBPMLineStatus()
    {
        float maxBeat = 0f;

        // Find type bpm to add in list and sort it.
        Root root = JsonReader.Instance.GetTheChart("test");
        foreach(Object objectTemp in root.objects)
        {
            if(objectTemp.type == "bpm")
            {
                BPMs.Add(new BPM(objectTemp.beat, objectTemp.bpm, objectTemp.time, objectTemp.meter, -1f));
            }

            if(objectTemp.beat >= maxBeat)
            {
                maxBeat = objectTemp.beat;
            }
        }
        BPMs = BPMs.OrderBy(i => i.Beat).ToList();

        // Set each bpms next final generate.
        int currentIndex = 0;
        var tempBPMs = BPMs;

        foreach(BPM bpmTemp in BPMs)
        {
            currentIndex++;
            if(tempBPMs.Count - 1 >= currentIndex)
            {
                float beatTemp = tempBPMs[currentIndex].Beat;
                bpmTemp.NextBeat = beatTemp;
            }
            else
                bpmTemp.NextBeat = maxBeat;
        }
    }
    #endregion

    public IEnumerator Single()
    {
        float lastBeat = 0;

        int indexBPM = 0;
        
        BPM currentBPM = null;
        if(currentBPM == null)
        {
            currentBPM = BPMs[indexBPM];
        }

        foreach(Single singleTemp in Singles)
        {
            float beatBPM = 4f / currentBPM.Meter * currentBPM.Time;
            float generateTime = 60f / currentBPM.Tempo * (singleTemp.Beat - lastBeat);

            lastBeat = singleTemp.Beat;

            if(singleTemp.Beat > currentBPM.Beat)
            {
                indexBPM++;
                if(indexBPM < BPMs.Count - 1)
                {
                    currentBPM = BPMs[indexBPM];
                }                
            }

            yield return new WaitForSeconds(generateTime);
            GameObject tempNote = Instantiate(single, transform);

            // textBPM.text = "";
            // textBPM.text += "BPM: " + bpmTemp.Value.Tempo + ", " + bpmTemp.Value.Time + "/" + bpmTemp.Value.Meter;
        }
    }

    void GetSingleStatus()
    {
        Root root = JsonReader.Instance.GetTheChart("test");
        foreach(Object temp in root.objects)
        {
            if(temp.type == "single")
            {
                Singles.Add(new Single(temp.beat, temp.critical,
                 temp.lane, temp.size, temp.timeScaleGroup, temp.trace));
            }
        }
        
        //int currentIndex = 0;
        Singles = Singles.OrderBy(i => i.Beat).ToList();

        // foreach(Single singleTemp in Singles)
        // {
        //     print(singleTemp.Beat);
        // }
    }
}
