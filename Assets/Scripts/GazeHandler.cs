using UnityEngine;
using System.Collections;

public class GazeHandler : MonoBehaviour {
    private GameObject _hitObject;

    [SerializeField] private Material _hitMaterial;
    private Material _oldMaterial;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
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
}
