using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Ranking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // IEnumerator RankingSet() 
    // {
    //     var url = "https://docs.google.com/spreadsheets/d/" + SheetID + "/gviz/tq?tqx=out:csv&sheet=" + SheetName;        
    //     UnityWebRequest request = UnityWebRequest.Get(url);
    //     yield return request.SendWebRequest();
    //     var reader = new StringReader(t);
    //     reader.ReadLine();  //ヘッダ読み飛ばし
    //     var rows = new List<string[]>();
    //     while (reader.Peek() >= 0)
    //     {
    //         var line = reader.ReadLine();        // 一行ずつ読み込み
    //         var elements = line.Split(',');    // 行のセルは,で区切られる
    //         for (var i = 0; i < elements.Length; i++)
    //         {
    //             elements[i] = elements[i].TrimStart('"').TrimEnd('"');
    //         }
    //         rows.Add(elements);
    //         yield return null;
    //     }
    //     yield break;
    //     S 読み込みは簡単だけど書き込みめんどくさいわ、ネット環境ないと無理になるしcsv格納した方が無難かもしれんねすまないえへ;
    //     ps.ガチャが引きたいです;
    //     そろそろ一回戻ろうかな;
    //     昼休み何時まで？飯食いたい;
    //     BattleBitがやりたいです;
    // }
}
