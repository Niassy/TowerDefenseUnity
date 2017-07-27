using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTest : Projectile {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        // get potential target
        TargetingSystem targetSys = Owner.GetComponent<TargetingSystem>();

        if ( targetSys.Target != null /*&& target.IsActive*/) //If the target isn't null and the target isn't dead
        {

            float speed = Owner.transform.GetChild(1).GetComponent<AttackSystem>().ProjectileSpeed;
            Tower target = targetSys.Target;
            //Move towards the position
            transform.position = Vector3.MoveTowards(transform.position,  target.transform.position, Time.deltaTime * speed/*tower.ProjectileSpeed*/);

            //Calculates the direction of the projectile
            Vector2 dir = target.transform.position - transform.position;

            //Calculates the angle of the projectile
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            //Sets the rotation based on the angle
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (transform.position == target.transform.position)
            {
                //GameManager.Instance.Pool.ReleaseObject(gameObject);
                myAnimator.SetTrigger("Impact");
            }
        }
        /*else if (!target.IsActive) //If the target is inactive then we don't need the projectile anymore
        {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }*/
    }

    // NIASSY WORK
    // collision with a gameObject
    // to allow subclasses ti redefine it
    // This method will be invoqued withing the
    // OnTrigger2D event
    public override void Collide(Collider2D other)
    {
        // get potential target
        TargetingSystem targetSys = Owner.GetComponent<TargetingSystem>();

        /*if (other.gameObject == targetSys.Target.gameObject)
        {
            Debug.Log("Colliding target");
            myAnimator.SetTrigger("Impact");
            

        }*/

        //if (other.tag == "Monster")
        //{
        //    Debug.Log(" ProjectileTest : Colliding Monster");
        //  //  myAnimator.SetTrigger("Impact");

        //}

        //If we hit a monster
        /*if (other.tag == "Monster")
        {
            //Creates a reference to the monster script
            Monster target = other.GetComponent<Monster>();

            //Makes the monster take damage based on the tower stats
            target.TakeDamage(tower.Damage, tower.ElementType);

            //Triggers the impact animation
            myAnimator.SetTrigger("Impact");

            //Tries to apply a debuff
            ApplyDebuff();
        }*/

    }
}
