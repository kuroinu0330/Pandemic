using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class Kyori : MonoBehaviour
{
    Transform playerTr;//�v���C���[��Transform
    Transform riceTr;//�Ă�Transform
    [SerializeField] float speed = 0.8f;//�G�̓����X�s�[�h
    public static Kyori instance;
    //instance���̐ݒ�
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //���G�͈͎w��ŏ��̃v���O����
    public bool isSearching;
    public GameObject player;
    public GameObject rice;
    public void kyori()
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            int score = 0;

        
            while (true)
            {
                if (col.gameObject.tag == "Player")
                {
                    // �v���C���[�Ɍ����Đi��(���ɂ���)
                    this.transform.position = Vector2.MoveTowards(
                        this.transform.position,
                      new Vector2(playerTr.position.x, playerTr.position.y),
                      speed * Time.deltaTime);
                }
                else if (col.gameObject.tag == "rice")
                {
                    // �v���C���[�Ɍ����Đi��(��)
                    this.transform.position = Vector2.MoveTowards(
                        this.transform.position,
                        new Vector2(riceTr.position.x, riceTr.position.y),
                        speed * Time.deltaTime);
                }
                //�X�s�[�h�̕ω�
                if (score < 21f)
                {
                    speed = 0.8f;
                }
                else if (score < 41f)
                {
                    speed = 1.0f;
                }
                else if (score < 61f)
                {
                    speed = 1.2f;
                }
                else if (score < 81f)
                {
                    speed = 1.5f;
                }
            }
        }
   }
}*/
