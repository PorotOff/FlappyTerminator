using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FlappyTerminatorAnimator : MonoBehaviour
{
    private readonly int Died = Animator.StringToHash(nameof(Died));
    private readonly int StartedGame = Animator.StringToHash(nameof(StartedGame));
    private readonly int RestartedGame = Animator.StringToHash(nameof(RestartedGame));
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Die()
    {
        _animator.SetTrigger(Died);
    }

    public void StartGame()
    {
        _animator.SetTrigger(StartedGame);
    }

    public void RestartGame()
    {
        _animator.SetTrigger(RestartedGame);
    }
}