using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Ui ���� �Լ� ���� ����
using TMPro; //�ؽ�Ʈ mesh pro ���� �Լ� ���� ����
using UnityEngine.SceneManagement; // �� ���� ���� ���� ����

public class StartToTouch : MonoBehaviour
{
    // enum�� ������ switch ���� ����Ҷ� ������ ����
    public enum FadeState { FadeIn = 0, FadeOut, FadeInOut, FadeLoop } //FadeIn�� 0���� �����ϸ� �� ���� 1,2,3.. �̷� ��

    [SerializeField]    //�ν����Ϳ����� ���� ����, �ܺ� ��ũ��Ʈ���� ���� �Ұ�
    [Range(0.01f, 10f)]
    private float fadeTime; //fadespeed ���� 10�̸� 1�� (���� Ŭ���� ����)
    [SerializeField]
    private FadeState fadeState; //���̵� ȿ�� ����

    TextMeshProUGUI text;   //�ؽ�Ʈ ������Ʈ�� ���� ����
    public Image image; //Fade �� �̹����� ���� ����
    Button stt_btn; //��ư ������Ʈ�� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        stt_btn = gameObject.GetComponent<Button>();    //��ư �Ӽ� ����
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();    //�ؽ�Ʈ �Ӽ� ����
        OnFade(FadeState.FadeLoop);  //text�� OnFade �Լ��� ���� ���Ѷ� Fade ���´� FadeLoop
        StartCoroutine(Fade(1, 0, image));  //�����Ҷ� Image ȭ�� FadeIn
    }

    public void OnFade(FadeState state) //Fade ���¿� ���� Fade ȿ���� �ִ� �Լ�
    {
        fadeState = state;  //���� �Ű������� fadeState�� ����

        switch (fadeState)
        {
            case FadeState.FadeIn:  //���°� FadeIn �̶��
                StartCoroutine(Fade(1, 0)); //Fade �ڷ�ƾ ���� �Ű������� 1,0
                break;
            case FadeState.FadeOut:
                StartCoroutine(Fade(0, 1)); //Fade �ڷ�ƾ ���� �Ű������� 0,1
                break;
            case FadeState.FadeInOut:   //���°� FadeInOut �̶��
            case FadeState.FadeLoop:    //���°� FadeLoop �̶��
                StartCoroutine(FadeinOut());    //FadeinOut �ڷ�ƾ ����
                break;
        }
    }

    public IEnumerator FadeinOut()  //FadeIn, Out �� �ݺ��ϴ� �ڷ�ƾ
    {
        while (true)    //While ���ѹ�
        {
            yield return StartCoroutine(Fade(1, 0));    //FadeIn ȿ�� 

            yield return StartCoroutine(Fade(0, 1));    //FadeOut ȿ�� 


            if (fadeState == FadeState.FadeInOut)   //���� FadeInOut ���¶��(1ȸ) ���ѹ� ���� ����
            {
                break;
            }
        }
    }

    public IEnumerator Fade(float start, float end) //������ Fade ����Ʈ�� ����ϴ� �ڷ�ƾ
    {
        //�Ű������� start , end 

        float currentTime = 0.0f;   //���� �ð�
        float percent = 0.0f;   // ���൵

        while (percent < 1) //���൵�� 100�ۼ�Ʈ�� �Ǳ� ������
        {
            currentTime += Time.deltaTime;  //���� �ð��� +Time.deltaTime

            //���൵�� ���� �ð� / fadeTime �� ���� fadeTime�� �м��ϱ� �������� ���൵�� �� ����
            percent = currentTime / fadeTime;

            Color color;
            color = text.color;
            //Mathf.Lerp�� ���� ������ ����Ͽ� �ε巯�� �������� ǥ�� 
            //�� �� Start , end�� ������� �� ���� ������ ��(percent)�� ���� 
            color.a = Mathf.Lerp(start, end, percent);
            text.color = color;  //text Į�� ���� ����

            yield return null;

        }
    }

    //�� �ڷ�ƾ�� ���� ȿ���̰� �Ű����� image�� �߰��� image ���� Fade �ڷ�ƾ
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

    public void OnClick()   //�� ��ȯ �Լ�
    {
        stt_btn.interactable = false;   //STT ��ư Ŭ���� ��Ȱ��ȭ
        StartCoroutine(Fade(0, 1, image));  // ȭ�� FadeOut
        Invoke("ChangeScene", 2f);  //2�� �� �� ��ȯ
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Main_Scene");   //���� ���� �ҷ��ɴϴ�
    }


}