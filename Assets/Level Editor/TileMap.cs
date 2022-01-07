using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public static TileMap sSingleton { get; private set; }
    private Dictionary<Vector2, Block> _tiles;
    [SerializeField]
    private GameObject _basicTile;
    [SerializeField]
    private Sprite[] _tileCornerSprites;

    public Sprite[] pTileCornerSprites => _tileCornerSprites;

    private const int AdjacentTileCount = 8;

    private void Awake()
    {
        sSingleton = this;
        _tiles = new Dictionary<Vector2, Block>();
    }

    public void PlaceTile(Vector2 pos)
    {
        var tilePos = GetTilePositon(pos);
        if (!_tiles.ContainsKey(tilePos))
        {
            var tile = Instantiate(_basicTile, tilePos, Quaternion.identity).GetComponent<Block>();
            NotifyAdjacentTiles(true, tilePos, tile);
            _tiles.Add(tilePos, tile);
        }
    }

    private void NotifyAdjacentTiles(bool add, Vector2 tilePos, Block tile)
    {
        for (int i = 0; i < AdjacentTileCount; ++i)
        {
            var directionEnum = DirectionUtility.GetDirectionEnum(i);
            Block adjacentTile;
            if (_tiles.TryGetValue(tilePos.MoveVector(directionEnum), out adjacentTile))
            {
                tile.AdjacentTileChanged(add, directionEnum);
                adjacentTile.AdjacentTileChanged(add, directionEnum.Reverse());
            }
        }
    }

    public void RemoveTile(Vector2 pos)
    {
        var tilePos = GetTilePositon(pos);
        Block tile;
        if (_tiles.TryGetValue(tilePos, out tile))
        {
            _tiles.Remove(tilePos);
            NotifyAdjacentTiles(false, tilePos, tile);
            Destroy(tile.gameObject);
        }
    }

    private Vector2 GetTilePositon(Vector2 pos)
    {
        var x = Mathf.RoundToInt(pos.x);
        var y = Mathf.RoundToInt(pos.y);
        return new Vector2(x, y);
    }


}
