using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator
{
    private float _deltaSize;
    private float _borderSize;

    public Vector2 MapSize { get; private set; }
    public Vector2 MapBorder { get; private set; }

    public MapGenerator(Vector2 mapSize, float borderSize, float deltaSize)
    {
        MapSize = mapSize;
        _borderSize = borderSize;
        _deltaSize = deltaSize;
    }

    public void Generate(ObjectFactory factory)
    {
        GameObject ground = factory.Get(GenerateObjectType.Ground);
        GameObject water = factory.Get(GenerateObjectType.Water);

        ground.transform.localScale = new Vector3(MapSize.x, MapSize.y);
        Vector3 waterScale = ground.transform.localScale;
        waterScale.x += _borderSize;
        waterScale.y += _borderSize;
        water.transform.localScale = new Vector3(waterScale.x, waterScale.y);

        MapBorder = ground.transform.localScale / 2;
        MapBorder -= Vector2.one * _deltaSize;
    }
}
