using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    [SerializeField] private BallController _ball;

    public void ResetBall()
    {
        if(_ball == null) return;
        _ball.ResetPositionAndMovementVector();
    }
}
