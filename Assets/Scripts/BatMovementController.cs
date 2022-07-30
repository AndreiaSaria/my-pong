using System;
using System.Collections.Generic;
using UnityEngine;

public class BatMovementController : MonoBehaviour
{
    [SerializeField] private string _upKey;
    [SerializeField] private string _downKey;
    [SerializeField] private float _speed;
    
    private bool _userPressedUp;
    private bool _userPressedDown;
    private float _upperLimit;
    private float _lowerLimit;

    private enum DirectionBatShouldMove
    {
        none,
        up,
        down
    }

    // Start is called before the first frame update
    private void Start()
    {
        float boxColliderYHalfExtent = GetBoxHalfYExtent();
        _upperLimit = GetYContactPointWithWall(Vector2.up) - boxColliderYHalfExtent;
        _lowerLimit = GetYContactPointWithWall(Vector2.down) + boxColliderYHalfExtent;
    }

    private float GetYContactPointWithWall(Vector2 directionOfWall)
    {
        var raycastHit = new List<RaycastHit2D>();
        Physics2D.Raycast(transform.position, directionOfWall, 
            new ContactFilter2D().NoFilter(), raycastHit);

        foreach (var hit in raycastHit)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                return hit.point.y;
            }
        }

        Debug.LogError($"[BatMovementController] Missing wall at direction {directionOfWall.ToString()}");
        return 0;
    }

    private float GetBoxHalfYExtent()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        if (boxCollider != null) return boxCollider.bounds.extents.y;
        
        Debug.LogError("[BatMovementController] This object is missing a box collider 2D");
        return 0;
    }
    
    //Project settings >Time >Fixed Time Step
    //it kinda lies.
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
        Vector3 directionToMove;

        switch (GetDirectionBatShouldMove())
        {
            case DirectionBatShouldMove.none:
                return;
            case DirectionBatShouldMove.up:
                directionToMove = Vector3.up;
                break;
            case DirectionBatShouldMove.down:
                directionToMove = Vector3.down;
                break;
            default:
                throw new ArgumentOutOfRangeException(); 
        }
        
        //Delta time for time difference between frames, fixed delta time for time difference between physics updates.
        //https://docs.unity3d.com/ScriptReference/Transform.Translate.html
        //transform.Translate(directionToMove * (Time.deltaTime * _speed), Space.World);
        //transform.position = (transform.position + directionToMove) * (Time.deltaTime * _speed);
        
        transform.Translate(directionToMove * (Time.fixedDeltaTime * _speed), Space.World);
        
        
        //Other way to do it https://answers.unity.com/questions/215377/transformtranslate-vs-rigidbodymoveposition.html
    }

    private DirectionBatShouldMove GetDirectionBatShouldMove()
    {
        if (_userPressedDown && !_userPressedUp)
        {
            if (transform.position.y <= _lowerLimit) return DirectionBatShouldMove.none;
            return DirectionBatShouldMove.down;
        }
        if (_userPressedUp && !_userPressedDown)
        {
            if (transform.position.y >= _upperLimit) return DirectionBatShouldMove.none;
            return DirectionBatShouldMove.up;
        }
        return DirectionBatShouldMove.none;
    }
}
