using UnityEngine;

public class WaveEffect : MonoBehaviour
{
    [SerializeField]
    private float initialSize = 0.2f; // 初期サイズ
    [SerializeField]
    private float expandSpeed = 2f; // 拡大速度

    private SpriteRenderer spriteRenderer;
    private bool isMouseClicked = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseClicked = true;

            // マウスの位置をワールド座標に変換
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            // エフェクトを生成する位置をマウスの位置に設定
            transform.position = mousePosition;
        }

        if (isMouseClicked)
        {
            // 拡大アニメーション
            transform.localScale += new Vector3(expandSpeed, expandSpeed, 0f) * Time.deltaTime;

            // アルファ値の計算
            Color color = spriteRenderer.color;
            float alpha = 1f - (transform.localScale.x / initialSize);
            color.a = alpha;

            // アルファ値を設定
            spriteRenderer.color = color;

            // エフェクトの終了判定
            if (transform.localScale.x >= initialSize)
            {
                Destroy(gameObject);
            }
        }
    }
}
