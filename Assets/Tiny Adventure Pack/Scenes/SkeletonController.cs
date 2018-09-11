using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    Animator thisAnim;
    float lastX, lastY;
    GridMove thisGridMove;

    void Start()
    {
        thisGridMove = GetComponent<GridMove>();
        thisAnim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAnimation(thisGridMove.input);
    }

    void UpdateAnimation(Vector2 dir)
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
}
