using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab; //
    public Transform spawnLocation; //

    public void SpawnBoss()
    {
        Instantiate(bossPrefab, spawnLocation.position, spawnLocation.rotation);
    }
}

