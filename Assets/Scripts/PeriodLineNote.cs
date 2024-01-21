using System.Collections;
using UnityEngine;

namespace RhythmGame.Notes
{
    public class PeriodLineNote : MonoBehaviour
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
                    , Time.deltaTime * GameManager.Instance.speed * 20);
                    
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
}