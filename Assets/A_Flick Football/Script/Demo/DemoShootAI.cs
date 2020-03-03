using UnityEngine;
using System.Collections;

public class DemoShootAI : MonoBehaviour
{

    public bool forceShootByAI = true;

	// Use this for initialization

    public float shootAfter = 1.5f;
    

    private bool _autoShoot;
    public bool AutoShoot
    {
        get { return _autoShoot; }
        set { _autoShoot = value; }
    }

    void Awake()
    {
        AutoShoot = forceShootByAI;
        Shoot.EventDidPrepareNewTurn += OnNewTurn;
    }

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();       // wait for the execution of Start method of DemoShoot

        GoalKeeperHorizontalFly.share.IsAIControl = true;
        ShootAI.shareAI.willBeShootByUser = false;
        SwitchCamera.share.OnToggle_BeGoalKeeper(false);
    }

    void OnDestroy()
    {
        Shoot.EventDidPrepareNewTurn -= OnNewTurn;
    }

    void OnNewTurn()
    {
        RunAfter.removeTasks(gameObject);

        if (AutoShoot)
        {
            RunAfter.runAfter(gameObject, () =>
            {
                ShootAI.shareAI.shoot();
            }, shootAfter);
        }
    }

}
