using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShotHandler : MonoBehaviour
{
    //global variables  

    [Header("Line Renderers")]
    [SerializeField] private LineRenderer leftLineRenderer;
    [SerializeField] private LineRenderer rightLineRenderer;

    [Header("Transforms")]
    [SerializeField] private Transform leftStartPosition;   //right sling shot line starts here
    [SerializeField] private Transform rightStartPosition;  //right sling shot line starts here
    [SerializeField] private Transform centerPosition;      //center of sling shot line starts here
    [SerializeField] private Transform idlePosition;        //idle sling shop is here

    [Header("Position Limits")]
    [SerializeField] private float maxDistance = 3.5f;
    private Vector2 slingShotLinesMaxPosition;  //limit how far lines can go

    [Header("Area Management")]
    [SerializeField] private SlingShotArea slingshotArea;
    private bool withinArea;

    [Header("Bird")]
    [SerializeField] private GameObject angieBird;

    private GameObject spawnedBird;

    private void Awake()
    {
        //makes sure the renderers aren't visible until mouse is pressed
        leftLineRenderer.enabled = false;
        rightLineRenderer.enabled = false;

        SpawnBird();

    }
    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && slingshotArea.isWithinArea())
        {
            withinArea = true;
        }

        if (Mouse.current.leftButton.isPressed && withinArea)
        {
            DrawSlingShot();
            PositionAngieBird();

        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            withinArea = false;
        }

    }

    #region Slingshot Methods
    private void DrawSlingShot()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //mathematically does not allow the line to go passed a certain point from the center
        slingShotLinesMaxPosition = centerPosition.position + Vector3.ClampMagnitude(touchPosition - centerPosition.position, maxDistance);

        SetLines(slingShotLinesMaxPosition);
    }

    private void SetLines(Vector2 position)
    {
        //makes lines visible
        if (!leftLineRenderer.enabled && !rightLineRenderer.enabled)
        {
            leftLineRenderer.enabled = true;
            rightLineRenderer.enabled = true;
        }

        leftLineRenderer.SetPosition(0, position);
        leftLineRenderer.SetPosition(1, leftStartPosition.position);

        rightLineRenderer.SetPosition(0, position);
        rightLineRenderer.SetPosition(1, rightStartPosition.position);
    }
    #endregion

    #region Angiebird Methods
    private void SpawnBird()
    {
        //slingshot idle position
        SetLines(idlePosition.position);

        //adds angry bird
        spawnedBird = Instantiate(angieBird, idlePosition.position, Quaternion.identity);
    }

    private void PositionAngieBird()
    {
        spawnedBird.transform.position = slingShotLinesMaxPosition;

    }

    private void RotateAngieBird()
    {
        
    }

    #endregion
}
