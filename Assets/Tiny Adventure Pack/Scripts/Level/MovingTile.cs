using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTile : MonoBehaviour {
    
    public const float stepDuration = 0.5f;
    private Coroutine playerMovement;

    private void Update()
    {
        if (playerMovement == null)
        {
            if (Input.GetKeyDown(KeyCode.W))        //In general not a good idea to use Input.GetKey; use Input.GetButton instead
                playerMovement = StartCoroutine(Move(Vector2.up));
            else if (Input.GetKeyDown(KeyCode.S))
                playerMovement = StartCoroutine(Move(Vector2.down));
            else if (Input.GetKeyDown(KeyCode.D))
                playerMovement = StartCoroutine(Move(Vector2.right));
            else if (Input.GetKeyDown(KeyCode.A))
                playerMovement = StartCoroutine(Move(Vector2.left));
        }
    }

    private IEnumerator Move(Vector2 direction)
    {
        Vector2 startPosition = transform.position;
        Vector2 temp = transform.position;
        Vector2 destinationPosition = temp + direction;
        float t = 0.0f;

        while (t < 1.0f)
        {
            transform.position = Vector2.Lerp(startPosition, destinationPosition, t);
            t += Time.deltaTime / stepDuration;
            yield return new WaitForEndOfFrame();
        }

        transform.position = destinationPosition;

        playerMovement = null;
    }

    private IEnumerator Move(Vector3 direction)
    {
        Vector3 startPosition = transform.position;
        Vector3 destinationPosition = transform.position + direction;
        float t = 0.0f;

        if (!Physics.Linecast(startPosition, destinationPosition) && Physics.Linecast(destinationPosition, destinationPosition + Vector3.down * 3.0f))
        {
            while (t < 1.0f)
            {
                transform.position = Vector3.Lerp(startPosition, destinationPosition, t);
                t += Time.deltaTime / stepDuration;
                yield return new WaitForEndOfFrame();
            }

            transform.position = destinationPosition;
        }

        playerMovement = null;
    }

}
