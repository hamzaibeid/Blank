using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ProceduralGenerationAlgorithims
{ 
   public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition , int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPosition);
        var previousPosition = startPosition;
        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }
    public static List<Vector2Int> RandomWalkCorridore(Vector2Int startPosition,int corridoreLength)
    {
        List<Vector2Int> corridore =  new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridore.Add(currentPosition);
        for (int i = 0; i < corridoreLength; i++)
        {
            currentPosition += direction;
            corridore.Add(currentPosition);
        }
        return corridore;
    }
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit,int minWidth,int minHeight)
    {
        //FIFO
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomList = new List<BoundsInt>();
        
        //We remove the object from queue
        roomsQueue.Enqueue(spaceToSplit);
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if(Random.value < 0.5f)
                {
                    if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontaly(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth * 2)
                    {
                        SplitVerticaly(minWidth, roomsQueue, room);
                    }
                    else
                    {
                        roomList.Add(room);
                    }
                }
                else
                {
                    
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVerticaly(minWidth, roomsQueue, room);
                    }
                   else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontaly( minHeight, roomsQueue, room);
                    }
                    else
                    {
                        roomList.Add(room);
                    }

                }
            }
        }
        return roomList;
        
    }

    private static void SplitVerticaly(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1,room.size.x);
        BoundsInt room1 = new BoundsInt(room.min,new Vector3Int(xSplit,room.size.y,room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x+xSplit,room.min.y,room.min.z),
                          new Vector3Int(room.size.x-xSplit,room.size.y,room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);

    }

    private static void SplitHorizontaly(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        // (minHeight , room.size.y - minhHeight)
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
                          new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));


        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);

    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1),//up
         new Vector2Int(1,0),//right
          new Vector2Int(0,-1),//down
           new Vector2Int(-1,0),//left

    };
    public static List<Vector2Int> diaganolDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1,1),//up-right
         new Vector2Int(1,-1),//down-right
          new Vector2Int(-1,-1),//down-left
           new Vector2Int(-1,1),//up-left

    };
    public static List<Vector2Int> eightDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0,1),//up
           new Vector2Int(1,1),//up-right
        new Vector2Int(1,0),//right
           new Vector2Int(1,-1),//down-right
        new Vector2Int(0,-1),//down
           new Vector2Int(-1,-1),//down-left
        new Vector2Int(-1,0),//left
           new Vector2Int(-1,1),//up-left

    };
    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0 , cardinalDirectionsList.Count)];
    }
}
