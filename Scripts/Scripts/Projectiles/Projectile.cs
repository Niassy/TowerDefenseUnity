using UnityEngine;

/// <summary>
/// The projectile script
/// </summary>
public class Projectile : MonoBehaviour
{
    /// <summary>
    /// The projectile's target
    /// </summary>
    /// 

    // NIASSY WORK
    // modified to protected
    private Monster target;

    /// <summary>
    /// The projectile's tower
    /// </summary>
    /// 
    // niassy WORK
    private Tower tower;

    /// <summary>
    /// The projectile's animator
    /// </summary>
    protected Animator myAnimator;

    /// <summary>
    /// The element type of the projectile
    /// </summary>
    private Element element;


    // NIASSY WORK
    // Owner of this projectile
    private Unit owner;

    // NIASSY WORK
    // property for accessing projectile
    // owner
    public Unit Owner
    {
        get { return owner; }
    }

    /// <summary>
    /// The projectile's awake function
    /// </summary>
    void Awake()
    {
        //Creates a reference to the animator
        this.myAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// Initializes the projectile
    /// </summary>
    /// <param name="tower"></param>
    public void Initialize(Tower tower)
    {
        //Sets the values
        this.target = tower.Target;
        this.tower = tower;
        this.element = tower.ElementType;
    }

    public void Initialize(Unit unit)
    {
        //Debug.Log("Initialization okay");
        owner = unit;   
    }


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected virtual void Update()
    {
        if (target != null && target.IsActive) //If the target isn't null and the target isn't dead
        {
            //Move towards the position
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * tower.ProjectileSpeed);

            //Calculates the direction of the projectile
            Vector2 dir = target.transform.position - transform.position;

            //Calculates the angle of the projectile
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            //Sets the rotation based on the angle
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if(!target.IsActive) //If the target is inactive then we don't need the projectile anymore
        {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }

    /// <summary>
    /// When the projectile hits something
    /// </summary>
    /// <param name="other">The object the projectil hit</param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        Collide(other);
    }

    /// <summary>
    /// Tries to apply a debuff to the target
    /// </summary>
    private void ApplyDebuff()
    {
        //Checks if the target is immune to the debuff
        if (target.ElementType != element)
        {
            //Does a roll to check if we have to apply a debuff
            float roll = UnityEngine.Random.Range(0, 100);

            if (roll <= tower.Proc)
            {   
                //applies the debuff
                target.AddDebuff(tower.GetDebuff());
            }
        }

    }

    // NIASSY WORK
    // collision with a gameObject
    // to allow subclasses ti redefine it
    // This method will be invoqued withing the
    // OnTrigger2D event
    public virtual void Collide(Collider2D other)
    {
        //If we hit a monster
        if (other.tag == "Monster")
        {
            //Creates a reference to the monster script
            Monster target = other.GetComponent<Monster>();

            //Makes the monster take damage based on the tower stats
            target.TakeDamage(tower.Damage, tower.ElementType);

            //Triggers the impact animation
            myAnimator.SetTrigger("Impact");

            //Tries to apply a debuff
            ApplyDebuff();
        }

    }
}
