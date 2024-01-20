using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodLine : MonoBehaviour
{
    GameObject noteDeletor;
    IEnumerator enumerator;
    void Awake()
    {
        noteDeletor = GameObject.Find("NoteDeletor");
        enumerator = GenerateNote();
        StartCoroutine(enumerator);
    }

    IEnumerator GenerateNote()
    {
        try
        {
            while (gameObject != null) 
            {
                Vector3 newPosition = Vector3.MoveTowards
                (transform.position, noteDeletor.transform.position
                , Time.deltaTime * GameManager.Instance.speed * 8);
                
                transform.position = newPosition;

                if(transform.position.y <= noteDeletor.transform.position.y + 0.001f)
                {
                    Destroy(gameObject);
                }
                yield return null;
            }
        }
        finally
        {
            Debug.Log("Destory line");
        }
    }
    
    void OnDestory()
    {
        StopCoroutine(enumerator);
    }
}
