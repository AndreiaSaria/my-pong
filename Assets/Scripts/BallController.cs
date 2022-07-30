using UnityEngine;
using Random = UnityEngine.Random;

//https://docs.unity3d.com/Manual/CollidersOverview.html
public class BallController : MonoBehaviour
{
    public float Speed;
    public float SpeedGain;
    
    private Vector2 _movementVector;
    private Vector2 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.position;
        ResetPositionAndMovementVector();
    }

    public void ResetPositionAndMovementVector()
    {
        transform.position = _initialPosition;
        _movementVector = new Vector2(RandomToGetMinusOneOrOne(), RandomToGetMinusOneOrOne());
    }
    
    private void FixedUpdate()
    {
        MoveBall();
    }

    private int RandomToGetMinusOneOrOne()
    {
        int value = Random.Range(0, 2); //Max is exclusive
        
        if (value == 0)
        {
            return -1;
        }
        else
        {
            return value;
        }
    }

    private void MoveBall()
    {
        //transform.Translate(_movementVector * (Time.deltaTime * Speed), Space.World);
        
        transform.Translate(_movementVector * (Time.fixedDeltaTime * Speed), Space.World);
    }

    private void MirrorMovementY()
    {
        _movementVector = new Vector2(_movementVector.x, -_movementVector.y);
    }

    private void MirrorMovementX()
    {
        _movementVector = new Vector2(-_movementVector.x, _movementVector.y);
    }

    private void IncreaseSpeed()
    {
        _movementVector *= SpeedGain;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Ball has triggered enter");
        if (other.CompareTag("Wall"))
        {
            MirrorMovementY();
        }

        if (other.CompareTag("Player"))
        {
            MirrorMovementX();
            IncreaseSpeed();
        }
    }
}
