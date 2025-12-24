using UnityEngine;

public static class DirectionProvider
{
    public static Vector2 GetHorizontalDirection(bool isLeftDirection)
    {
        if (isLeftDirection)
            return new Vector2(-1f, 0f);
        else
            return new Vector2(1f, 0f);
    }
}