using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour,IAbility
{
    int projectileCount;
    public int requrieProjectileCount;
    public bool IsActive;
    public virtual void Activate(GameObject damage)
    {
        throw new System.NotImplementedException();
    }

    public virtual void Deactivate()
    {
        throw new System.NotImplementedException();
    }
    

    public int GetProjectileCount()
    {
        return projectileCount;
    }
    public void AddProjectile()
    {
        projectileCount++;
    }
    public void RestProjectile()
    {
        projectileCount = 0;
    }
    
}
