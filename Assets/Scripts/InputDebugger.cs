using System.Collections.Generic;
using RhythmGame.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class InputDebugger : MonoBehaviour, ILinesSubject
{
    List<ILine> lineObserver;
    int lastTouchCount = 0;
    bool isTouchCountChanged;
    // bool setLineDown;

    public Text textTouchCount;

    public InputDebugger()
    {
        lineObserver = new List<ILine>();
    }

    void Awake()
    {
        //Debug.Log(GameManager.Instance.screenScaleWidth);
        // Screen.height / 9 * 16 / 120 == 16:9's width;
        transform.localScale = new Vector3(Screen.height / 9 * 16 / 120, 80, 1);
        Transform LinesObj = transform.GetChild(0);

        float lineWidth = 1f / LinesObj.childCount;
        float currentPos = -0.5f;
        float halfLineWidth = lineWidth / 2;
        foreach(Transform temp in LinesObj)
        {
            if(LinesObj.childCount % 2 == 0)
            {
                temp.localScale = new Vector3(lineWidth, 1, 1);
                temp.localPosition = new Vector3(currentPos + halfLineWidth , 0, 0);

                currentPos += lineWidth;
            }
        }

        // setLineDown = true;
        textTouchCount.text = Screen.width.ToString();
        // Debug.Log(Screen.width);
    }
    
    #region "ILinesSubject
    public void RegisterLine(ILine line)
    {
        lineObserver.Add(line);
    }

    public void RemoveLine(ILine line)
    {
        lineObserver.Remove(line);
    }

    // public Line NotifyListeners(int index)
    // {
    //     Line correctLine = null;
    //     foreach (Line line in lineObserver)
    //         if(line.gameObject.name.EndsWith(index.ToString()))
    //         {
    //             correctLine = line;
    //             break;
    //         }
    //     return correctLine;
    // }

    public void NotifyListeners()
    {
        foreach (Line line in lineObserver)
            line.OnLeaveLine();
    }

    public Line NotifyListeners(Vector3 hitPoint, float width, int touchId, bool touchCountChanged)
    {
        Line correctLine = null;

        foreach (Line line in lineObserver)
            line.LineStatus(hitPoint, width, touchId, touchCountChanged);
        return correctLine;
    }
    #endregion

    
    // Update is called once per frame
    void Update()
    {
        #region "8key"
        // if(Input.GetKeyDown(KeyCode.A))
        //     NotifyListeners(1)?.OnClick();
        // else if (Input.GetKeyUp(KeyCode.A))
        //     NotifyListeners(1)?.OnLeave();

        // if(Input.GetKeyDown(KeyCode.S))
        //     NotifyListeners(2)?.OnClick();
        // else if (Input.GetKeyUp(KeyCode.S))
        //     NotifyListeners(2)?.OnLeave();
            
        // if(Input.GetKeyDown(KeyCode.D))
        //     NotifyListeners(3)?.OnClick();
        // else if (Input.GetKeyUp(KeyCode.D))
        //     NotifyListeners(3)?.OnLeave();
        
        // if(Input.GetKeyDown(KeyCode.F))
        //     NotifyListeners(4)?.OnClick();
        // else if (Input.GetKeyUp(KeyCode.F))
        //     NotifyListeners(4)?.OnLeave();

        // if(Input.GetKeyDown(KeyCode.J))
        //     NotifyListeners(5)?.OnClick();
        // else if (Input.GetKeyUp(KeyCode.J))
        //     NotifyListeners(5)?.OnLeave();

        // if(Input.GetKeyDown(KeyCode.K))
        //     NotifyListeners(6)?.OnClick();
        // else if (Input.GetKeyUp(KeyCode.K))
        //     NotifyListeners(6)?.OnLeave();

        // if(Input.GetKeyDown(KeyCode.L))
        //     NotifyListeners(7)?.OnClick();
        // else if (Input.GetKeyUp(KeyCode.L))
        //     NotifyListeners(7)?.OnLeave();

        // if(Input.GetKeyDown(KeyCode.Semicolon))
        //     NotifyListeners(8)?.OnClick();
        // else if (Input.GetKeyUp(KeyCode.Semicolon))
        //     NotifyListeners(8)?.OnLeave();
        #endregion

        #region "6key"
        // if(Input.GetKeyDown(KeyCode.S))
        //     NotifyListeners(1)?.OnClick();
        // else if (Input.GetKeyUp(KeyCode.S))
        //     NotifyListeners(1)?.OnLeave();
            
        // if(Input.GetKeyDown(KeyCode.D))
        //     NotifyListeners(2)?.OnClick();
        // else if (Input.GetKeyUp(KeyCode.D))
        //     NotifyListeners(2)?.OnLeave();
        
        // if(Input.GetKeyDown(KeyCode.F))
        //     NotifyListeners(3)?.OnClick();
        // else if (Input.GetKeyUp(KeyCode.F))
        //     NotifyListeners(3)?.OnLeave();

        // if(Input.GetKeyDown(KeyCode.J))
        //     NotifyListeners(4)?.OnClick();
        // else if (Input.GetKeyUp(KeyCode.J))
        //     NotifyListeners(4)?.OnLeave();

        // if(Input.GetKeyDown(KeyCode.K))
        //     NotifyListeners(5)?.OnClick();
        // else if (Input.GetKeyUp(KeyCode.K))
        //     NotifyListeners(5)?.OnLeave();

        // if(Input.GetKeyDown(KeyCode.L))
        //     NotifyListeners(6)?.OnClick();
        // else if (Input.GetKeyUp(KeyCode.L))
        //     NotifyListeners(6)?.OnLeave();
        #endregion

        // textTouchCount.text = Input.touchCount.ToString();

        if (Input.touchCount > 0)
        {
            if(lastTouchCount != Input.touchCount)
            {
                lastTouchCount = Input.touchCount;
                isTouchCountChanged = true;
            }
            else
            {
                isTouchCountChanged = false;
            }

            
            foreach(Touch touch in Input.touches)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit))
                {
                    NotifyListeners(hit.point, transform.localScale.x / transform.childCount, touch.fingerId, isTouchCountChanged);
                }
            }
        }
        else
            NotifyListeners();
    }
}
