using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HybridInput
{
    private static HybridInput INSTANCE = new HybridInput();

    private Dictionary<string, TouchButton> TouchButtons = new Dictionary<string, TouchButton>();
    private Dictionary<string, TouchAxis> TouchAxes = new Dictionary<string, TouchAxis>();

    private bool DebugMode = true;

    private HybridInput()
    {
    }

    private void register(TouchButton input)
    {
        if (TouchButtons.ContainsKey(input.ButtonName))
        {
            Debug.LogWarning("Trying to register multiple touch inputs for button '" + input.ButtonName + "'");
            return;
        }
        TouchButtons.Add(input.ButtonName, input);
    }

    private void register(TouchAxis input)
    {
        if (TouchAxes.ContainsKey(input.AxisName))
        {
            Debug.LogWarning("Trying to register multiple touch inputs for axis '" + input.AxisName + "'");
            return;
        }
        TouchAxes.Add(input.AxisName, input);
    }

    private void unregister(TouchButton input)
    {
        TouchButtons.Remove(input.ButtonName);
    }

    private void unregister(TouchAxis input)
    {
        TouchAxes.Remove(input.AxisName);
    }

    private bool getButton(string buttonName)
    {
        TouchButton tInput = getTouchButton(buttonName);
        return Input.GetButton(buttonName) || (tInput != null && tInput.IsDown);
    }

    private float getAxis(string axisName)
    {
        TouchAxis tInput = getTouchAxis(axisName);
        return tInput == null || Mathf.Abs(tInput.Axis) < 0.01f ? Input.GetAxis(axisName) : tInput.Axis;
    }



    private TouchButton getTouchButton(string buttonName)
    {
        TouchButton tInput;
        bool touchInputFound = TouchButtons.TryGetValue(buttonName, out tInput);
        return touchInputFound ? tInput : null;
    }

    private TouchAxis getTouchAxis(string axisName)
    {
        TouchAxis tInput;
        bool touchInputFound = TouchAxes.TryGetValue(axisName, out tInput);
        return touchInputFound ? tInput : null;
    }

    /*
     * ==================================
     * STATIC WRAPPERS
     * ==================================
     */

    public static void Register(TouchButton input) {
        INSTANCE.register(input);
    }

    public static void Register(TouchAxis input)
    {
        INSTANCE.register(input);
    }

    public static void Unregister(TouchButton input)
    {
        INSTANCE.unregister(input);
    }

    public static void Unregister(TouchAxis input)
    {
        INSTANCE.unregister(input);
    }

    public static bool GetButton(string buttonName)
    {
        return INSTANCE.getButton(buttonName);
    }

    public static float GetAxis(string axisName)
    {
        return INSTANCE.getAxis(axisName);
    }

    public static bool IsDebugMode()
    {
        return INSTANCE.DebugMode;
    }

    public static void SetDebugMode(bool debugMode)
    {
        INSTANCE.DebugMode = debugMode;
    }

}
