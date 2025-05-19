using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager
{
    // 외부에서 플레이어 정보를 받고 싶을때,
    // 어떤 경로로?
    // Player에서 직접 주는게 맞아.
    // 하지만 이렇게 하면 Manager의 의미가 없어져.
    // 즉 Manager.Char.Player.value?
    // 이건 너무 긴데...
    // 또 이것도 Player에 기능을 다 추가했을 때의 일이고,
    // 즉 Manager.Char.Player.PlayerCondition.value?
    // 헐......
    // 이걸 해결하려면 PlayerCondition의 기능을 플레이어안에서 구현하면 되긴해.

    // 그리고 UI... 변수를 UI에서 선언하고, 그걸 Player에서 받아오는 게 맞나?
    // 절대 아니야

    // 그렇지만... 저 많은 스킬들을 Player안에서 모두 구현하는 것도 말이 안되.
    // 클래스를 쪼개야 하는데,

    // 그렇다고 그걸 CharacterManager에서 공용기능을 만들어 도와주는 것도 말이 안돼.
    // 그건 캐릭터 클래스 아래의, 오직 캐릭터 클래스와만 대화를 하는 하위클래스들이 알아서 할 일이야.

    // 즉 이것의 역할은 플레이어의 연결통로 외의 역할을 하면 헷갈려.
    // 그렇다고 플레이어의 데이터만 받기엔 만든 의미가 없어.
    // 그냥 플레이어든, 다른 오브젝트든, 적이든 그걸 총괄해서 관리하는게 맞을까?

    // 그럴꺼면 그렇게 받은 데이터를 이 CharacterManager필드에 작성된 딕셔너리에 넣어서 보관하고
    // 또 그렇게 딕셔너리에 주고 받는 기능을 여기서 작성해서 쓰면 되겠다.


    public Player _player;
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    public void OnUpdate()
    {
        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();
    }

    public Action KeyAction = null;


}
