using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultTest : MonoBehaviour
{
    //座標
    [SerializeField]
    public Vector3 _imagePos;
    //米のPrefab
    [SerializeField]
    public GameObject _monsterImage;
    //米の位置
    [SerializeField]
    public Transform _transform;

    //別のスクリプトを呼び出している変数
    //このスクリプトが無いとmissing(無いよ)って怒られる 
    //GameSceneIndex gameSceneIndex;
    Score score;


    // Start is called before the first frame update
    void Start()
    {
        //コルーチンの呼び出し
        StartCoroutine("ResultImage", 0.2f);
        //GameSceneIndex gameSceneIndex = GetComponent<GameSceneIndex>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //コルーチンの関数
    IEnumerator ResultImage()
    {
        /*
        *for文でiがGameSceneIndex._gameSceneScoreIndex
        *より小さかったらループしてiをカウントアップする
        */
        for (int i = 0; i <GameSceneIndex._gameSceneScoreIndex; i++)
        {
            /*
             new Vector3(30,34,54)の部分を改良する
             */
            //ループしている時にここの処理をする
            GameObject obj = Instantiate(_monsterImage, new Vector3(0, 0, 0), Quaternion.identity, _transform);
            
            obj.transform.localPosition = new Vector3(-22, 1340, 0);

            

            //Instantiate(_monsterImage, Vector3.zero, Quaternion.identity, _transform);
            //0.5frameの間に実行する
            yield return new WaitForSeconds(0.05f);
        }
        GameSceneIndex.instance.ResetGameSceneScore();
        yield return null;
    }
}
