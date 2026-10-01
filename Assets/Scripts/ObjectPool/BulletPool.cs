using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private List<GameObject> bulletList = new List<GameObject>();

    private static BulletPool instance;
    public static BulletPool Instance {  get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        AddBulletsToPool(poolSize);
    }

    private void AddBulletsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(BulletPrefab);
            bullet.SetActive(false);
            bulletList.Add(bullet);
            bullet.transform.parent = transform;
        }
    }

    public GameObject RequestBullet()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (bulletList[i] == null)
                continue;

            if (!bulletList[i].activeSelf)
            {
                bulletList[i].SetActive(true);
                return bulletList[i];
            }
        }
        // Si el pool está lleno, expande la lista
        AddBulletsToPool(1);
        GameObject bullet = bulletList[bulletList.Count - 1];
        bullet.SetActive(true);
        return bullet;
    }

}
