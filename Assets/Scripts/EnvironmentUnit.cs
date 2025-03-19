using UnityEngine;

public class EnvironmentUnit : MonoBehaviour
{ 

    private void Update()
    {
        transform.position += Vector3.back * GameManager.Instance.environmentSpeed * Time.deltaTime;
    }
}
