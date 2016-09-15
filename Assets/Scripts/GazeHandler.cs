using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;
using UnityEngine.Windows.Speech;

#if !UNITY_WSA
using Windows.UI.Input.Spatial;
#endif

using System;

public class GazeHandler : MonoBehaviour
{
    private GameObject _hitObject = null;
    private GameObject _draggedObject = null;

    [SerializeField]
    private Material _hitMaterial;
    private Material _oldMaterial;

    private GestureRecognizer _recognizer;
    private KeywordRecognizer _speechRecognizer;
    private GestureRecognizer _manipulationRecognizer;

    private Vector3 _navigationRailsStartOffset = new Vector3(0, 0, 0);
    private bool _holdState = false;
    private GestureSettings _oldRecognizedGestures = (GestureSettings.Tap | GestureSettings.Hold);

    private float _positionSnapMultiplier = 0.1f;


    // Use this for initialization
    void Start()
    {

        String[] recognizedWords = new String[] { "A", "B", "C", "1", "2", "3", "alpha", "beta", "gamma", "shoot" };
        _speechRecognizer = new KeywordRecognizer(recognizedWords);
        _speechRecognizer.OnPhraseRecognized += SpeechRecognizerOnSpeechRecognizedEvent;
        _speechRecognizer.Start();

        _recognizer = new GestureRecognizer();
        _recognizer.SetRecognizableGestures(GestureSettings.Tap | GestureSettings.Hold | GestureSettings.ManipulationTranslate);

        _recognizer.TappedEvent += RecognizerOnTappedEvent;
        _recognizer.HoldStartedEvent += RecognizerOnHoldStartedEvent;
        _recognizer.HoldCompletedEvent += RecognizerOnHoldCompletedEvent;

        _recognizer.StartCapturingGestures();


        _manipulationRecognizer = new GestureRecognizer();
        _manipulationRecognizer.SetRecognizableGestures(GestureSettings.ManipulationTranslate);

        _manipulationRecognizer.ManipulationStartedEvent += RecognizerOnManipulationStartedEvent;
        _manipulationRecognizer.ManipulationUpdatedEvent += RecognizerOnManipulationUpdatedEvent;
        _manipulationRecognizer.ManipulationCompletedEvent += RecognizerOnManipulationCompletedEvent;
        _manipulationRecognizer.ManipulationCanceledEvent += RecognizerOnManipulationCanceledEvent;

        _manipulationRecognizer.StartCapturingGestures();

#if !UNITY_WSA
        SpatialInteractionManager mgr = SpatialInteractionManager.GetForCurrentView();
        mgr.InteractionDetected += ManagerOnInteractionDetected;
#endif
    }

    private void SpeechRecognizerOnSpeechRecognizedEvent(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Recognized phrase:" + args);
    }



#if !UNITY_WSA
    private void ManagerOnInteractionDetected(SpatialInteractionManager sender, SpatialInteractionDetectedEventArgs args)
    {
        throw new NotImplementedException();
    }
#endif

    // Update is called once per frame
    void Update()
    {
        Ray cameraRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(cameraRay, out hitInfo))
        {
            if (_hitObject == hitInfo.transform.gameObject)
            {
                return;
            }
            _hitObject = hitInfo.transform.gameObject;
            var myRenderer = _hitObject.GetComponent<MeshRenderer>();

            if (myRenderer != null)
            {
                _oldMaterial = myRenderer.material;
                myRenderer.material = _hitMaterial;
            }
        }
        else
        {
            if (_hitObject == null)
            {
                return;
            }
            var myRenderer = _hitObject.GetComponent<MeshRenderer>();
            if (myRenderer != null)
            {
                myRenderer.material = _oldMaterial;
            }

            _hitObject = null;
        }
    }

    private void RecognizerOnTappedEvent(InteractionSourceKind source, int tapCount, Ray straaltje)
    {
        //if (_hitObject != null)
        //{
        //    _hitObject.SendMessage("onHit");
        //}

    }

    private void RecognizerOnHoldStartedEvent(InteractionSourceKind source, Ray headRay)
    {
        
        //_holdState = true;


    }

    private void RecognizerOnHoldCompletedEvent(InteractionSourceKind source, Ray headRay)
    {
        

        //_recognizer.StopCapturingGestures();

        //_recognizer.SetRecognizableGestures(_oldRecognizedGestures);
        //_recognizer.NavigationStartedEvent -= RecognizerOnNavigationStartedEvent;
        //_recognizer.NavigationUpdatedEvent -= RecognizerOnNavigationUpdatedEvent;

        //_recognizer.StartCapturingGestures();

        _holdState = false;
    }

    private float roundToNearest(float input) {
        return (float)Math.Round(Convert.ToDouble(input * 10f *_positionSnapMultiplier));
    }

    private void RecognizerOnManipulationUpdatedEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
    {
        if (_draggedObject != null)
        {
            Vector3 proposedPosition = _draggedObject.transform.position + (normalizedOffset - _navigationRailsStartOffset);
            //Vector3 newPosition = new Vector3(roundToNearest(proposedPosition.x), roundToNearest(proposedPosition.y), roundToNearest(proposedPosition.z));
           
            _draggedObject.transform.position = proposedPosition;
        }
    }

    private void RecognizerOnManipulationStartedEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
    {
        Debug.Log("Start Drag.");
        _holdState = true;
        _draggedObject = _hitObject;
        _navigationRailsStartOffset = normalizedOffset;
    }

    private void RecognizerOnManipulationCanceledEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
    {
        Debug.Log("Cancel Drag.");
        _draggedObject = null;
        _holdState = false;
    }

    private void RecognizerOnManipulationCompletedEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
    {
        Debug.Log("Completed Drag.");
        _draggedObject = null;
        _holdState = false;
    }





}