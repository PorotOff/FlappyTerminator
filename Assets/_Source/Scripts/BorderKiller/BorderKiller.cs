using UnityEngine;

public class BorderKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IKillable killable))
            killable.Die();
    }
}