using UnityEngine;

//Although it has state on the name its not a state of a state machine.
public class GameController : MonoSingleton<GameController>
{
    [SerializeField] private BallController _ball;

    public void ResetBall()
    {
        if(_ball == null) return;
        _ball.ResetPositionAndMovementVector();
    }
}
