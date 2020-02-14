using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Grid grid;

    private void Start()
    {
        grid = new Grid(4, 2, 10f);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            grid.SetValue(Utils.GetMouseWorldPosition(), 56);

        if (Input.GetMouseButtonDown(1))
            grid.GetValue(Utils.GetMouseWorldPosition());
    }
}
