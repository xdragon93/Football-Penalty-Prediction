using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class GKDebug : MonoBehaviour {

    public Text _labelPredictFactor;
    public Slider _sliderPredictFactor;

    public Text _labelLevelGK;
    public Slider _sliderLevelGK;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        
        if (_sliderPredictFactor)
        {
            _sliderPredictFactor.value = (1f - 1f) / (20f - 1f);
        }
        if (_sliderLevelGK)
        {
            _sliderLevelGK.value = 2f / GoalKeeperLevel.share.getMaxLevel();
        }

        GoalKeeper.share._predictFactor = 10;
        GoalKeeperLevel.share.setLevel(3);
    }

    public void onValueChange_PredictFactor(float val)
    {
        GoalKeeper.share._predictFactor = (int)Mathf.Lerp(1, 20, val);
        _labelPredictFactor.text = "" + GoalKeeper.share._predictFactor;
    }

    public void onValueChanged_LevelGK(float val)
    {
        if (GoalKeeperLevel.share != null)
        {
            int level = (int)Mathf.Lerp(0, GoalKeeperLevel.share.getMaxLevel(), val);
            GoalKeeperLevel.share.setLevel(level);
            _labelLevelGK.text = "" + level;
        }
    }
}
