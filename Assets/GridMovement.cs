using System.Collections;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    bool isMoving;
    Vector3 originPos, targetPos;
    private float defRadius = 0.25f;
    [SerializeField] private Transform up, right, left, down;
    [SerializeField] float timeToMove = 0.2f;

    
    void Update()
    {
        if(Input.GetKey(KeyCode.W) && !isMoving && !Physics2D.OverlapCircle(up.position, defRadius, 1 << 8))
        {
            StartCoroutine(MovePlayer(Vector3.up));
        }
        else if(Input.GetKey(KeyCode.S) && !isMoving && !Physics2D.OverlapCircle(down.position, defRadius, 1 << 8))
        {
            StartCoroutine(MovePlayer(Vector3.down));
        }
        else if(Input.GetKey(KeyCode.A) && !isMoving && !Physics2D.OverlapCircle(left.position, defRadius, 1 << 8))
        {
            StartCoroutine(MovePlayer(Vector3.left));
        }
        else if(Input.GetKey(KeyCode.D) && !isMoving && !Physics2D.OverlapCircle(right.position, defRadius, 1 << 8))
        {
            StartCoroutine(MovePlayer(Vector3.right));
        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;
        originPos = transform.position;
        targetPos = originPos + direction;

        while(elapsedTime < timeToMove){
            transform.position = Vector3.Lerp(originPos, targetPos, (elapsedTime/timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }
}
