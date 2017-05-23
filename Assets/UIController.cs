using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIController : MonoBehaviour {

	private GameObject _popup;
	private GameObject _popupFader;
	private GameObject _firstScreen;
	private GameObject _secondScreen;

	private List<GameObject> _screens;

	private float _startFrameTime = 0;

	private Vector3 _screenHiddenPosition;
	private Vector3 _screenVisiblePosition;

	const float BASE_ANIMATION_DURATION = 0.5f;
	const LeanTweenType LEAN_TWEEN_EASE_TYPE = LeanTweenType.easeInOutCirc;
	const iTween.EaseType ITWEEN_EASE_TYPE = iTween.EaseType.easeInOutCirc;
	const Ease DOTWEEN_EASE_TYPE = Ease.InOutCirc;

	void Start () {
		DOTween.Init(false, false);
		DOTween.defaultEaseType = DOTWEEN_EASE_TYPE;

		_popup = GameObject.Find ("Popup");
		_popupFader = GameObject.Find ("PopupFader");
		_firstScreen = GameObject.Find ("Screen_1");
		_secondScreen = GameObject.Find ("Screen_2");

		_screenVisiblePosition = _firstScreen.transform.position;
		_screenHiddenPosition = new Vector3 (_screenVisiblePosition.x, Screen.currentResolution.height, _screenVisiblePosition.z);

		_screens = new List<GameObject> ();
		_screens.Add (_popup);
		_screens.Add (_firstScreen);
		_screens.Add (_secondScreen);

		_firstScreen.transform.Find ("ShowPopupButton").GetComponent<Button> ().onClick.AddListener(delegate() {
			ShowPopup ();
		});

		_firstScreen.transform.Find ("ChangeScreenButton").GetComponent<Button> ().onClick.AddListener(delegate() {
			RemoveScreen(_firstScreen, _secondScreen);
		});

		_secondScreen.transform.Find ("ShowPopupButton").GetComponent<Button> ().onClick.AddListener(delegate() {
			ShowPopup ();
		});

		_secondScreen.transform.Find ("ChangeScreenButton").GetComponent<Button> ().onClick.AddListener(delegate() {
			RemoveScreen (_secondScreen, _firstScreen);
		});

		_popup.transform.Find ("Window").Find ("CloseButton").GetComponent<Button> ().onClick.AddListener(delegate() {
			ClosePopup ();
		});

		RemoveAllScreens (true);
		ShowScreen (_firstScreen, true);
	}
	
	void Update () {
//		ShowFrameTime ();
	}

	private void ShowPopup(bool immediately = false) {
		TweenerShowScreen (_popup, immediately);
		ShowFader ();
	}

	private void ClosePopup(bool immediately = false) {
		TweenerRemoveScreen (_popup, immediately);
		RemoveFader ();
	}

	private void ShowFader(bool immediately = false) {
		TweenerShowFader (immediately);
	}

	private void RemoveFader(bool immediately = false) {
		TweenerRemoveFader (immediately);
	}

	private void RemoveScreen(GameObject screenToRemove, GameObject screenToOpenOnComplete = null, bool immediately = false) {
		//LEANTWEEN
//		LTDescr tween = TweenerRemoveScreen (screenToRemove, immediately);
//		if (screenToOpenOnComplete) {
//			tween.setOnComplete (OnTweenCompleteCallback)
//				.setOnCompleteParam ((object) screenToOpenOnComplete);
//		}

		//DOTWEEN
		Tween tween = TweenerRemoveScreen (screenToRemove, immediately);
		if (screenToOpenOnComplete) {
			tween.OnComplete (() => OnTweenCompleteCallback((object) screenToOpenOnComplete) );
		}

		//ITween
//		TweenerRemoveScreen (screenToRemove, immediately);
//		if (screenToOpenOnComplete) {
//			foreach (Hashtable tween in iTween.tweens) {
//				if (tween ["name"] == "moveTween") {
//					tween.Add ("oncomplete", "OnTweenCompleteCallback");
//					tween.Add ("oncompleteparams", iTween.Hash("screenToOpen", screenToOpenOnComplete));
//					tween.Add ("oncompletetarget", gameObject);
//				}
//			}
//		}
	}

	private void RemoveAllScreens(bool immediately = false) {
		foreach (GameObject screen in _screens) {
			TweenerRemoveScreen (screen, immediately);
		}

		RemoveFader (immediately);
	}

	private void ShowScreen(GameObject screenToOpen, bool immediately = false) {
		TweenerShowScreen (screenToOpen, immediately);
	}

	//TWEENERS METHODS
	private void TweenerShowScreen(GameObject screenToOpen, bool immediately = false) {
		float startTime = Time.realtimeSinceStartup;
		float animationTime = immediately ? 0f : BASE_ANIMATION_DURATION;

		//LEANTWEEN
//		float animationTime = immediately ? 0f : BASE_ANIMATION_DURATION;
//		LeanTween.moveY (screenToOpen, _screenVisiblePosition.y, animationTime)
//			.setEase (LEAN_TWEEN_EASE_TYPE);

		//DOTWEEN
		Tween tween = DOTween.To (() => screenToOpen.transform.position,
			x => screenToOpen.transform.position = x,
			_screenVisiblePosition,
			animationTime);

		//ITween
//		Hashtable moveScreenAnimationParams = iTween.Hash (
//			"y"      	, _screenVisiblePosition.y,
//			"time"    	, animationTime,
//			"easeType"	, ITWEEN_EASE_TYPE
//		);
//		iTween. MoveTo (screenToOpen, moveScreenAnimationParams);

		DebugTimePeriod (startTime, "LeanTweenShow starting tween");
	}

	private Tween TweenerRemoveScreen(GameObject screenToRemove, bool immediately = false) {
		float startTime = Time.realtimeSinceStartup;
		float animationTime = immediately ? 0f : BASE_ANIMATION_DURATION;

		//LEANTWEEN
//		LTDescr tween = LeanTween.moveY (screenToRemove, _screenHiddenPosition.y, animationTime)
//			.setEase (LEAN_TWEEN_EASE_TYPE);

		//DOTWEEN
		Tween tween = DOTween.To (() => screenToRemove.transform.position,
			x => screenToRemove.transform.position = x,
			_screenHiddenPosition,
			animationTime);

		//ITween
//		Hashtable moveScreenAnimationParams = iTween.Hash (
//			"y"      	, _screenHiddenPosition.y,
//			"name"		, "moveTween",
//			"time"    	, animationTime,
//			"easeType"	, ITWEEN_EASE_TYPE
//		);
//		iTween. MoveTo (screenToRemove, moveScreenAnimationParams);

		DebugTimePeriod (startTime, "LeanTweenRemove starting tween");
		return tween;//(string)moveScreenAnimationParams ["name"];
	}

	private void TweenerShowFader(bool immediately = false) {
		float startTime = Time.realtimeSinceStartup;
		float animationTime = immediately ? 0f : BASE_ANIMATION_DURATION;

		//LEANTWEEN
		//LeanTween.alpha(_popupFader.GetComponent<RectTransform>(), 0.5f, animationTime)
		//	.setEase(LEAN_TWEEN_EASE_TYPE);

		//DOTWEEN
		DOTween.ToAlpha (() => _popupFader.GetComponent<Image> ().color,
			x => _popupFader.GetComponent<Image> ().color = x,
			0.5f,
			animationTime);

		//ITween
//		Hashtable fadeParams = iTween.Hash (
//			"from"      , 0f,
//			"to"		, 0.5f,
//			"name"		, "fadeTween",
//			"time"    	, animationTime,
//			"easeType"	, ITWEEN_EASE_TYPE,
//			"onupdate"	, "UpdateAlpha"
//		);
//		iTween.ValueTo(gameObject, fadeParams);

		DebugTimePeriod (startTime, "LeanTweenShowFader starting tween");
	}
		
	private void TweenerRemoveFader(bool immediately = false) {
		float startTime = Time.realtimeSinceStartup;
		float animationTime = immediately ? 0f : BASE_ANIMATION_DURATION;

		//LEANTWEEN
//		LeanTween.alpha (_popupFader.GetComponent<RectTransform> (), 0f, animationTime)
//			.setEase (LEAN_TWEEN_EASE_TYPE);

		//DOTWEEN
		DOTween.ToAlpha (() => _popupFader.GetComponent<Image> ().color,
			x => _popupFader.GetComponent<Image> ().color = x,
			0f,
			animationTime);

		//ITween
//		Hashtable unfadeParams = iTween.Hash (
//			"from"      , 0.5f,
//			"to"		, 0f,
//			"name"		, "fadeTween",
//			"time"    	, animationTime,
//			"easeType"	, ITWEEN_EASE_TYPE,
//			"onupdate"	, "UpdateAlpha"
//		);
//		iTween.ValueTo(gameObject, unfadeParams);

		DebugTimePeriod (startTime, "LeanTweenRemoveFader starting tween");
	}

	public void UpdateAlpha(float newValue) {
		Color newColor = _popupFader.GetComponent<Image> ().color;
		newColor.a = newValue;
		_popupFader.GetComponent<Image> ().color = newColor;
	}

	private void OnTweenCompleteCallback(object parameters) {
		GameObject screenToOpen = (GameObject)parameters;
		if (screenToOpen != null) {
			ShowScreen (screenToOpen);
		}

//		Debug.Log ("OnTweenCompleteCallback");
//		GameObject screenToOpen = (GameObject)parameters ["screenToOpen"];
//		if (screenToOpen != null) {
//			ShowScreen (screenToOpen);
//		}
	}

	//Helping methods
	private void DebugTimePeriod(float startTime, string timePeriodName) {
		float time = Time.realtimeSinceStartup - startTime;
		Debug.Log (timePeriodName + " time = " + (time * 1000f) + " ms");
	}

	private void ShowFrameTime() {
		float frameTime = (Time.realtimeSinceStartup - _startFrameTime);
		Debug.Log ("Frame time = " + (frameTime * 1000f) + " ms");
		_startFrameTime = Time.realtimeSinceStartup;
	}
}