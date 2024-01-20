using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class NoteGenerator : MonoBehaviour
{
    public float tempo;
    public int meter;
    public int beat;
    public GameObject periodLine;
    float periodLineTime;

    IEnumerator songStart;
    // Start is called before the first frame update
    void Awake()
    {
        periodLineTime = 60f / tempo * meter;

        songStart = WaitTime(periodLineTime);
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        if(GameManager.Instance.GetIsEnterGameScene())
        {
            StartCoroutine(songStart);
            GameManager.Instance.SetIsEnterGameScene(false);
        }
    }
    
    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(3);

        for(int i = 0; i < 11; i++)
        {
            GameObject tempNote = Instantiate(periodLine, transform);
            //StartCoroutine(GenerateNote(tempNote));
            yield return new WaitForSeconds(time);
        }
    }
}
