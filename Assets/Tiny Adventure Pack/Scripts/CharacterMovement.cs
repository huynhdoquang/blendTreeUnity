using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    
    public float moveSpeed = 4f;

    Animator thisAnim;
    float lastX, lastY;
    
    // Use this for initialization
    void Start()
    {
        thisAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Attack(true);
        }
        if (Input.GetKeyUp("space"))
        {
            Attack(false);
        }

        Move();
    }

    private void Move()
    {
        Vector3 rightMovement = Vector3.right * moveSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal");
        Vector3 upMovement = Vector3.up * moveSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.position += rightMovement;
        transform.position += upMovement;

        UpdateAnimation(heading);
    }

    void UpdateAnimation(Vector3 dir)
    {
        if (dir.x == 0 && dir.y == 0)
        {
            thisAnim.SetFloat("LastDirX", lastX);
            thisAnim.SetFloat("LastDirY", lastY);
            thisAnim.SetBool("Movement", false);
        }
        else
        {
            lastX = dir.x;
            lastY = dir.y;
            thisAnim.SetBool("Movement", true);
        }

        thisAnim.SetFloat("DirX", dir.x);
        thisAnim.SetFloat("DirY", dir.y);
    }
    void Attack(bool isAttack)
    {
        thisAnim.SetBool("Attack", isAttack);
    }
}
