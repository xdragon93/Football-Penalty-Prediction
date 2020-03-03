using UnityEngine;
using System.Collections;

public class DemoShoot : MonoBehaviour
{
    public static DemoShoot share;

    [SerializeField] private int initialGKLevel = 2;

    void Awake()
    {
        share = this;
    }

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        GoalKeeperLevel.share.setLevel(initialGKLevel);
    }
 
    public void Reset(bool shouldRandomNewPos)
    {
        /*
        // ShootAI reset logic must be called first, to reset new ball poosition, reset() method of other components must come after this
        if(shouldRandomNewPos)
            ShootAI.shareAI.reset();                // used this method to reset new randomised ball's position
        else
            ShootAI.shareAI.reset(ShootAI.shareAI.BallPositionX, ShootAI.shareAI.BallPositionZ);        // call like this to reset new turn with current ball position
        */
        ShootAI.shareAI.reset(0f, -11f);

        SlowMotion.share.reset();                   // reset the slowmotion logic

        GoalKeeperHorizontalFly.share.reset();      // reset goalkeeperhorizontalfly logic
        GoalKeeper.share.reset();                   // reset goalkeeper logic
        GoalDetermine.share.reset();                // reset goaldetermine logic so that it's ready to detect new goal

        CameraManager.share.reset();                // reset camera position
    }

    public void OnClick_NewTurnRandomPosition()
    {
        Reset(true);
    }

    public void OnClick_NewTurnSamePosition()
    {
        Reset(false);
    }
}
