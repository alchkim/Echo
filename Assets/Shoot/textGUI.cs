using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class textGUI : MonoBehaviour {
	float width;
	float height;
	Text text;
	// Use this for initialization
	void Start () {
		width = Screen.width;
		height = Screen.height;
		text = GetComponent< Text > ();
		int val = int.Parse(gameObject.tag);

		switch (val) {
		case 1:
//			RectTransform pos1 = GetComponent< Text > ().rectTransform.anchoredPosition;
			RectTransform pos1 = GetComponent< Text > ().rectTransform;
			//			pos1 = new Rect (-1 * width / 2, 1 * height / 2, width, height);
//			pos1.anchoredPosition = new Vector2 (-1 * width / 8, 1 * height / 4);
			pos1.anchoredPosition = new Vector2 (0, 0);

			break;
		case 2:
//			Vector2 pos2 = GetComponent< Text > ().rectTransform.anchoredPosition;
			RectTransform pos2 = GetComponent< Text > ().rectTransform;
//			pos2 = new Rect (-1 * width / 2, -1 * height / 2, width, height);
//			pos2.anchoredPosition = new Vector2 (-1 * width / 8, -1 * height / 4);
			pos2.anchoredPosition = new Vector2 (0, 0);
			break;
		default:
//			Vector2 pos3 = GetComponent< Text > ().rectTransform.anchoredPosition;
			RectTransform pos3 = GetComponent< Text > ().rectTransform;
//			pos3 = new Rect (0, 1 * height / 2, width, height);
			pos3.anchoredPosition = new Vector2 (0, 1 * height / 2);
			break;
		}
	}
}
