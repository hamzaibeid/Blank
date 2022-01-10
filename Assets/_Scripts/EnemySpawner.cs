using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : TileMapVisualizer
{
    [SerializeField]
    private float spawnRaduis = 7, time = 1.5f;
    private  Vector2 spawnPosition = new Vector2();
    
    public GameObject [] Enemies;
    private Vector2Int locateFloor;

    public static void LoacateEnemies(HashSet<Vector2Int> floorPositons,TileMapVisualizer tileMapVisualizer){
        var locateFloor  = FindFloor(floorPositons);    }

    private static object FindFloor(HashSet<Vector2Int> floorPositons)
    {
        HashSet<Vector2Int> enemyposinos = new HashSet<Vector2Int>();
        foreach (var positon in floorPositons)
        {
            enemyposinos.Add(positon);
        }
        return enemyposinos;
    }

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
             Vector2Int enemyS = Vector2Int.RoundToInt(spawnPosition);
        
        
           
                StartCoroutine(SpawnAnEnemy());

            
       
        
    }
}
