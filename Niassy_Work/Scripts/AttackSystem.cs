using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour {

    [SerializeField]
    private Projectile projectilePrefab;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float proc;

    // niassy work
    // modified to protected
    [SerializeField]
    protected float attackCooldown;

    [SerializeField]
    private float debuffDuration;

    public TowerUpgrade[] Upgrades { get; protected set; }

    // niassy work
    // modified to protected
    protected float attackTimer;

    // niassy work
    // modofied to protected
    protected bool canAttack = true;

    public int Level { get; protected set; }

    // NIASSY WORK
    // MODOFIED TO protected
    

    // NIASSY WORK
    // reference to tile script
    private TileScript tile;

    public TileScript Tile
    {
        get { return tile; }

        set { tile = value; }
    }

    public int Damage
    {
        get
        {
            return damage;
        }
    }
    
    
    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }
    }

    // prope

    
    private void Awake()
    {
    }


    // NIASSY WORK
    // modefied access to protected
    protected virtual void Update()
    {
        
        Attack();
    }
    
    protected virtual void Attack()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }


            //Debug.Log("I can not attack");
        }

        if (canAttack)
        {
            //Debug.Log("I can attack");

        }

        // get potential target
        TargetingSystem targetSys =  transform.parent.GetComponent<TargetingSystem>();

        if ( targetSys.Target != null)
        {

            if (canAttack && targetSys.IsTargetShootable())
            {

                //Debug.Log("target shootable");
                Shoot();

               // myAnimator.SetTrigger("Attack");

                canAttack = false;
            }

            //Debug.Log("Get a target NOT SHOOTABLE");

        }
        
    }

    /// <summary>
    /// Make the tower shoot
    /// </summary>
    public void Shoot()
    {

        //Debug.Log("sssss");
        //Creates the projectile
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectilePrefab.name).GetComponent<Projectile>();

        //Sets the projectiles position
        projectile.transform.position = transform.position;

        //Initializes the projectile
        //projectile.Initialize(this);

        Unit unit = transform.parent.GetComponent<Unit>();

        projectile.Initialize(unit);
    }


    // Use this for initialization
    void Start () {
		
	}
	
}
