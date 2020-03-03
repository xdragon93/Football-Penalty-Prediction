using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GKHorizontalFlyDebug : MonoBehaviour
{
    public static GKHorizontalFlyDebug share;

    public Slider _sliderGKDeltaDistance;
    public Text _labelGKDeltaDistance;
    

    public GameObject _buttonGKLeft;
    public GameObject _buttonGKRight;
    public GameObject _buttonShoot;

    private void Awake()
    {
        share = this;
        GoalKeeperHorizontalFly.EventChangeFlyDistance += OnChangeFlyDistance;
        GoalKeeperHorizontalFly.EventChangeIsAIControl += OnChangeIsAIControl;
    }

    void Start () {
    }

    void OnDestroy()
    {
        GoalKeeperHorizontalFly.EventChangeFlyDistance -= OnChangeFlyDistance;
        GoalKeeperHorizontalFly.EventChangeIsAIControl -= OnChangeIsAIControl;
    }


    void OnChangeIsAIControl(bool val)
    {
        if (val == false)
        {
            if (_buttonGKLeft)
            {
                _buttonGKLeft.SetActive(true);
            }
            if (_buttonGKRight)
            {
                _buttonGKRight.SetActive(true);
            }
            if (_buttonShoot)
            {
                _buttonShoot.SetActive(false);
            }
        }
        else
        {
            if (_buttonGKLeft)
                _buttonGKLeft.SetActive(false);
            if (_buttonGKRight)
                _buttonGKRight.SetActive(false);
            if (_buttonShoot)
            {
                _buttonShoot.SetActive(true);
            }
        }
    } 

    void OnChangeFlyDistance(float val)
    {
        if (_sliderGKDeltaDistance)
        {
            _sliderGKDeltaDistance.value = val;
        }
    }

    public void OnToggle_BeGoalKeeper(bool val)
    {
        GoalKeeperHorizontalFly.share.IsAIControl = !val;
    }

    public void onValueChange_GKDeltaDistance(float val)
    {
        GoalKeeperHorizontalFly.share._deltaDistance = Mathf.Lerp(0f, 4f, val);
        _labelGKDeltaDistance.text = "" + GoalKeeperHorizontalFly.share._deltaDistance;
    }

    public void OnClick_GKMoveLeft()
    {
        GoalKeeperHorizontalFly.share.MoveGKToLeft();
    }

    public void OnClick_GKMoveRight()
    {
        GoalKeeperHorizontalFly.share.MoveGKToRight();
    }
}
