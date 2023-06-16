using UnityEngine;

public class WaveEffect : MonoBehaviour
{
    [SerializeField]
    private float initialSize = 0.2f; // �����T�C�Y
    [SerializeField]
    private float expandSpeed = 2f; // �g�呬�x

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

            // �}�E�X�̈ʒu�����[���h���W�ɕϊ�
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            // �G�t�F�N�g�𐶐�����ʒu���}�E�X�̈ʒu�ɐݒ�
            transform.position = mousePosition;
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
