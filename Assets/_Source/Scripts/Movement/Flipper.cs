using UnityEngine;

public class Flipper
{
    public void FlipX(SpriteRenderer spriteRenderer, float xVelocity)
    {
        if (xVelocity > 0)
            spriteRenderer.flipX = false;
        else if (xVelocity < 0)
            spriteRenderer.flipX = true;
    }
}