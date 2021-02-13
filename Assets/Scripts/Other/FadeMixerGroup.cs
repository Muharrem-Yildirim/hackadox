﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public static class FadeMixerGroup
{
	public static IEnumerator FadeOut(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume, Action callback = null)
	{
		float currentTime = 0;
		float currentVol;
		audioMixer.GetFloat(exposedParam, out currentVol);
		currentVol = Mathf.Pow(10, currentVol / 20);
		float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
			audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
			yield return null;
		}

		if (callback != null)
			callback();
		yield break;
	}
}
