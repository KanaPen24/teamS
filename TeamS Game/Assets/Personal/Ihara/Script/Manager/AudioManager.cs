/**
 * @file   AudioManager.cs
 * @brief  音管理のクラス
 * @author IharaShota
 * @date   2023/04/17
 * @Update 2023/04/17 作成
 * @Update 2023/10/13 改修
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===============================================
// SEType
// … SEの種類を管理する列挙体
// ===============================================
public enum SEType
{
    SE_PlayerWalk,   // Playerの歩行SE
    SE_PlayerJump,   // Playerの跳躍SE
    SE_PlayerLanding,// Playerの着地SE
    SE_PlayerAtk,    // Playerの攻撃SE
    SE_Avoidance,    // 回避の音
    SE_GameClear,    // ゲームクリアSE
    SE_Select,       // 選択SE
    SE_AddScore,     // スコア加算SE
    SE_EFly,
    SE_Bound,
    SE_PlayerSPAtk,
    SE_EExplosion,
    SE_PEAtkDamage,

    MAX_SE
}

// ===============================================
// BGMType
// … BGMの種類を管理する列挙体
// ===============================================
public enum BGMType
{
    BGM_TITLE, // タイトルBGM
    BGM_GAME,  // ゲームBGM
    BGM_END,   // エンドBGM
    BGM_GAMEOVER,// ゲームオーバーBGM
    BGM_BOSS,
    MAX_BGM
}
public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    private class C_SE
    {
        public AudioSource m_SEData;
        public SEType m_SEType;
    }
    [System.Serializable]
    private class C_BGM
    {
        public AudioSource m_BGMData;
        public BGMType m_BGMType;
    }

    [SerializeField] private List<C_SE> SESources; // SEのデータ
    [SerializeField] private List<C_BGM> BGMSources;// BGMのデータ
    public static AudioManager instance;         // IS_AudioManagerのインスタンス

    /**
     * @fn
     * 初期化処理(外部参照を除く)
     * @brief  メンバ初期化処理
     * @detail 特に無し
     */
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("AudioManagerはすでに存在しています");
            Destroy(gameObject);
        }
    }

    /**
     * @fn
     * 初期化処理(外部参照あり)
     * @brief  メンバ初期化処理
     * @detail SEとBGMの数があっているか確認する
     */
    private void Start()
    {
        // SEと列挙型の数が違えばログ出力
        if (SESources.Count != (int)SEType.MAX_SE)
        {
            Debug.Log("SEとSETypeの数が合いません");
        }

        // BGMと列挙型の数が違えばログ出力
        if (BGMSources.Count != (int)BGMType.MAX_BGM)
        {
            Debug.Log("BGMとBGMTypeの数が合いません");
        }
    }

    /**
     * @fn
     * SE再生
     * @param seType … SEの種類
     * @brief  SEの種類を指定して再生
     * @detail SEとseTypeの数があっていることが前提
     */
    public void PlaySE(SEType seType)
    {
        for (int i = 0, size = SESources.Count; i < size; ++i)
        {
            if (SESources[i].m_SEType == seType)
            {
                SESources[i].m_SEData.Play();
                return;
            }
        }
    }

    /**
     * @fn
     * BGM再生
     * @param bgmType … BGMの種類
     * @brief  BGMの種類を指定して再生
     * @detail BGMとbgmTypeの数があっていることが前提
     */
    public void PlayBGM(BGMType bgmType)
    {
        for (int i = 0, size = BGMSources.Count; i < size; ++i)
        {
            if (BGMSources[i].m_BGMType == bgmType)
            {
                BGMSources[i].m_BGMData.Play();
                return;
            }
        }
    }

    /**
     * @fn
     * SEストップ
     * @param seType … SEの種類
     * @brief  SEの種類を指定してストップ
     * @detail SEとseTypeの数があっていることが前提
     */
    public void StopSE(SEType seType)
    {
        for (int i = 0, size = SESources.Count; i < size; ++i)
        {
            if (SESources[i].m_SEType == seType)
            {
                SESources[i].m_SEData.Stop();
                return;
            }
        }
    }

    /**
     * @fn
     * BGMストップ
     * @param bgmType … BGMの種類
     * @brief  BGMの種類を指定してストップ
     * @detail BGMとbgmTypeの数があっていることが前提
     */
    public void StopBGM(BGMType bgmType)
    {
        for (int i = 0, size = BGMSources.Count; i < size; ++i)
        {
            if (BGMSources[i].m_BGMType == bgmType)
            {
                BGMSources[i].m_BGMData.Stop();
                return;
            }
        }
    }

    /**
     * @fn
     * BGM全ストップ
     * @param なし
     * @brief  BGM全ストップ
     * @detail BGMとbgmTypeの数があっていることが前提
     */
    public void AllStopBGM()
    {
        for (int i = 0, size = BGMSources.Count; i < size; ++i)
        {
            BGMSources[i].m_BGMData.Stop();
        }
    }

    /**
    * @fn
    * SE全ストップ
    * @param なし
    * @brief  SE全ストップ
    * @detail SEとseTypeの数があっていることが前提
    */
    public void AllStopSE()
    {
        for (int i = 0, size = SESources.Count; i < size; ++i)
        {
            SESources[i].m_SEData.Stop();
        }
    }

    public AudioSource GetSE(SEType seType)
    {
        for (int i = 0, size = SESources.Count; i < size; ++i)
        {
            if (SESources[i].m_SEType == seType)
            {
                return SESources[i].m_SEData;
            }
        }

        // 無かった場合…
        return null;
    }

    public AudioSource GetBGM(BGMType bgmType)
    {
        for (int i = 0, size = BGMSources.Count; i < size; ++i)
        {
            if (BGMSources[i].m_BGMType == bgmType)
            {
                return BGMSources[i].m_BGMData;
            }
        }

        // 無かった場合…
        return null;
    }
}
