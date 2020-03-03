using UnityEngine;
using System.Collections;

public class Pole : MonoBehaviour {

	public Area _myPole;

	void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag.Equals("Ball")) {
			GoalDetermine.share.hitPole(_myPole, other.contacts[0].point);
            if (SoundManager.share != null)
			    SoundManager.share.playSoundSFX(SOUND_NAME.Ball_Hit_Bar);


			// rat' quan trong, khi banh trung cot doc hay xa ngang roi thi set _isTouchedTheBall = 0 de goalkeeper
			// ko update parameter Height dzo animator nua~, dieu nay se fix duoc su thay doi~ dot ngot ve 
			// Height trong animator, lam cho thu mon dang bat' bong' tren cao o~ fram truoc', frame sau da~ bat' bong' o~ thap'
			// duoi' dat nhin` rat' la` ki`
			GoalKeeper.share._isTouchedTheBall = 0;		
		}
	}
}
