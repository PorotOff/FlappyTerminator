public static class DirectionProvider
{
    public static float GetHorizontalDirection(bool isLeftDirection)
    {
        if (isLeftDirection)
            return -1f;
        else
            return 1f;
    }
}