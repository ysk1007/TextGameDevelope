using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stroy : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public GameObject question_obj;
    public string input;

    private void Start()
    {
        input = Text.text;

        question_obj.SetActive(false);  // 선택지 보이지 않게 설정
        StartCoroutine(OnTyping());     // 타이핑 이펙트 시작
    }

    IEnumerator OnTyping()
    {
        string output;
        string mainstry = Text.text;        // mainstry에 유니티에서 가져온 Text문장을 저장
        Text.text = "";                     // 유니티에서 가져온 Text 초기화

        yield return new WaitForSeconds(0.5f);      // 0.5f 이후 문장 보여줌

        for(int i = 0; i <= mainstry.Length; i++)   // mainstry의 길이 만큼 반복
        {
            output = mainstry.Substring(0, i);      // mainstry안에 저장된 문자의 처음부터 i번째까지 변수 output에 저장
            Text.text = output;                     // 유니티의 Text = output
            yield return new WaitForSeconds(0.07f); // 0.07f의 간격을 두고 글자가 나타남
        }

        question_obj.SetActive(true);               // 선택지 보이게 설정
    }
    
    public void Skip()  // Skip 모션
    {
        StopAllCoroutines();    // 코루틴 전체 정지

        Text.text = input;      

        question_obj.SetActive(true);   // 선택지 보이게 설정
    }

}