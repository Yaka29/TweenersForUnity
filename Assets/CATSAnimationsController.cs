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

	private int _detailCounter;
    private const int _detailsQuantity = 7;
    private GameObject _detailsPresentations;
	private List<GameObject> _details;

	void Start () {
		DOTween.Init(false, false);

        _detailsPresentations = GameObject.Find("DetailsPresentation");
		_details = new List<GameObject>();
		for (int i = 0; i < _detailsQuantity; i++) {
			GameObject detail = (GameObject)Instantiate(Resources.Load("DetailImage"));
			detail.transform.SetParent(_detailsPresentations.transform, false);
			detail.transform.localScale = Vector3.zero;
			_details.Add(detail);
		}

		//_detail = GameObject.Find ("Detail");
		//_detailName = _detail.transform.Find ("DetailName").gameObject;
		//_detailImage = _detail.transform.Find ("DetailImage").gameObject;
		//      _detailParams = _detail.transform.Find ("DetailParams").gameObject;
		//_detailName.transform.localScale = 
		//          _detailImage.transform.localScale = _detailParams.transform.localScale = Vector3.zero;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			//StartAnimateDetail ();
			AnimateDetailsPresentation();
		}
	}

    private void AnimateDetailsPresentation() {
		foreach (GameObject detail in _details) {
			detail.transform.localScale = Vector3.zero;
		}

		_detailCounter = 0;
		ITweenAnimateDetailInPresentation();
	}

	private void ITweenAnimateDetailInPresentation() {
		const iTween.EaseType ITWEEN_SCALE_EASE_TYPE = iTween.EaseType.easeInOutBack;
		float scaleTime = BASE_ANIMATION_DURATION * 0.5f;
		Hashtable imageScaleParams = iTween.Hash(
			"scale", Vector3.one,
			"time", scaleTime,
			"easeType", ITWEEN_SCALE_EASE_TYPE,
			"oncomplete", "OnITweenDetailInRepresentationComplete",
			"oncompletetarget", gameObject
		);

		iTween.ScaleTo(_details[_detailCounter], imageScaleParams);
	}

	public void OnITweenDetailInRepresentationComplete() {
		_detailCounter++;

		if (_detailCounter >= _detailsQuantity) {
			return;
		}
		
		ITweenAnimateDetailInPresentation();
	}


	private void StartAnimateDetail() {
		ITweenAnimateSingle();
	}

	private void ITweenAnimateSingle() {
        _detailImage.transform.GetComponent<Image>().color = Color.white;
        _detailImage.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Blurred-quad-blur");
        _detailImage.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        float rotationPeriod = BASE_ANIMATION_DURATION * 0.8f;
        float rotationTimes = -20f;
        Hashtable imageRotationParams = iTween.Hash(
            "z"   , 360f * rotationTimes,
            "time", rotationPeriod,
            "oncomplete", "OnITweenComplete",
            "oncompletetarget", gameObject
        );

        const iTween.EaseType ITWEEN_SCALE_EASE_TYPE = iTween.EaseType.easeInOutBack;
		float scaleTime = BASE_ANIMATION_DURATION * 1.2f;
		Hashtable imageScaleParams = iTween.Hash (
			"scale"			  , Vector3.one,
			"time"    		  , scaleTime,
			"easeType"		  , ITWEEN_SCALE_EASE_TYPE
		);

        const iTween.EaseType ITWEEN_ALPHA_EASE_TYPE = iTween.EaseType.easeInOutBack;
        float unfadeTime = BASE_ANIMATION_DURATION;
        Hashtable imageFadeParams = iTween.Hash(
            "from", _detailImage.transform.GetComponent<Image>().color.a,
            "to", 1f,
            "name", "fadetween",
            "time", unfadeTime,
            "easetype", ITWEEN_ALPHA_EASE_TYPE,
            "onupdate", "UpdateAlpha"
        );

        float namePositionOffsetY = 200f;
        _detailName.transform.localScale = Vector3.zero;
        _detailName.transform.localPosition = new Vector3(
            _detailName.transform.localPosition.x,
            _detailName.transform.localPosition.y - namePositionOffsetY,
            _detailName.transform.localPosition.z);

        const iTween.EaseType NAME_ITWEEN_SCALE_EASE_TYPE = iTween.EaseType.easeOutBack;
        const float NAME_ITWEEN_SCALE_DELAY = 0.2f;
        float nameScaleTime = BASE_ANIMATION_DURATION;
        Hashtable nameScaleParams = iTween.Hash(
            "scale", Vector3.one,
            "time", nameScaleTime,
            "easeType", NAME_ITWEEN_SCALE_EASE_TYPE,
            "delay", NAME_ITWEEN_SCALE_DELAY
        );

        const iTween.EaseType NAME_POSITION_EASE_TYPE = iTween.EaseType.easeOutBack;
        const float NAME_ITWEEN_POSITION_DELAY = 0.2f;
        float namePositionTime = BASE_ANIMATION_DURATION;
        Hashtable namePositionParams = iTween.Hash(
            "y", _detailName.transform.localPosition.y + namePositionOffsetY,
            "time", namePositionTime,
            "easeType", NAME_POSITION_EASE_TYPE,
            "delay", NAME_ITWEEN_POSITION_DELAY,
            "islocal", true
        );

        float paramsPositionOffsetY = 30f;
        _detailParams.transform.localScale = Vector3.zero;
        _detailParams.transform.localPosition = new Vector3(
            _detailParams.transform.localPosition.x,
            _detailParams.transform.localPosition.y - paramsPositionOffsetY,
            _detailParams.transform.localPosition.z);

        iTween.EaseType PARAMS_ITWEEN_SCALE_EASE_TYPE = iTween.EaseType.easeOutBack;
        const float PARAMS_ITWEEN_SCALE_DELAY = 0.3f;
        float paramsScaleTime = BASE_ANIMATION_DURATION * 0.6f;
        Hashtable paramsScaleParams = iTween.Hash(
            "scale", Vector3.one,
            "time", paramsScaleTime,
            "easeType", PARAMS_ITWEEN_SCALE_EASE_TYPE,
            "delay", PARAMS_ITWEEN_SCALE_DELAY
        );

        const iTween.EaseType PARAMS_POSITION_EASE_TYPE = iTween.EaseType.easeInBack;
        float paramsPositionTime = BASE_ANIMATION_DURATION;
        Hashtable paramsPositionParams = iTween.Hash(
            "y", _detailParams.transform.localPosition.y + paramsPositionOffsetY,
            "time", paramsPositionTime,
            "easeType", PARAMS_POSITION_EASE_TYPE,
            "delay", NAME_ITWEEN_SCALE_DELAY,
            "islocal", true
        );

        iTween.ValueTo(gameObject, imageFadeParams);
        iTween.ScaleTo(_detailImage, imageScaleParams);
        iTween.RotateTo(_detailImage, imageRotationParams);
        
        iTween.ScaleTo(_detailName, nameScaleParams);
        iTween.MoveTo(_detailName, namePositionParams);

        iTween.ScaleTo(_detailParams, paramsScaleParams);
        iTween.MoveTo(_detailParams, paramsPositionParams);
    }

    public void UpdateAlpha(float newValue) {
        //Color newColor = _detailImage.GetComponent<Image>().color;
        //newColor.a = newValue;
        //_detailImage.GetComponent<Image>().color = newColor;
    }

    private void OnITweenComplete() {
		_detailImage.transform.localRotation = Quaternion.Euler (Vector3.zero);
        _detailImage.transform.GetComponent<Image>().sprite = null;
        Color newColor = new Color();
        ColorUtility.TryParseHtmlString("#D0D0D0", out newColor);
        _detailImage.transform.GetComponent<Image>().color = newColor;
    }
}
