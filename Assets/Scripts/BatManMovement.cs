using UnityEngine;
using System.Collections;
using UnityEngine.VR;
using VRStandardAssets.Common;
using VRStandardAssets.Utils;

public class BatManMovement : MonoBehaviour {


	public float m_Damping = 0.2f;
	public float m_MaxYRotation = 50f;
	public float m_MinYRotation = -50f;
	public VRInput m_VRInput;
	public float m_Speed = 100f;   		// the forward movement speed of batman
	public GameObject m_BatMan;




	private const float k_ExpDampCoef = -50f;

	// get the rotation angle, and let batman rotate
	private void Update()
	{
		var eulerRotation = transform.rotation.eulerAngles;

		eulerRotation.x = 0;
		eulerRotation.z = 0;
		eulerRotation.y = InputTracking.GetLocalRotation (VRNode.Head).eulerAngles.y;

		if (eulerRotation.y < 270)
			eulerRotation.y += 360;

		eulerRotation.y = Mathf.Clamp (eulerRotation.y, 360 + m_MinYRotation, 360 + m_MaxYRotation);

		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (eulerRotation), 
			m_Damping * (1 - Mathf.Exp (k_ExpDampCoef * Time.deltaTime)));
	}


	// handle the click movement 
	private void OnEnable()
	{
		m_VRInput.OnClick += HandleClick;
	}


	private void OnDisable()
	{
		m_VRInput.OnClick -= HandleClick;
	}

	private void HandleClick()
	{
		m_BatMan.transform.Translate(Vector3.forward * Time.deltaTime * m_Speed);
	}

}
