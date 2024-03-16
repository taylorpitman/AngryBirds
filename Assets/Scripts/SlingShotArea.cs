using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShotArea : MonoBehaviour
{
    [SerializeField] private LayerMask slingShotAreaMask;

    // Update is called once per frame
    void Update()
    {

    }

    public bool isWithinArea()
    {
        bool isWithinArea = false;

        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //checks if mouse is in sling shot area
        if (Physics2D.OverlapPoint(worldPosition, slingShotAreaMask))
        {
            isWithinArea = true;
        }

        return isWithinArea;
    }
}
