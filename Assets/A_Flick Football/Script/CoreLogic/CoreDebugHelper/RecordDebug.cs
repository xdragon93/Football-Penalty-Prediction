using UnityEngine;
using System.Collections;

public class RecordDebug : MonoBehaviour {

    public void Replay()
    {
        Shoot.share.BallPositionX = ShootRecord.share.posBallReplay.x;
        Shoot.share.BallPositionZ = ShootRecord.share.posBallReplay.z;
        DemoShoot.share.Reset(false);
        StartCoroutine(_Replay());
    }

    IEnumerator _Replay()
    {
        yield return new WaitForSeconds(1f);
        CameraManager.share.MoveCameraWhenShoot();
        ShootRecord.share.shouldReplay = true;
        GoalKeeperRecord.share.Replay();
    }

    public void StopReplay()
    {
        ShootRecord.share.shouldReplay = false;
        GoalKeeperRecord.share.StopReplay();
    }
}
