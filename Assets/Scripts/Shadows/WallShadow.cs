using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallShadow : MonoBehaviour
{
    public float centerX, centerY, left, right, top, bottom;
   
   BoxCollider2D boxCollider;


    //TilemapCollider2D boxCollider;

    void Start()
    {
        /*
        boxCollider = GetComponent<BoxCollider2D>();
        //boxCollider = GetComponent<TilemapCollider2D>();
        centerX = transform.position.x + boxCollider.bounds.extents.x;
        centerY = transform.position.y - boxCollider.bounds.extents.y;
        left = transform.position.x;
        top = transform.position.y;
        right = transform.position.x + boxCollider.bounds.size.x;
        bottom = transform.position.y - boxCollider.bounds.size.y;
        */

        boxCollider = GetComponent<BoxCollider2D>();

        Bounds bounds = boxCollider.bounds;
        centerX = bounds.center.x;
        centerY = bounds.center.y;
        left = bounds.min.x;
        right = bounds.max.x;
        top = bounds.max.y;
        bottom = bounds.min.y;

        
    }
}


