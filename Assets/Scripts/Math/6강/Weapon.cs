using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public float normalRate = 1;
    public int p_critical = 30;
    public float criticalRate = 150;
}

public class Dagger : Weapon
{
    public Dagger()
    {
        normalRate = 0.1f;
        p_critical = 40;
        criticalRate = 1.5f;
    }
}
public class LongSword : Weapon
{
    public LongSword()
    {
        normalRate = 0.2f;
        p_critical = 30;
        criticalRate = 2f;
    }
}
public class Ax : Weapon
{
    public Ax()
    {
        normalRate = 0.3f;
        p_critical = 20;
        criticalRate = 3f;
    }
}