using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweUpSpwaner1 : MonoBehaviour
{
    public Transform[] powerupSpwanPoints;
    public GameObject[] powerupPrefab;
    public GameObject powerUpGameobject;
    int randomSpawnPoint;
    int randomPowerup;
    DestroyEnemy ed;

    private void Start()
    {
        InvokeRepeating("SpwanPoweUp", 8f, 10f);
        ed = GetComponent<DestroyEnemy>();
    }

    void SpwanPoweUp()
    {
        randomSpawnPoint = Random.Range(0, powerupSpwanPoints.Length);
        randomPowerup = Random.Range(0, powerupPrefab.Length);
        Instantiate(powerupPrefab[randomPowerup], powerupSpwanPoints[randomSpawnPoint].position, Quaternion.identity, powerUpGameobject.transform);
    }
}
