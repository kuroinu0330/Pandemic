using UnityEngine;

public class WaveEffectController : MonoBehaviour
{
    public GameObject wavePrefab; // Waveプレハブの参照
    public float spawnInterval = 0.5f; // エフェクト生成の間隔
    private float timer = 0f; // タイマー

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
