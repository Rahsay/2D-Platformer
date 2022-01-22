using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{

    private bool hasTileSelected;
    private Block selectedBlock;
    [SerializeField]
    private SpriteRenderer underCursorSprite;
    private Transform underCursorSpriteTransform;

    public Block[] blocks;
    // Start is called before the first frame update
    void Start()
    {
        underCursorSpriteTransform = underCursorSprite.transform;
    }

    Vector2 position;
    Vector2 world_position;

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (hasTileSelected)
        {
            GetTileSelectedInput();
        }
        else if (Input.GetMouseButton(1))
        {
            GetTileRemovingInput();
        }
    }

    private void GetTileSelectedInput()
    {
        position = Input.mousePosition;
        world_position = Camera.main.ScreenToWorldPoint(position);
        underCursorSpriteTransform.position = world_position;
        if (Input.GetMouseButtonDown(1))
            ChangeCursorToNormal();
        else if (Input.GetMouseButton(0))
            PlaceTile(world_position);
    }

    private void GetTileRemovingInput()
    {
        position = Input.mousePosition;
        world_position = Camera.main.ScreenToWorldPoint(position);
        RemoveTile(world_position);
    }

    private void PlaceTile(Vector2 pos)
    {
        TileMap.sSingleton.PlaceTile(pos);
    }

    private void RemoveTile(Vector2 pos)
    {
        TileMap.sSingleton.RemoveTile(pos);
    }

    private void ChangeCursorToNormal()
    {
        hasTileSelected = false;
        underCursorSprite.enabled = false;
    }

    private void ChangeCursorToTile()
    {
        underCursorSprite.sprite = selectedBlock.GetDefaultSprite();
        underCursorSprite.enabled = true;
        hasTileSelected = true;
    }

    public void SelectTile(int index)
    {
        if (index >= 0 && index < blocks.Length)
        {
            selectedBlock = blocks[index];
            ChangeCursorToTile();
        }
    }


}
