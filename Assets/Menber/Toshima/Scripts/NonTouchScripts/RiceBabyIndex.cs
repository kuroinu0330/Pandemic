using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceBabyIndex : MonoBehaviour
{
    //プレイヤーのオブジェクトに設定するタグの文字列
    [SerializeField]
    private string RiceBallTag;

    //エネミーのオブジェクトに設定するタグの文字列
    [SerializeField]
    private string BreadTag;

    /// <summary>
    /// 当たり判定に触れた際に実行する関数
    /// </summary>
    /// <param name="obj">当たったオブジェクトの情報を入れる(タグを取得するため「String」でも良い、ただその際は引数の型も変更する必要がある)</param>
    public void HarvestThis(GameObject obj)
    {
        //オブジェクトのタグがプレイヤーのものだった場合、以下の処理を実行する
        if(obj.tag == RiceBallTag)
        {
            //プレイヤー取得時の処理を記載
            //加点処理


            //米粒消去処理
            Destroy(this.gameObject);
        }
        //オブジェクトのタグがエネミーのものだった場合、以下の処理を実行する
        else if(obj.tag == BreadTag)
        {
            //エネミー取得時の処理を記載
            //エネミー増殖処理


            //米粒消去処理
            Destroy(this.gameObject);
        }
        else
        {
            //不具合が発生した時のデバックログ
            Debug.Log("[不具合]正常にタグが設定されていない可能性を検知");
        }
    }
}