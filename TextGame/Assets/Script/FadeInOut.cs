using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ���� ���� ���� ����
using UnityEngine.UI; //Ui ���� �Լ� ���� ����

public class FadeInOut : MonoBehaviour
{
    // enum�� ������ switch ���� ����Ҷ� ������ ����
    public enum FadeState { FadeIn = 0, FadeOut, FadeInOut, FadeLoop } //FadeIn�� 0���� �����ϸ� �� ���� 1,2,3.. �̷� ��

    [SerializeField]    //�ν����Ϳ����� ���� ����, �ܺ� ��ũ��Ʈ���� ���� �Ұ�
    [Range(0.01f, 10f)] 
    private float fadeTime; //fadespeed ���� 10�̸� 1�� (���� Ŭ���� ����)
    [SerializeField]
    private FadeState fadeState; //���̵� ȿ�� ����

    Image image;    // Image �� image ������Ʈ�� ���� ����

    // Start is called before the first frame update
    void Start()
    {

        image = gameObject.GetComponent<Image>();   //��ũ��Ʈ�� ��� ������Ʈ�� Image �Ӽ��� ������
        OnFade(FadeState.FadeIn);   //OnFade �Լ��� ���� ���Ѷ� Fade ���´� FadeIn

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
            color = image.color;

            //Mathf.Lerp�� ���� ������ ����Ͽ� �ε巯�� �������� ǥ�� 
            //�� �� Start , end�� ������� �� ���� ������ ��(percent)�� ���� 
            color.a = Mathf.Lerp(start, end, percent);

            image.color = color;    //image Į�� ���� ����

            yield return null;

        }

        this.gameObject.SetActive(false);   //������Ʈ �� Ȱ��ȭ
    }

}
