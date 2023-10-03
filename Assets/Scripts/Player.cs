using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public bool onChair = false;
    public bool canSit;
    public Vector2 nextVec;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    CapsuleCollider2D capCollider;
    Vector2 curChairPos;

    public Joystick Joy;

    // Start is called before the first frame update
    void Start(){
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capCollider = GetComponent<CapsuleCollider2D>();
    }

    // 하나의 프레임 마다 한 번씩 호출되는 함수
    void Update(){
        inputVec.x = Joy.Horizontal;
        //Input.GetAxisRaw("Horizontal");
        inputVec.y = Joy.Vertical;
        //Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)){
            if (canSit && !onChair){
                // 앉을 수 있고 의자에 앉아있지 않다면
                onChair = true;
                Debug.Log("의자에 앉음");
                capCollider.enabled = false;
            } else if (onChair){
                // 앉을 수 있고 이미 의자에 앉아있다면
                onChair = false;
                Debug.Log("의자에서 일어섬");
                capCollider.enabled = true;
            }
        }
    }

    void FixedUpdate() {
        if (!onChair){
            nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
            // fixedDeltaTime : 물리 프레임 하나가 소모한 시간
            // 앞으로 나아가야 할 위치 = rigid Component의 위치 + 입력받은 벡터
            rigid.MovePosition(rigid.position + nextVec);
        } else if (onChair){
            rigid.MovePosition(curChairPos + new Vector2(0,0.7f)); // 의자 위치로 이동
        }
    }

    void LateUpdate() { // 보통 후처리를 할 때 LateUpdate를 사용함
        anim.SetFloat("Speed", inputVec.magnitude);
        // SetFloat("벡터 이름", float값)
        // speed의 방향은 필요 없고, 크기가 0 이상인지 아닌지만 알면 되므로 magnitude 사용

        if (inputVec.x != 0) {
            spriter.flipX = inputVec.x < 0;
            // inputVec.x 가 0보다 작으면 true
            // 즉, 조건문이 성립하면 true
        }
    }

    void OnCollisionStay2D(Collision2D collid) {
        if (collid.transform.CompareTag("Chair")){
            canSit = true;
            curChairPos = collid.transform.position;
        }
    }
}
