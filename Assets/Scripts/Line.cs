using UnityEngine;
using RhythmGame.Interfaces;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Line : MonoBehaviour , ILine
{
    SpriteRenderer spriteRenderer;
    Color lineColor;
    InputDebugger inputDebugger;
    public Text textTouchCount;
    
    Dictionary<int, TouchPhase> touchStatusDict = new Dictionary<int, TouchPhase>();
    Dictionary<int, bool> isTouchDict = new Dictionary<int, bool>();


    void Awake()
    {
        inputDebugger = transform.parent.parent.GetComponent<InputDebugger>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        lineColor = spriteRenderer.color;
            
        spriteRenderer.color = new Color(lineColor.r, lineColor.g, lineColor.b, 0.2f);
    }

    void Start()
    {
        inputDebugger.RegisterLine(this);
    }

    void LateUpdate()
    {
        // if(Input.touchCount > 0)
        //     foreach(Touch touch in Input.touches)
        //     {
        //         touchStatusDict[touch.fingerId] = touch.phase;

        //         if(touch.phase == TouchPhase.Ended)// || !isTouchDict.ContainsKey(touch.fingerId))
        //         {
        //             isTouchDict[touch.fingerId] = false;
        //         }
        //         else if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
        //         {
        //             Ray ray = Camera.main.ScreenPointToRay(touch.position);
        //             RaycastHit hit;
        //             if(Physics.Raycast(ray, out hit, LayerMask.GetMask("Line")))
        //             {
        //                 float leftEdge = transform.position.x - (16 / 6f / 2);
        //                 float rightEdge = transform.position.x + (16 / 6f / 2);

        //                 if(hit.point.x >= leftEdge && hit.point.x <= rightEdge)
        //                 {
        //                     isTouchDict[touch.fingerId] = true;
        //                 }
        //                 else if(hit.point.x <= leftEdge || hit.point.x >= rightEdge)
        //                 {
        //                     isTouchDict[touch.fingerId] = false;
        //                 }
        //             }
        //         }
        //     }
        // else
        //     for(int i = 0; i < isTouchDict.Count; i++)
        //         isTouchDict[i] = false;
        
        LineStatus();
    }

    public void OnEnterLine()
    {
        spriteRenderer.color = new Color(lineColor.r, lineColor.g, lineColor.b, 0.5f);
        // stillOnIt = true;
    }

    public void OnExitLine()
    {
        spriteRenderer.color = new Color(lineColor.r, lineColor.g, lineColor.b, 0.2f);
        // stillOnIt = false;
        //lastTouchId = -1;
    }

    public void LineStatus()
    {
        for(int i = 0; i < isTouchDict.Count; i++)
        {
            if(isTouchDict[i] == true)
            {
                OnEnterLine();
                return;
            }
            OnExitLine();
        }
    }

    public void SetIsTouchDict(int Id, bool state)
    {
        isTouchDict[Id] = state;
    }

    public void SetTouchStatusDict(int Id, TouchPhase phase)
    {
        touchStatusDict[Id] = phase;
    }
}

