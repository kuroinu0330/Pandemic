using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiceBollsMoveTest : MonoBehaviour
{
    /*
    private Collider2D _collider01;
    private Collider2D _collider02;
    */

    public Text ScoreText;
    #region�@�Ċl��
    [SerializeField]
    private int _level = 0; //���x��
    [SerializeField]
    private int _HighScore = 0; //�Ă̊l����
    #endregion
    #region �ړ��֌W
    private GameObject _nearObj; //�ł��߂��I�u�W�F�N�g
    private float _serchTime;�@
    [SerializeField]
    private float _speed;�@//���x

    private bool _isArea;

    float m_radius;
    Vector3 center;

    #endregion
    // Start is called before the first frame updSate
    void Start()
    {
        _level = 0;
        //�ł��߂��I�u�W�F�N�g���擾
        _nearObj = serchTag(gameObject, "RiceBaby");
    }

    // Update is called once per frame
    /// <summary>
    /// �߂��ɂ����Obj�̕����Ɍ����ĒǏ]����B
    /// </summary>
    void Update()
    {
        _serchTime += Time.deltaTime;
        if (_serchTime >= 0)
        {
            _nearObj = serchTag(gameObject, "RiceBaby");
            _serchTime = 0;
            //�Ώۂ̈ʒu�̕���������
            //transform.LookAt(_nearObj.transform);
            if (_nearObj == null)
            {
                return;

            }
     
            Vector3 diff = (this.gameObject.transform.position - _nearObj.transform.position);

            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, -diff);

            CameraMoveController.Instance.CameraPositionUpdate();

            //�������g�̈ʒu���瑊�ΓI�Ɉړ�����
            //transform.Translate(Vector3.forward * 0.1f);
            transform.position = Vector3.MoveTowards(
          transform.position,
          _nearObj.transform.position,
          _speed * Time.deltaTime);

        }
        ScoreText.text = "�Ċl����: " + _HighScore.ToString();
    }
    public void blowoff()
    {
        Collider[] hitColliders = Physics.OverlapSphere(center,m_radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            //kome���͈͂ɓ�������ǂ�������

        }
    }

    
    /// <summary>
    /// ��Ă��擾���邲�Ƃ�10%���x���㏸
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RiceBaby"))
        {
            _level += 1;
            _HighScore += 1;
            other.gameObject.SetActive(false);
            #region ���x��
            if (_level == 1)
            {
                //�R���[�`��Start
                StartCoroutine(CountCoroutine());
                Debug.Log("1���x������");
                _speed = 110f;
                _nearObj = null;
            }
            if (_level == 2)
            {
                StartCoroutine(CountCoroutine());
                Debug.Log("2���x������");
                _speed = 120f;
            }
            if (_level == 3)
            {
                StartCoroutine(CountCoroutine());
                Debug.Log("3���x������");
                _speed = 130f;
            }
            if (_level == 4)
            {
                
                StartCoroutine(CountCoroutine());
                Debug.Log("4���x������");
                _speed = 140f;
            }
            if (_level == 5)
            {
               
                StartCoroutine(CountCoroutine());
                Debug.Log("5���x������");
                _speed = 150f;
            }
            if (_level == 6)
            {
             
                StartCoroutine(CountCoroutine());
                Debug.Log("6���x������");
                _speed = 160f;
            }
            if (_level == 7)
            {
                
                StartCoroutine(CountCoroutine());
                Debug.Log("7���x������");
                _speed = 170f;
            }
            if (_level == 8)
            {
              
                StartCoroutine(CountCoroutine());
                Debug.Log("8���x������");
                _speed = 180f;
            }
            if (_level == 9)
            {
             
                StartCoroutine(CountCoroutine());
                Debug.Log("9���x������");
                _speed = 190f;
            }
            if (_level == 10)
            {
                
                StartCoroutine(CountCoroutine());
                Debug.Log("10���x������");
                _speed = 200f;
            }
            #endregion
        }
    }
    //��b�ԕĂ��l���ł��Ȃ�������X�R�A��0�ɂ���B
    bool _Clear =  false;
    /// <summary>
    /// 3�b�ԕĂ��l���ł��Ȃ�������X�R�A�Ƒ��x���������ɂ���B
    /// </summary>
    /// <returns></returns>
    IEnumerator CountCoroutine()
    {
        _Clear = true;
        yield return null;
        float timer = 0.0f;
        _Clear = false;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= 2.0f)
            {
                _level = 0;
                _speed = 100f;
                Debug.Log("���x�ƃ��x���������ɖ߂�������");
                _Clear = false;
                yield break;
            }
            if (_Clear)
            {
                _Clear = false;
                yield break;
            }
            yield return null;
        }
        /*yield return new WaitForSeconds(1.0f);
        _score = 0;
        _speed = 1.0f;
        Debug.Log("�R���[�`���J�n");
        yield break;*/
    }
    public void ChallengeClear()
    {
        _Clear = true;
    }
 
    /// <summary>
    /// �ĂƂ��ɂ���̋������v�Z���ċ߂��ɂ����obj�ɒǏ]����
    /// </summary>
    /// <param name="nowObj">�������g</param>
    /// <param name="tagName">�Ă�T�m</param>
    /// <returns></returns>
    #region �T�m�n
    GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //�����p�ꎞ�ϐ�
        float nearDis = 0;          //�ł��߂��I�u�W�F�N�g�̋���
        GameObject targetObj = null;
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            /*����
             * bool��false��true���g���ĉ�ʓ��ɗv��Ƃ���true���Ȃ��Ƃ���false
             *Vector2.Distance�ł͂Ȃ�sqrMagnitude���g���������������y��
             */
            //���g�Ǝ擾�����I�u�W�F�N�g�̋������擾
             tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //�I�u�W�F�N�g�̋������߂����A����0�ł���΃I�u�W�F�N�g�����擾
            //�ꎞ�ϐ��ɋ������i�[
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }

        }
        //�ł��߂������I�u�W�F�N�g��Ԃ�
        //return GameObject.Find(nearObjName);
        return targetObj;
    }
    #endregion
}
