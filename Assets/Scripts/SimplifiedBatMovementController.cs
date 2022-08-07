using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class SimplifiedBatMovementController : MonoBehaviour
{
    [SerializeField] private string _upKey;
    [SerializeField] private string _downKey;
    [SerializeField] private float _speed;
    
    private bool _userPressedUp;
    private bool _userPressedDown;
    
    private void FixedUpdate()
    {
        ReceiveInput();
        MoveBat();
    }

    private void ReceiveInput()
    {
        _userPressedUp = Input.GetKey(_upKey);
        _userPressedDown = Input.GetKey(_downKey);
    }

    private void MoveBat()
    {
        if (_userPressedDown && !_userPressedUp)
        {
            transform.Translate(Vector2.down * (Time.fixedDeltaTime * _speed), Space.World);
        }
        else if (_userPressedUp && !_userPressedDown)
        {
            transform.Translate(Vector2.up * (Time.fixedDeltaTime * _speed), Space.World);
        }
    }
}
