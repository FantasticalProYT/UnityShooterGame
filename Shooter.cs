using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum ShootingMethod 
{
    Single,
    Charged,
    Auto
}
public class Shooter : MonoBehaviour
{
    [SerializeField] private ShootingMethod method;
    [HideInInspector] public bool ableToShoot;
    WeaponAnimations weapon;
    public Transform shootingPoint;
    public bool aiOn = true;
    public GameObject projectile;
    public bool aiAbleToshoot {get; private set;}
    private Enemy enemy;
    [SerializeField] GameObject gun;
    public float timeBtwnShots = 0.2f;
    private Methods methodToShoot;
    public float damageToDeal = 10f;
    [SerializeField] float randomRange1, randomRange2;
    [HideInInspector] public Quaternion degreesToRotate;
    private void Awake()
    {
        if(!aiOn)
        {
            weapon = GetComponent<WeaponAnimations>();
        }
        else if(aiOn)
        {
            enemy = this.GetComponent<Enemy>();
        }
        switch(method)
        {
            case ShootingMethod.Charged:
            gun.AddComponent<Charged>();
            methodToShoot = this.GetComponent<Charged>();
            break;

            case ShootingMethod.Auto:
            gun.AddComponent<Auto>();
            methodToShoot = this.GetComponent<Auto>();
            break;

            case ShootingMethod.Single:
            gun.AddComponent<Single>();
            methodToShoot = this.GetComponent<Single>();
            break;
        }
    }
    private void Update()
    {
        switch(aiOn)
        {
            case true:
            degreesToRotate = transform.rotation;
            aiAbleToshoot = enemy.shouldFollow;
            break;

            case false:
            Vector3 mousePos;
            transform.GetComponentInChildren<Transform>().localScale = transform.localScale;
            mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
            degreesToRotate = Quaternion.Euler(0, 0, Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg);
            break;
        }
    }
    public float GetTimeBetweenShots()
    {
        switch(aiOn)
        {
            case true:
            float time = Random.Range(randomRange1, randomRange2);
            return time;
            case false: 
            return timeBtwnShots;
        }
    }
    void OnFire(InputValue value)
    {
        if(!aiOn)
        {
            ableToShoot = value.isPressed;
            if(!ableToShoot)
            weapon.EndFireAnimation(methodToShoot.animationsToSet);
        }
    }   
}