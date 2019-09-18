using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAxis
{
    public string AxisName { get; private set; }
    public float Axis;
    
    public TouchAxis(string axisName)
    {
        AxisName = axisName;
    }
}
