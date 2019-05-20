using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Transform obstaclePrefab;
    public Vector2 mapSize;

    [Range(0, 1)]
    public float outlinePrecent;

    public int seed = 10;

    private List<Coord> allTileCords;
    private Queue<Coord> shuffledTIleCoords;
    
    void Start()
    {
        GenerateMap();
    }
    
    void Update()
    {
        
    }

    public void GenerateMap()
    {
        allTileCords = new List<Coord>();
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCords.Add(new Coord(x, y));
            }
        }
        shuffledTIleCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCords.ToArray(), seed));

        string holderName = "Generated map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        
        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;
        
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePosition = CordToPosition(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90));
                newTile.localScale = Vector3.one * (1 - outlinePrecent);
                newTile.parent = mapHolder;
            }
        }

        int obstacleCount = 10;
        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            Vector3 obstaclePosition = CordToPosition(randomCoord.x, randomCoord.y);
            Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition + Vector3.up * .5f, Quaternion.identity);
            newObstacle.parent = mapHolder;
        }
    }

    Vector3 CordToPosition(int x, int y)
    {
        return  new Vector3(-mapSize.x/2 + .5f + x, 0f, -mapSize.y/2 + .5f + y);
    }

    public Coord GetRandomCoord()
    {
        Coord randomCord = shuffledTIleCoords.Dequeue();
        shuffledTIleCoords.Enqueue(randomCord);
        return randomCord;
    }
    
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
