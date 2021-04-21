using UnityEngine;
using UnityEngine.UI;

public class Test_1 : MonoBehaviour
{
    public Vector3 Begin;

    public Vector3 End;

    public float Time;

    private float Speed;

    private bool Isfirst;

    private bool IsChangePostion;

    private string type;


    void Start()
    {
        GameObject.Find("Button_in").GetComponent<Button>().onClick.AddListener(() => { Button_in(); });
        GameObject.Find("Button_out").GetComponent<Button>().onClick.AddListener(() => { Button_out(); });
        GameObject.Find("Button_pingpang").GetComponent<Button>().onClick.AddListener(() => { Button_pingpang(); });
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
            gameObject.transform.position = begin;
        }
        if (pingpang)
        {
            // change begin and end positon
            if (gameObject.transform.position.Equals(end) && IsChangePostion == false)
            {
                IsChangePostion = true;
            }
            if (gameObject.transform.position.Equals(begin) && IsChangePostion == true)
            {
                IsChangePostion = false;
            }
            if (IsChangePostion)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, begin, Speed * UnityEngine.Time.deltaTime);
            }
            else
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, end, Speed * UnityEngine.Time.deltaTime);
            }
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, end, Speed * UnityEngine.Time.deltaTime);
        }
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
}
