/**
 * @file   IS_XboxInput.cs
 * @brief  Xboxの入力クラス
 * @author IharaShota
 * @date   2023/06/12
 * @Update 2023/06/12 作成
 * @Update 2023/10/27 Inspectorで入力確認できるように修正
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckInput
{
    public bool A;
    public bool B;
    public bool X;
    public bool Y;
    public bool LB;
    public bool RB;
    public bool View;
    public bool Menu;
    public bool LStick;
    public bool RStick;

    public float LStick_H;
    public float LStick_V;
    public float RStick_H;
    public float RStick_V;
    public float DPad_H;
    public float DPad_V;
    public float L_Trigger;
    public float R_Trigger;
}

public class IS_XBoxInput : MonoBehaviour
{
    public CheckInput checkInput;

    static public string A = "joystick button 0";
    static public string B = "joystick button 1";
    static public string X = "joystick button 2";
    static public string Y = "joystick button 3";
    static public string LB = "joystick button 4";
    static public string RB = "joystick button 5";
    static public string View = "joystick button 6";
    static public string Menu = "joystick button 7";
    static public string LStick = "joystick button 8";
    static public string RStick = "joystick button 9";

    static public float LStick_H;
    static public float LStick_V;
    static public float RStick_H;
    static public float RStick_V;
    static public float DPad_H;
    static public float DPad_V;
    static public float LR_Trigger;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LStick_H = Input.GetAxis("L_Stick_H");
        LStick_V = Input.GetAxis("L_Stick_V");
        RStick_H = Input.GetAxis("R_Stick_H");
        RStick_V = Input.GetAxis("R_Stick_V");
        DPad_H = Input.GetAxis("D_Pad_H");
        DPad_V = Input.GetAxis("D_Pad_V");
        LR_Trigger = Input.GetAxis("L_R_Trigger");

        if (Input.GetKey(A))
            checkInput.A = true;
        else checkInput.A = false;

        if (Input.GetKey(B))
            checkInput.B = true;
        else checkInput.B = false;

        if (Input.GetKey(X))
            checkInput.X = true;
        else checkInput.X = false;

        if (Input.GetKey(Y))
            checkInput.Y = true;
        else checkInput.Y = false;

        if (Input.GetKey(LB))
            checkInput.LB = true;
        else checkInput.LB = false;

        if (Input.GetKey(RB))
            checkInput.RB = true;
        else checkInput.RB = false;

        if (Input.GetKey(View))
            checkInput.View = true;
        else checkInput.View = false;

        if (Input.GetKey(Menu))
            checkInput.Menu = true;
        else checkInput.Menu = false;

        if (Input.GetKey(LStick))
            checkInput.LStick = true;
        else checkInput.LStick = false;

        if (Input.GetKey(RStick))
            checkInput.RStick = true;
        else checkInput.RStick = false;

        checkInput.LStick_H = LStick_H;
        checkInput.LStick_V = LStick_V;
        checkInput.RStick_H = RStick_H;
        checkInput.RStick_V = RStick_V;
        checkInput.DPad_H = DPad_H;
        checkInput.DPad_V = DPad_V;

        if (LR_Trigger > 0)
        {
            checkInput.R_Trigger = LR_Trigger;
            checkInput.L_Trigger = 0f;
        }
        else
        {
            checkInput.L_Trigger = LR_Trigger;
            checkInput.R_Trigger = 0f;
        }
    }
}
