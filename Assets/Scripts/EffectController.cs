using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

namespace ZomboTerrain
{
    public class EffectController : MonoBehaviour
    {
        [SerializeField] private Button _nuarEffectButton;
        [SerializeField] private Button _fishEyeEffectButton;
        [SerializeField] private Button _eyeFocusEffectButton;
        [SerializeField] private Button _eyeAddictiveEffectButton;
        [SerializeField] private PostProcessVolume _postProcessVolume;

        private ColorGrading _colorGrading;
        private LensDistortion _lensDistortion;
        private DepthOfField _depthOfField;
        private AutoExposure _autoExposure;

        private float _eyeDistance;

        private void Start()
        {
            _colorGrading = _postProcessVolume.profile.GetSetting<ColorGrading>();
            _lensDistortion = _postProcessVolume.profile.GetSetting<LensDistortion>();
            _depthOfField = _postProcessVolume.profile.GetSetting<DepthOfField>();
            _autoExposure = _postProcessVolume.profile.GetSetting<AutoExposure>();

            var effects = new PostProcessEffectSettings[] { _colorGrading, _lensDistortion, _depthOfField, _autoExposure };

            _nuarEffectButton.onClick.AddListener(() => ChangeEffect(_nuarEffectButton, effects[0]));
            _fishEyeEffectButton.onClick.AddListener(() => ChangeEffect(_fishEyeEffectButton, effects[1]));
            _eyeFocusEffectButton.onClick.AddListener(() => ChangeEffect(_eyeFocusEffectButton, effects[2]));
            _eyeAddictiveEffectButton.onClick.AddListener(() => ChangeEffect(_eyeAddictiveEffectButton, effects[3]));
        }

        private void Update()
        {
            if (_depthOfField.active == true)
                EyeFocusEffect();
        }

        private void OnDestroy()
        {
            _nuarEffectButton.onClick.RemoveAllListeners();
            _fishEyeEffectButton.onClick.RemoveAllListeners();
            _eyeFocusEffectButton.onClick.RemoveAllListeners();
            _eyeAddictiveEffectButton.onClick.RemoveAllListeners();
        }

        private void ChangeEffect(Button button, PostProcessEffectSettings effect)
        {
            effect.active = (effect.active == false) ? true : false;
            button.image.color = effect.active ? Color.green : Color.white;
        }

        private void EyeFocusEffect()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                _eyeDistance = (hit.point - transform.position).magnitude;

            _depthOfField.focusDistance.value = _eyeDistance;
        }
    }
}
