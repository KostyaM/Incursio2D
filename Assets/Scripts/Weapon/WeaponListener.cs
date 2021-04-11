using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface WeaponListener
{
    void onBulletChange(int bulletCount);
    void onHeChange(int heCount);
}
