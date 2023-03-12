using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed; // 캐릭터의 스피드 담당
    private int walkCount = 20;
    private int currentWalkCount;
    //speed = 2.4 , walkCount = 20
    //2.4 * 20 = 48 -> 48픽셀 만큼 움직이게 하겠다는 의미
    //2.4를 매번 20번 곱하지 않고, currentWalkCount가 while문에서 1씩 증가하여 20이 되면 빠져나오면 48픽셀만큼을 이동할 수 있다. 

    private Vector3 vector; //Vector3(x,y,z)

    public float runSpeed;
    private float applyRunSpeed; // shift키를 눌렀을 때 달리기
    private bool applyRunFlag = false; 

    private bool canMove = true;

    void Start()
    {
     
    }

    IEnumerator MoveCoroutine()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            applyRunSpeed = runSpeed;
            applyRunFlag = true; // shift가 눌릴경우 true로
        }
        else
        {
            applyRunSpeed = 0;
            applyRunFlag = false;
        }

        vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z); // x값, y값, z값

        while (currentWalkCount < walkCount)
        {
            if (vector.x != 0)
            {
                transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                // Translate는 현재 값에 괄호 안의 수치만큼 더해주는 함수이다. 
                //이 외의 움직이는 방법
                //transform.position = vector;
                //rigid body 이용
            }

            else if (vector.y != 0)
            {
                transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
            }

            if (applyRunFlag) // shift가 눌리면 currentWalkCount가 2씩 증가하도록
                currentWalkCount++;

            currentWalkCount++;
            yield return new WaitForSeconds(0.01f); // 대기
        }
        currentWalkCount = 0;
        
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove) // canMove가 true일 때만 실행되도록
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) // 상하좌우 방향키가 눌렸을 경우
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }

    }
}
