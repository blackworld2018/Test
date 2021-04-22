using UnityEngine;
using UnityEngine.UI;

public class Test_1 : MonoBehaviour
{
    public Vector3 Begin;

    public Vector3 End;

    public float Time;

    private float Speed;

    private float frameTime;

    private float Change_x;

    private float Change_y;

    private float Change_z;

    private bool Isfirst;

    private bool IsChangePostion;

    private string type;

    private bool IsEaseIn;

    private bool IsEaseOut;

    private bool IsEaseInOut;


    void Start()
    {
        Application.targetFrameRate = 30;
        GameObject.Find("Button_in").GetComponent<Button>().onClick.AddListener(() => { Button_in(); });
        GameObject.Find("Button_out").GetComponent<Button>().onClick.AddListener(() => { Button_out(); });
        GameObject.Find("Button_pingpang").GetComponent<Button>().onClick.AddListener(() => { Button_pingpang(); });
        GameObject.Find("Toggle_easeIn").GetComponent<Toggle>().onValueChanged.AddListener((changeValue) => { Toggle_easeIn(changeValue); });
        GameObject.Find("Toggle_easeOut").GetComponent<Toggle>().onValueChanged.AddListener((changeValue) => { Toggle_easeOut(changeValue); });
        GameObject.Find("Toggle_easeInOut").GetComponent<Toggle>().onValueChanged.AddListener((changeValue) => { Toggle_easeInOut(changeValue); });
    }

    void Update()
    {
        switch (type)
        {
            case "in":
                Move(transform.gameObject, Begin, End, Time, false);
                break;
            case "out":
                Move(transform.gameObject, End, Begin, Time, false);
                break;
            case "pingpang":
                Move(transform.gameObject, Begin, End, Time, true);
                break;
        }

    }

    public void Move(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpang)
    {
        // initialize
        if (Isfirst == false)
        {
            Isfirst = true;
            float distance = Vector3.Distance(begin, end);
            Speed = distance / time;
            frameTime = 0;
            Change_x = end.x - begin.x;
            Change_y = end.y - begin.y;
            Change_z = end.z - begin.z;
            gameObject.transform.position = begin;
        }
        if (pingpang)
        {
            // change begin and end positon
            if (isEase() == false)
            {
                if (gameObject.transform.position.Equals(end) && IsChangePostion == false)
                {
                    IsChangePostion = true;
                }
                if (gameObject.transform.position.Equals(begin) && IsChangePostion == true)
                {
                    IsChangePostion = false;
                }
            }
            else
            {
                if (frameTime >= time && IsChangePostion == false)
                {
                    frameTime = 0;
                    Change_x = begin.x - end.x;
                    Change_y = begin.y - end.y;
                    Change_z = begin.z - end.z;
                    IsChangePostion = true;
                }
                if (frameTime >= time && IsChangePostion == true)
                {
                    frameTime = 0;
                    Change_x = end.x - begin.x;
                    Change_y = end.y - begin.y;
                    Change_z = end.z - begin.z;
                    IsChangePostion = false;
                }
            }
            if (IsChangePostion)
            {
                MoveGroup(end, begin, time);
            }
            else
            {
                MoveGroup(begin, end, time);
            }
        }
        else
        {
            MoveGroup(begin, end, time);
        }
    }

    public bool isEase()
    {
        if (IsEaseIn == false && IsEaseOut == false && IsEaseInOut == false)
        {
            return false;
        }
        return true;
    }

    public void MoveGroup(Vector3 begin, Vector3 end, float time)
    {
        if (IsEaseIn)
        {
            EaseInGameObject(begin, end, time);
        }
        else
           if (IsEaseOut)
        {
            EaseOutGameObject(begin, end, time);
        }
        else
           if (IsEaseInOut)
        {
            EaseInOutGameObject(begin, end, time);
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, end, Speed * UnityEngine.Time.deltaTime);
        }
    }

    public void EaseInGameObject(Vector3 begin, Vector3 end, float time)
    {
        float x = EaseIn(frameTime, begin.x, Change_x, time);
        float y = EaseIn(frameTime, begin.y, Change_y, time);
        float z = EaseIn(frameTime, begin.z, Change_z, time);
        gameObject.transform.position = new Vector3(x, y, z);
        frameTime += UnityEngine.Time.deltaTime;
    }

    public void EaseOutGameObject(Vector3 begin, Vector3 end, float time)
    {
        float x = EaseOut(frameTime, begin.x, Change_x, time);
        float y = EaseOut(frameTime, begin.y, Change_y, time);
        float z = EaseOut(frameTime, begin.z, Change_z, time);
        gameObject.transform.position = new Vector3(x, y, z);
        frameTime += UnityEngine.Time.deltaTime;
    }

    public void EaseInOutGameObject(Vector3 begin, Vector3 end, float time)
    {
        float x = EaseInOut(frameTime, begin.x, Change_x, time);
        float y = EaseInOut(frameTime, begin.y, Change_y, time);
        float z = EaseInOut(frameTime, begin.z, Change_z, time);
        gameObject.transform.position = new Vector3(x, y, z);
        frameTime += UnityEngine.Time.deltaTime;
    }

    public float EaseIn(float time, float begin, float change, float keepTime)
    {
        time = time > keepTime ? keepTime : time;
        time /= keepTime;
        return change * time * time + begin;
    }

    public float EaseOut(float time, float begin, float change, float keepTime)
    {
        time = time > keepTime ? keepTime : time;
        time /= keepTime;
        return -change * time * (time - 2) + begin;
    }

    public float EaseInOut(float time, float begin, float change, float keepTime)
    {
        time = time > keepTime ? keepTime : time;
        time /= (keepTime / 2);
        if (time < 1)
        {
            return change / 2 * time * time + begin;
        }
        time--;
        return -change / 2 * (time * (time - 2) - 1) + begin;
    }

    private void Button_in()
    {
        Isfirst = false;
        type = "in";
    }

    private void Button_out()
    {
        Isfirst = false;
        type = "out";
    }

    private void Button_pingpang()
    {
        Isfirst = false;
        IsChangePostion = false;
        type = "pingpang";
    }

    private void Toggle_easeIn(bool changeValue)
    {
        IsEaseIn = changeValue;
    }

    private void Toggle_easeOut(bool changeValue)
    {
        IsEaseOut = changeValue;
    }

    private void Toggle_easeInOut(bool changeValue)
    {
        IsEaseInOut = changeValue;
    }
}
