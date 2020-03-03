using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShootAIDebug : MonoBehaviour {

    public Text _labelCurve;
    public Text _labelYMiddle;
    public Text _labelYEnd;
    public Slider _sliderCurveRange;

    void Start()
    {
        ShootAI.EventChangeCurveRange += onChange_CurveRange;
    }

    void OnDestroy()
    {
        ShootAI.EventChangeCurveRange -= onChange_CurveRange;
    }

    void onChange_CurveRange(float val)
    {
        if (_sliderCurveRange)
        {
            _sliderCurveRange.value = (val - ShootAI.shareAI._maxLeft) / (ShootAI.shareAI._maxRight - ShootAI.shareAI._maxLeft);
            _labelCurve.text = val.ToString("0.0000");
        }

    }

    public void onValueChanged_PopupShootDirection(int val)
    {
        int temp = val;
        if (temp == 0 )// temp.Equals("Left only"))
        {
            ShootAI.shareAI._shootDirection = Direction.Left;
        }
        else if (temp == 1) // temp.Equals("Right only"))
        {
            ShootAI.shareAI._shootDirection = Direction.Right;
        }
        else
        {
            ShootAI.shareAI._shootDirection = Direction.Both;
        }
    }

    public void onChange_CurveLevel(float val)
    {
        ShootAI.shareAI._curveLevel = val;
    }

    public void onChange_Difficulty(float val)
    {
        ShootAI.shareAI._difficulty = val;
    }

    public void onChange_TestForceGKAI(bool val)
    {
        ShootAI.shareAI.willBeShootByUser = val;
    }

    public void onChange_yMiddle(float val)
    {
        ShootAI.shareAI.yMiddle = Mathf.Lerp(0.145f, 4.3f, val);
        _labelYMiddle.text = "" + ShootAI.shareAI.yMiddle;
    }

    public void onChange_yEnd(float val)
    {
        ShootAI.shareAI.yEnd = Mathf.Lerp(0.145f, 3.5f, val);
        _labelYEnd.text = "" + ShootAI.shareAI.yEnd;
    }

    public void OnClick_Shoot()
    {
        ShootAI.shareAI.shoot();
    }
}
