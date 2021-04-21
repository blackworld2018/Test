using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_3_manager : Singleton<Test_3_manager>
{
    private Dictionary<string, GameObject> GameObjectAllDictionary { set; get; }

    private Dictionary<string, GameObject> GameObjectInCarmeraDictionary { set; get; }

    public Test_3_manager()
    {
        GameObjectAllDictionary = new Dictionary<string, GameObject>();
        GameObjectInCarmeraDictionary = new Dictionary<string, GameObject>();
    }

    public void SetGameObjectAll(GameObject gameObject)
    {
        if (GameObjectAllDictionary.TryGetValue(gameObject.name, out _))
        {

        }
        else
        {
            GameObjectAllDictionary.Add(gameObject.name, gameObject);
        }
    }

    public GameObject GetGameObjectAll(string name)
    {
        GameObject gameObject;
        GameObjectAllDictionary.TryGetValue(name, out gameObject);
        return gameObject;
    }

    // set in camera of gameobject
    public void SetInCameraGameObject(GameObject gameObject)
    {
        if (GameObjectInCarmeraDictionary.TryGetValue(gameObject.name, out _))
        {

        }
        else
        {
            Debug.Log("In gameobject of gamera name = " + gameObject.name);
            GameObjectInCarmeraDictionary.Add(gameObject.name, gameObject);
        }
    }

    public GameObject GetGameObjectInCarmera(string name)
    {
        GameObject gameObject;
        GameObjectInCarmeraDictionary.TryGetValue(name, out gameObject);
        return gameObject;
    }

    public void DeleteGameObjectInCarmera(GameObject gameObject)
    {
        Debug.Log("delete gameobject of gamera name = " + gameObject.name);
        GameObjectInCarmeraDictionary.Remove(gameObject.name);
    }
}
