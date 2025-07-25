using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�Q�[���S�̂Ɋ֌W������̂��Ǘ�����}�l�[�W���[�X�N���v�g
public class GameManager : MonoBehaviour
{
    //�ϐ��錾
    public static GameManager Instance { get; private set; }        //���X�N���v�g��GameManager���擾�ł���p�i�V���O���g�����j
    public Camera mainCamera;       //CameraComponent�Q�Ɨp

    private List<GameObject> blocksPrehub;      //�u���b�N���X�g
    private List<GameObject> ballsPrehub;       //�{�[�����X�g

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�X�e�[�W���ɂ���u���b�N�y�у{�[����List<>�Ŏ擾
        blocksPrehub = new List<GameObject>(GameObject.FindGameObjectsWithTag("Block"));
        ballsPrehub = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ball"));
        
        //�J�����̍Đݒ�
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //��ʓ���1�ł��u���b�N�܂��̓{�[��������ۂ̃t���O
        // ���t���[���A�t���O��false�ɏ�����
        bool anyBlockInView = false;
        bool anyBallInView = false;

        //�u���b�N�̉�ʓ�����
        //foreach : �R���N�V�����i�z��⃊�X�g�Ȃǂ̕����̗v�f���܂Ƃ߂����́j�����Ԃ�1�����o���ď���������
        //var block -> blocksPrehub����1�̗v�f
        foreach (var block in blocksPrehub)
        {
            //������j��ς݃u���b�N�ɓ������Ă��܂�����X�L�b�v����
            if (block == null) continue;

            //���[���h���W���r���[�|�[�g���W�ɕϊ��i�r���[�|�[�g���W��(0,0)����ʍ����A(1,1)����ʉE��ŕ\�����j
            Vector3 viewportPos = mainCamera.WorldToViewportPoint(block.transform.position);
            //��ʓ��Ƀu���b�N�����݂��Ă��邩�ǂ����iviewportPos.z > 0 -> �J�����̑O�ɂ��邩�ǂ����j
            if (viewportPos.x >= 0 && viewportPos.x <= 1 &&�@viewportPos.y >= 0 && viewportPos.y <= 1 &&�@viewportPos.z > 0)
            {
                //�u���b�N��1�ł����݂��Ă���̂�true�ɕύX
                anyBlockInView = true;
                break;
            }
        }

        //�{�[���̉�ʓ�����i���g�̓u���b�N�̔���Ɠ��l�j
        foreach (var ball in ballsPrehub)
        {
            //������j��ς݃{�[���ɓ������Ă��܂�����X�L�b�v����
            if (ball == null) continue;

            Vector3 viewportPos = mainCamera.WorldToViewportPoint(ball.transform.position);
            if (viewportPos.x >= 0 && viewportPos.x <= 1 &&�@viewportPos.y >= 0 && viewportPos.y <= 1 &&�@viewportPos.z > 0)
            {
                //�{�[����1�ł����݂��Ă���̂�true�ɕύX
                anyBallInView = true;
                break;
            }
        }

        //�u���b�N�܂��̓{�[����1����ʓ��ɂȂ���΁itrue�łȂ������ꍇ�j�Q�[�����Z�b�g
        if (!anyBlockInView || !anyBallInView)
        {
            ResetGame();
        }
    }

    // �u���b�N������X�N���v�g���ŌĂԊ֐�
    public void OnBlockDestroyed(GameObject destroyedBlock)
    {
        //�X�R�A���Z
        ScoreManager.Instance.AddScore(100);

        blocksPrehub.Remove(destroyedBlock);
    }

    //�{�[�������X�g�ɓ��I�ɒǉ�����֐�
    public void AddBall(GameObject newBall)
    {
        if (!ballsPrehub.Contains(newBall))
        {
            ballsPrehub.Add(newBall);
        }
    }

    // �{�[����j�����ꂽ���ɌĂԊ֐�
    public void OnBallDestroyed(GameObject destroyedBall)
    {
        //Ball.cs��null�𔭐������Ȃ��ׂ̈��S��
        if (destroyedBall != null && ballsPrehub.Contains(destroyedBall))
        {
            ballsPrehub.Remove(destroyedBall);
        }
    }

    //�Q�[�����Z�b�g
    private void ResetGame()
    {
        //�X�R�A���Z�b�g
        ScoreManager.Instance.ResetScore();

        // ���݂̃V�[�����ēǂݍ��݂��ă��Z�b�g�i����͌�ɕʂ�Scene�ւ̈ڍs�֕ύX�j
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
