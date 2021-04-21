using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Test_3 : MonoBehaviour
{
    private bool startMoveCarmer;

    private bool onceMove;

    private GameObject Camera;

    private Vector3 targetVector3;

    void Start()
    {
        GameObject logo_small = GameObject.Find("logo_small");
        StartCoroutine(CreatGameObject(logo_small, StartMoveCamera));
    }

    void Update()
    {
        MoveCamera();
    }

    private IEnumerator CreatGameObject(GameObject gameObject, Action action)
    {
        // creat one hundred thousand gameobject,area of -200 to 200
        for (int i = 0; i < 100000; i++)
        {
            GameObject clone = Instantiate(gameObject);
            clone.name = i.ToString();
            clone.transform.position = new Vector3(Random.Range(-200.0f, 200.0f), Random.Range(-200.0f, 200.0f), 0);
            Test_3_manager.Instance.SetGameObjectAll(gameObject);
        }
        yield return new WaitForEndOfFrame();
        action();
    }

    private void StartMoveCamera()
    {
        startMoveCarmer = true;
        Camera = GameObject.Find("Main Camera");
    }

    private void MoveCamera()
    {
        if (startMoveCarmer == false)
        {
            return;
        }
        if (onceMove == false)
        {
            onceMove = true;
            targetVector3 = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
        }
        if (onceMove)
        {
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, targetVector3, Time.deltaTime);
        }
        if (onceMove && Camera.transform.position.Equals(targetVector3))
        {
            onceMove = false;
        }
    }

}
