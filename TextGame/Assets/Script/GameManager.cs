using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //텍스트 mesh pro 관련 함수 참조 선언

public class GameManager : MonoBehaviour
{
    public int player_str   =   5;
    public int player_dex   =   6;
    public int player_agi  =   3;
    public int player_luk = 0;

    public TextMeshProUGUI str_text;
    public TextMeshProUGUI dex_text;
    public TextMeshProUGUI agi_text;
    public TextMeshProUGUI luk_text;

    public bool item_overlap = false;
    // Start is called before the first frame update
    void Start()
    {
        stats_update();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void item_equip(int item_str, int item_dex, int item_agi, int item_luk)
    {
        player_str += item_str;
        player_dex += item_dex;
        player_agi += item_agi;
        player_luk += item_luk;
        stats_update();
    }

    public void item_dequip(int item_str, int item_dex, int item_agi, int item_luk)
    {
        if (!item_overlap)
        {
            player_str -= item_str;
            player_dex -= item_dex;
            player_agi -= item_agi;
            player_luk -= item_luk;
        
        }
        stats_update();
        item_overlap = false;
    }

    public void stats_update()
    {
        str_text.text = player_str.ToString();
        dex_text.text = player_dex.ToString();
        agi_text.text = player_agi.ToString();
        luk_text.text = player_luk.ToString();
    }
}
