using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YK_Goal : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (ObjPlayer.instance.GetSetPos.x >= this.gameObject.transform.position.x)
        {
            //�g�����W�V�������|���ăV�[���J�ڂ���
            Fade.instance.FadeIn(1f, () =>
        {
            GameManager.GetSetGameState = GameState.Result;
            YK_JsonSave.instance.MyScoreSave(YK_Score.instance.GetSetScore);
            SceneManager.LoadScene("ResultScene");
        });
        }
    }
}
