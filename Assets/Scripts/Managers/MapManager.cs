using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager
{
    // 직접호출 x, player onTrigger로만 호출
    public GameObject LoadMap(int mapid)
    {
        string mapName = "Map_" + mapid.ToString("000");
        GameObject map = Managers.Resource.Instantiate($"Map/{mapName}");


        return map;
    }

    // 시작 맵설정
    // public void SetdefaultMap()
    // {
    //     Managers.Resource.Instantiate("Map/Map_default");
    //     GameObject map = Managers.Resource.Instantiate($"Map/Map_001");

    //     float mapWidth = Managers.Map.GetMapWorldWidth(map);
    //     map.transform.position = new Vector3(mapWidth, 0);
    // }


    // public float GetMapWorldWidth(GameObject tilemapObj)
    // {
    //     Tilemap tilemap = tilemapObj.GetComponent<Tilemap>();

    //     if (tilemap == null)
    //     {
    //         Debug.Log("fail to find tilemap. default = 20");
    //         return 20;
    //     }

    //     tilemap.CompressBounds();
    //     Bounds tilemapBounds = tilemap.localBounds;
    //     Vector3 worldSize = Vector3.Scale(tilemapBounds.size, tilemap.transform.lossyScale);
    //     return worldSize.x;
    // }


    // ----SetItem()--------

    //     List<GameObject> Item = new List<GameObject>();
    //     GameObject[] Maps = Resources.LoadAll<GameObject>("Prefabs/Map");
    //     List<Transform> _env = new List<Transform>();
    //     List<GameObject> _itemList = new List<GameObject>();

    //     for (int i = 0; i < Maps.Length; i++)
    //     {
    //         _env[i] = Maps[i].transform.GetChild(1);
    //         _itemList[i] = _env[i].transform.GetChild(i).gameObject;
    //     }
    //     foreach (GameObject _item in _itemList)
    //     {
    //         if (_item.name.Contains("_Item"))
    //             Item.Add(_item);
    //     }
    
    //     Item.SetActive(true);

}
