using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public abstract class Tower : MonoBehaviour
{
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

    protected Animator myAnimator;

    protected Queue<Monster> monsters = new Queue<Monster>();

    protected SpriteRenderer mySpriteRenderer;

    // NIASSY WORK
    // modified to protected
    protected Monster target;

    public Element ElementType { get; protected set; }

    public int Price { get; set; }


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

    public Monster Target
    {
        get
        {
            return target;
        }
    }

    public float Proc
    {
        get
        {
            return proc;
        }
    }

    public float ProjectileSpeed
    {
        get
        {
            return projectileSpeed;
        }
    }

    public TowerUpgrade NextUpgrade
    {
        get
        {
            if (Upgrades.Length > Level-1)
            {
                return Upgrades[Level - 1];
            }
            return null;
        }
    }

    public float DebuffDuration
    {
        get
        {
            return debuffDuration;
        }

        set
        {
            this.debuffDuration = value;
        }
    }

    private void Awake()
    {
        myAnimator = transform.parent.GetComponent<Animator>();

        mySpriteRenderer = transform.GetComponent<SpriteRenderer>();

        Level = 1;
    }


    // NIASSY WORK
    // modefied access to protected
    protected virtual void Update()
    {
        Attack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Monster")
        {
            monsters.Enqueue(other.GetComponent<Monster>());

            Debug.Log("colliding Monster");

        }

        if (other.tag == "TestProjectile")
        {
            Debug.Log("colliding Projectile");
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Monster")
        {
            target = null;
        }
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


            Debug.Log("can not attack");
        }
        if (Target != null && Target.IsActive)
        {
            if (canAttack)
            {
                Shoot();

                myAnimator.SetTrigger("Attack");

                canAttack = false;
            }

            Debug.Log("Get a target NOT SHOOTABLE");

        }
        else if (monsters.Count > 0)
        {
            target = monsters.Dequeue();
            //Debug.Log("Get a target");

        }
        if (target != null && !target.Alive)
        {
            target = null;
        }    
    }

    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
        GameManager.Instance.UpdateTooltip();
    }

    /// <summary>
    /// Make the tower shoot
    /// </summary>
    public void Shoot()
    {
        //Creates the projectile
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectilePrefab.name).GetComponent<Projectile>();

        //Sets the projectiles position
        projectile.transform.position = transform.position;

        //Initializes the projectile
        projectile.Initialize(this);
    }

    /// <summary>
    /// Returns the towers current stats and upgraded stats
    /// </summary>
    /// <returns>Tool tip</returns>
    public virtual string GetStats()
    {
        if (NextUpgrade != null)
        {
            return string.Format("\nLevel: {0} \nDamage: {1} <color=#00ff00ff> +{4}</color>\nProc: {2}% <color=#00ff00ff>+{5}%</color>\nDebuff: {3}sec <color=#00ff00ff>+{6}</color>", Level, damage, proc, DebuffDuration, NextUpgrade.Damage, NextUpgrade.ProcChance, NextUpgrade.DebuffDuration);
        }

        return string.Format("\nLevel: {0} \nDamage: {1}\nProc: {2}% \nDebuff: {3}sec", Level, damage, proc, DebuffDuration);

    }

    public virtual void Upgrade()
    {
        GameManager.Instance.Currency -= NextUpgrade.Price;
        Price += NextUpgrade.Price;
        this.damage += NextUpgrade.Damage;
        this.proc += NextUpgrade.ProcChance;
        this.DebuffDuration += NextUpgrade.DebuffDuration;
        Level++;
        GameManager.Instance.UpdateTooltip();

    }

    public abstract Debuff GetDebuff();

}
