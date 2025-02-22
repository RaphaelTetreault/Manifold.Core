using System;
using System.Numerics;

namespace Manifold.Spline;

/// <summary>
///     
/// </summary>
public static class Bezier
{
    // Cubic Bezier, ^3

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p0">Start position</param>
    /// <param name="p1">Start out tangent</param>
    /// <param name="p2">End in tangent</param>
    /// <param name="p3">End position</param>
    /// <param name="t">Time 0 through 1</param>
    /// <returns></returns>
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Math.Clamp(t, 0, 1);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * oneMinusT * p0 +
            3f * oneMinusT * oneMinusT * t * p1 +
            3f * oneMinusT * t * t * p2 +
            t * t * t * p3;
    }

    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Math.Clamp(t, 0, 1);
        float oneMinusT = 1f - t;
        return
            3f * oneMinusT * oneMinusT * (p1 - p0) +
            6f * oneMinusT * t * (p2 - p1) +
            3f * t * t * (p3 - p2);
    }


    // Quadratic Bezier, ^2

    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        t = Math.Clamp(t, 0f, 1f);
        float invT = 1f - t;

        Vector3 quadraticBezierPoint =
            invT * invT * p0 +
            2f * invT * t * p1 +
            t * t * p2;

        return quadraticBezierPoint;
    }

    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 firstDerivitive =
            2f * (1f - t) * (p1 - p0) +
            2f * t * (p2 - p1);

        return firstDerivitive;
    }



}
