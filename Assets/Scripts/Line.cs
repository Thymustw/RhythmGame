using UnityEngine;
using RhythmGame.Interfaces;
using UnityEngine.UI;

public class Line : MonoBehaviour , ILine
{
    SpriteRenderer spriteRenderer;
    Color lineColor;
    InputDebugger inputDebugger;
    int lastTouchId;
    bool stillOnIt;

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
        
    public void OnEnterLine()
    {
        spriteRenderer.color = new Color(lineColor.r, lineColor.g, lineColor.b, 0.5f);
    }

    public void OnLeaveLine()
    {
        spriteRenderer.color = new Color(lineColor.r, lineColor.g, lineColor.b, 0.2f);
        lastTouchId = -1;
    }

    public void LineStatus(Vector3 touchPoint, float lineWidth, int inputTouchId, bool inputCountChanged)
    {
        float leftEdge = transform.position.x - (lineWidth / 2);
        float rightEdge = transform.position.x + (lineWidth / 2);

        if (lastTouchId == -1)
            lastTouchId = inputTouchId;

        //TODO:Write more make sense.

        if(touchPoint.x >= leftEdge && touchPoint.x <= rightEdge)
        {
            OnEnterLine();
        }

        if (lastTouchId == inputTouchId || inputCountChanged)
        {
            if(touchPoint.x <= leftEdge || touchPoint.x >= rightEdge)
            {
                OnLeaveLine();
            }
        }
    }
}

