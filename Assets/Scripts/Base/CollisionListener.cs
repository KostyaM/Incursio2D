using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CollisionListener
{
    void onCollide();
    void onExitCollide();
}
