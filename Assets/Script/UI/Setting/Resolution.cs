using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resolution
{
    public int width;
    public int height;

    public Resolution(int w, int h)
    {
        width = w;
        height = h;
    }

    public override string ToString()
    {
        return width + "x" + height;
    }
}
