using UnityEngine;

public class Equipment
{

}

public class WoodenSword : Equipment
{

}

public class WoodenArmor : Equipment
{

}

public class Monster : MonoBehaviour
{
    public static Monster Create()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = "Moster";

        // 몬스터 생성하는 스태틱
        // 실제로 한다면 큐브 대신 프리팹을 생성.
        return go.AddComponent<Monster>();
    }

    public Monster SetPosition(Vector3 position)
    {
        transform.position = position;

        //함수가 호출된 당사자 인스턴스를 반환하겠다
        return this;
    }

    public Monster SetScale(Vector3 scale)
    {
        transform.localScale = scale;

        return this;
    }

    public Monster AddEquipment<T>() where T : Equipment
    {
        // 몬스터의 장비를 추가함
        
        return this;
    }
}
