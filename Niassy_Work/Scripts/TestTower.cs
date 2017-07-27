using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTower : Tower {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update () {
        Attack();
	}

    /// <summary>
    /// Gets a debuff
    /// </summary>
    /// <returns>A storm debuff</returns>
    public override Debuff GetDebuff()
    {
        return new StormDebuff(Target, DebuffDuration);
    }

    /// <summary>
    /// Returns the towers current stats and upgraded stats
    /// </summary>
    /// <returns>Tool tip</returns>
    public override string GetStats()
    {
        return "TestTower";
        //return String.Format("<color=#add8e6ff>{0}</color>{1}", "<size=20><b>Storm</b></size>", base.GetStats());
    }


    // NIASSY WORK
    // ONLY FOR TEST
    protected override void Attack()
    {
        //Debug.Log("Attacking");
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
            //Debug.Log("can not attack");
        }
        if (Target != null && Target.IsActive)
        {
            if (canAttack)
            {
                Shoot();

                myAnimator.SetTrigger("Attack");

                canAttack = false;
            }

            //Debug.Log("Get a target NOT SHOOTABLE");

        }
        else if (monsters.Count > 0)
        {
            target = monsters.Dequeue();
            //Debug.Log("Get a target");

        }
        if (target != null && !target.Alive)
        {
            //Debug.Log("Target propaably die");
            //target = null;
        }
    }
}
