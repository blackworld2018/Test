using UnityEngine;

public class Test_3_one_gameobject : MonoBehaviour
{

    void OnBecameVisible()
    {
        Test_3_manager.Instance.SetInCameraGameObject(gameObject);
    }

    void OnBecameInvisible()
    {
        Test_3_manager.Instance.DeleteGameObjectInCarmera(gameObject);
    }
}
