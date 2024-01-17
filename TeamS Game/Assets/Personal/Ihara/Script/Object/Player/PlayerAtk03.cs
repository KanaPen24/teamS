/**
 * @file   PlayerAtk02.cs
 * @brief  Playerの「攻撃03」クラス
 * @author IharaShota
 * @date   2023/10/27
 * @Update 2023/10/27 作成
 **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerAtk03 : PlayerStrategy
{
    [SerializeField] private Vector3 m_vAtkArea;
    [SerializeField] private float m_fLength;
    [SerializeField] private float m_fAtkTime;
    [SerializeField] private VisualEffect atkEffect;
    private int atknum = -1;
    private float m_fTime;

    public override void UpdateState()
    {
        // アニメーションが終わったら
        if (ObjPlayer.instance.Anim.GetAnimNormalizeTime(PlayerAnimState.Atk03,1f))
        {
            // 攻撃 → 待ち
            if (ObjPlayer.instance.GetSetGround.m_bStand)
            {
                ObjPlayer.instance.m_PlayerState = PlayerState.Idle;
                EndState();
                return;
            }
        }
    }

    public override void UpdatePlayer()
    {
        // 遷移最初の処理
        if (m_bStartFlg) StartState();

        // 攻撃中は
        if (atknum != -1)
        {
            // 座標更新
            if (ObjPlayer.instance.GetSetDir == ObjDir.RIGHT)
                ON_HitManager.instance.SetCenter(atknum, ObjPlayer.instance.GetSetPos + new Vector3(m_fLength, 0f, 0f));
            else if (ObjPlayer.instance.GetSetDir == ObjDir.LEFT)
                ON_HitManager.instance.SetCenter(atknum, ObjPlayer.instance.GetSetPos - new Vector3(m_fLength, 0f, 0f));

            // 攻撃時間が終わったら
            if (m_fTime <= 0)
            {
                // 攻撃の当たり判定削除
                ON_HitManager.instance.DeleteHit(atknum);

                // 攻撃の当たり判定IDを-1にする
                atknum = -1;

                m_fTime = 0;
            }
            else m_fTime -= Time.deltaTime;
        }

        if (ObjPlayer.instance.GetSetHitStopParam.m_bHitStop)
        {
            atkEffect.playRate = 0.1f;
        }
        else atkEffect.playRate = 1f;

        // 速度は減速(仮)
        ObjPlayer.instance.GetSetSpeed *= new Vector2(0.8f, 1f);
    }

    public override void StartState()
    {
        // アニメ―ション変更
        ObjPlayer.instance.Anim.ChangeAnim(PlayerAnimState.Atk02);

        // 攻撃の当たり判定生成(向きによって生成位置が変わる)
        if (ObjPlayer.instance.GetSetDir == ObjDir.RIGHT)
        {
            atknum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos + new Vector3(m_fLength, 0f, 0f),
            m_vAtkArea, true, HitType.ATTACK, ObjPlayer.instance.GetSetObjID);

            ObjPlayer.instance.GetSetSpeed += new Vector2(5f, 0f);

            // エフェクト再生
            Invoke(nameof(StartHitEffect), 0.3f);
            atkEffect.transform.position = this.gameObject.transform.position + new Vector3(0.4f,0.5f,0.0f);
            atkEffect.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (ObjPlayer.instance.GetSetDir == ObjDir.LEFT)
        {
            atknum = ON_HitManager.instance.GenerateHit(ObjPlayer.instance.GetSetPos - new Vector3(m_fLength, 0f, 0f),
            m_vAtkArea, true, HitType.ATTACK, ObjPlayer.instance.GetSetObjID);

            ObjPlayer.instance.GetSetSpeed += new Vector2(-5f, 0f);

            // エフェクト再生
            Invoke(nameof(StartHitEffect), 0.3f);
            atkEffect.transform.position = this.gameObject.transform.position + new Vector3(-0.4f, 0.5f, 0.0f); ;
            atkEffect.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        // 当たり判定時間セット
        m_fTime = m_fAtkTime;

        // 攻撃SEを再生
        AudioManager.instance.PlaySE(SEType.SE_PlayerAtk);

        // 遷移最初のフラグをoff
        m_bStartFlg = false;
    }

    public override void EndState()
    {
        // 遷移最初のフラグをONにしておく
        m_bStartFlg = true;
    }

    public void StartHitEffect()
    {
        atkEffect.Play();
    }
}
