using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

namespace ZomboTerrain
{
    public class DisplayEffectController: IInitialisible, IUpdatable, ICleanable, IController
    {        
        private PostProcessVolume _postProcessVolume;

        private Button _nuarEffectButton;
        private Button _fishEyeEffectButton;
        private Button _eyeFocusEffectButton;
        private Button _eyeAddictiveEffectButton;
        private ColorGrading _colorGrading;
        private LensDistortion _lensDistortion;
        private DepthOfField _depthOfField;
        private AutoExposure _autoExposure;
        private Camera _mainCamera;
        private float _eyeDistance;

        public DisplayEffectController(Camera mainCamera, GameUIController gameUIController, PostProcessVolume postProcessVolume)
        {
            _mainCamera = mainCamera;
            _postProcessVolume = postProcessVolume;
            _nuarEffectButton = gameUIController.NuarEffectButton;
            _fishEyeEffectButton = gameUIController.FishEyeEffectButton;
            _eyeFocusEffectButton = gameUIController.EyeFocusEffectButton;
            _eyeAddictiveEffectButton = gameUIController.EyeAddictiveEffectButton; 
        }

        public void Initialization()
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

        public void LocalUpdate(float deltaTime)
        {
            if (_depthOfField.active == true)
                EyeFocusEffect();
        }

        public void CleanUp()
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
            Ray ray = _mainCamera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                _eyeDistance = (hit.point - _mainCamera.transform.position).magnitude;

            _depthOfField.focusDistance.value = _eyeDistance;
        }
    }
}
