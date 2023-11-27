using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultTest : MonoBehaviour
{
    //���W
    [SerializeField]
    public Vector3 _imagePos;
    //�Ă�Prefab
    [SerializeField]
    public GameObject _monsterImage;
    //�Ă̈ʒu
    [SerializeField]
    public Transform _transform;

    //�ʂ̃X�N���v�g���Ăяo���Ă���ϐ�
    //���̃X�N���v�g��������missing(������)���ē{���� 
    //GameSceneIndex gameSceneIndex;
    Score score;


    // Start is called before the first frame update
    void Start()
    {
        //�R���[�`���̌Ăяo��
        StartCoroutine("ResultImage", 0.2f);
        //GameSceneIndex gameSceneIndex = GetComponent<GameSceneIndex>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //�R���[�`���̊֐�
    IEnumerator ResultImage()
    {
        /*
        *for����i��GameSceneIndex._gameSceneScoreIndex
        *��菬���������烋�[�v����i���J�E���g�A�b�v����
        */
        for (int i = 0; i <GameSceneIndex._gameSceneScoreIndex; i++)
        {
            /*
             new Vector3(30,34,54)�̕��������ǂ���
             */
            //���[�v���Ă��鎞�ɂ����̏���������
            GameObject obj = Instantiate(_monsterImage, new Vector3(0, 0, 0), Quaternion.identity, _transform);
            
            obj.transform.localPosition = new Vector3(-22, 1550, 0);

            

            //Instantiate(_monsterImage, Vector3.zero, Quaternion.identity, _transform);
            //0.5frame�̊ԂɎ��s����
            yield return new WaitForSeconds(0.05f);
        }
        GameSceneIndex.instance.ResetGameSceneScore();
        yield return null;
    }
}
