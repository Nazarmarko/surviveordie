using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRenderSorter : MonoBehaviour
{
    
    private Renderer myRenderer;
    [SerializeField]
    private int SortingOrderBase = 1;
    [SerializeField]
    private int offset = 0;
    [SerializeField]
    private bool runOnlyOnce;
    private void Awake() 
    {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        myRenderer.sortingOrder = (int)(SortingOrderBase - transform.position.y - offset);
        if (runOnlyOnce) 
        {
        Destroy(this);
        }
    }
}
