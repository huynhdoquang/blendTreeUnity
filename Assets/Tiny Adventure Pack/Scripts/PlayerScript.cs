using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{


    private float speed = 2.0f;
    private Vector3 pos;
    private Transform tr;

    void Start()
    {
        //Here we set the Values from the current position
        pos = transform.position;
        tr = transform;
    }

    void Update()
    {
        Movement();

    }

    private void Movement()
    {
        //If we press any Key we will add a direction to the position ...
        //using Vector3.'anydirection' will add 1 to that direction


        //But we Check if we are at the new Position, before we can add some more
        //it will prevent to move before you are at your next 'tile'
        if (Input.GetKeyDown(KeyCode.D) && tr.position == pos)
        {
            pos += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.A) && tr.position == pos)
        {
            pos += Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.W) && tr.position == pos)
        {
            pos += Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && tr.position == pos)
        {
            pos += Vector3.down;
        }

        //Here you will move Towards the new position ...
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);


    }
}