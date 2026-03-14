using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance { get; private set; }

    // Por cada prefab, una lista de objetos disponibles
    private Dictionary<GameObject, List<GameObject>> pool = new Dictionary<GameObject, List<GameObject>>();

    public static PoolManager GetInstance() => instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Precrea X instancias de un prefab y las mete en la pool
    public void SetPool(GameObject prefab, int minValues)
    {
        if (!pool.ContainsKey(prefab))
            pool[prefab] = new List<GameObject>();

        for (int i = 0; i < minValues; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool[prefab].Add(obj);
        }
    }

    // Saca un objeto de la pool (o crea uno nuevo si no hay disponibles)
    public GameObject Get(GameObject prefab)
    {
        if (!pool.ContainsKey(prefab))
            pool[prefab] = new List<GameObject>();

        // Buscar uno inactivo
        foreach (GameObject obj in pool[prefab])
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // Si no hay ninguno libre, crear uno nuevo
        GameObject newObj = Instantiate(prefab, transform);
        pool[prefab].Add(newObj);
        return newObj;
    }

    // Devuelve un objeto a la pool (en vez de Destroy)
    public void Return(GameObject obj)
    {
        obj.SetActive(false);
    }
}