using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ScoreRankingManager : MonoBehaviour 
{
    //外部出力して保持するためのCSVファイル
    // [SerializeField] 
    // private TextAsset _csvData;

    //CSVのファイルへアクセスするための相対パスを保持するstring型変数
    [SerializeField] 
    private string _csvFilePath;
    
    //CSVに外部出力した情報をまとめるリスト
    private List<ScoreIndex> _scores = new List<ScoreIndex>();
    
    //外部出力した情報をまとめる情報体(こいつでリストを作る)
    private struct ScoreIndex {
        public string Name { get; set; }
        public int Score { get; set; }
    }

    //シングルトン用のインスタンス
    public static ScoreRankingManager instance;

    private void Awake() 
    {
        //シングルトン化
        if (instance == null) {
            instance = this;
        }
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //外部出力した情報体を格納する
        //LoadCSVData();
        
        //情報体を外部出力
        //WriteCSVFile();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            DebugLog();
        }
    }

    /// <summary>
    /// CSVファイルを読み込む
    /// </summary>
    private void LoadCSVData()
    {
        // //CSVファイルをStringReaderに変換
        // StringReader reader = new StringReader(_csvData.text);
        //
        // //先頭の解説行をスキップする
        // reader.ReadLine();
        //
        // //CSVファイルの全文を読み込む
        // string csv = reader.ReadToEnd();
        //
        // //読み込んだ全文を一行に切り分ける(要素数が縦列の総数)
        // string[] line = csv.Split('\n');
        //
        // //外部出力した情報を受け取るための情報体を定義
        // ScoreIndex index = new ScoreIndex();
        //
        // //行数分以下の処理を実行する
        // for (int i = 0; i < line.Length; i++) 
        // {
        //     //一行に切り分けたものを一文字に切り分ける(要素数が横列の総数)
        //     string[] value = line[i].Split(',');
        //
        //     //名前を更新
        //     index.Name = value[0];
        //     //スコアを更新
        //     index.Score = int.Parse(value[1]);
        //     
        //     //情報体をリストに格納
        //     _scores.Add(index);
        // }
        
        //
    }

    /// <summary>
    /// ランキング用のリストをScore降順ソート
    /// </summary>
    private void SortingRanking()
    {
        _scores.Sort((a,b) => b.Score - a.Score);
    }

    /// <summary>
    /// CSVファイルに書き込む
    /// </summary>
    public void WriteCSVFile() 
    {
        // //CSVファイルをStringWriterに変換
        // //StringWriter writer = new StringWriter(new StringBuilder(_csvData.text),false,Encoding.GetEncoding("UTF-8"));
        // StringWriter writer = new StringWriter(new StringBuilder(_csvData.text),false);
        // //StreamWriter writer = new StreamWriter(@"Assets/Menber/Toshima/CSV/Test.csv",true,Encoding.GetEncoding("UTF-8"));
        //
        // //Listをソートする
        // SortingRanking();
        //
        // //先頭の解説行をスキップする
        // writer.WriteLine();
        //
        // for (int i = 0; i < _scores.Count; i++)
        // {
        //     string line = (_scores[i].Name + "," + _scores[i].Score.ToString());
        //
        //     Debug.Log(line);
        //     
        //     writer.WriteLine(line);
        // }
        //
        // //_csvData = new TextAsset(writer.ToString());
        
        
    }

    private void DebugLog() 
    {
        //結果表示
         for (int i = 0; i < _scores.Count; i++) 
         {
             Debug.Log(" プレイヤー名 "+_scores[i].Name+" 獲得スコア "+_scores[i].Score);
         }
        
        //StringReader reader = new StringReader(_csvData.text);
        //Debug.Log(reader.ReadToEnd().ToString());
    }
}
