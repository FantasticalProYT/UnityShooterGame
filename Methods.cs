using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Methods : MonoBehaviour
{
    #region ClassVariables
    public string animationsToSet {get; set;}
    public float actualTime { get; private set; } = 0;
    public WeaponAnimations weapon {get; private set;}
    public Shooter gun {get; private set;}
    #endregion
    #region Functions
    public virtual void Awake()
    {
        gun = GetComponent<Shooter>();
        weapon = this.GetComponent<WeaponAnimations>();
    }
    public virtual void Update()
    {
        if(gun.ableToShoot || gun.aiAbleToshoot)
        {
            CheckTime();
        }
    }
    public virtual void SpawnProjectile()
    {
        GameObject bullet =  
        Instantiate(gun.projectile, gun.shootingPoint.position,
        gun.degreesToRotate);
        bullet.GetComponent<Bullet>().damageAmount = gun.damageToDeal;

        bool alreadySet = false;
        if(!alreadySet)
        {
            if(weapon != null)
            weapon.StartAnimation(animationsToSet);
            alreadySet = true;
        }
    }
    public bool CheckTime()
    {
        SubtractTime();
        if(actualTime <= 0)
        {
            OnTimeFineshed();
        }
        return actualTime <= 0;
    }
    public virtual void OnTimeFineshed()
    {
        actualTime = gun.GetTimeBetweenShots();
        SpawnProjectile();
    }
    public virtual void SubtractTime()
    {
        actualTime -= Time.deltaTime;
    }
    public void ResetActualTime()
    {
        actualTime = gun.GetTimeBetweenShots();
    }
    #endregion
}
