using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Block
{
    public float textIndex;


    public Block(int ind)
    {
        this.textIndex = ind;
    }



    public static Block[] blocks = new Block[] {};
}
