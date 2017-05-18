using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CATSAnimationsController : MonoBehaviour {

	const float BASE_ANIMATION_DURATION = 0.5f;

	private GameObject _detail;
	private GameObject _detailName;
	private GameObject _detailImage;
	private GameObject _detailParams;

	void Start () {
		DOTween.Init(false, false);

		_detail = GameObject.Find ("Detail");
		_detailName = _detail.transform.Find ("DetailName").gameObject;
		_detailImage = _detail.transform.Find ("DetailImage").gameObject;
		_detailParams = _detail.transform.Find ("DetailParams").gameObject;
		_detailName.transform.localScale = _detailImage.transform.localScale = _detailParams.transform.localScale = Vector3.zero;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			StartAnimate ();
		}
	}

	private void StartAnimate() {
		ITweenAnimate ();
	}

	private void ITweenAnimate() {
		_detailName.transform.localScale = _detailImage.transform.localScale = _detailParams.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		float rotationPeriod = BASE_ANIMATION_DURATION / 8f;
		Hashtable rotationParams = iTween.Hash (
			"z"      	, 90f,
			"time"    	, rotationPeriod,
			"looptype"  , iTween.LoopType.loop
		);

		const iTween.EaseType ITWEEN_EASE_TYPE = iTween.EaseType.easeInOutBack;
		float scaleTime = BASE_ANIMATION_DURATION * 1.2f;
		Hashtable scaleParams = iTween.Hash (
			"scale"			  , Vector3.one,
			"time"    		  , scaleTime,
			"easeType"		  , ITWEEN_EASE_TYPE,
			"oncomplete"	  , "OnITweenComplete",
			"oncompletetarget", gameObject
		);

		iTween.ScaleTo (_detailImage, scaleParams);
		iTween.RotateTo (_detailImage, rotationParams);
	}

	private void OnITweenComplete() {
		_detailImage.transform.localRotation = Quaternion.Euler (Vector3.zero);
		iTween.Stop ();
	}
}
