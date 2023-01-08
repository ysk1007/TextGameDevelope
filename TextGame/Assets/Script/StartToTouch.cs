using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Ui 관련 함수 참조 선언
using TMPro; //텍스트 mesh pro 관련 함수 참조 선언
using UnityEngine.SceneManagement; // 씬 관련 제어 참조 선언

public class StartToTouch : MonoBehaviour
{
    // enum은 열거형 switch 문을 사용할때 가독성 좋음
    public enum FadeState { FadeIn = 0, FadeOut, FadeInOut, FadeLoop } //FadeIn을 0으로 지정하면 그 다음 1,2,3.. 이런 식

    [SerializeField]    //인스펙터에서는 접근 가능, 외부 스크립트에서 접근 불가
    [Range(0.01f, 10f)]
    private float fadeTime; //fadespeed 값이 10이면 1초 (값이 클수록 빠름)
    [SerializeField]
    private FadeState fadeState; //페이드 효과 상태

    TextMeshProUGUI text;   //텍스트 오브젝트를 담을 변수
    public Image image; //Fade 용 이미지를 담을 변수
    Button stt_btn; //버튼 오브젝트를 담을 변수

    // Start is called before the first frame update
    void Start()
    {
        stt_btn = gameObject.GetComponent<Button>();    //버튼 속성 담음
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();    //텍스트 속성 담음
        OnFade(FadeState.FadeLoop);  //text에 OnFade 함수를 실행 시켜라 Fade 상태는 FadeLoop
        StartCoroutine(Fade(1, 0, image));  //시작할때 Image 화면 FadeIn
    }

    public void OnFade(FadeState state) //Fade 상태에 따라 Fade 효과를 주는 함수
    {
        fadeState = state;  //받은 매개변수를 fadeState에 저장

        switch (fadeState)
        {
            case FadeState.FadeIn:  //상태가 FadeIn 이라면
                StartCoroutine(Fade(1, 0)); //Fade 코루틴 시작 매개변수는 1,0
                break;
            case FadeState.FadeOut:
                StartCoroutine(Fade(0, 1)); //Fade 코루틴 시작 매개변수는 0,1
                break;
            case FadeState.FadeInOut:   //상태가 FadeInOut 이라면
            case FadeState.FadeLoop:    //상태가 FadeLoop 이라면
                StartCoroutine(FadeinOut());    //FadeinOut 코루틴 시작
                break;
        }
    }

    public IEnumerator FadeinOut()  //FadeIn, Out 을 반복하는 코루틴
    {
        while (true)    //While 무한문
        {
            yield return StartCoroutine(Fade(1, 0));    //FadeIn 효과 

            yield return StartCoroutine(Fade(0, 1));    //FadeOut 효과 


            if (fadeState == FadeState.FadeInOut)   //만약 FadeInOut 상태라면(1회) 무한문 빠져 나옴
            {
                break;
            }
        }
    }

    public IEnumerator Fade(float start, float end) //실질적 Fade 이펙트를 담당하는 코루틴
    {
        //매개변수는 start , end 

        float currentTime = 0.0f;   //현재 시간
        float percent = 0.0f;   // 진행도

        while (percent < 1) //진행도가 100퍼센트가 되기 전까지
        {
            currentTime += Time.deltaTime;  //현재 시간에 +Time.deltaTime

            //진행도에 현재 시간 / fadeTime 을 넣음 fadeTime이 분수니까 작을수록 진행도가 더 빠름
            percent = currentTime / fadeTime;

            Color color;
            color = text.color;
            //Mathf.Lerp는 선형 보간을 사용하여 부드러운 움직임을 표현 
            //두 점 Start , end로 만들어진 두 직선 사이의 값(percent)를 구함 
            color.a = Mathf.Lerp(start, end, percent);
            text.color = color;  //text 칼라에 투명도 적용

            yield return null;

        }
    }

    //위 코루틴과 같은 효과이고 매개변수 image를 추가해 image 전용 Fade 코루틴
    public IEnumerator Fade(float start, float end, Image image)    
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color;
            color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;

        }
    }

    public void OnClick()   //씬 전환 함수
    {
        stt_btn.interactable = false;   //STT 버튼 클릭을 비활성화
        StartCoroutine(Fade(0, 1, image));  // 화면 FadeOut
        Invoke("ChangeScene", 2f);  //2초 뒤 씬 전환
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Main_Scene");   //메인 씬을 불러옵니다
    }


}