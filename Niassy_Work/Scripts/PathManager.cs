using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : LevelManager {

    // path for a monster
    // can be used to attacked tower for example
    private Stack<Node> monsterPath;

    // only for test
    private Point randomPoint;

    // ONLY FOR TEST
    // a random prefab
    [SerializeField]
    private GameObject randomPrefab;


    // Use this for initialization
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}


    /// <summary>
    /// Spawns the portals
    /// </summary>
    protected override void SpawnPortals()
    {
        //Spawns the red portal
        randomPoint = new Point(6, 6);
        GameObject randomGameObj = (GameObject)Instantiate(randomPrefab, Tiles[randomPoint].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }

    /// <summary>
    /// Generates a path with the AStar algorithm
    /// between source and destination 
    /// </summary>
    public void GeneratePath(Point source,Point destination)
    {
        //Generates a path from start to finish and stores it in fullPath
        //fullPath = AStar.GetPath(blueSpawn, RedSpawn);
    }

    public Stack<Node> MonsterPath
    {
        get
        {
            return null;
        }
    }

}
