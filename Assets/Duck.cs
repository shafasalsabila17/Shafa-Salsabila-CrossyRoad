using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Duck : MonoBehaviour
{
    [SerializeField]
    [Range (0,1)] 
    float moveDuration = 0.1f;

    [SerializeField]
    [Range (0,1)] 
    float jumpHeight = 0.5f;

    [SerializeField] int leftMoveLimit;
    [SerializeField] int rightMoveLimit;
    [SerializeField] int backMoveLimit;


    public UnityEvent <Vector3> OnJumpEnd;
    public UnityEvent<int> OnGetCoin;
    public UnityEvent OnCarCollision;
    public UnityEvent OnDie;

    private bool isMoveable =  false;
 
        
    void Update()
    {
        if (isMoveable == false)
        return;

        if (DOTween.IsTweening(transform))
            return;

        Vector3 direction = Vector3.zero; 

        if (Input.GetKeyDown(KeyCode.W) 
            || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction += Vector3.forward;
        }

        else if (Input.GetKeyDown(KeyCode.S) 
            || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction += Vector3.back;
        }

        else if (Input.GetKeyDown(KeyCode.D) 
            || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction += Vector3.right;
        }

        else if (Input.GetKeyDown(KeyCode.A) 
            || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction += Vector3.left;
        }

        if (direction == Vector3.zero)
            return;

        Move(direction);
    }

    public void Move(Vector3 direction)
    {
       var targetPosition = transform.position + direction;

       if (targetPosition.x < leftMoveLimit ||
          targetPosition.x > rightMoveLimit || 
          targetPosition.z < backMoveLimit  ||
          Tree.AllPositions.Contains(targetPosition))
            {
            targetPosition= transform.position;
            }

       transform
            .DOJump(
            targetPosition,
            jumpHeight,
            1,
            moveDuration)
            .onComplete = BroadCastPositionOnJumpEnd;
                        
        transform.forward = direction;
    }

    public void SetMoveable (bool value)
    {
        isMoveable =  value;
    }

    public void UpdateMoveLimit(int horizontalSize, int backLimit)
    {
        leftMoveLimit = -horizontalSize/2;
        rightMoveLimit = horizontalSize/2;
        backMoveLimit = backLimit;

    }

    private void BroadCastPositionOnJumpEnd()
    {
        OnJumpEnd.Invoke(transform.position);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (transform.localScale.y == 0.1)
             return;
        
             transform.DOScale(new Vector3 (2, 0.1f), 0.2f);
        
             isMoveable = false;
             OnCarCollision.Invoke();
             Invoke("Die", 3);
        }

        else if (other.CompareTag("Coin"))
        {
            Debug.Log("CoinEnter");
            var coin = other.GetComponent<Coin>();
            OnGetCoin.Invoke(coin.Value);
            coin.Collected();
        }

        else if (other.CompareTag("Eagle"))
        {
            if (this.transform != other.transform)
            {
                this.transform.SetParent(other.transform);
                Invoke("Die", 3);
            }
        }
    }

    private void Die()
    {
        OnDie.Invoke();
    }

}
