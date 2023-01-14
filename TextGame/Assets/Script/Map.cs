using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
  
    public void Location()
    {
        // 지도를 맨 처음 위치로 옮김(254, 451, 0은 전체 유니티에서의 좌표값)
        transform.position = new Vector3(254, 451, 0);      
    }
}
