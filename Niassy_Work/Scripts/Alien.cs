using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : Monster {

	// Use this for initialization
	void Start () {
		
	}

    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
	}


    // ONLY FOR TEST
    public override void Spawn(int health)
    {
        //Resets the health
        //this.monsterHealth.MaxVal = health;
        //this.monsterHealth.CurrentValue = health;

        //removes all debuffs
        debuffs.Clear();

        //Sets the position
        //transform.position = LevelManager.Instance.BluePortal.transform.position;

        transform.position = LevelManager.Instance.RandomGameObject.transform.position;


        //Starts to scale the monsters
        StartCoroutine(Scale(new Vector3(0.1f, 0.1f), new Vector3(1, 1), false));

        //Sets the monsters path

        // FOR TESTING
        //if (LevelManager.Instance.MonsterPath != null)
          //  SetPath(LevelManager.Instance.MonsterPath, false);
    }


    // override move method from Unit Class
    // ONLY FOR TEST
    // NIASSY WORK
    public override void Move()
    {
        base.Move();
        //IsActive = true;
    }

    // new Niassy work
    protected override void InitialiseHealth()
    {
        //Initializes the monsters health bar
        //monsterHealth.Initialize();
    }


    // ONLY FOR TEST
    // Niassy work
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        //If we hit the red portal, then we need to despawn
        /*if (other.name == "RedPortal")
        {
            //Animates the portal
            other.GetComponent<Portal>().Animate();

            //Scales the monster down
            StartCoroutine(Scale(new Vector3(1, 1), new Vector3(0.1f, 0.1f), true));

            //Reduces the amount of player lives
            GameManager.Instance.Lives--;
        }*/
    }

    // NIASSY WORK
    // ONLY FOR TEST
    // called when colliding with red portals
    // We do not want alien enter on red portals
    // so this method does nothing special
    public override void CollideRedPortal(Collider2D other)
    {
    }


    // NIASSY WORK
    // ONLY FOR TEST
    public override void TakeDamage(float damage, Element dmgSource)
    {
    }

}
