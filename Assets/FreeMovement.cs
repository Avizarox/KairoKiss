using System.Collections;
using UnityEngine;

public class FreeMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    float horInput, verInput;
    Rigidbody2D _rb;
    Collider2D _col;
    private Vector2 moveVector;
    private bool isDashing;
    [SerializeField] private float dashDistance = 2f; 
    [SerializeField] private float dashCoolDown;
    private float dashCoolDownTimer;
    [SerializeField] private float dashSpeed;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }

    void Update()
    {
        verInput = Input.GetAxisRaw("Vertical");
        horInput = Input.GetAxisRaw("Horizontal");
        moveVector = new Vector2(horInput, verInput).normalized;
        if(!isDashing){
            _rb.velocity = moveVector*moveSpeed;
            dashCoolDownTimer -= Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space) && moveVector.magnitude != 0 && dashCoolDownTimer <= 0){
            dashCoolDownTimer = dashCoolDown;
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash(){
        Vector2 originPos = transform.position;
        Vector2 currentPos = originPos;
        isDashing = true;
        Vector2 dashMoveVector = moveVector*moveSpeed*dashSpeed;
        while((currentPos - originPos).magnitude <= dashDistance){
            currentPos = transform.position;
            _rb.velocity = dashMoveVector;
            yield return null;
        }
        isDashing = false;
    }
}
