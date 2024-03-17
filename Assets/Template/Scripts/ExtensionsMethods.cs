using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Template.Scripts
{
	public static class ExtensionsMethods
	{
		public static void RemoveAllChildren(this RectTransform trans)
		{
			foreach (RectTransform child in trans)
			{
				Object.Destroy(child.gameObject);
			}
		}

		public static void RemoveAllChildren(this Transform trans)
		{
			foreach (Transform child in trans)
			{
				Object.DestroyImmediate(child.gameObject);
			}
		}

		public static void ActivateAtIndex(this List<GameObject> listObject, int index)
		{
			foreach (var item in listObject)
			{
				item.SetActive(false);
			}
			listObject[index].SetActive(true);
		}

		public static void ToggleActivateAll(this List<GameObject> listObject, bool isOn)
		{
			foreach (var item in listObject)
			{
				item.SetActive(isOn);
			}
		}

		public static void ShuffleMe<T>(this IList<T> list)
		{
			var random = new System.Random();

			for (var i = list.Count - 1; i > 1; i--)
			{
				var rnd = random.Next(i + 1);

				(list[rnd], list[i]) = (list[i], list[rnd]);
			}
		}

		public static int RandomSign()
		{
			return Random.value < .5f ? 1 : -1;
		}

		public static bool IsInLayerMask(int layer, LayerMask layerMask)
		{
			return layerMask == (layerMask | (1 << layer));
		}

		public static Gradient ToAlpha(this Color color, float alphaTime)
		{
			var grad = new Gradient();
			grad.SetKeys(new[] { new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f) }, new[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, alphaTime) });
			return grad;
		}
		
		public static float WrapAngle(float angle)
		{
		    switch (angle)
		    {
		        case > 180:
		            angle -= 360;
		            break;
		        case < -180:
		            angle += 360;
		            break;
		    }
		
		    return angle;
		}

		public static string ConvertToDotDecimal(string input)
		{
			return input.Replace(',', '.');
		}

		public static Vector3 WorldToUISpace(Canvas parentCanvas, Vector3 worldPos, Camera cam)
		{
			var screenPos = cam.WorldToScreenPoint(worldPos);
			RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out var movePos);
			return parentCanvas.transform.TransformPoint(movePos);
		}

		public static bool IsEven(int number)
		{
			return number % 2 == 0;
		}

		public static GameObject FindTopMostParent(this GameObject childObject)
		{
			var currentTransform = childObject.transform;

			while (currentTransform.parent != null)
			{
				currentTransform = currentTransform.parent;
			}

			return currentTransform.gameObject;
		}

		public static T GetRandomEnumValue<T>()
		{
			var values = System.Enum.GetValues(typeof(T));
			var randomEnumValue = (T)values.GetValue(Random.Range(0, values.Length));
			return randomEnumValue;
		}

		public static void RemoveAt<T>(ref T[] arr, int index)
		{
			for (var a = index; a < arr.Length - 1; a++)
			{
				arr[a] = arr[a + 1];
			}

			Array.Resize(ref arr, arr.Length - 1);
		}

		public static Vector2[] TransformListToVector2Array(Transform[] transforms)
		{
			Vector2[] vector2Array = new Vector2[transforms.Length];

			for (int i = 0; i < transforms.Length; i++)
			{
				vector2Array[i] = new Vector2(transforms[i].position.x, transforms[i].position.y);
			}

			return vector2Array;
		}
	}
}
