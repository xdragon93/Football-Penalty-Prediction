using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;
using Holoville.HOTween.Path;
using Holoville.HOTween.Plugins;
using Holoville.HOTween.Plugins.Core;
using System;


public class CameraManager : MonoBehaviour {

	public static CameraManager share;

    public static Action EventReset = delegate { };
    public static Action EventBeginIntroMovement = delegate { };
    public static Action EventEndIntroMovement = delegate { };

	public GameObject _cameraMain;
	public Camera[] _camerasMain;

    //[SerializeField]
    int _FOVportrait = 40;
    //[SerializeField]
    int _FOVlandscape = 30;

	private bool isPortrait = true;
	public Camera _cameraMainComponent;
	public Collider _colliderFullScreen;

    [SerializeField]
    private float cameraMainDistanceToBall = -5f;
    [SerializeField]
    private float cameraMainY = 1.7f;

	void Awake() {
		share = this;
        _cameraMainComponent = _cameraMain.GetComponent<Camera>();
	}

	// Use this for initialization
	void Start () {
        updateCameraFOV();
		Shoot.EventShoot += eventShoot;
	}

	void OnDestroy() {
		Shoot.EventShoot -= eventShoot;
	}

	public void turnOn() {
		_cameraMain.SetActive(true);
	}

	public void turnOff() {
		_cameraMain.SetActive(false);
	}

	private bool _isCameraMoving = false;
	Tweener _tweenCamera;

    [SerializeField] private float testXZ = 0.8f;
    [SerializeField] private float testY = 5f;

	private void eventShoot()
	{
	    MoveCameraWhenShoot();
	}

    public void MoveCameraWhenShoot()
    {
        if (_isCameraMoving == true)
            return;

        _isCameraMoving = true;

        Transform ballTrans = Shoot.share._ball.transform;
        _cameraMain.GetComponent<SmoothLookAt>().target = ballTrans;

        _tweenCamera = HOTween.To(_cameraMain.transform, 3f, new TweenParms()
                  .Prop("position", new Vector3(ballTrans.position.x * testXZ, testY, ballTrans.position.z * testXZ))
                  .Loops(1, LoopType.Restart)
                  .Ease(EaseType.Linear)
                  .OnComplete(cameraFinishMoving)
                  );
        _tweenCamera.autoKillOnComplete = true;


        _currentFOV = _FOVportrait;
    }


	public float _currentFOV;
	private void cameraFinishMoving() {
//		_cameraMain.GetComponent<SmoothLookAt>().target = null;
	}

	public void introMovement(Action callback, Transform target)
	{
	    EventBeginIntroMovement();

		Vector3[] path = new Vector3[3];
		path[2] = target.position;
		path[0] = new Vector3( 0, 1.5f, -60f);
		path[1] = (path[2] + path[0]) / 2;
		path[1].y = 6f;

		Quaternion rotation = target.rotation;
		_cameraMain.transform.localEulerAngles = Vector3.zero;
		_cameraMain.transform.position = path[0];

		HOTween.To(_cameraMain.transform, 6f, new TweenParms()
		           .Prop( "position", new PlugVector3Path(path, PathType.Curved))
		           .Prop( "rotation", rotation)
		           .Loops(1, LoopType.Restart)
		           .Ease(EaseType.EaseInOutQuad)
		           .OnComplete(() => {
						
						if(callback != null)
							callback();

		                EventEndIntroMovement();
		           })
		           .AutoKill(true)
		           );
	}

	public void reset() {
		if(_tweenCamera != null)
			_tweenCamera.Kill();
		_cameraMain.GetComponent<SmoothLookAt>().target = null;
		_isCameraMoving = false;

	    updateCameraFOV();

		StartCoroutine(resetPosition());

	    EventReset();
	}
    

	private IEnumerator resetPosition() {

		Transform ball = Shoot.share._ball.transform;
		
		Vector3 diff = -ball.position;
		diff.Normalize();
		float angleRadian = Mathf.Atan2(diff.x, diff.z);
		float angle = angleRadian * Mathf.Rad2Deg;			// goc lech so voi goc toa do
		angleRadian = angle * Mathf.Deg2Rad;
		
		Vector3 pos = ball.position;		// pos se duoc gan' la vi tri cua camera
        pos.y = cameraMainY;			// camera cach' mat dat 1.7m
		
		if(isPortrait) {		// neu la portrait thi camera nam dang sau trai banh 4m va huong ve goc toa do, noi cach khac' la cung huong' voi' truc z cua parent cua ball
            pos.x += cameraMainDistanceToBall * Mathf.Sin(angleRadian);
            pos.z += cameraMainDistanceToBall * Mathf.Cos(angleRadian);
		}
		else {		// neu la landscape thi camera nam dang sau trai banh 4m va huong ve goc toa do, noi cach khac' la cung huong' voi' truc z cua parent cua ball
            pos.x += cameraMainDistanceToBall * Mathf.Sin(angleRadian);
            pos.z += cameraMainDistanceToBall * Mathf.Cos(angleRadian);
		}
		
		_cameraMain.transform.position = pos;
		
		Vector3 rotation = _cameraMain.transform.eulerAngles;
		rotation.y = angle;		
		rotation.x = 6f;			// quay 6 do theo truc x
		rotation.z = 0f;
		_cameraMain.transform.eulerAngles = rotation;

	    float distanceBackCameraToBall = 10f; // 18.31871f;

        float x = distanceBackCameraToBall * Mathf.Sin(angleRadian);
        float z = distanceBackCameraToBall * Mathf.Cos(angleRadian);

		yield break;
	}

	public void updateCameraFOV() {
		
		if( Screen.height > Screen.width ) {		// portrait
			isPortrait = true;
			foreach(Camera camera in _camerasMain) {
				if(camera.orthographic == false) {
					camera.fieldOfView = _FOVportrait;
				    _currentFOV = _FOVportrait;
				}
			}
		}	
		else {			// landscape
			isPortrait = false;
			foreach(Camera camera in _camerasMain) {
				if(camera.orthographic == false) {
					camera.fieldOfView = _FOVlandscape;
                    _currentFOV = _FOVlandscape;
				}
			}
		}
		
        //reset();
	}
}
