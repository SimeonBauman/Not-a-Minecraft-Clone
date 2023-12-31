using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree 
{
    public int[,,] treePrint;
  

   
  

    public static int[,,] treeTemp = new int[6,5,5]{


        {{ 0,0,0,0,0},{ 0,0,3,0,0},{0,3,3,3,0 },{ 0,0,3,0,0},{ 0,0,0,0,0} },
        {{ 0,0,0,0,0},{ 0,0,3,0,0},{0,3,4,3,0 },{ 0,0,3,0,0},{ 0,0,0,0,0} },
        {{ 3,3,3,3,3},{ 3,3,3,3,3},{3,3,4,3,3 },{ 3,3,3,3,3},{ 3,3,3,3,3} },
        {{ 3,3,3,3,3},{ 3,3,3,3,3},{3,3,4,3,3 },{ 3,3,3,3,3},{ 3,3,3,3,3} },
        {{ 0,0,0,0,0},{ 0,0,0,0,0},{0,0,4,0,0 },{ 0,0,0,0,0},{ 0,0,0,0,0} },
        {{ 0,0,0,0,0},{ 0,0,0,0,0},{0,0,4,0,0 },{ 0,0,0,0,0},{ 0,0,0,0,0} }
        

    };
    public static int[,,] fallTree = new int[6, 5, 5]{


        {{ 0,0,0,0,0},{ 0,0,9,0,0},{0,9,9,9,0 },{ 0,0,9,0,0},{ 0,0,0,0,0} },
        {{ 0,0,0,0,0},{ 0,0,9,0,0},{0,9,4,9,0 },{ 0,0,9,0,0},{ 0,0,0,0,0} },
        {{ 9,9,9,9,9},{ 9,9,9,9,9},{9,9,4,9,9 },{ 9,9,9,9,9},{ 9,9,9,9,9} },
        {{ 9,9,9,9,9},{ 9,9,9,9,9},{9,9,4,9,9 },{ 9,9,9,9,9},{ 9,9,9,9,9} },
        {{ 0,0,0,0,0},{ 0,0,0,0,0},{0,0,4,0,0 },{ 0,0,0,0,0},{ 0,0,0,0,0} },
        {{ 0,0,0,0,0},{ 0,0,0,0,0},{0,0,4,0,0 },{ 0,0,0,0,0},{ 0,0,0,0,0} }


    };
    public static int[,,] fellenTree1 = new int[6, 5, 5]{


        {{ 0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0 },{0,0,0,0,0},{ 0,0,0,0,0} },
        {{ 0,0,0,0,0},{ 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 },{ 0,0,0,0,0} },
        {{ 0,0,0,0,0},{ 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } },
        {{ 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }},
        {{ 0,0,0,0,0},{ 0,0,0,0,0},{0,0,0,0,0},{ 0,0,0,0,0},{ 0,0,0,0,0} },
        {{ 0,0,11,0,0},{ 0,0,11,0,0},{0,0,11,0,0},{ 0,0,11,0,0},{ 0,0,11,0,0} }


    };
    public static int[,,] fellenTree2 = new int[6, 5, 5]{


        {{ 0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0 },{0,0,0,0,0},{ 0,0,0,0,0} },
        {{ 0,0,0,0,0},{ 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 },{ 0,0,0,0,0} },
        {{ 0,0,0,0,0},{ 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } },
        {{ 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }},
        {{ 0,0,0,0,0},{ 0,0,0,0,0},{0,0,0,0,0},{ 0,0,0,0,0},{ 0,0,0,0,0} },
        {{ 0,0,0,0,0},{ 0,0,0,0,0},{11,11,11,11,11},{ 0,0,0,0,0},{ 0,0,0,0,0} }


    };

    public static List<int[,,]> trees = new List<int[,,]>();

    public static void createList()
    {
         trees.Add(Tree.treeTemp);
         trees.Add(Tree.fallTree);
         trees.Add(Tree.treeTemp);
         trees.Add(Tree.fallTree);
         trees.Add(Tree.fellenTree1);
         trees.Add(Tree.fellenTree2);
         
    }



    public static int[,,] Temple = new int[6, 5, 5]{


        {{ 0,0,0,0,0},{ 0,10,10,10,0},{ 0,10,10,10,0 },{  0,10,10,10,0},{ 0,0,0,0,0} },
        {{ 10,10,10,10,10},{ 10,0,0,0,10},{10,0,0,0,10 },{ 10,0,0,0,10},{ 10,10,10,10,10} },
        {{ 10,0,0,0,10},{ 0,0,0,0,0},{0,0,0,0,0},{ 0,0,0,0,0},{ 10,0,0,0,10} },
        {{ 10,0,0,0,10},{ 0,0,0,0,0},{0,0,0,0,0},{ 0,0,0,0,0},{ 10,0,0,0,10} },
        {{ 10,0,0,0,10},{ 0,0,0,0,0},{0,0,0,0,0},{ 0,0,0,0,0},{ 10,0,0,0,10} },
        {{ 10,10,10,10,10},{ 10,10,10,10,10},{10,10,10,10,10 },{ 10,10,10,10,10},{ 10,10,10,10,10} }


    };
}
