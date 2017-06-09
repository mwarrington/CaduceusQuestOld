using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Emotion
{
	public char EmotionType;
	public int EmotionIntensity;

	public Emotion (char type, int intensity)
	{
		EmotionType = type;
		EmotionIntensity = intensity;
	}

	public Color GetEmotionColor ()
	{
		if (EmotionType == 'a') {
			if (EmotionIntensity == 4) {
				return new Color (0.93f, 0.76f, 0f, 1);
			} else if (EmotionIntensity == 3) {
				return new Color (0.97f, 0.85f, 0.30f, 1);
			} else if (EmotionIntensity == 2) {
				return new Color (.99f, .95f, .51f, 1);
			} else if (EmotionIntensity == 1) {
				return new Color (.99f, .99f, .8f, 1);
			} else {
				Debug.LogError ("That shit don't exist. Emotion Type: " + EmotionType + " Emotion Intensity: " + EmotionIntensity);
				return Color.black;
			}
		} else if (EmotionType == 'b') {
			if (EmotionIntensity == 4) {
				return new Color (.48f, .74f, .05f, 1);
			} else if (EmotionIntensity == 3) {
				return new Color (.6f, .8f, .2f, 1);
			} else if (EmotionIntensity == 2) {
				return new Color (.77f, .88f, .38f, 1);
			} else if (EmotionIntensity == 1) {
				return new Color (.85f, .92f, .62f, 1);
			} else {
				Debug.LogError ("That shit don't exist. Emotion Type: " + EmotionType + " Emotion Intensity: " + EmotionIntensity);
				return Color.black;
			}

		} else if (EmotionType == 'c') {
			if (EmotionIntensity == 4) {
				return new Color (0, .45f, .18f, 1);
			} else if (EmotionIntensity == 3) {
				return new Color (.21f, .64f, .31f, 1);
			} else if (EmotionIntensity == 2) {
				return new Color (.46f, .76f, .47f, 1);
			} else if (EmotionIntensity == 1) {
				return new Color (.8f, .93f, .80f, 1);
			} else {
				Debug.LogError ("That shit don't exist. Emotion Type: " + EmotionType + " Emotion Intensity: " + EmotionIntensity);
				return Color.black;
			}
		} else if (EmotionType == 'd') {
			if (EmotionIntensity == 4) {
				return new Color (0, .51f, .67f, 1);
			} else if (EmotionIntensity == 3) {
				return new Color (.24f, .64f, .75f, 1);
			} else if (EmotionIntensity == 2) {
				return new Color (.05f, .78f, .84f, 1);
			} else if (EmotionIntensity == 1) {
				return new Color (.85f, .92f, .62f, 1);
			} else {
				Debug.LogError ("That shit don't exist. Emotion Type: " + EmotionType + " Emotion Intensity: " + EmotionIntensity);
				return Color.black;
			}
		} else if (EmotionType == 'e') {
			if (EmotionIntensity == 4) {
				return new Color (.12f, .42f, .67f, 1);
			} else if (EmotionIntensity == 3) {
				return new Color (.45f, .62f, .79f, 1);
			} else if (EmotionIntensity == 2) {
				return new Color (.64f, .73f, .85f, 1);
			} else if (EmotionIntensity == 1) {
				return new Color (.79f, .87f, .92f, 1);
			} else {
				Debug.LogError ("That shit don't exist. Emotion Type: " + EmotionType + " Emotion Intensity: " + EmotionIntensity);
				return Color.black;
			}
		} else if (EmotionType == 'f') {
			if (EmotionIntensity == 4) {
				return new Color (.48f, .31f, .64f, 1);
			} else if (EmotionIntensity == 3) {
				return new Color (.62f, .47f, .73f, 1);
			} else if (EmotionIntensity == 2) {
				return new Color (.73f, .6f, .8f, 1);
			} else if (EmotionIntensity == 1) {
				return new Color (.80f, .70f, .85f, 1);
			} else {
				Debug.LogError ("That shit don't exist. Emotion Type: " + EmotionType + " Emotion Intensity: " + EmotionIntensity);
				return Color.black;
			}
		} else if (EmotionType == 'g') {
			if (EmotionIntensity == 4) {
				return new Color (.86f, 0f, .28f, 1);
			} else if (EmotionIntensity == 3) {
				return new Color (.90f, .19f, .36f, 1);
			} else if (EmotionIntensity == 2) {
				return new Color (.77f, .88f, .38f, 1);
			} else if (EmotionIntensity == 1) {
				return new Color (.93f, .65f, .64f, 1);
			} else {
				Debug.LogError ("That shit don't exist. Emotion Type: " + EmotionType + " Emotion Intensity: " + EmotionIntensity);
				return Color.black;
			}
		} else if (EmotionType == 'h') {
			if (EmotionIntensity == 4) {
				return new Color (.91f, .44f, .0f, 1);
			} else if (EmotionIntensity == 3) {
				return new Color (.95f, .6f, .23f, 1);
			} else if (EmotionIntensity == 2) {
				return new Color (.97f, .73f, .41f, 1);
			} else if (EmotionIntensity == 1) {
				return new Color (.99f, .85f, .64f, 1);
			} else {
				Debug.LogError ("That shit don't exist. Emotion Type: " + EmotionType + " Emotion Intensity: " + EmotionIntensity);
				return Color.black;
			}
		} else {
			Debug.LogError ("That shit don't exist. Emotion Type: " + EmotionType + " Emotion Intensity: " + EmotionIntensity);
			return Color.black;
		}
	}

	#region GetFunctions

	public char GetNextEmotionType ()
	{
		if (EmotionType == 'a') {
			return 'b';
		} else if (EmotionType == 'b') {
			return 'c';
		} else if (EmotionType == 'c') {
			return 'd';
		} else if (EmotionType == 'd') {
			return 'e';
		} else if (EmotionType == 'e') {
			return 'f';
		} else if (EmotionType == 'f') {
			return 'g';
		} else if (EmotionType == 'g') {
			return 'h';
		} else if (EmotionType == 'h') {
			return 'a';
		} else {
			Debug.LogError ("That emotion type doesn't exist");
			return '?';
		}
	}

	public char GetLastEmotionType ()
	{
		if (EmotionType == 'a') {
			return 'h';
		} else if (EmotionType == 'h') {
			return 'g';
		} else if (EmotionType == 'g') {
			return 'f';
		} else if (EmotionType == 'f') {
			return 'e';
		} else if (EmotionType == 'e') {
			return 'd';
		} else if (EmotionType == 'd') {
			return 'c';
		} else if (EmotionType == 'c') {
			return 'b';
		} else if (EmotionType == 'b') {
			return 'a';
		} else {
			Debug.LogError ("That emotion type doesn't exist");
			return '?';
		}
	}

	#endregion
}