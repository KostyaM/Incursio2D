using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DistanceCheckListener
{
    void onMinDistance();
    void onMaxDistance();

    void onInsaneDistance();
}
