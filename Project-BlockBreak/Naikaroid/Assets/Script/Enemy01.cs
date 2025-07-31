using UnityEngine;

public class Enemy01 : MonoBehaviour
{
    public EnemySpawner enemySpawner;       //敵を生成するスクリプトへの参照

    public AudioClip soundEffect;        //効果音を設定する
    private AudioSource audioSource;

    [Header("円移動")]
    public float radius = 1f;
    public float angularSpeed = 2f;
    public float circleMoveSpeed = 1f;

    [Header("ゆらゆら＆下移動")]
    public float waveAmplitude = 1f;
    public float waveFrequency = 1f;
    public float fallSpeed = 1f;

    [Header("時間設定")]
    public float circleDuration = 4f;
    public float waveDuration = 6f;
    public float transitionDuration = 1f;       //切り替えのフェード時間

    private Vector2 circleCenter;
    private float angle = 0f;
    private float elapsedTime = 0f;
    private Vector2 startPosition;

    private enum State { Circle, TransitionToWave, Wave, TransitionToCircle }
    private State currentState = State.Circle;

    private float stateTimer = 0f;
    private float blend = 0f;       //0=円移動のみ、1=ゆらゆらのみ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 敵を生成したスクリプトの参照を取得
        enemySpawner = FindFirstObjectByType<EnemySpawner>();

        //AudioSourceスクリプトの取得
        audioSource = GetComponent<AudioSource>();

        startPosition = transform.position;
        circleCenter = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        stateTimer += Time.deltaTime;

        switch (currentState)
        {
            case State.Circle:
                DoCircleMovement();
                if (stateTimer >= circleDuration)
                {
                    currentState = State.TransitionToWave;
                    stateTimer = 0f;
                }
                break;

            case State.TransitionToWave:
                blend = Mathf.Clamp01(stateTimer / transitionDuration);
                DoBlendedMovement(blend);
                if (stateTimer >= transitionDuration)
                {
                    currentState = State.Wave;
                    stateTimer = 0f;
                }
                break;

            case State.Wave:
                DoWaveMovement();
                if (stateTimer >= waveDuration)
                {
                    currentState = State.TransitionToCircle;
                    stateTimer = 0f;
                    //円移動開始時の中心を現在位置にリセット
                    circleCenter = transform.position;
                    angle = 0f;
                }
                break;

            case State.TransitionToCircle:
                blend = 1f - Mathf.Clamp01(stateTimer / transitionDuration);
                DoBlendedMovement(blend);
                if (stateTimer >= transitionDuration)
                {
                    currentState = State.Circle;
                    stateTimer = 0f;
                }
                break;
        }
    }

    void DoCircleMovement()
    {
        circleCenter += Vector2.down * circleMoveSpeed * Time.deltaTime;
        angle += angularSpeed * Time.deltaTime;
        if (angle > Mathf.PI * 2f) angle -= Mathf.PI * 2f;

        Vector2 circlePos = circleCenter + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        transform.position = circlePos;
    }

    void DoWaveMovement()
    {
        float x = Mathf.Sin(elapsedTime * waveFrequency) * waveAmplitude;
        float y = startPosition.y - fallSpeed * elapsedTime;
        transform.position = new Vector2(x, y);
    }

    void DoBlendedMovement(float blend)
    {
        //円移動位置
        circleCenter += Vector2.down * circleMoveSpeed * Time.deltaTime;
        angle += angularSpeed * Time.deltaTime;
        if (angle > Mathf.PI * 2f) angle -= Mathf.PI * 2f;
        Vector2 circlePos = circleCenter + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

        //ゆらゆら移動位置
        float x = Mathf.Sin(elapsedTime * waveFrequency) * waveAmplitude;
        float y = startPosition.y - fallSpeed * elapsedTime;
        Vector2 wavePos = new Vector2(x, y);

        //位置を補間（線形補間）
        transform.position = Vector2.Lerp(circlePos, wavePos, blend);
    }

    void OnDestroy()
    {
        // 敵が破壊されたときに、敵の数を減らす
        if (enemySpawner != null)
        {
            enemySpawner.EnemyDestroyed();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーかボールに接触すると破棄される
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Player"))
        {
            //爆発音を鳴らす
            if (soundEffect != null)
            {
                //PlayOneShotで効果音を再生
                audioSource.PlayOneShot(soundEffect);
                Debug.Log("効果音再生中");
            }
            else
            {
                Debug.Log("効果音が再生できません");
            }

            //ボールに当たった時のみスコア加算を行う
            if (collision.gameObject.CompareTag("Ball"))
            {
                //スコア加算
                ScoreManager.Instance.AddScore(500);
            }

            //破棄処理
            OnDestroy();
            Destroy(this.gameObject);
        }
    }
}
