using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum DirectionEnum
{
    None = 0,
    Up = 1,
    UpRight = 2,
    Right = 4,
    RightDown = 8,
    Down = 16,
    DownLeft = 32,
    Left = 64,
    LeftUp = 128,
};
public static class DirectionUtility
{
    public static int sCount = 8;
    public static DirectionEnum Reverse(this DirectionEnum directionEnum)
    {
        switch (directionEnum)
        {
            case DirectionEnum.Up: return DirectionEnum.Down;
            case DirectionEnum.Down: return DirectionEnum.Up;
            case DirectionEnum.Right: return DirectionEnum.Left;
            case DirectionEnum.Left: return DirectionEnum.Right;
            case DirectionEnum.UpRight: return DirectionEnum.DownLeft;
            case DirectionEnum.DownLeft: return DirectionEnum.UpRight;
            case DirectionEnum.RightDown: return DirectionEnum.LeftUp;
            case DirectionEnum.LeftUp: return DirectionEnum.RightDown;
            default: return DirectionEnum.None;
        }
    }
    public static int GetIndex(this DirectionEnum directionEnum)
    {
        switch (directionEnum)
        {
            default: return -1;
            case DirectionEnum.Up: return 0;
            case DirectionEnum.UpRight: return 1;
            case DirectionEnum.Right: return 2;
            case DirectionEnum.RightDown: return 3;
            case DirectionEnum.Down: return 4;
            case DirectionEnum.DownLeft: return 5;
            case DirectionEnum.Left: return 6;
            case DirectionEnum.LeftUp: return 7;
        }
    }
    public static DirectionEnum GetDirectionEnum(this int index)
    {
        index = index % sCount;
        return (DirectionEnum)(Math.Pow(2, index));
    }
    public static DirectionEnum GetClockwise(this DirectionEnum directionEnum, int step = 1)
    {
        var index = GetIndex(directionEnum);
        index += step;
        return index.GetDirectionEnum();
    }
    public static DirectionEnum GetCounterClockwise(this DirectionEnum directionEnum, int step = 1)
    {
        var index = GetIndex(directionEnum);
        index -= step;
        index += sCount;
        return index.GetDirectionEnum();
    }
    public static Vector2 MoveVector(this Vector2 vector, DirectionEnum directionEnum)
    {
        switch (directionEnum)
        {
            default: return vector;
            case DirectionEnum.Up: return new Vector2(vector.x, vector.y + 1);
            case DirectionEnum.UpRight: return new Vector2(vector.x + 1, vector.y + 1);
            case DirectionEnum.Right: return new Vector2(vector.x + 1, vector.y);
            case DirectionEnum.RightDown: return new Vector2(vector.x + 1, vector.y - 1);
            case DirectionEnum.Down: return new Vector2(vector.x, vector.y - 1);
            case DirectionEnum.DownLeft: return new Vector2(vector.x - 1, vector.y - 1);
            case DirectionEnum.Left: return new Vector2(vector.x - 1, vector.y);
            case DirectionEnum.LeftUp: return new Vector2(vector.x - 1, vector.y + 1);
        }
    }
}
