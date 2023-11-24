using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YK_ReturnDeath : MonoBehaviour
{
    /// <summary>
    /// �f�o�b�O���[�h .
    /// </summary>
    public bool DebugMode = true;
    /// <summary>�t�F�[�h���̓����x</summary>
    private float fadeAlpha = 0;
    /// <summary>�t�F�[�h�����ǂ���</summary>
    private bool isFading = false;
    /// <summary>�t�F�[�h�F</summary>
    public Color fadeColor = Color.black;
    /// <summary>�t�F�[�h�F</summary>
    [SerializeField] private float interval = 0.2f;


    private void Update()
    {
        //�e�X�g
        if (Input.GetKeyDown(KeyCode.F6))
        {
            StartCoroutine(Fade(interval));
        }
    }

    /// <summary>
    /// �V�[���J�ڗp�R���[�`�� .
    /// </summary>
    /// <param name='scene'>�V�[����</param>
    /// <param name='interval'>�Ó]�ɂ����鎞��(�b)</param>
    private IEnumerator Fade(float interval)
    {
        //���񂾂�Â� .
        this.isFading = true;
        float time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        //���񂾂񖾂邭 .
        time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        this.isFading = false;
    }
}