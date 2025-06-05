using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class Tweening : MonoBehaviour
{
    [SerializeField]
    private Vector3 vector;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float Power;
    [SerializeField]
    private int count;
    [SerializeField]
    private Ease ease;
    [SerializeField]
    private LoopType loopType;

    //조금 더 세부적인 커브가 필요하면 지정된 ease 대신 curve를 사용한다.
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private bool isSpeedBased;

    private GameObject _cube;
    private Tween _tween;

    [SerializeField]
    private Image img;
    [SerializeField]
    private Text txt;
    [SerializeField]
    private string message;

    private void Awake()
    {
        _cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    void Update()
    {

    }

    [Button]
    private void Move()
    {
        List<int> list = new();

        // Fluent Method Chaining 플루언트 메서드 체이닝
        // 반환값을 계속 맞춰주면 된다
        // 빌더 패턴, 팩토리, 트윈, 객체 초기화


        //만약 무한반복 시키고 싶으면 Count에 -1을 주면 무한반복된다.
        _tween = _cube.transform.DOMove(endValue: vector, duration: duration)
            .SetEase(ease)
            .SetLoops(loops: count, loopType)
            // 이걸 true로 바꾸면 기존 duration의 역할이 바뀐다.
            .SetSpeedBased(isSpeedBased)
            //트윈이 시작을 할때 delegate를 실행하게 된다.
            .OnStart(() => Debug.Log("Tween시작."))
            //트윈이 끝날 때, 특정한 작업이 끝났을 때, 추가로 할 작업을 지정. 즉 '콜백'한다.
            .OnComplete(() => Debug.Log("Tween끝."))
            .OnUpdate(() => Debug.Log("Tween실행중."));
    }

    [Button]
    private void Rotation()
    {
        _tween = _cube.transform.DORotate(endValue: vector, duration: duration, RotateMode.FastBeyond360);
    }

    [Button]
    private void Scale()
    {
        _tween = _cube.transform.DOScale(endValue: vector, duration: duration);
    }

    [Button]
    private void Jump()
    {
        _tween = _cube.transform.DOJump(endValue: vector, jumpPower: Power, numJumps: count, duration: duration);
    }

    [Button]
    private void Shake()
    {
        _tween = _cube.transform.DOShakePosition(duration, strength: vector, vibrato: count);
    }

    [Button]
    private void ResetCube()
    {
        _tween.Kill();

        _cube.transform.position = Vector3.zero;
        //_cube.transform.rotation = Vector3
        _cube.transform.localScale = Vector3.one;
    }

    IEnumerator MyEnumerator()
    {
        Debug.Log("코루틴 시작");
        
        yield return _cube.transform.DOMove(vector, duration)
            .WaitForCompletion();

        Debug.Log("코루틴 종료");
    }


    [Button]
    private void Fade()
    {
        //Image가 상속받는 Graphic을 상속 받는 것들은 모두 사용 가능.
        // txt 등등
        img.DOFade(endValue: Power, duration: duration);
    }

    [Button]
    private void ChangeText()
    {
        // To가 모든 DoTween함수들의 원형
        // getter = 변화시키고자 하는 값을 불러올 때,
        DOTween.To(getter: () => txt.text, setter: x => txt.text = x, endValue : message, duration: duration);
    }

}
