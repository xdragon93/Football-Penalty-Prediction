using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShootDebug : MonoBehaviour {

    public Slider _sliderVelocityZ;
    public Text _labelSpeedZ;

    public Slider _sliderBallZ;
    public Text _labelBallZ;

    public Slider _sliderBallX;
    public Text _labelBallX;

    public Slider _sliderBallControlLimit;
    public Text _labelBallControlLimit;


    void Start()
    {
        Shoot.EventChangeSpeedZ += OnChangeSpeedZ;
        Shoot.EventChangeBallZ += OnChangeBallZ;
        Shoot.EventChangeBallX += OnChangeBallX;
        Shoot.EventChangeBallLimit += OnChangeBallControlLimit;
        Shoot.EventOnCollisionEnter += OnCollisionEnter;

        if (_sliderBallZ)
        {
            _sliderBallZ.value = 0.5f;
        }
        if (_sliderBallX)
        {
            _sliderBallX.value = 0.5f;
        }
    }

    void OnDestroy()
    {
        Shoot.EventChangeSpeedZ -= OnChangeSpeedZ;
        Shoot.EventChangeBallZ -= OnChangeBallZ;
        Shoot.EventChangeBallX -= OnChangeBallX;
        Shoot.EventChangeBallLimit -= OnChangeBallControlLimit;
        Shoot.EventOnCollisionEnter -= OnCollisionEnter;
    }


    void OnChangeSpeedZ(float val)
    {
        if (_sliderVelocityZ)
        {
            _sliderVelocityZ.value = (val - Shoot.share._speedMin) / (Shoot.share._speedMax - Shoot.share._speedMin);
        }
        if (_labelSpeedZ)
        {
            _labelSpeedZ.text = Shoot.share._zVelocity.ToString("00.00") + "m";
        }
    }

    public void OnChange_SlideSpeedZ(float val)
    {
        Shoot.share._zVelocity = Mathf.Lerp(Shoot.share._speedMin, Shoot.share._speedMax, val);

        if (_labelSpeedZ)
            _labelSpeedZ.text = Shoot.share._zVelocity.ToString("00.00") + "m";
    }


    void OnChangeBallZ(float val)
    {
        if (_sliderBallZ)
        {
            //_sliderBallZ.value = (-val - Shoot.share._distanceMinZ)/(Shoot.share._distanceMaxZ - Shoot.share._distanceMinZ);
            //_labelBallZ.text = (-Shoot.share.BallPositionZ).ToString("00.00") + "m";
        }
    }

    public void OnChange_SliderBallZ(float val)
    {
        if (val == 0)
            Shoot.share.BallPositionZ = -16.5f;
        else
        {
            Shoot.share.BallPositionZ = (int)-Mathf.Lerp(Shoot.share._distanceMinZ, Shoot.share._distanceMaxZ, val);
        }
        _labelBallZ.text = "" + (-Shoot.share.BallPositionZ).ToString("00.00") + "m"; ;

    }

    void OnChangeBallX(float val)
    {
        if (_sliderBallX)
        {
            //_sliderBallX.value = (val - Shoot.share._distanceMinX) / (Shoot.share._distanceMaxX - Shoot.share._distanceMinX);
            //_labelBallX.text = "" + (Shoot.share.BallPositionX).ToString("00.00") + "m"; ;    
        }
        
    }

    public void OnChange_SliderBallX(float val)
    {
        Shoot.share.BallPositionX = (int)Mathf.Lerp(Shoot.share._distanceMinX, Shoot.share._distanceMaxX, val);
        _labelBallX.text = "" + Shoot.share.BallPositionX.ToString("00.00") + "m";
    }


    void OnChangeBallControlLimit(float val)
    {
        _sliderBallControlLimit.value = val/10f;
        _labelBallControlLimit.text = "" + Shoot.share._ballControlLimit;
    }

    public void OnChange_SliderBallControlLimit(float val)
    {
        Shoot.share._ballControlLimit = (int)Mathf.Lerp(0, 10, val);
        _labelBallControlLimit.text = "" + Shoot.share._ballControlLimit;
    }


    void OnCollisionEnter(Collision other)
    {
        string tag = other.gameObject.tag;
        if (tag.Equals("Player") || tag.Equals("Obstacle") || tag.Equals("Net") || tag.Equals("Wall"))
        {	// banh trung thu mon hoac khung thanh hoac da vao luoi roi thi ko cho banh bay voi van toc nua, luc nay de~ cho physics engine tinh' toan' quy~ dao bay
            if (tag.Equals("Wall"))
            {
                if (SoundManager.share != null)
                    SoundManager.share.playSoundSFX(SOUND_NAME.Ball_Hit_Player_Wall);
            }
            else if (tag.Equals("Net"))
            {
                if (SoundManager.share != null)
                    SoundManager.share.playSoundSFX(SOUND_NAME.Ball_Hit_Net);
            }
        }
    }
}
