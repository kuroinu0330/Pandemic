using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileIndex : MonoBehaviour
{
    //プレイヤーのオブジェクトのみを認知するためのタグ照合用テキスト
    [SerializeField]
    private string playerTag;

    //タイル同士を区別するためのナンバリング(タイルのナンバリングを二次元化)
    [SerializeField]
    //private int _mapTileNumber;
    private Vector2 _mapTileCoordinateNum;

    // /// <summary>
    // /// タイルのナンバーを確認するための処理
    // /// </summary>
    // /// <returns>タイルのナンバー</returns>
    // public int GetThisTileNumber()
    // {
    //     //タイルのナンバーを返す
    //     return _mapTileNumber;
    // }

    /// <summary>
    /// タイルのナンバーを確認するための処理(新)
    /// </summary>
    /// <returns>タイルのナンバー</returns>
    public Vector2 GetThisTileNumber()
    {
        //タイルのナンバーを返す
        return _mapTileCoordinateNum;
    }

    // /// <summary>
    // /// タイルのナンバーを変更するための処理(旧、タイルナンバーを二次元化させたため)
    // /// </summary>
    // /// <param name="num">タイルのナンバー</param>
    // public void SetThisTileNumber(int num)
    // {
    //     //タイルのナンバーを上書きする
    //     _mapTileNumber = num;
    // }

    /// <summary>
    /// タイルのナンバーを変更するための処理(新)
    /// </summary>
    /// <param name="CoordinateNum">タイルの二次元化されたナンバー</param>
    public void SetThisTileNumber(Vector2 CoordinateNum)
    {
        //タイルのナンバーを上書きする
        _mapTileCoordinateNum = CoordinateNum;
    }

    /// <summary>
    /// プレイヤーがタイルを踏んだ場合マップスクロール用の配列に自身をねじ込む行為
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
       //Debug.Log("ヴァニってる");
        if(playerTag == other.gameObject.tag)
        {
            TileScrollManager.Instance.AddTriggersObject(this.gameObject);
        }
    }

    /// <summary>
    /// プレイヤーがタイル上から離れた場合マップスクロール用の配列から自身を除外する
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("ふぁっきゅー");
        if(playerTag == other.gameObject.tag)
        {
            TileScrollManager.Instance.RemoveTriggersObject(this.gameObject);
        }
    }
}
