using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NIASSY WORK
// Targeting system class

public class TargetingSystem : MonoBehaviour {

    // owner of targeting system
    private Unit owner;

    // target tower
    private Tower target;

    //  Range tile
    public int RangeTile;


    public Unit Owner
    {
        get
        {
            return owner;
        }

        set { owner = value; }
    }

    public Tower Target
    {
        get { return target; }
    }


	// Use this for initialization
	void Start () {
        owner = gameObject.GetComponent<Alien>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        SearchForTarget();
	}

    // This function scan for all target
    // and assign the closest if exist,
    // to the unit owner
    void SearchForTarget()
    {
        if (target != null)
            return;

        //  Get all towers
        List<Tower> towers = GameManager.Instance.Towers;

        foreach (Tower tower in towers)
        {
            if (tower!= null)
            {
                target = tower;

                // generate a path
                
                Point source = LevelManager.Instance.randomPoint ;
                Point dest = tower.Tile.GridPosition;

                // Search for position to shoot
                // There are only for direction allowed for moving
                // for optimisation you can use a loop (do it after)


                Point left = dest + new Point(-RangeTile, 0);
                Point right = dest + new Point(+RangeTile, 0);
                Point up = dest + new Point(0, RangeTile);
                Point down = dest + new Point(0, RangeTile);

                List<Point> destinations = new List<Point>();
                destinations.Add(left);
                destinations.Add(right);
                destinations.Add(up);
                destinations.Add(down);

                Point closestPoint = new Point(10000, 10000);
                //dest = left;
                //foreach (Point current in destinations)
                //{
                //    //if (current == dest)
                //    //  continue;
                //    if (!LevelManager.Instance.CheckPath(current))
                //        continue;

                //    if (Point.Distance(current, source) < Point.Distance(closestPoint, source))
                //    {
                //        // Hold the minimum distance
                //        closestPoint = current;
                //    }
                //}

                // Loop through x axis
                for (int x = -RangeTile; x <= RangeTile; x++)
                {
                    Point xTile = dest + new Point(x, 0);

                    if (!LevelManager.Instance.InBounds(xTile))
                        continue;

                    if (!LevelManager.Instance.CheckPath(xTile))
                        continue;

                    if (Point.Distance(xTile, source) < Point.Distance(closestPoint, source))
                    {
                        // Hold the minimum distance
                        closestPoint = xTile;
                    }
                }

                // Loop through y axis
                for (int y = -RangeTile; y <= RangeTile; y++)
                {
                    Point yTile = dest + new Point(0, y);

                    if (!LevelManager.Instance.InBounds(yTile))
                        continue;

                    if (!LevelManager.Instance.CheckPath(yTile))
                        continue;

                    if (Point.Distance(yTile, source) < Point.Distance(closestPoint, source))
                    {
                        // Hold the minimum distance
                        closestPoint = yTile;
                    }
                }

                /*if (LevelManager.Instance.CheckPath(left))
                    dest = left;

                if (LevelManager.Instance.CheckPath(right) && dest == tower.Tile.GridPosition)
                    dest = right;


                if (LevelManager.Instance.CheckPath(up) && dest == tower.Tile.GridPosition)
                    dest = up;


                if (LevelManager.Instance.CheckPath(down) && dest == tower.Tile.GridPosition)
                    dest = down;
                
                for (int y = 0;y <= RangeTile;y+=RangeTile)
                {
                    for (int x =-RangeTile;x<= RangeTile;x++ )
                    {

                    }
                }*/

                LevelManager.Instance.GeneratePath(source ,closestPoint);

                if (LevelManager.Instance.MonsterPath != null)
                     owner.SetPath(LevelManager.Instance.MonsterPath, true);

                //Debug.Log("Getting a target");
                break;
            }
        }
    }

    // NIASSY WORK
    // Determine if the target is shootable
    public bool IsTargetShootable()
    {
        //return GetComponent<Alien>().IsActive == false;

        if (target == null)
            return false;

        if ( owner.Path != null && owner.Path.Count == 0 && owner.IsActive == false )
            return true;

        return false;
    }
}
