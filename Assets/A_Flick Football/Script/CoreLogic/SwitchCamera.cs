using UnityEngine;
using System.Collections;

public class SwitchCamera : MonoBehaviour {

	public static SwitchCamera share;

	public GameObject _cameraFront;

	private bool _isFront = true;

	public bool IsFront {
		get {
			return _isFront;
		}
		set {
			_isFront = value;

			_cameraFront.SetActive(_isFront);
			
			
			float a;
			if(_isFront) {
				a = 1f;
				_matGK3.shader = Shader.Find("Diffuse");
			}
			else {
				a = 0.4f;
				_matGK3.shader = Shader.Find("Transparent/Diffuse");
			}
			
			Color c = _matGK1.color;
			c.a = a;
			_matGK1.color = c;

			c = _matGK2.color;
			c.a = a;
			_matGK2.color = c;

			c = _matGK3.color;
			c.a = a;
			_matGK3.color = c;

			c = _matNetGoal.color;
			c.a = a;
			_matNetGoal.color = c;
		}
	}

	public Material _matGK1;
	public Material _matGK2;
	public Material _matGK3;
	public Material _matNetGoal;

	void Awake() {
		share = this;
	}

    public void OnToggle_BeGoalKeeper(bool val)
    {
        IsFront = !val;
    }

	public void onValueChange_FrontCamera(bool val) {
		IsFront = val;
	}
}
