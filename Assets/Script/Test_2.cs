using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Test_2 : MonoBehaviour
{
    private Date result;

    private GameObject Canvas;

    // use thread-safe queue
    private ConcurrentQueue<Date> concurrentQueue = new ConcurrentQueue<Date>();

    void Start()
    {
        Canvas = GameObject.Find("Canvas").gameObject;
        GameObject.Find("Button_download").GetComponent<Button>().onClick.AddListener(() => { Button_download(); });
    }

    void Update()
    {
        if (concurrentQueue.TryDequeue(out result))
        {
            Texture2D texture2D = new Texture2D(256, 256);
            texture2D.LoadImage(result.Buffer);
            RectTransform rectTransform = result.gameObject.GetComponent<RectTransform>();
            rectTransform.SetParent(Canvas.transform);
            rectTransform.localPosition = new Vector3(Random.Range(-200.0f, 200.0f), Random.Range(-200.0f, 200.0f), 0);
            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.sizeDelta = new Vector2(50, 50);
            result.gameObject.GetComponent<RawImage>().texture = texture2D;
        }
    }

    public void DownloadTexture(Date date)
    {
        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fhbimg.b0.upaiyun.com%2F3fe119e3f7d386b01543df0f73e18706b0f1630a2d35-klIaPd_fw658&refer=http%3A%2F%2Fhbimg.b0.upaiyun.com&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=jpeg?sec=1621552629&t=4e9000bbf93c57fbbb9e7ad3fd0c08db");
        HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        byte[] buffer = new byte[httpWebResponse.ContentLength];
        Stream stream = httpWebResponse.GetResponseStream();
        stream.Read(buffer, 0, buffer.Length);
        date.Buffer = buffer;
        concurrentQueue.Enqueue(date);
    }

    public void Button_download()
    {
        Date date = new Date();
        // creat empty gameobect of RawImage
        GameObject RawImage = new GameObject("RawImage");
        RawImage.AddComponent<RawImage>();
        date.gameObject = RawImage;
        // new Thread
        Thread thread = new Thread(() => { DownloadTexture(date); });
        thread.Start();
    }

    public class Date
    {
        public GameObject gameObject { set; get; }

        public byte[] Buffer { set; get; }
    }

}
