using System.Collections;
using RhythmGame.Interfaces;
using UnityEngine;

namespace RhythmGame.Notes
{
    public class NormalNote : MonoBehaviour, INote
    {
        GameObject noteDeletor;
        IEnumerator enumerator;
        bool isEnter;
        void Awake()
        {
            noteDeletor = GameObject.Find("NoteDeletor");
            isEnter = true;
            // enumerator = GenerateNote();
            // StartCoroutine(enumerator);
        }

        void FixedUpdate()
        {
            if(isEnter)
            {
                enumerator = GenerateNote();
                StartCoroutine(enumerator);
                isEnter = false; 
            }
        }

        public IEnumerator GenerateNote()
        {
            while (gameObject != null) 
            {
                Vector3 newPosition = Vector3.MoveTowards
                (transform.position, noteDeletor.transform.position
                , Time.fixedDeltaTime * GameManager.Instance.speed * 5);
                
                transform.position = newPosition;

                if(transform.position.y <= noteDeletor.transform.position.y + 0.001f)
                {
                    //Destroy(gameObject);
                }
                yield return null;
            }
        }
            
        void OnDestory()
        {
            StopCoroutine(enumerator);
        }
    }
}