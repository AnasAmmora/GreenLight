using UnityEngine;

public class EnemyCarController : MonoBehaviour
{
    private void Update()
    {
        transform.position += Vector3.back * GameManager.Instance.enemyCarsSpeed * Time.deltaTime;
    }
}

