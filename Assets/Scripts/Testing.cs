using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Grid<bool> grid;

    void Start()
    {
        grid = new Grid<bool>(20, 10, 8f, new Vector3(-80f, -40f, 0f), (Grid<bool> g, int x, int y) => new bool());
    }
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 position = Utils.GetMouseWorldPosition();
            grid.SetGridValue(position, true);
        }
    }
}
