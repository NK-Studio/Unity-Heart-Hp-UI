using UnityEngine;
using UnityEngine.UI;

public class HpUI2 : MonoBehaviour
{
    //사용중인 하트 UI를 모아놓은 집합체
    public Image[] Heart;

    //읽는건 자유, 쓰는 건 private로 표기한다.
    public float Hp { get; private set; }

    //Hp의 최대치 정의
    private float Hp_Max;

    //초기화를 담당함
    private void Awake()
    {
        //귀찮으니까 부모를 찾은 다음 자식으로 컴포넌트를 스위칭 한다.
        for (int i = 0; i < Heart.Length; i++)
            Heart[i] = Heart[i].transform.GetChild(0).GetComponent<Image>();

        //Hp_Max의 사이즈를 정의
        Hp_Max = Heart.Length;

        //Hp 초기화.
        Hp = Hp_Max;
    }
    
    [ContextMenu("추가 초기화")] //->다이렉트로 추가했다면, 초기화를 해주어야 한다.
    public void ExInit()
    {
        //자식의 수 만큼 공간을 확보함
        Heart = new Image[transform.childCount];

        //이미지 컴포넌트 참조
        for (int i = 0; i < Heart.Length; i++)
            Heart[i] = transform.GetChild(i).GetChild(0).GetComponent<Image>();

        //전체 Hp초기화
        Hp_Max = Heart.Length;

        //Hp_Max값을 넘기지 못하도록 처리함
        Hp = Mathf.Clamp(Hp, 0, Hp_Max);

        //다시 그리기 ->
        //전체적으로 안보이게 한다.
        for (int i = 0; i < Hp_Max; i++)
        {
            //전부 비어둔 상태에서,
            Heart[i].fillAmount = 0;

            //Pull로 채워줘야 되는것은 100% 채우고
            if ((int)Hp > i)
            {
                Heart[i].fillAmount = 1;
            }

            //세밀하게 이미지를 그릴 수 있도록 함
            if ((int)Hp == i)
                Heart[i].fillAmount = Hp - (int)Hp; //ex -> Hp = 3.4의 경우 3.4에 3을 빼면 0.4만 남는다.
        }
    }

    //Hp의 쓰기를 담당하는 함수
    public void SetHp(float val)
    {
        //값 적용
        Hp += val;

        //Hp_Max값을 넘기지 못하도록 처리함
        Hp = Mathf.Clamp(Hp, 0, Hp_Max);

        //전체적으로 안보이게 한다.
        for (int i =0; i < Hp_Max; i++)
            Heart[i].fillAmount = 0;

        //전체적으로 안보이게 한다.
        for (int i = 0; i < Hp_Max; i++)
        {
            //전부 비워둔 상태에서,
            Heart[i].fillAmount = 0;

            //Pull로 채워줘야 되는것은 100% 채우고
            if ((int)Hp > i)
            {
                Heart[i].fillAmount = 1;
            }

            //세밀하게 이미지를 그릴 수 있도록 함
            if ((int)Hp == i)
                Heart[i].fillAmount = Hp - (int)Hp;  //ex -> Hp = 3.4의 경우 3.4에 3을 빼면 0.4만 남는다.
        }
    }
}
