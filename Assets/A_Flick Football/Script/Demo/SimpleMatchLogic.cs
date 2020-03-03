using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleMatchLogic : MonoBehaviour
{
    [SerializeField] private Text textCountGoalMe;
    [SerializeField] private Text textCountGoalAI;
    [SerializeField] private Text textLevelShootAI;
    [SerializeField] private Text textCountTurn;
    [SerializeField] private Text textResult;
    [SerializeField] private GameObject panelResult;

    public int maxTurn = 5;

    private int _count;
    private float _curveLevel;
    private float _difficultyShootLevel;

    private float[] _curveLevels = new float[] { 0f, 0.4f, 0.8f, 1f };
    private float[] _difficultyShootLevels = new float[]{0f, 0.4f, 0.8f, 1f};

    private int _countGoalMe;
    

    public int CountGoalMe
    {
        get { return _countGoalMe; }
        set
        {
            _countGoalMe = value;
            textCountGoalMe.text = "" + _countGoalMe;
        }
    }

    public int CountGoalAi
    {
        get { return _countGoalAI; }
        set
        {
            _countGoalAI = value;
            textCountGoalAI.text = "" + _countGoalAI;
        }
    }

    private int _countGoalAI;

    private void Start()
    {
        _curveLevel = _curveLevels[0];
        _difficultyShootLevel = _difficultyShootLevels[0];
        GoalDetermine.EventFinishShoot += EventShootFinish;

        GoalKeeperLevel.share.setLevel(0);
        OnChange_ShootAILevel(0f);

        OnClick_RestartGame();
    }

    private void OnDestroy()
    {
        GoalDetermine.EventFinishShoot -= EventShootFinish;
    }

    void NextTurn()
    {
        if (_count / 2 >= maxTurn)
        {
            Finalize();    
            
        }
        else
        {
            textCountTurn.text = (_count / 2 + 1).ToString();
            Reset();
            if (_count % 2 == 0)       // even turn is my turn to shoot
            {
                GoalKeeperHorizontalFly.share.IsAIControl = true;
                SwitchCamera.share.IsFront = true;
            }
            else        //  odd turn is AI turn to shoot
            {
                GoalKeeperHorizontalFly.share.IsAIControl = false;
                SwitchCamera.share.IsFront = false;
                RunAfter.runAfter(gameObject, DoShootAI, 2f);
            }
        }
    }

    void Finalize()
    {
        textResult.text = "You Won";
        if (_countGoalMe < _countGoalAI)
        {
            textResult.text = "You Lose";    
        }
        else if (_countGoalMe == _countGoalAI)
        {
            textResult.text = "Duel";
        }

        panelResult.SetActive(true);
        textResult.enabled = true;
    }

    void DoShootAI()
    {
        ShootAI.shareAI.shoot(Direction.Both, _curveLevel, _difficultyShootLevel);
    }


    /// <summary>
    /// How to reset:
    /// 1/ Reset ball position
    /// 2/ Reset Wall position, this can be optional if we don't want to have wall kick in this turn
    /// 3/ Reset GoalKeeperHorizontalFly 
    /// 4/ Reset GoalKeeper
    /// 5/ Reset GoalDetermine
    /// 6/ Reset CameraManager
    /// 7/ Reset SlowMotion (optional)
    /// </summary>
    void Reset()
    {
        /*
        if (_count % 2 == 1)        // even means same turn
        {
            Shoot.share.reset(Shoot.share.BallPositionX, Shoot.share.BallPositionZ);        // reset with current ball position
        }
        else    // even means new turn, so will random new ball position
        {
            Shoot.share.reset();        // reset with new random ball position
        }
        */
        Shoot.share.reset(Shoot.share.BallPositionX, Shoot.share.BallPositionZ);

        Wall.share.IsWall = ((_count / 2) % 2 != 0) ? true : false;     // each odd turn will be a wall kick
        if (Wall.share.IsWall)  // if wall kick
        {
            Wall.share.setWall(Shoot.share._ball.transform.position);   
        }

        /// must have reset call
        GoalKeeperHorizontalFly.share.reset();
        GoalKeeper.share.reset();
        GoalDetermine.share.reset();
        CameraManager.share.reset();

        /// optianal reset call
        SlowMotion.share.reset();
    }

    private void EventShootFinish(bool isGoal, Area area)
    {
        if (isGoal)
        {
            if (_count%2 == 0)
            {
                ++CountGoalMe;
            }
            else
            {
                ++CountGoalAi;
            }
        }

        ++_count;
        RunAfter.runAfter(gameObject, NextTurn, 2f);
    }

    public void OnChange_ShootAILevel(float val)
    {
        int level = (int)Mathf.Lerp(0, 3, val);
        textLevelShootAI.text = "" + level;

        _curveLevel = _curveLevels[level];
        _difficultyShootLevel = _difficultyShootLevels[level];
    }

    public void OnClick_RestartGame()
    {
        _count = 0;
        CountGoalMe = 0;
        CountGoalAi = 0;
        panelResult.SetActive(false);
        RunAfter.removeTasks(gameObject);
        NextTurn();
    }
}
