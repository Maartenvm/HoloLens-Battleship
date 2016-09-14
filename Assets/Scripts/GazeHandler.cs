using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;
using UnityEngine.Windows.Speech;

public class GazeHandler : MonoBehaviour {
    private GameObject _hitObject;

    [SerializeField] private Material _hitMaterial;
    private Material _oldMaterial;

	// Use this for initialization
	void Start () {
        GestureRecognizer recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.TappedEvent += RecognizerOnTappedEvent;
        recognizer.StartCapturingGestures();

        //KeywordRecognizer wordRecognizer = new KeywordRecognizer(new [StringArray]);
        //wordRecognizer.OnPhraseRecognized += WordRecognizerOnWordRecognized;
      
    }

    void WordReconizerOnWordRecognized()
    {
        return;

    }
	
	// Update is called once per frame
	void Update () {
        Ray cameraRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;
        var layerMask = (1 << 8);
        layerMask = ~layerMask;

        if (Physics.Raycast(cameraRay, out hitInfo))
        {
            if (_hitObject == hitInfo.transform.gameObject)
            {
                return;
            }
            _hitObject = hitInfo.transform.gameObject;
            var myRenderer = _hitObject.GetComponent<MeshRenderer>();
            _oldMaterial = myRenderer.material;
            myRenderer.material = _hitMaterial;
        }
        else
        {
            if (_hitObject == null)
            {
                return;
            }
            var myRenderer = _hitObject.GetComponent<MeshRenderer>();
            myRenderer.material = _oldMaterial;
        }
    }

    private void RecognizerOnTappedEvent(InteractionSourceKind source, int tapCount, Ray straaltje)
    {
        if (_hitObject == null) return;
     
        _hitObject.SendMessage("Hit");
        

    }
}
