using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VRStandardAssets.Common;
using VRStandardAssets.Utils;

public class VRUIController : MonoBehaviour
{
	public UIFader m_IntroUI;
	public UIFader m_OutroUI;
	//public UIFader m_PlayerUI;
	//public Text    m_Timer;

	public IEnumerator ShowIntroUI()
	{
		yield return StartCoroutine (m_IntroUI.InteruptAndFadeIn ());
	}

	public IEnumerator HideIntroUI()
	{
		yield return StartCoroutine (m_IntroUI.InteruptAndFadeOut ());
	}

	public IEnumerator ShowOutroUI()
	{
		yield return StartCoroutine (m_OutroUI.InteruptAndFadeIn ());
	}

	public IEnumerator HideOutroUI()
	{
		yield return StartCoroutine (m_OutroUI.InteruptAndFadeOut ());
	}
//	public IEnumerator ShowPlayerUI()
//	{
//		yield return StartCoroutine (m_PlayerUI.InteruptAndFadeIn ());
//	}
//
//	public IEnumerator HidePlayerUI()
//	{
//		yield return StartCoroutine (m_PlayerUI.InteruptAndFadeOut ());
//	}
}