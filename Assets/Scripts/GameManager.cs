using RhythmGame.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public float speed;
    bool isEnterGameScene;
    public Text textTouchCount;
    //public int screenScaleWidth;

    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 120;
        //screenScaleWidth = Screen.height / 9 * 16 / 120;
        //GameObject.Find("Field").SetActive(true);

        DontDestroyOnLoad(this.gameObject);
        isEnterGameScene = true;
    }

    void Update()
    {
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // RaycastHit hit;
        // LayerMask layerMask = LayerMask.GetMask("Default");
        
        // if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
        // {
        //     textTouchCount.text = hit.collider.name;
        // }
        // Touch touch = Input.GetTouch(0);

        // Vector3 screenPosDepth = touch.position;
        // screenPosDepth.z = 10f;
        // Vector3 touchPosition = Camera.main.ScreenToWorldPoint(screenPosDepth);

        // if(touch.phase == TouchPhase.Began)
        // {
            // Ray ray = Camera.main.ScreenPointToRay(touch.position);
            // RaycastHit hit;
            // if(Physics.Raycast(ray, out hit))
            // {
            //     textPos.text = "(" + hit.point.x + "," + hit.point.y + "," + hit.point.z + ")";
            //     hit.collider.GetComponent<ILine>().OnClick();
            // }
        // }

        //TODO:Need to Change.
        // foreach(Touch touch in Input.touches)
        // {
        //     Ray ray = Camera.main.ScreenPointToRay(touch.position);
        //     RaycastHit hit;

        //     if(Physics.Raycast(ray, out hit))
        //     {
        //         if(lastHit.name != hit.collider.name)
        //             lastHit.GetComponent<ILine>().OnLeave();
        //         hit.collider.GetComponent<ILine>().OnClick();
        //         lastHit = hit.collider;
        //     }
        // }
    }

    public bool GetIsEnterGameScene() { return isEnterGameScene; }
    public void SetIsEnterGameScene(bool value) { isEnterGameScene = value; }
}
