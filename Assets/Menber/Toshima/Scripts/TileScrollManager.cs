using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileScrollManager : MonoBehaviour
{
    //画面比率をスクリプト上で保管しておくためのVector配列(柔軟性の向上を試行)
    [SerializeField]
    private Vector2 DisplayResolution = new Vector2(2048f,2732);

    //全てのタイルの元となるプレハブ(＊プレハブ内にてプレイヤー識別用のタグを設定する必要あり「Assets/Menber/Toshima/Prefab」＊)
    [SerializeField]
    private GameObject TileBasePrefab;

    //全てのタイルにアクセスもしくはデバックするためのタイル名簿
    [SerializeField]
    private List<GameObject> _mapTiles;

    //全てのタイルに貼り付ける画像を保持する変数
    [SerializeField]
    private List<Sprite> _mapTileTextures;

    //現在プレイヤーが最終的に立っていたタイルのナンバーを保持する変数(複数タイル上に存在した場合、単一タイル上に存在するまで更新しない)(二次元化適応済)
    [SerializeField]
    //private int _centerNum = 5;
    private Vector2 _centerCoordinateNum = new Vector2(1,1);

    //プレイヤーが踏んでいるタイルの情報を保持する名簿(「GameObject」じゃなくて「MapTileIndex」でリスト化した方がメモリの節約になるし「GetComponent」する必要もなくなるから他処理が軽くなる。α版までの制作期間で修正予定)
    [SerializeField]
    private List<GameObject> _enteredTriggers;

    [SerializeField]
    private float _riceBabyGenerationDelay = 10f;

    private float _enerationDelayScale = 1f;

    private float _generationTimer = 10f;

    private bool _raiceBabyGenerationStart = false;

    //シングルトン処理(正味「SingletonMonoBehaviour」の書き方と理屈が知りたい)
    public static TileScrollManager Instance;

    //使うかわからない進行方向の列挙型配列(前科二犯、まじで使わなかったのが2回ある。追記、使います、やったね( ＾∀＾)。追記「めっちゃ拡張した…」)
    private enum ScrollDirection
    {
        None,
        Up,
        UpperRight,
        Right,
        BottomRight,
        Under,
        BottomLeft,
        Left,
        UpperLeft
    }

    void Awake()
    {
        //先んじたシングルトン処理(当スクリプトの処理を他スクリプトでは「Start」以下の優先度で利用すればバグは出ない)
        if(Instance == null)
        {
            Instance = this;
        }
        
        //画面比率のVector配列に現在利用しているウィンドウの解像度を適応(スマホ媒体だと強制的にフルスクリーンになるように見えるため現状問題はなさそう問題が発生した場合は
                                                                //「Screen.CurrentResolution.width」もしくは「Screen.CurrentResolution.height」を「Screen.width」の代わりに使用する)
        DisplayResolution = new Vector2(Screen.width,Screen.height);

        //念の為に初期化しておく
        _centerCoordinateNum = new Vector2(1,1);
    }

    // Start is called before the first frame update
    void Start()
    {
        //生成したタイルがエディター上を混沌とさせないようにまとめておくオブジェクトを生成
        var EmptyObj = new GameObject(" MapTileOffice");

        for(int y = 0; y < 3; y++)
        {
            for(int x = 0; x < 3; x++)
            {
                //タイルの生成処理(イメージとしては左下からタイルを敷き詰めるイメージ)
                var obj = Instantiate(TileBasePrefab,
                                        new Vector3(-DisplayResolution.x + DisplayResolution.x * x,
                                                    -DisplayResolution.y + DisplayResolution.y * y,
                                                    0f),
                                        Quaternion.identity) as GameObject;

                //タイルのエディター上での名前を決める
                obj.name = "MapTile" + (y * 3 + x);

                //タイルの縦横幅を調整
                obj.GetComponent<SpriteRenderer>().size = DisplayResolution;

                //タイルの柄を元々用意していたものに置き換える
                obj.GetComponent<SpriteRenderer>().sprite = _mapTileTextures[y * 3 + x];

                //整理用のオブジェクト参加にしてエディターを散らかさないようにする
                obj.gameObject.transform.SetParent(EmptyObj.gameObject.transform);

                //タイルのナンバリングを設定する(二次元化適応済み)
                //obj.GetComponent<MapTileIndex>().SetThisTileNumber(y * 3 + x);
                obj.GetComponent<MapTileIndex>().SetThisTileNumber(new Vector2(x,y));

                //生成したタイルをアクセスもしくはデバックするための配列に捩じ込む
                _mapTiles.Add(obj.gameObject);
            }
        }
    }

    void Update()
    {
        //_raiceBabyGenerationStartがtrueなら以下の処理を実行する
        if(_raiceBabyGenerationStart)
        {        
            //_generationTimerが_riceBabyGenerationDelay以上なら以下の処理を実行する
            if(_generationTimer >= _riceBabyGenerationDelay)
            {
                //タイルの数だけ実行する
                for(int i = 0; i < _mapTiles.Count;i++)
                {
                    //自信がいるタイル以外なら実行する
                    if(_mapTiles[i] != _enteredTriggers[0])
                    {
                        //ランダム生成用の座標を用立てる
                        Vector3 posRange = new Vector3(Random.Range(-1024,1024),Random.Range(-1366,1366),0f); 
                        Vector3 createPos = _mapTiles[i].transform.position + posRange;

                        //コメのランダム生成処理
                        RiceBabyCreateManager.Instance.CreateRiceBaby(new Vector2(createPos.x,createPos.y),1);
                        Debug.Log(i + " 番目 " + createPos + " の位置に米を生成したぞい！");
                    }
                }
                //タイマーの初期化
                _generationTimer = 0f;
            }
            else
            {
                //タイマーの加算
                _generationTimer += _enerationDelayScale * Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// コメのランダム生成を実質的にスタートする関数
    /// </summary>
    public void RiceBabyRandomGenerationStart()
    {
        _raiceBabyGenerationStart = true;
    }

    /// <summary>
    /// タイルが接触を検知した時にタイルを管理する配列にタイルが自身を捩じ込むための関数
    /// </summary>
    /// <param name="tileObject">プレイヤーと接触したタイル</param>
    public void AddTriggersObject(GameObject tileObject)
    {
        //配列の最後尾にタイルを捩じ込む処理
        _enteredTriggers.Add(tileObject);
    }

    /// <summary>
    /// タイルが非接触を検知した時にタイルを管理する配列にタイルが自身を痕跡を抹消するための関数
    /// </summary>
    /// <param name="tileObject">プレイヤーとの接触が終了したタイル</param>
    public void RemoveTriggersObject(GameObject tileObject)
    {
        //配列内の自身を消去して開いた部分を後列の要素で塞ぐ完璧処理(Remove好き)
        _enteredTriggers.Remove(tileObject);

        // 軽量化処置
        Vector2 coefficientVector2 = _enteredTriggers[0].GetComponent<MapTileIndex>().GetThisTileNumber();

        //一瞬アリかと思ったけどバグの元であったため却下
        //if(coefficientVector2 != _centerCoordinateNum)

        //現在接触中のタイルがひとつになった場合以下の処理を実行する(1マス移動)(改良？)
        //if(_enteredTriggers.Count == 1)

        //現在接触中のタイルが1~2つになった場合以下の処理を実行する(改良成功)
        if(_enteredTriggers.Count == 1 || _enteredTriggers.Count == 2)
        {
            //タイルのスクロールを始める
            RecycleGroundProtocol(coefficientVector2);

            //現在の座標を更新する(二次元化適応済)
            //_centerNum = _enteredTriggers[0].GetComponent<MapTileIndex>().GetThisTileNumber();
            _centerCoordinateNum = coefficientVector2;
        }
        //現在接触中のタイルが二つ以上の時に接触者リストからセンタータイルがなくなった時に以下の処理を実行する(境界線移動時に使用)
        
    }

    /// <summary>
    /// タイルの通常移動に必要な要素をまとめた関数(やや粗雑になった)
    /// </summary>
    /// <param name="next">移動先のタイルナンバー</param>
    private void RecycleGroundProtocol(Vector2 next)
    {
        //現在のタイルナンバーと次のタイルナンバーから進行方向を定める
        ScrollDirection direction = TileMoveDirectionJudg(next);

        //移動させる必要があるタイルを選定し、タイルの移動処理に繋ぐ(見栄えが良くなるから分けたかったが、脳筋の定めよな、無理だった。)
        //Vector3 tilesNum = selectReusableTile(direction,next);
        selectReusableTile(direction,next);

        //タイルを移動させる
        //ShiftTileToAdjacentPosition(direction,tilesNum,next);     

        //デバック用
        //Debug.Log(direction);
        //Debug.Log(tilesNum);
    }

    /// <summary>
    /// 現在のタイルナンバーと次のタイルナンバーから進行方向を定める関数
    /// </summary>
    /// <param name="next">移動先のタイルナンバー</param>
    /// <returns></returns>
    private ScrollDirection TileMoveDirectionJudg(Vector2 next)
    {
        Vector2 ansVec2 = _centerCoordinateNum - next;

        //デバック用
        //Debug.Log(ansVec2);

        //yの値が「-1」もしくは「2」の時以下の処理を実行する(2 - 0 = 2のため)
        if(ansVec2.y == -1 || ansVec2.y == 2)
        {
            //xの値が「0」の場合以下の処理を実行する
            if(ansVec2.x == 0)
            {
                //「Up」を返す
                return ScrollDirection.Up;
            }
            //xの値が「-1」もしくは「2」の時以下の処理を実行する(2 - 0 = 2のため)
            else if(ansVec2.x == -1 || ansVec2.x == 2)   
            {
                //「UpperRight」を返す
                return ScrollDirection.UpperRight;
            }
            //xの値が「1」もしくは「-2」の時以下の処理を実行する(0 - 2 = -2のため)
            else if(ansVec2.x == 1 || ansVec2.x == -2)
            {
                //「UpperLeft」を返す
                return ScrollDirection.UpperLeft;
            }
        }
        //yの値が「1」もしくは「-2」の時以下の処理を実行する(0 - 2 = -2のため)
        else if(ansVec2.y == 1 || ansVec2.y == -2)
        {
            //xの値が「0」の場合以下の処理を実行する
            if(ansVec2.x == 0)
            {
                //「Under」を返す
                return ScrollDirection.Under;
            }
            //xの値が「-1」もしくは「2」の時以下の処理を実行する(2 - 0 = 2のため)
            else if(ansVec2.x == -1 || ansVec2.x == 2)   
            {
                //「BottomRight」を返す
                return ScrollDirection.BottomRight;
            }
            //xの値が「1」もしくは「-2」の時以下の処理を実行する(0 - 2 = -2のため)
            else if(ansVec2.x == 1 || ansVec2.x == -2)
            {
                //「BottomLeft」を返す
                return ScrollDirection.BottomLeft;
            }           
        }
        //xの値が「-1」もしくは「2」の時以下の処理を実行する(2 - 0 = 2のため)
        else if(ansVec2.x == -1 || ansVec2.x == 2)
        {
            //「Right」を返す
            return ScrollDirection.Right;
        }
        //xの値が「1」もしくは「-2」の時以下の処理を実行する(0 - 2 = -2のため)
        else if(ansVec2.x == 1 || ansVec2.x == -2)
        {
            //「Right」を返す
            return ScrollDirection.Left;
        }
        
        //エラー検知用
        return ScrollDirection.None;
    }

    /// <summary>
    /// 移動させる必要があるタイルを選定し、タイルの移動処理に繋ぐ
    /// </summary>
    /// <param name="direction">移動方向</param>
    /// <param name="next">移動先のタイルナンバー</param>
    private void selectReusableTile(ScrollDirection direction, Vector2 next)
    {
        float slectedTiles = 0f;

        Vector3 tilesNumber = Vector3.zero;

        switch (direction)
        {           
            //「Up」だった場合
            case ScrollDirection.Up:

                //現在のタイルから一つ下のタイルをピックアップ(現在のタイルが高さ0(底辺)ならその下は2(天井))
                slectedTiles = (_centerCoordinateNum.y == 0f) ? 2f  : _centerCoordinateNum.y - 1f;

                //見つけたタイルの一番左側の配列上の要素数に変換(タイルのナンバリングはVectorで行なっているがゲームオブジェクトはリスト管理のため)
                slectedTiles = slectedTiles * 3f;

                //左側に変換された要素数に「0,1,2」を足して列をピックアップ(yが0なら「0,1,2」,yが1なら「3,4,5」)
                tilesNumber = new Vector3(slectedTiles + 0,slectedTiles + 1,slectedTiles + 2);

                //ごーしゅー(ピックアップされた列をプレイヤーの進行方向に配置してリサイクルする。リユースだっけ？)
                ShiftTileToAdjacentPosition(direction,tilesNumber,next);

                break;
            //「Right」だった場合
            case ScrollDirection.Right:

                //現在のタイルから一つ左のタイルをピックアップ(現在のタイルが0(左端)ならその左は2(右端))
                slectedTiles = (_centerCoordinateNum.x == 0f) ? 2f  : _centerCoordinateNum.x - 1f;

                //選ばれたタイルにyによる上昇分を加味して列をピックアップ(xが0なら「0,3,6」,xが1なら「1,4,7」)
                tilesNumber = new Vector3(0 + slectedTiles,3 + slectedTiles,6 + slectedTiles);

                //ごーしゅー(ピックアップされた列をプレイヤーの進行方向に配置してリサイクルする。リサイクルっていったんバラすんじゃなかったっけ？)
                ShiftTileToAdjacentPosition(direction,tilesNumber,next);

                break;
            //「Under」だった場合
            case ScrollDirection.Under:

                //現在のタイルから一つ上のタイルをピックアップ(現在のタイルが高さ2(天井)ならその上は0(底辺))
                slectedTiles = (_centerCoordinateNum.y == 2f) ? 0f  : _centerCoordinateNum.y + 1f;

                //見つけたタイルの一番左側の配列上の要素数に変換(タイルのナンバリングはVectorで行なっているがゲームオブジェクトはリスト管理のため)
                slectedTiles = slectedTiles * 3f;

                //左側に変換された要素数に「0,1,2」を足して列をピックアップ(yが0なら「0,1,2」,yが1なら「3,4,5」)
                tilesNumber = new Vector3(slectedTiles + 0,slectedTiles + 1,slectedTiles + 2);

                //ごーしゅー(ピックアップされた列をプレイヤーの進行方向に配置してリサイクルする。じゃあリユースやないかい…)
                ShiftTileToAdjacentPosition(direction,tilesNumber,next);

                break;
            //「Left」だった場合
            case ScrollDirection.Left:

                //現在のタイルから一つ左のタイルをピックアップ(現在のタイルが0(左端)ならその左は2(右端))
                slectedTiles = (_centerCoordinateNum.x == 2f) ? 0f  : _centerCoordinateNum.x + 1f;

                //選ばれたタイルにyによる上昇分を加味して列をピックアップ(xが0なら「0,3,6」,xが1なら「1,4,7」)
                tilesNumber = new Vector3(0 + slectedTiles,3 + slectedTiles,6 + slectedTiles);

                //ごーしゅー(ピックアップされた列をプレイヤーの進行方向に配置してリユースする)
                ShiftTileToAdjacentPosition(direction,tilesNumber,next);

                break;
            case ScrollDirection.UpperRight:

                //まず「Up」の処理を済ませる

                //現在のタイルから一つ下のタイルをピックアップ(現在のタイルが高さ0(底辺)ならその下は2(天井))
                slectedTiles = (_centerCoordinateNum.y == 0f) ? 2f  : _centerCoordinateNum.y - 1f;

                //見つけたタイルの一番左側の配列上の要素数に変換(タイルのナンバリングはVectorで行なっているがゲームオブジェクトはリスト管理のため)
                slectedTiles = slectedTiles * 3f;

                //左側に変換された要素数に「0,1,2」を足して列をピックアップ(yが0なら「0,1,2」,yが1なら「3,4,5」)
                tilesNumber = new Vector3(slectedTiles + 0,slectedTiles + 1,slectedTiles + 2);

                //ごーしゅー(ピックアップされた列をプレイヤーの進行方向に配置してリユースする)
                ShiftTileToAdjacentPosition(ScrollDirection.Up,tilesNumber,next);

                //次に「Right」の処理を済ませる

                //現在のタイルから一つ左のタイルをピックアップ(現在のタイルが0(左端)ならその左は2(右端))
                slectedTiles = (_centerCoordinateNum.x == 0f) ? 2f  : _centerCoordinateNum.x - 1f;

                //選ばれたタイルにyによる上昇分を加味して列をピックアップ(xが0なら「0,3,6」,xが1なら「1,4,7」)
                tilesNumber = new Vector3(0 + slectedTiles,3 + slectedTiles,6 + slectedTiles);

                //ごーしゅー(ピックアップされた列をプレイヤーの進行方向に配置してリユースする)
                ShiftTileToAdjacentPosition(ScrollDirection.Right,tilesNumber,next);

                break;
            case ScrollDirection.BottomRight:

                //まず「Under」の処理を済ませる

                //現在のタイルから一つ上のタイルをピックアップ(現在のタイルが高さ2(天井)ならその上は0(底辺))
                slectedTiles = (_centerCoordinateNum.y == 2f) ? 0f  : _centerCoordinateNum.y + 1f;

                //見つけたタイルの一番左側の配列上の要素数に変換(タイルのナンバリングはVectorで行なっているがゲームオブジェクトはリスト管理のため)
                slectedTiles = slectedTiles * 3f;

                //左側に変換された要素数に「0,1,2」を足して列をピックアップ(yが0なら「0,1,2」,yが1なら「3,4,5」)
                tilesNumber = new Vector3(slectedTiles + 0,slectedTiles + 1,slectedTiles + 2);

                //ごーしゅー(ピックアップされた列をプレイヤーの進行方向に配置してリユースする)
                ShiftTileToAdjacentPosition(ScrollDirection.Under,tilesNumber,next);

                //次に「Right」の処理を済ませる

                //現在のタイルから一つ左のタイルをピックアップ(現在のタイルが0(左端)ならその左は2(右端))
                slectedTiles = (_centerCoordinateNum.x == 0f) ? 2f  : _centerCoordinateNum.x - 1f;

                //選ばれたタイルにyによる上昇分を加味して列をピックアップ(xが0なら「0,3,6」,xが1なら「1,4,7」)
                tilesNumber = new Vector3(0 + slectedTiles,3 + slectedTiles,6 + slectedTiles);

                //ごーしゅー(ピックアップされた列をプレイヤーの進行方向に配置してリユースする)
                ShiftTileToAdjacentPosition(ScrollDirection.Right,tilesNumber,next);

                break;
            case ScrollDirection.BottomLeft:

                //まず「Under」の処理を済ませる

                //現在のタイルから一つ上のタイルをピックアップ(現在のタイルが高さ2(天井)ならその上は0(底辺))
                slectedTiles = (_centerCoordinateNum.y == 2f) ? 0f  : _centerCoordinateNum.y + 1f;

                //見つけたタイルの一番左側の配列上の要素数に変換(タイルのナンバリングはVectorで行なっているがゲームオブジェクトはリスト管理のため)
                slectedTiles = slectedTiles * 3f;

                //左側に変換された要素数に「0,1,2」を足して列をピックアップ(yが0なら「0,1,2」,yが1なら「3,4,5」)
                tilesNumber = new Vector3(slectedTiles + 0,slectedTiles + 1,slectedTiles + 2);

                //ごーしゅー(ピックアップされた列をプレイヤーの進行方向に配置してリユースする)
                ShiftTileToAdjacentPosition(ScrollDirection.Under,tilesNumber,next);

                //次に「Left」の処理を済ませる

                //現在のタイルから一つ右のタイルをピックアップ(現在のタイルが2(右端)ならその右は0(左端))
                slectedTiles = (_centerCoordinateNum.x == 2f) ? 0f  : _centerCoordinateNum.x + 1f;

                //選ばれたタイルにyによる上昇分を加味して列をピックアップ(xが0なら「0,3,6」,xが1なら「1,4,7」)
                tilesNumber = new Vector3(0 + slectedTiles,3 + slectedTiles,6 + slectedTiles);

                //ごーしゅー(ピックアップされた列をプレイヤーの進行方向に配置してリユースする)
                ShiftTileToAdjacentPosition(ScrollDirection.Left,tilesNumber,next);

                break;
            case ScrollDirection.UpperLeft:

                //まず「Up」の処理を済ませる

                //現在のタイルから一つ下のタイルをピックアップ(現在のタイルが高さ0(底辺)ならその下は2(天井))
                slectedTiles = (_centerCoordinateNum.y == 0f) ? 2f  : _centerCoordinateNum.y - 1f;

                //見つけたタイルの一番左側の配列上の要素数に変換(タイルのナンバリングはVectorで行なっているがゲームオブジェクトはリスト管理のため)
                slectedTiles = slectedTiles * 3f;

                //左側に変換された要素数に「0,1,2」を足して列をピックアップ(yが0なら「0,1,2」,yが1なら「3,4,5」)
                tilesNumber = new Vector3(slectedTiles + 0,slectedTiles + 1,slectedTiles + 2);

                //ごーしゅー(ピックアップされた列をプレイヤーの進行方向に配置してリユースする)
                ShiftTileToAdjacentPosition(ScrollDirection.Up,tilesNumber,next);

                //次に「Left」の処理を済ませる

                //現在のタイルから一つ右のタイルをピックアップ(現在のタイルが2(右端)ならその右は0(左端))
                slectedTiles = (_centerCoordinateNum.x == 2f) ? 0f  : _centerCoordinateNum.x + 1f;

                //選ばれたタイルにyによる上昇分を加味して列をピックアップ(xが0なら「0,3,6」,xが1なら「1,4,7」)
                tilesNumber = new Vector3(0 + slectedTiles,3 + slectedTiles,6 + slectedTiles);

                //ごーしゅー(ピックアップされた列をプレイヤーの進行方向に配置してリユースする)
                ShiftTileToAdjacentPosition(ScrollDirection.Left,tilesNumber,next);

                break;
            //想定外の判定を吸うためのコード
            default:

                break;
        }
    }

    /// <summary>
    /// タイルの移動処理
    /// </summary>
    /// <param name="direction">移動方向</param>
    /// <param name="tiles">移動するタイルの要素数(_mapTilesでの)</param>
    /// <param name="next">移動さきのタイルナンバー</param>
    private void ShiftTileToAdjacentPosition(ScrollDirection direction,Vector3 tiles,Vector2 next)
    {

        int tileNumber = (int)next.y * 3 + (int)next.x;

        switch(direction)
        {
            //
            case ScrollDirection.Up:

                _mapTiles[(int)tiles.x].transform.position = new Vector3(_mapTiles[(int)tiles.x].transform.position.x,
                                                                    _mapTiles[tileNumber].transform.position.y + DisplayResolution.y,
                                                                    _mapTiles[(int)tiles.x].transform.position.z);

                _mapTiles[(int)tiles.y].transform.position = new Vector3(_mapTiles[(int)tiles.y].transform.position.x,
                                                                    _mapTiles[tileNumber].transform.position.y + DisplayResolution.y,
                                                                    _mapTiles[(int)tiles.x].transform.position.z);

                _mapTiles[(int)tiles.z].transform.position = new Vector3(_mapTiles[(int)tiles.z].transform.position.x,
                                                                    _mapTiles[tileNumber].transform.position.y + DisplayResolution.y,
                                                                    _mapTiles[(int)tiles.x].transform.position.z);                                                    

                break;
            //
            case ScrollDirection.Right:

                _mapTiles[(int)tiles.x].transform.position = new Vector3(_mapTiles[tileNumber].transform.position.x + DisplayResolution.x,
                                                                        _mapTiles[(int)tiles.x].transform.position.y,
                                                                        _mapTiles[(int)tiles.x].transform.position.z);

                _mapTiles[(int)tiles.y].transform.position = new Vector3(_mapTiles[tileNumber].transform.position.x + DisplayResolution.x,
                                                                        _mapTiles[(int)tiles.y].transform.position.y,
                                                                        _mapTiles[(int)tiles.x].transform.position.z);

                _mapTiles[(int)tiles.z].transform.position = new Vector3(_mapTiles[tileNumber].transform.position.x + DisplayResolution.x,
                                                                        _mapTiles[(int)tiles.z].transform.position.y,
                                                                        _mapTiles[(int)tiles.x].transform.position.z);
                break;
            //
            case ScrollDirection.Under:

                _mapTiles[(int)tiles.x].transform.position = new Vector3(_mapTiles[(int)tiles.x].transform.position.x,
                                                                        _mapTiles[tileNumber].transform.position.y - DisplayResolution.y,
                                                                        _mapTiles[(int)tiles.x].transform.position.z);

                _mapTiles[(int)tiles.y].transform.position = new Vector3(_mapTiles[(int)tiles.y].transform.position.x,
                                                                        _mapTiles[tileNumber].transform.position.y - DisplayResolution.y,
                                                                        _mapTiles[(int)tiles.x].transform.position.z);

                _mapTiles[(int)tiles.z].transform.position = new Vector3(_mapTiles[(int)tiles.z].transform.position.x,
                                                                        _mapTiles[tileNumber].transform.position.y - DisplayResolution.y,
                                                                        _mapTiles[(int)tiles.x].transform.position.z);

                break;
            //
            case ScrollDirection.Left:

                _mapTiles[(int)tiles.x].transform.position = new Vector3(_mapTiles[tileNumber].transform.position.x - DisplayResolution.x,
                                                                        _mapTiles[(int)tiles.x].transform.position.y,
                                                                        _mapTiles[(int)tiles.x].transform.position.z);

                _mapTiles[(int)tiles.y].transform.position = new Vector3(_mapTiles[tileNumber].transform.position.x - DisplayResolution.x,
                                                                        _mapTiles[(int)tiles.y].transform.position.y,
                                                                        _mapTiles[(int)tiles.x].transform.position.z);

                _mapTiles[(int)tiles.z].transform.position = new Vector3(_mapTiles[tileNumber].transform.position.x - DisplayResolution.x,
                                                                        _mapTiles[(int)tiles.z].transform.position.y,
                                                                        _mapTiles[(int)tiles.x].transform.position.z);

                break;
        }
    }

    /// <summary>
    /// タイルの縦横幅を取得する関数
    /// </summary>
    /// <returns>タイルの縦幅と横幅を持ったVector配列</returns>
    public Vector2 GetTileWidthHeight()
    {
        return DisplayResolution;
    }
}
