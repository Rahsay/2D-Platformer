using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private float _colorShade;
    [SerializeField]
    private Color _color;
    [SerializeField]
    private BlockAdjacencySprites[] _adjacencySprites;

    private DirectionEnum _adjacentTiles;

    private void Awake()
    {
        SetColor(_color);
    }


    public void SetColor(Color color)
    {
        _color = color;

        var shade = _colorShade + 1;
        color.r *= shade;
        color.g *= shade;
        color.b *= shade;
        _spriteRenderer.color = color;

        for (int i = 0; i < _adjacencySprites.Length; ++i)
            _adjacencySprites[i].SetColor(_color);
    }
    public void AdjacentTileChanged(bool add, DirectionEnum direction)
    {
        if (add)
            _adjacentTiles |= direction;
        else
            _adjacentTiles &= ~direction;

        for (int i = 0; i < _adjacencySprites.Length; ++i)
        {
            var adjacencySprite = _adjacencySprites[i];
            var adjacencyDirection = direction | direction.GetClockwise() | direction.GetCounterClockwise()|direction.GetClockwise(2) | direction.GetCounterClockwise(2);
            if ((adjacencySprite.DirectionEnum & adjacencyDirection) != DirectionEnum.None)
            {
                adjacencySprite.Rebuild(_adjacentTiles);
            }
        }
    }

    [System.Serializable]
    private class BlockAdjacencySprites
    {
        public DirectionEnum DirectionEnum;
        public SpriteRenderer MainSR;
        public SpriteRenderer CounterClockwiseSR;
        public SpriteRenderer ClockwiseSR;
        public float ColorShade;

        public void SetColor(Color color)
        {
            var shade = ColorShade + 1;

            color.r *= shade;
            color.g *= shade;
            color.b *= shade;
            MainSR.color = color;
            CounterClockwiseSR.color = color;
            ClockwiseSR.color = color;
        }
        public void Rebuild(DirectionEnum direction)
        {
            if (direction.HasFlag(DirectionEnum))
            {
                MainSR.enabled = false;
                CounterClockwiseSR.enabled = false;
                ClockwiseSR.enabled = false;
            }
            else
            {
                MainSR.enabled = true;
                CounterClockwiseSR.enabled = true;
                ClockwiseSR.enabled = true;

                var sprite = TileMap.sSingleton.pTileCornerSprites[0];

                if (!direction.HasFlag(DirectionEnum.GetCounterClockwise()))
                {
                    if (direction.HasFlag(DirectionEnum.GetCounterClockwise(2)))
                      sprite = TileMap.sSingleton.pTileCornerSprites[1]; 
                    else
                      sprite = TileMap.sSingleton.pTileCornerSprites[2];
                }
                CounterClockwiseSR.sprite = sprite;

                sprite = TileMap.sSingleton.pTileCornerSprites[0];

                if (!direction.HasFlag(DirectionEnum.GetClockwise()))
                {
                    if (direction.HasFlag(DirectionEnum.GetClockwise(2)))
                        sprite = TileMap.sSingleton.pTileCornerSprites[1];
                    else
                        sprite = TileMap.sSingleton.pTileCornerSprites[2];
                }
                ClockwiseSR.sprite = sprite;
            }
        }
    }
}
