using UnityEngine;

public class Collectable : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PoolManager.GetInstance().Return(gameObject);
            GameManager.GetInstance().OnObjectCollected();
        }
    }
}