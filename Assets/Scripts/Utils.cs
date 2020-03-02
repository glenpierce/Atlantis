using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utils
{
    public static void Log(string message, params object[] args)
    {
        Debug.Log(string.Format(message, args.Select(a => a.ToString()).ToArray()));
    }

    private static Color[] _colors = new[]
    {
        Color.black,
        Color.blue,
        Color.green,
        Color.red,
        Color.yellow,
        Color.magenta,
    };

    private static int _colorIndex = 0;

    public static Color GetNextColor()
    {
        if (_colorIndex > _colors.Length)
        {
            _colorIndex = 0;
        }

        return _colors[_colorIndex++];
    }

    public static bool FloatEquals(this float a, float b)
    {
        return a <= (b + 0.001f) && a >= (b - 0.01f);
    }
}
