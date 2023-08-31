using UnityEngine;
using System.Collections;

public class kaunntoup : MonoBehaviour
{

    private int m_Mode = 0;             // -1:値を減少させる(動作中), 0:動作していない 1:値を増加させる(動作中)
    private float m_Value = 0.0f;       // 現在値
    private float m_Start = 0.0f;       // 開始値
    private float m_Goal = 0.0f;        // 最終目的値
    private decimal m_PerTime = 0.0m;   // 値が"1"(または-1)変化するのに必要な時間
    private decimal m_Time = 0.0m;      // 経過時間

    public float Value
    {
        get
        {
            return m_Value;
        }
    }

    // Use this for initialization
    void Start()
    {
        ResetVariable();
    }

    // Update is called once per frame
    void Update()
    {
        // カウントアップ
        if (m_Mode > 0)
        {
            if (m_Value > m_Goal)
            {
                // 終了予告
                m_Value = m_Goal;
                return;
            }
            else if (m_Value == m_Goal)
            {
                // 終了
                m_Mode = 0;
                return;
            }
            m_Value = (float)((decimal)m_Start + m_Time / m_PerTime);   // ※１ 割る数(m_PerTime)が0だと例外発生するので注意
        }
        // カウントダウン
        else if (m_Mode < 0)
        {
            if (m_Value < m_Goal)
            {
                // 終了予告
                m_Value = m_Goal;
                return;
            }
            else if (m_Value == m_Goal)
            {
                // 終了
                m_Mode = 0;
                return;
            }
            m_Value = (float)((decimal)m_Start - m_Time / m_PerTime);   // ※１ 割る数(m_PerTime)が0だと例外発生するので注意
            Debug.Log("m_Value = " + m_Value);
        }
        // 動作していないとき
        else
            return;

        m_Time += (decimal)Time.deltaTime;  // 初回は"m_Value==m_Start"にしたいので、m_Timeは最後に更新する
    }

    // 各変数を初期化する
    private void ResetVariable()
    {
        m_Mode = 0;
        m_Value = 0.0f;
        m_Start = 0.0f;
        m_Goal = 0.0f;
        m_PerTime = 0.0m;
        m_Time = 0.0m;
    }

    // カウントアップ(またはダウン)を開始する
    public void CountToInt(int start, int goal, int time)
    {
        // 起動中ならば実行できないものとする
        if (IsWorking())
            return;

        // カウントアップ(またはダウン)の各種値を設定①
        if (start < goal)
            m_Mode = 1;
        else if (start > goal)
            m_Mode = -1;
        else
        {
            m_Mode = 0;
            return;
        }
        m_PerTime = (decimal)(goal - start) / (decimal)time;// 1.0秒毎の変化量
        m_PerTime = 1.0m / m_PerTime;                       // 値"1"(または-1)変化するのに必要な時間
        if (m_PerTime < 0)
            m_PerTime *= (-1.0m);                           // m_PerTimeは時間なので、"m_PerTime>=0"

        // m_PerTime==0.0mになると、※１で例外が発生するので実行せずに終了する。
        if (m_PerTime <= 0.0m)
            ResetVariable();

        // カウントアップ(またはダウン)の各種値を設定②
        m_Value = start;    // 現在値(開始値)を設定
        m_Start = start;    // 開始値を設定
        m_Goal = goal;      // 最終目的値を設定
        m_Time = 0.0m;      // 経過時間をリセット
    }

    // このクラスが動作中か否か
    public bool IsWorking()
    {
        return (m_Mode == 0) ? false : true;
    }
}