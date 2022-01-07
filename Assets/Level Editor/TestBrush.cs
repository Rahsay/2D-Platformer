using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBrush : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TileMap.sSingleton.PlaceTile(pos);
        }
        if (Input.GetMouseButton(1))
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TileMap.sSingleton.RemoveTile(pos);
        }
    }
}
