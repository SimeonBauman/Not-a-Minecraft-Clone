using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Block 
{
    public float textIndex;
    public bool isEdge = false;

    public Block(float ind)
    {
        this.textIndex = ind;   
    }

}
