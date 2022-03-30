using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single : Methods
{
    [SerializeField] private bool alreadyShot = false;
    public override void Awake()
    {
        base.Awake();
        animationsToSet = "Single";
    }
    public override void SubtractTime()
    {   
        if(alreadyShot)
        {
            base.SubtractTime();
        }
    }
    public override void Update()
    {
        CheckTime();
        if(actualTime <= 0 && !gun.ableToShoot)
        alreadyShot = false;
    }
    public override void SpawnProjectile()
    {
        base.SpawnProjectile();
        alreadyShot = true;
    }
    public override void OnTimeFineshed()
    {
        if(!alreadyShot && gun.ableToShoot)
        base.OnTimeFineshed();
    }
    private void EndAnimation()
    {
        weapon.EndFireAnimation(animationsToSet);
    }
}
