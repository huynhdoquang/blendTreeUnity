using System.Collections;
using UnityEngine;

//http://wiki.unity3d.com/index.php/GridMove
class GridMove : MonoBehaviour
{
    public float moveSpeed = 3f; //move speed
    public float gridSize = 1f; // grid size
    public enum Orientation
    {
        XZ,
        XY
    };

    //Whether movement is on the horizontal (X/Z) plane or the vertical (X/Y) plane.
    public Orientation gridOrientation = Orientation.XY;
    // If checked, diagonal movement can be done by holding down the appropriate buttons simultaneously.
    // Otherwise, only straight horizontal or vertical movement is possible.
    //Biến dưới đây để lưu khả năng được đi trogn đường chéo hay hk
    //và biến dưới nữa là tốc độ chuẩn khi di chuyển đường chéo
    private bool allowDiagonals = false;
    private bool correctDiagonalSpeed = true;
    public Vector2 input;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;


    public void Update()
    {
        if (!isMoving )
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (!allowDiagonals)
            {
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                {
                    input.y = 0;
                }
                else
                {
                    input.x = 0;
                }
            }

            if (input != Vector2.zero)
            {
                input = new Vector2(System.Math.Sign(input.x), System.Math.Sign(input.y));
                StartCoroutine(move(transform));
            }
        }
    }

    public IEnumerator move(Transform transform)
    {
        isMoving = true;
        startPosition = transform.position;
        t = 0;

        if (gridOrientation == Orientation.XZ)
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y, startPosition.z + System.Math.Sign(input.y) * gridSize);
        }
        else
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        }

        if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0)
        {
            factor = 0.7071f;
        }
        else
        {
            factor = 1f;
        }

        while (t < 1f)
        {
            t += Time.deltaTime * (moveSpeed / gridSize) * factor;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        isMoving = false;
        yield return 0;
    }
}