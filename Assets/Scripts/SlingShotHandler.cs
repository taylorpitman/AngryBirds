using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShotHandler : MonoBehaviour
{
    //global variables
    [SerializeField] private LineRenderer leftLineRenderer;
    [SerializeField] private LineRenderer rightLineRenderer;

    [SerializeField] private Transform leftStartPosition;   //right sling shot line starts here
    [SerializeField] private Transform rightStartPosition;  //right sling shot line starts here
    [SerializeField] private Transform centerPosition;      //center of sling shot line starts here
    [SerializeField] private Transform idlePosition;        //idle sling shop is here

    [SerializeField] private float maxDistance = 3.5f;
    private Vector2 slingShotLinesMaxPosition;  //limit how far lines can go
 
 
    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.isPressed)
        {
            DrawSlingShot();
        }

    }

    private void DrawSlingShot()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //mathematically does not allow the line to go passed a certain point from the center
        slingShotLinesMaxPosition = centerPosition.position + Vector3.ClampMagnitude(touchPosition - centerPosition.position, maxDistance);

        SetLines(slingShotLinesMaxPosition);
    }

    private void SetLines(Vector2 position)
    {
        leftLineRenderer.SetPosition(0, position);
        leftLineRenderer.SetPosition(1, leftStartPosition.position);

        rightLineRenderer.SetPosition(0, position);
        rightLineRenderer.SetPosition(1, rightStartPosition.position);
    }
}
