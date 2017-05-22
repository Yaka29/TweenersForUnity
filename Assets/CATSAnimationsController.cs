using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

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
        Color newColor = _detailImage.transform.GetComponent<Image>().color;
        newColor.a = 0.7f;
        _detailImage.transform.GetComponent<Image>().color = newColor;
        _detailName.transform.localScale = _detailImage.transform.localScale = _detailParams.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        float rotationPeriod = BASE_ANIMATION_DURATION * 0.8f;// BASE_ANIMATION_DURATION / 10f;
        float rotationTimes = -20f;
        Hashtable rotationParams = iTween.Hash(
            "z"      	, 360f * rotationTimes,
            "time", rotationPeriod
        );

		const iTween.EaseType ITWEEN_SCALE_EASE_TYPE = iTween.EaseType.easeInOutBack;
		float scaleTime = BASE_ANIMATION_DURATION * 1.2f;
		Hashtable scaleParams = iTween.Hash (
			"scale"			  , Vector3.one,
			"time"    		  , scaleTime,
			"easeType"		  , ITWEEN_SCALE_EASE_TYPE,
			"oncomplete"	  , "OnITweenComplete",
			"oncompletetarget", gameObject
		);

        const iTween.EaseType ITWEEN_ALPHA_EASE_TYPE = iTween.EaseType.easeInOutBack;
        float unfadeTime = BASE_ANIMATION_DURATION;
        Hashtable fadeParams = iTween.Hash(
            "from", _detailImage.transform.GetComponent<Image>().color.a,
            "to", 1f,
            "name", "fadetween",
            "time", unfadeTime,
            "easetype", ITWEEN_ALPHA_EASE_TYPE,
            "onupdate", "UpdateAlpha"
        );

        iTween.ValueTo(gameObject, fadeParams);
        iTween.ScaleTo (_detailImage, scaleParams);
		iTween.RotateTo (_detailImage, rotationParams);
	}

    public void UpdateAlpha(float newValue) {
        Color newColor = _detailImage.GetComponent<Image>().color;
        newColor.a = newValue;
        _detailImage.GetComponent<Image>().color = newColor;
    }

    private void OnITweenComplete() {
		_detailImage.transform.localRotation = Quaternion.Euler (Vector3.zero);
		iTween.Stop ();
	}
}
