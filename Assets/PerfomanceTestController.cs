using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PerfomanceTestController : MonoBehaviour {

	const float BASE_ANIMATION_DURATION_MIN = 0.5f;
	const float BASE_ANIMATION_DURATION_MAX = 3f;
	const LeanTweenType LEAN_TWEEN_EASE_TYPE = LeanTweenType.easeInOutCirc;
	const iTween.EaseType ITWEEN_EASE_TYPE = iTween.EaseType.easeInOutCirc;
	const Ease DOTWEEN_EASE_TYPE = Ease.InOutCirc;

	private int _unitsNumber = 500;
	private List<GameObject> _testUnits;

	void Start () {
		DOTween.Init(true, false);
		DOTween.defaultEaseType = DOTWEEN_EASE_TYPE;
		LeanTween.init (_unitsNumber);
		GenerateUnits ();
	}
	
	void Update () {
		
	}

	public void GenerateUnits() {
		_testUnits = new List<GameObject> ();
		for (int i = 0; i < _unitsNumber; i++) {
			GameObject testUnit = (GameObject)Instantiate (Resources.Load ("PerfomanceTestUnit"));
			testUnit.transform.SetParent (GameObject.Find("CanvasPerfomance").transform, false);
			_testUnits.Add (testUnit);
		}

		StartTweens ();
	}

	private void StartTweens() {
		float minX = 0f;//-Screen.width/2f;
		float maxX = Screen.width;//Screen.width/2f;
		float minY = 0f;//-Screen.height/2f;
		float maxY = Screen.height;//Screen.height/2f;
		float startTime = Time.realtimeSinceStartup;

		foreach (GameObject testUnit in _testUnits) {
			Vector3 randomPosToTween = new Vector3 (Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
            LEANTweenMove(testUnit, randomPosToTween);
            //ITweenMove (testUnit, randomPosToTween);
//			DOTWeenMove (testUnit, randomPosToTween);
		}

		DebugTimePeriod (startTime, "Start all tweens time");
	}

	private void ITweenMove(GameObject target, Vector3 toPos) {
		float animationTime = Random.Range (BASE_ANIMATION_DURATION_MIN, BASE_ANIMATION_DURATION_MAX);
		Hashtable moveScreenAnimationParams = iTween.Hash (
			"x"			, toPos.x,
			"y"      	, toPos.y,
			"time"    	, animationTime,
			"easeType"	, ITWEEN_EASE_TYPE,
			"looptype"  , iTween.LoopType.loop
		);
		iTween. MoveTo (target, moveScreenAnimationParams);		
	}

	private void DOTWeenMove(GameObject target, Vector3 toPos) {
		float animationTime = Random.Range (BASE_ANIMATION_DURATION_MIN, BASE_ANIMATION_DURATION_MAX);
		Tween tween = DOTween.To (() => target.transform.position,
			x => target.transform.position = x,
			toPos,
			animationTime)
			.SetLoops(-1);
	}

	private void LEANTweenMove(GameObject target, Vector3 toPos) {
		float animationTime = Random.Range (BASE_ANIMATION_DURATION_MIN, BASE_ANIMATION_DURATION_MAX);
		LeanTween.move (target, toPos, animationTime)
			.setEase (LEAN_TWEEN_EASE_TYPE)
			.setLoopType (LeanTweenType.clamp);
	}

	private void DebugTimePeriod(float startTime, string timePeriodName) {
		float time = Time.realtimeSinceStartup - startTime;
		Debug.Log (timePeriodName + " time = " + (time * 1000f) + " ms");
	}
}
