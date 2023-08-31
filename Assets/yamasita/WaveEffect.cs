/*using UnityEngine;

public class WaveEffect : MonoBehaviour
{
    // �����T�C�Y�̂͂����ő�T�C�Y�ɂȂ��Ă���ۂ�
    [SerializeField]
    private float initialSize = 0.75f;
    // �g�呬�x
    [SerializeField]
    private float expandSpeed = 2f;

    private SpriteRenderer spriteRenderer;
    private bool isMouseClicked = false;

    public static WaveEffect Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    // �}�E�X�̈ʒu�����[���h���W�ɕϊ�
        //    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    mousePosition.z = 0f;

        //    // �G�t�F�N�g�𐶐�����ʒu���}�E�X�̈ʒu�ɐݒ�
        //    transform.position = mousePosition;

        //    // �G�t�F�N�g����
        //    GameObject effect = Instantiate(gameObject, mousePosition, Quaternion.identity);
        //    WaveEffect waveEffect = effect.GetComponent<WaveEffect>();
        //    waveEffect.isMouseClicked = true;
        //}

        if (isMouseClicked)
        {
            // �g��A�j���[�V����
            transform.localScale += new Vector3(expandSpeed, expandSpeed, 0f) * Time.deltaTime;

            // �A���t�@�l�̌v�Z
            Color color = spriteRenderer.color;
            //float alpha = 1f - (transform.localScale.x / initialSize);
            //color.a = alpha;

            color.a -= 1f * Time.deltaTime;

            // �A���t�@�l��ݒ�
            spriteRenderer.color = color;

            Debug.Log(color);


            // �G�t�F�N�g�̏I������
            if (transform.localScale.x >= initialSize)
            {
                Destroy(gameObject);
            }
        }
    }

    public void CreateEffectObject(Vector2 position)
    {
        if (Input.GetMouseButtonDown(0))
        {
            // �}�E�X�̈ʒu�����[���h���W�ɕϊ�
            Vector3 mousePosition = position;
            mousePosition.z = 0f;

            // �G�t�F�N�g�𐶐�����ʒu���}�E�X�̈ʒu�ɐݒ�
            transform.position = mousePosition;

            // �G�t�F�N�g����
            GameObject effect = Instantiate(gameObject, mousePosition, Quaternion.identity);
            WaveEffect waveEffect = effect.GetComponent<WaveEffect>();
            waveEffect.GetComponent<SpriteRenderer>().color = new Color(255f,255f,255f,1f);
            waveEffect.isMouseClicked = true;
        }
    }
}
*/