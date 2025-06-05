using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{

}

public class TallTree : MonoBehaviour
{

}
public class IEnumeratorTest : MonoBehaviour
{
    float duration;
    GameObject _cube;
    Vector3 _dest;
    private IEnumerator MoveCube()
    {
        float elapsed = 0f;

        Vector3 start = _cube.transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;

            _cube.transform.position = Vector3.Lerp(start, _dest, t);

            yield return null;
        }
    }
    Player player = new Player();
    TallTree tt = new TallTree();
    protected Stack<GameObject> _object = new Stack<GameObject>();

    public int Object => _object.Count;
    public void AddToPile(GameObject go, bool jump = false)
    {
        _object.Push(go);

        Vector3 pos;
    }

    public void Update()
    {
        StartCoroutine(nameof(MoveCube));
    }

    private Vector3 GetPositionAt(Player player, TallTree talltree)
    {
        Vector3 offset;
        return new Vector3();
    }

    //확장 메서드
    
}
