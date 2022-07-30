using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [Tooltip("The goal behind player 1.")]
    [SerializeField] private GoalDetector _player1GoalDetector;
    [Tooltip("The goal behind player 2.")]
    [SerializeField] private GoalDetector _player2GoalDetector;

    [SerializeField] private TextMeshProUGUI _player1ScoreText;
    [SerializeField] private TextMeshProUGUI _player2ScoreText;

    private int _player1Score = 0;
    private int _player2Score = 0;

    //https://docs.unity3d.com/Manual/ExecutionOrder.html
    private void OnEnable()
    {
        _player1GoalDetector.GoalDetected += AcknowledgePlayer2Point;
        _player2GoalDetector.GoalDetected += AcknowledgePlayer1Point;
    }

    private void OnDisable()
    {
        _player1GoalDetector.GoalDetected -= AcknowledgePlayer2Point;
        _player2GoalDetector.GoalDetected -= AcknowledgePlayer1Point;
    }

    private void AcknowledgePlayer2Point()
    {
        Debug.Log("Player 2 point!");
        GameController.instance.ResetBall();
        _player2Score++;
        _player2ScoreText.text = _player2Score.ToString();
    }

    private void AcknowledgePlayer1Point()
    {
        Debug.Log("Player 1 point!");
        GameController.instance.ResetBall();
        _player1Score++;
        _player1ScoreText.text = _player1Score.ToString();
    }
}
