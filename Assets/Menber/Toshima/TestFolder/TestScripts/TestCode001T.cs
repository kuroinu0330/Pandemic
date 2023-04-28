using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode001T : MonoBehaviour
{
    // [議題]現在作成された操作方法にて発生しうる不具合について
    // 現在は「タップ」された瞬間その座標に米粒を生成してしまうため今後「一時停止ボタン」や「コンフィグ」の機能を
    // 実装する際にボタンの裏側に米粒が生成される恐れがある。
    // そのため等問題の解決のため以下の対策を提案する
    
    // 1,米の生成を1フレーム遅らせその間に「タップ」された箇所に「ボタン」などの他のオブジェクトがないかを確認する
    
    // 2、画面の背景にでかいボタンを設置して、UIレイヤーになんとかしてもらう方法。

    //[結果]第1法案の原典が問題を解決、1フレーム待つ必要性もUnityには不要だった

    public void ButtonEnter()
    {
        //PlayerInputManagement.Instance.InputSwitching();
        PlayerInputManagement.Instance.PlayerActionOff();
        //Debug.Log("ファッキュー");
    }
    public void ButtonExit()
    {
        PlayerInputManagement.Instance.PlayerActionOn();
        //PlayerInputManagement.Instance.InputSwitching();
    }
}
