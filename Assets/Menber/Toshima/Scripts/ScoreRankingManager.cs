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
    //CSVのファイルへアクセスするための相対パスを保持するstring型変数
    [SerializeField] 
    private string _csvFilePath;

    //CSVに外部出力した情報をまとめるリスト
    [SerializeField]
    private List<ScoreIndex> _scoreList = new List<ScoreIndex>();
    
    //外部出力した情報をまとめる情報体(こいつでリストを作る)
    public struct ScoreIndex 
    {
        //名前
        private string name;
        //スコア
        private int score;
        
        //名前にアクセスするためのプロパティ
        public string Name {
            get 
            {
                return this.name;
            }
            set {
                //Debug.Log(value.Length);
                if (value.Length <= 3) 
                {
                    this.name = value;
                }
                else {
                    value = value.Remove(3);
                    this.name = value;
                }
            }
        }

        //スコアにアクセスするためのプロパティ
        public int Score 
        {
            get 
            {
                return this.score;
            }
            set {
                this.score = value;
            }
        }
    }

    //シングルトン用のインスタンス
    public static ScoreRankingManager instance;

    private void Awake() 
    {
        //シングルトン化
        if (instance == null) {
            instance = this;
        }

        //CSVにアクセスするためのパスを取得する(パスの内容が変わるためたくさん分岐する)
        //エディターだった場合以下の処理を実行する
        if (Application.isEditor) 
        { 
            //「StreamingAssets」内の「Test.csv」ファイルを指定する(エディター用)
            _csvFilePath = Application.streamingAssetsPath + "/Test.csv";
        }
        //実機だった場合(正確にはエディターではないなら)以下の処理を実行する
        else 
        {
            //現状ではiPhoneもしくはiPad環境でのファイルパスを指定
            //_csvFilePath = Application.dataPath + "/Raw" + "/Test.csv";
            _csvFilePath = Application.persistentDataPath + "/ScoreRankingData.csv";
            if (_csvFilePath == null) 
            {
                _csvFilePath = "ScoreRankingData.csv";
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //外部出力した情報体を格納する
        LoadCSVData();
    }

    /// <summary>
    /// CSVファイルを読み込む
    /// </summary>
    private void LoadCSVData()
    {
        //CSVファイルをStreamReaderに変換
        StreamReader reader = new StreamReader(_csvFilePath);
        
        //解説行をスキップ
        reader.ReadLine();

        //外部出力した情報を受け取るための構造体を定義
        ScoreIndex index = new ScoreIndex();

        //最終行ではない限り繰り返し処理を継続
        while (!reader.EndOfStream) 
        {
            // CSVファイルの一行を読み込む
            string line = reader.ReadLine();
            // 読み込んだ一行をカンマ毎に分けて配列に格納
            string[] values = line.Split(',');
            
            //名前の更新
            index.Name = values[0];
            //スコアの更新
            index.Score = int.Parse(values[1]);
            
           //配列に追加
           _scoreList.Add(index);
        }
        
        //readerを閉じる
        reader.Close();
    }

    /// <summary>
    /// CSVファイルに書き込む
    /// </summary>
    public void WriteCSVFile() 
    {
        //CSVファイルをStreamReaderに変換
        StreamWriter writer = new StreamWriter(_csvFilePath, false,Encoding.UTF8);
        
        //スコアリストをソートする
        SortingRanking();
        
        //CSVに書き込むための文字列を定義
        string line = "名前" + "," + "スコア";
        
        //解説行を挿入
        writer.WriteLine(line);

        //「_scoreList」の要素数の分だけ繰り返し処理を実行する
        for (int i = 0; i < _scoreList.Count; i++) 
        {
            //CSVに外部出力する情報をまとめる
            line = _scoreList[i].Name + "," + _scoreList[i].Score;
            
            //まとめた情報をCSVに外部出力する
            writer.WriteLine(line);
        }
        
        //writerを閉じる
        writer.Close();
    }

    /// <summary>
    /// スコアリストの要素数を返す関数
    /// </summary>
    /// <returns></returns>
    public int GetScoreListCount() 
    {
        return _scoreList.Count;
    }

    /// <summary>
    /// スコア情報を共有するための関数
    /// </summary>
    public ScoreIndex GetScoreIndex(int num) 
    {
        return _scoreList[num];
    }

    /// <summary>
    /// プレイヤーの獲得したスコアを追加する関数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="num"></param>
    public void AddScoreIndex(string name, int num) 
    {
        //情報を追加するための構造体を定義
        ScoreIndex index = new ScoreIndex();
        //名前を更新
        index.Name = name;
        //スコアを更新
        index.Score = num;
        
        //まとめた情報体をリストに挿入
        _scoreList.Add(index);
    }
    
    /// <summary>
    /// ランキング用のリストをScore降順ソート
    /// </summary>
    public void SortingRanking()
    {
        _scoreList.Sort((a,b) => b.Score - a.Score);
    }
}
