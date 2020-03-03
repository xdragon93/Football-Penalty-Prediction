using UnityEngine;
using System.Collections;

public class DemoGoalKeeper : MonoBehaviour
{
    public float shootAfter = 1.5f;
    public bool forceGKAtInit = true;

    private bool _autoShoot;
    public bool AutoShoot
    {
        get { return _autoShoot; }
        set { _autoShoot = value; }
    }

    void Awake()
    {
        AutoShoot = forceGKAtInit;
        Shoot.EventDidPrepareNewTurn += OnNewTurn;
    }

    // Use this for initialization
	IEnumerator Start ()
	{
	    yield return new WaitForEndOfFrame();       // wait for the execution of Start method of DemoShoot
        
	    GKHorizontalFlyDebug.share.OnToggle_BeGoalKeeper(forceGKAtInit);
        SwitchCamera.share.OnToggle_BeGoalKeeper(forceGKAtInit);
	}

    void OnDestroy()
    {
        Shoot.EventDidPrepareNewTurn -= OnNewTurn;
    }

    void OnNewTurn()
    {
        if (AutoShoot)
        {
            RunAfter.runAfter(gameObject, () =>
            {
                ShootAI.shareAI.shoot();
            }, shootAfter);
        }
    }

}
