using UnityEngine;

public class Enemy01 : MonoBehaviour
{
    public EnemySpawner enemySpawner;       //�G�𐶐�����X�N���v�g�ւ̎Q��

    public AudioClip soundEffect;        //���ʉ���ݒ肷��
    private AudioSource audioSource;

    [Header("�~�ړ�")]
    public float radius = 1f;
    public float angularSpeed = 2f;
    public float circleMoveSpeed = 1f;

    [Header("����灕���ړ�")]
    public float waveAmplitude = 1f;
    public float waveFrequency = 1f;
    public float fallSpeed = 1f;

    [Header("���Ԑݒ�")]
    public float circleDuration = 4f;
    public float waveDuration = 6f;
    public float transitionDuration = 1f;       //�؂�ւ��̃t�F�[�h����

    private Vector2 circleCenter;
    private float angle = 0f;
    private float elapsedTime = 0f;
    private Vector2 startPosition;

    private enum State { Circle, TransitionToWave, Wave, TransitionToCircle }
    private State currentState = State.Circle;

    private float stateTimer = 0f;
    private float blend = 0f;       //0=�~�ړ��̂݁A1=�����̂�

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // �G�𐶐������X�N���v�g�̎Q�Ƃ��擾
        enemySpawner = FindFirstObjectByType<EnemySpawner>();

        //AudioSource�X�N���v�g�̎擾
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
                    //�~�ړ��J�n���̒��S�����݈ʒu�Ƀ��Z�b�g
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
        //�~�ړ��ʒu
        circleCenter += Vector2.down * circleMoveSpeed * Time.deltaTime;
        angle += angularSpeed * Time.deltaTime;
        if (angle > Mathf.PI * 2f) angle -= Mathf.PI * 2f;
        Vector2 circlePos = circleCenter + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

        //�����ړ��ʒu
        float x = Mathf.Sin(elapsedTime * waveFrequency) * waveAmplitude;
        float y = startPosition.y - fallSpeed * elapsedTime;
        Vector2 wavePos = new Vector2(x, y);

        //�ʒu���ԁi���`��ԁj
        transform.position = Vector2.Lerp(circlePos, wavePos, blend);
    }

    void OnDestroy()
    {
        // �G���j�󂳂ꂽ�Ƃ��ɁA�G�̐������炷
        if (enemySpawner != null)
        {
            enemySpawner.EnemyDestroyed();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[���{�[���ɐڐG����Ɣj�������
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Player"))
        {
            //��������炷
            if (soundEffect != null)
            {
                //PlayOneShot�Ō��ʉ����Đ�
                audioSource.PlayOneShot(soundEffect);
                Debug.Log("���ʉ��Đ���");
            }
            else
            {
                Debug.Log("���ʉ����Đ��ł��܂���");
            }

            //�{�[���ɓ����������̂݃X�R�A���Z���s��
            if (collision.gameObject.CompareTag("Ball"))
            {
                //�X�R�A���Z
                ScoreManager.Instance.AddScore(500);
            }

            //�j������
            OnDestroy();
            Destroy(this.gameObject);
        }
    }
}
