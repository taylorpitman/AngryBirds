using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System;

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
    [SerializeField] private Transform elasticTransform;

    [Header("Position Limits")]
    [SerializeField] private float maxDistance = 3.5f;
    [SerializeField] private float shotForce = 9f;
    private Vector2 slingShotLinesMaxPosition;  //limit how far lines can go    [SerializeField] private float elasticDivider =  1.2f;

    [Header("Area Management")]
    [SerializeField] private SlingShotArea slingshotArea;
    private bool withinArea;

    [Header("Bird")]
    [SerializeField] private AngieBird angieBird;
    [SerializeField] private float angieBirdPosOffset = .3f;
    private AngieBird spawnedBird;
    private Vector2 direction;
    private Vector2 directionNormalized;

    [Header("Animating Slingshot")]
    [SerializeField] private float elasticDivider =  1.2f;
    [SerializeField] private AnimationCurve elasticCurve;

    private bool birdOnSlingShot = false;

    [SerializeField] private float spawnDelay = 2f;


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
        //make this a public variable and do this in slingshotArea ??
        if (InputManager.wasLeftButtonPressed && slingshotArea.isWithinArea())
        {
            withinArea = true;
        }

        //nested if statement ?
        if (InputManager.isLeftButtonPressed && withinArea && birdOnSlingShot)
        {
            DrawSlingShot();
            PositionAngieBird();

        }

        if (InputManager.wasLeftButtonReleased && birdOnSlingShot && withinArea)
        {
            if(GameManager.gameManager.HasEnoughShots())
            {
                withinArea = false;
                birdOnSlingShot = false;

                spawnedBird.LaunchBird(direction, shotForce);

                GameManager.gameManager.UseShot();
                AnimateSlingShot();

                if(GameManager.gameManager.HasEnoughShots())
                {
                    StartCoroutine(SpawnAngieBirdDelay());
                }
            }
        }
    }

    #region Slingshot Methods
    private void DrawSlingShot()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(InputManager.mouseTouchPosition);

        //mathematically does not allow the line to go passed a certain point from the center
        slingShotLinesMaxPosition = centerPosition.position + Vector3.ClampMagnitude(touchPosition - centerPosition.position, maxDistance);

        SetLines(slingShotLinesMaxPosition);

        direction = (Vector2)centerPosition.position - slingShotLinesMaxPosition;
        directionNormalized = direction.normalized;     //ensures magnitured of direction doesnt go above 1

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
        //local variables
        Vector2 birdDirection;
        Vector2 spawnPos;
        
        //slingshot idle position
        SetLines(idlePosition.position);
        
        birdDirection = (centerPosition.position - idlePosition.position).normalized;
        spawnPos = (Vector2)idlePosition.position + birdDirection * angieBirdPosOffset;

        //adds angry bird
        spawnedBird = Instantiate(angieBird, spawnPos, Quaternion.identity);

        spawnedBird.transform.right = birdDirection;

        birdOnSlingShot = true;
    }

    private void PositionAngieBird()
    {
        //lines up bird correctly on sling shot
        spawnedBird.transform.position = slingShotLinesMaxPosition - directionNormalized * angieBirdPosOffset;
        RotateAngieBird();

    }

    private void RotateAngieBird()
    {

        spawnedBird.transform.right = directionNormalized;

    }

    private IEnumerator SpawnAngieBirdDelay()
    {
        yield return new WaitForSeconds(spawnDelay);

        SpawnBird();
    }

    #endregion
    
    #region Animating Slingshot
    private void AnimateSlingShot()
    {   
        //sets position of transform to where mouse position is on click
        elasticTransform.position = leftLineRenderer.GetPosition(0);

        //distance between elastic and center position
        float distance = Vector2.Distance(elasticTransform.position, centerPosition.position);

        //time based on how far the elatic is pulled back
        float time = distance / elasticDivider;

        //moves an invisible transform, follows a waveform(Ease) to simulate physics
        elasticTransform.DOMove(centerPosition.position, time).SetEase(elasticCurve);

        StartCoroutine(AnimateSlingShotLines(elasticTransform, time));
    }

    private IEnumerator AnimateSlingShotLines(Transform transform, float time)
    {
        float elapsedTime = 0f;

        while(elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            SetLines(transform.position);

            yield return null;
        }
    }

    #endregion
}
