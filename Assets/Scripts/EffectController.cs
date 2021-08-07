using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class EffectController : MonoBehaviour
{
    [SerializeField] private Button nuarEffectBtn;
    [SerializeField] private Button fishEyeEffectBtn;
    [SerializeField] private Button eyeFocusEffectBtn;
    [SerializeField] private Button eyeAddictiveEffectBtn;
    [SerializeField] private PostProcessVolume postProcessVolume;

    private ColorGrading colorGrading;
    private LensDistortion lensDistortion;
    private DepthOfField depthOfField;
    private AutoExposure autoExposure;

    private float eyeDistance;

    private void Start()
    {
        colorGrading = postProcessVolume.profile.GetSetting<ColorGrading>();
        lensDistortion = postProcessVolume.profile.GetSetting<LensDistortion>();
        depthOfField = postProcessVolume.profile.GetSetting<DepthOfField>();
        autoExposure = postProcessVolume.profile.GetSetting<AutoExposure>();

        var effects = new PostProcessEffectSettings[] { colorGrading, lensDistortion, depthOfField, autoExposure };

        nuarEffectBtn.onClick.AddListener(() => ChangeEffect(nuarEffectBtn, effects[0]));
        fishEyeEffectBtn.onClick.AddListener(() => ChangeEffect(fishEyeEffectBtn, effects[1]));
        eyeFocusEffectBtn.onClick.AddListener(() => ChangeEffect(eyeFocusEffectBtn, effects[2]));
        eyeAddictiveEffectBtn.onClick.AddListener(() => ChangeEffect(eyeAddictiveEffectBtn, effects[3]));
    }

    private void Update()
    {
        if (depthOfField.active == true)
            EyeFocusEffect();
    }

    private void OnDestroy()
    {
        nuarEffectBtn.onClick.RemoveAllListeners();
        fishEyeEffectBtn.onClick.RemoveAllListeners();
        eyeFocusEffectBtn.onClick.RemoveAllListeners();
        eyeAddictiveEffectBtn.onClick.RemoveAllListeners();
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
            eyeDistance = (hit.point - transform.position).magnitude;        

        depthOfField.focusDistance.value = eyeDistance;
    }
}
