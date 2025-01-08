using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public  class Tower : MonoBehaviour,IAttackable
{
    public  float Range { get; protected set; }
    public  float Damage { get; protected set; }
    public  float AttackCooldown { get; protected set; }
    public bool canAttack = true;
    public bool hasTarget = false;
    public IDamageable Target;
    public event System.Action OnAttack;
    public virtual void Update()
    {

    }
    public virtual void Attack()
    {
        OnAttack?.Invoke();
    }
    //Ability To Be Implemented


}
