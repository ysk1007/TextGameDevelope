using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropUi : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    public enum DropUiType{ inven, weapon, cloths, ring, hat }
    public DropUiType uitype;
    private Image image;
    private RectTransform rect;
    public GameManager gameManager;

    private void Awake()
    {
        
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    //마우스 포인터가 현재 아이템 슬롯 영역 내부로 들어갈 때 1회 호출
    public void OnPointerEnter(PointerEventData eventData)
    {
        //아이템 슬롯의 색상 변경
        image.color = Color.yellow;
    }



    //마우스 포인터가 현재 아이템 슬롯 영역을 빠져 나갈 때 1회 호출
    public void OnPointerExit(PointerEventData eventData)
    {
        //아이템 슬롯의 색상 변경
        image.color = Color.white;
    }   
    
    //현재 아이템 슬롯 영역 내부에서 드롭 했을 때 1회 호출
    public void OnDrop(PointerEventData eventData)
    {
        //pointerDrag는 현재 드래그 하고있는 대상(=아이템)
        if ( eventData != null)
        {
            eventData.pointerDrag.transform.SetParent(transform);
            string past_sloat = eventData.pointerDrag.GetComponent<DragUi>().previousParent.GetComponent<RectTransform>().gameObject.name;
            eventData.pointerDrag.GetComponent<DragUi>().previousParent.GetComponent<Image>().color = Color.white;

            DropUi drop_slot = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().GetComponentInParent<DropUi>();
            Debug.Log(eventData.pointerDrag.transform.GetComponentInParent<item_stats>().name);
            if(drop_slot.GetComponentInChildren<item_stats>().name != eventData.pointerDrag.transform.GetComponentInParent<item_stats>().name)
            {
                Debug.Log("중첩");
                DragUi ui = eventData.pointerDrag.GetComponent<DragUi>();
                gameManager.item_overlap = true;
                ui.posreset();
            }

            else if (drop_slot.GetComponent<DropUi>().uitype != DropUiType.inven &&
                drop_slot.GetComponent<DropUi>().uitype.ToString() != eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_type.ToString())
            {
                Debug.Log("템칸 다름");
                DragUi ui = eventData.pointerDrag.GetComponent<DragUi>();
                gameManager.item_overlap = true;
                ui.posreset();
            }
            else
            {
                int item_str = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_str;
                int item_dex = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_dex;
                int item_agi = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_agi;
                int item_luk = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_luk;
                string type = eventData.pointerDrag.transform.GetComponentInParent<item_stats>().item_type.ToString();
                string Ui = uitype.ToString();

                // 드래그 하고있는 대상의 부모를 현재 오브젝트로 설정하고, 위치를 현재 오브젝트 위치와 동일하게 설정
                eventData.pointerDrag.transform.SetParent(transform);
                eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;


                if (Ui == type && past_sloat != Ui)
                {
                    gameManager.item_equip(item_str, item_dex, item_agi, item_luk);
                }
            }


        }

    }
}
