using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed; // ĳ������ ���ǵ� ���
    private int walkCount = 20;
    private int currentWalkCount;
    //speed = 2.4 , walkCount = 20
    //2.4 * 20 = 48 -> 48�ȼ� ��ŭ �����̰� �ϰڴٴ� �ǹ�
    //2.4�� �Ź� 20�� ������ �ʰ�, currentWalkCount�� while������ 1�� �����Ͽ� 20�� �Ǹ� ���������� 48�ȼ���ŭ�� �̵��� �� �ִ�. 

    private Vector3 vector; //Vector3(x,y,z)

    public float runSpeed;
    private float applyRunSpeed; // shiftŰ�� ������ �� �޸���
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
            applyRunFlag = true; // shift�� ������� true��
        }
        else
        {
            applyRunSpeed = 0;
            applyRunFlag = false;
        }

        vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z); // x��, y��, z��

        while (currentWalkCount < walkCount)
        {
            if (vector.x != 0)
            {
                transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                // Translate�� ���� ���� ��ȣ ���� ��ġ��ŭ �����ִ� �Լ��̴�. 
                //�� ���� �����̴� ���
                //transform.position = vector;
                //rigid body �̿�
            }

            else if (vector.y != 0)
            {
                transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
            }

            if (applyRunFlag) // shift�� ������ currentWalkCount�� 2�� �����ϵ���
                currentWalkCount++;

            currentWalkCount++;
            yield return new WaitForSeconds(0.01f); // ���
        }
        currentWalkCount = 0;
        
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove) // canMove�� true�� ���� ����ǵ���
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) // �����¿� ����Ű�� ������ ���
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }

    }
}
