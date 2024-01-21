using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

public class NoteGenerator : MonoBehaviour
{
    // public float tempo;
    // int meter;
    // public int beat;
    public GameObject periodLine;
    public Text textBPM;

    IEnumerator songStart;
    // Start is called before the first frame update
    // void Awake()
    // {
    //     // periodLineTime = 60f / tempo * meter;
    //     // periodLineTime = 60f / tempo * meter;

    //     //songStart = WaitTime(periodLineTime);
    // }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        if(GameManager.Instance.GetIsEnterGameScene())
        {
            StartCoroutine(BPMLine());
            GameManager.Instance.SetIsEnterGameScene(false);
        }
    }
    
    public IEnumerator BPMLine()
    {
        Dictionary<double, BPM> BPMs = new Dictionary<double, BPM>(); 
        float maxBeat = 0f;

        Root root = JsonReader.Instance.GetTheChart("test");
        foreach(Object temp in root.objects)
        {
            if(temp.type == "bpm")
            {
                BPMs.Add(temp.beat, new BPM(temp.beat, temp.bpm, temp.time, temp.meter, -1f));
            }
            if(temp.beat >= maxBeat)
            {
                maxBeat = temp.beat;
            }
        }
        
        int currentIndex = 0;
        var tempList = BPMs.OrderBy(i => i.Key).ToList();

        foreach(KeyValuePair<double, BPM> bpmTemp in BPMs.OrderBy(i => i.Key))
        {
            currentIndex++;
            if(tempList.Count - 1 >= currentIndex)
            {
                float temp = tempList[currentIndex].Value.Beat;
                bpmTemp.Value.NextBeat = temp;
            }
            else
                bpmTemp.Value.NextBeat = maxBeat;

            //print(bpmTemp.Value.Beat + ", " + bpmTemp.Value.NextBeat);
        }

        foreach(KeyValuePair<double, BPM> bpmTemp in BPMs.OrderBy(i => i.Key))
        {
            float beat = 4f / bpmTemp.Value.Meter * bpmTemp.Value.Time;
            float time = 60f / bpmTemp.Value.Tempo * beat;

            float currentBeat = bpmTemp.Value.Beat;
            float loopMaxBeat = bpmTemp.Value.NextBeat;

            textBPM.text = "";
            textBPM.text += "BPM: " + bpmTemp.Value.Tempo + ", " + bpmTemp.Value.Time + "/" + bpmTemp.Value.Meter;
            
            while(currentBeat < loopMaxBeat)
            {
                if(currentBeat + beat > loopMaxBeat)
                {
                    beat = loopMaxBeat - currentBeat;
                    time = 60f / bpmTemp.Value.Tempo * beat;
                }

                GameObject tempNote = Instantiate(periodLine, transform);
                //print(currentBeat + "/" + bigistBeat);
                currentBeat += beat;
                yield return new WaitForSeconds(time);
            }
            //print(bpmTemp.Key + ", " + bpmTemp.Value.Tempo + ", " + bpmTemp.Value.Time + "/" + bpmTemp.Value.Meter);
        }

        //GameObject tempNote = Instantiate(periodLine, transform);
    }
}
