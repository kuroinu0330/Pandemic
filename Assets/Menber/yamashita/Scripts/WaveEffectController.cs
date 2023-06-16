using UnityEngine;

public class WaveEffectController : MonoBehaviour
{
    public GameObject wavePrefab; // Wave�v���n�u�̎Q��
    public float spawnInterval = 0.5f; // �G�t�F�N�g�����̊Ԋu
    private float timer = 0f; // �^�C�}�[

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnWave();
            timer = 0f;
        }
    }

    private void SpawnWave()
    {
        Instantiate(wavePrefab, transform.position, Quaternion.identity);
    }
}
