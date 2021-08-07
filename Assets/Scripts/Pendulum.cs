using TMPro;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private Rigidbody sinkerRGBody;
    [SerializeField] int sinkerMass;
    [SerializeField] float rodLength;
    [SerializeField] TextMeshProUGUI rodLengthTxt;
    [SerializeField] TextMeshProUGUI sinkerMassTxt;

    private CharacterJoint characterJoint;

    private void Awake()
    {
        characterJoint = GetComponentInChildren<CharacterJoint>();        
    }
    private void Start()
    {
        sinkerRGBody.AddForce(Vector3.forward * 50, ForceMode.Impulse);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            rodLength -= 1;
        if (Input.GetKeyDown(KeyCode.O))
            rodLength += 1;
        if (Input.GetKeyDown(KeyCode.K))
            sinkerMass -= 1;
        if (Input.GetKeyDown(KeyCode.L))
            sinkerMass += 1;

        if (rodLength < 0) rodLength = 0;
        if (sinkerMass < 0) sinkerMass = 0;

        sinkerRGBody.mass = sinkerMass;

        var connectedAnchor = characterJoint.connectedAnchor;
        connectedAnchor.y = rodLength;
        characterJoint.connectedAnchor = connectedAnchor;

        sinkerMassTxt.text = sinkerMass.ToString();
        rodLengthTxt.text = rodLength.ToString();
    }
}
