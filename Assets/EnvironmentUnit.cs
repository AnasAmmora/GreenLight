using UnityEngine;

public class EnvironmentUnit : MonoBehaviour
{
    public float moveSpeed = 10f; 

    private void Update()
    {
        transform.position += Vector3.back * moveSpeed * Time.deltaTime;
    }
}
