using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    public static PackageSpawner instance;
    public List<GameObject> spawnPoints;

    public GameObject package;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public IEnumerator SpawnPackage()
    {
        int spawnPointCount = spawnPoints.Count;
        GameObject randomPoint = spawnPoints[Random.Range(0, spawnPointCount)];

        Instantiate(package, new Vector3(randomPoint.transform.position.x, randomPoint.transform.position.y, randomPoint.transform.position.z), Quaternion.identity);

        yield return null;
    }
}
