using UnityEngine;
using System.Collections;

public class kaunntoup : MonoBehaviour
{

    private int m_Mode = 0;             // -1:�l������������(���쒆), 0:���삵�Ă��Ȃ� 1:�l�𑝉�������(���쒆)
    private float m_Value = 0.0f;       // ���ݒl
    private float m_Start = 0.0f;       // �J�n�l
    private float m_Goal = 0.0f;        // �ŏI�ړI�l
    private decimal m_PerTime = 0.0m;   // �l��"1"(�܂���-1)�ω�����̂ɕK�v�Ȏ���
    private decimal m_Time = 0.0m;      // �o�ߎ���

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
        // �J�E���g�A�b�v
        if (m_Mode > 0)
        {
            if (m_Value > m_Goal)
            {
                // �I���\��
                m_Value = m_Goal;
                return;
            }
            else if (m_Value == m_Goal)
            {
                // �I��
                m_Mode = 0;
                return;
            }
            m_Value = (float)((decimal)m_Start + m_Time / m_PerTime);   // ���P ���鐔(m_PerTime)��0���Ɨ�O��������̂Œ���
        }
        // �J�E���g�_�E��
        else if (m_Mode < 0)
        {
            if (m_Value < m_Goal)
            {
                // �I���\��
                m_Value = m_Goal;
                return;
            }
            else if (m_Value == m_Goal)
            {
                // �I��
                m_Mode = 0;
                return;
            }
            m_Value = (float)((decimal)m_Start - m_Time / m_PerTime);   // ���P ���鐔(m_PerTime)��0���Ɨ�O��������̂Œ���
            Debug.Log("m_Value = " + m_Value);
        }
        // ���삵�Ă��Ȃ��Ƃ�
        else
            return;

        m_Time += (decimal)Time.deltaTime;  // �����"m_Value==m_Start"�ɂ������̂ŁAm_Time�͍Ō�ɍX�V����
    }

    // �e�ϐ�������������
    private void ResetVariable()
    {
        m_Mode = 0;
        m_Value = 0.0f;
        m_Start = 0.0f;
        m_Goal = 0.0f;
        m_PerTime = 0.0m;
        m_Time = 0.0m;
    }

    // �J�E���g�A�b�v(�܂��̓_�E��)���J�n����
    public void CountToInt(int start, int goal, int time)
    {
        // �N�����Ȃ�Ύ��s�ł��Ȃ����̂Ƃ���
        if (IsWorking())
            return;

        // �J�E���g�A�b�v(�܂��̓_�E��)�̊e��l��ݒ�@
        if (start < goal)
            m_Mode = 1;
        else if (start > goal)
            m_Mode = -1;
        else
        {
            m_Mode = 0;
            return;
        }
        m_PerTime = (decimal)(goal - start) / (decimal)time;// 1.0�b���̕ω���
        m_PerTime = 1.0m / m_PerTime;                       // �l"1"(�܂���-1)�ω�����̂ɕK�v�Ȏ���
        if (m_PerTime < 0)
            m_PerTime *= (-1.0m);                           // m_PerTime�͎��ԂȂ̂ŁA"m_PerTime>=0"

        // m_PerTime==0.0m�ɂȂ�ƁA���P�ŗ�O����������̂Ŏ��s�����ɏI������B
        if (m_PerTime <= 0.0m)
            ResetVariable();

        // �J�E���g�A�b�v(�܂��̓_�E��)�̊e��l��ݒ�A
        m_Value = start;    // ���ݒl(�J�n�l)��ݒ�
        m_Start = start;    // �J�n�l��ݒ�
        m_Goal = goal;      // �ŏI�ړI�l��ݒ�
        m_Time = 0.0m;      // �o�ߎ��Ԃ����Z�b�g
    }

    // ���̃N���X�����쒆���ۂ�
    public bool IsWorking()
    {
        return (m_Mode == 0) ? false : true;
    }
}