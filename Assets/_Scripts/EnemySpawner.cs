using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnRaduis = 7, time = 1.5f;
    public GameObject [] Enemies;

    void Start()
    {
        StartCoroutine(SpawnAnEnemy());
    }

    // Update is called once per frame
    IEnumerator SpawnAnEnemy()
    {
        Vector2 spawnPosition = GameObject.Find("player_0").transform.position;
        spawnPosition += Random.insideUnitCircle.normalized * spawnRaduis;
        Instantiate(Enemies[Random.Range(0, Enemies.Length)], spawnPosition,Quaternion.identity);
        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnAnEnemy());
        
    }
}
