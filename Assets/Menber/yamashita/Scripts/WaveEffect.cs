using UnityEngine;

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

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // �}�E�X�̈ʒu�����[���h���W�ɕϊ�
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            // �G�t�F�N�g�𐶐�����ʒu���}�E�X�̈ʒu�ɐݒ�
            transform.position = mousePosition;

            // �G�t�F�N�g����
            GameObject effect = Instantiate(gameObject, mousePosition, Quaternion.identity);
            WaveEffect waveEffect = effect.GetComponent<WaveEffect>();
            waveEffect.isMouseClicked = true;
        }

        if (isMouseClicked)
        {
            // �g��A�j���[�V����
            transform.localScale += new Vector3(expandSpeed, expandSpeed, 0f) * Time.deltaTime;

            // �A���t�@�l�̌v�Z
            Color color = spriteRenderer.color;
            float alpha = 1f - (transform.localScale.x / initialSize);
            color.a = alpha;

            // �A���t�@�l��ݒ�
            spriteRenderer.color = color;

            // �G�t�F�N�g�̏I������
            if (transform.localScale.x >= initialSize)
            {
                Destroy(gameObject);
            }
        }
    }
}
