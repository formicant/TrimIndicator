using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

namespace TrimIndicator
{
	class TrimLabel
	{
		public TrimLabel(GameObject parent, Vector3 location, bool isVertical)
		{
			_sliderDirection = isVertical ? Vector3.up : Vector3.right;
			_sliderAmplitude = isVertical ? VerticalSliderAmplitude : HorizontalSliderAmplitude;
			var textAlignment = isVertical ? TextAlignmentOptions.BaselineLeft : TextAlignmentOptions.Baseline;

			var baseObject = MakeDummyObject(parent, location);
			_slider = MakeDummyObject(baseObject);

			_textMeshes = new[]
			{
				MakeTextMesh(_slider, -OutlineWidth * _sliderDirection, BackgroundColor, textAlignment),
				MakeTextMesh(_slider, +OutlineWidth * _sliderDirection, BackgroundColor, textAlignment),
				MakeTextMesh(_slider, Vector3.zero, ForegroundColor, textAlignment),
			};
		}

		public void SetValue(float value)
		{
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			if(value != _lastValue)
			{
				int steps = (int)Math.Round(value / Step);
				string text = steps != 0
					? (steps < 0 ? "−" : "+") + Math.Abs(steps).ToString(CultureInfo.InvariantCulture)
					: string.Empty;

				_slider.transform.localPosition = _sliderAmplitude * GetSliderPosition(value) * _sliderDirection;

				foreach(var textMesh in _textMeshes)
					textMesh.text = text;

				_lastValue = value;
			}
		}

		readonly Vector3 _sliderDirection;
		readonly float _sliderAmplitude;
		readonly GameObject _slider;
		readonly IEnumerable<TextMeshProUGUI> _textMeshes;

		float _lastValue;


		static GameObject MakeObject(GameObject parent, Vector3 relativeLocation)
		{
			var gameObject = new GameObject { layer = LayerMask.NameToLayer("UI") };
			gameObject.transform.SetParent(parent.transform, false);
			gameObject.transform.localPosition = relativeLocation;
			return gameObject;
		}

		static GameObject MakeDummyObject(GameObject parent, Vector3 relativeLocation = default(Vector3))
		{
			var gameObject = MakeObject(parent, relativeLocation);
			// Just an empty text mesh. Consider replacing by something more adequate
			gameObject.AddComponent<TextMeshProUGUI>();
			return gameObject;
		}

		static TextMeshProUGUI MakeTextMesh(GameObject parent, Vector3 relativeLocation, Color color, TextAlignmentOptions alignment)
		{
			var gameObject = MakeObject(parent, relativeLocation);

			var textMesh = gameObject.AddComponent<TextMeshProUGUI>();
			textMesh.autoSizeTextContainer = true;
			textMesh.isOverlay = true;
			textMesh.enableWordWrapping = false;
			textMesh.alignment = alignment;
			textMesh.font = Font;
			textMesh.fontMaterial = FontMaterial;
			textMesh.fontSize = FontSize;
			textMesh.color = color;

			return textMesh;
		}

		static float GetSliderPosition(float value) =>
			(float)(Math.Atan(value * SliderPositionSteepness) / Math.Atan(SliderPositionSteepness));

		static TMP_FontAsset _font;
		static TMP_FontAsset Font => _font ?? (_font =
			Resources.LoadAll<TMP_FontAsset>("Fonts")
				.FirstOrDefault(f => f.name == "Calibri SDF"));

		static Material _fontMaterial;
		static Material FontMaterial => _fontMaterial ?? (_fontMaterial =
			Resources.LoadAll<Material>("Fonts")
				.FirstOrDefault(f => f.name == "Calibri SDF Material"));

		static readonly Color BackgroundColor = new Color(0.235F, 0.274F, 0.310F); // #3C464F
		static readonly Color ForegroundColor = new Color(0.827F, 0.827F, 0.827F); // #D3D3D3
		const float FontSize = 17F;
		const float OutlineWidth = 1.5F;
		const float HorizontalSliderAmplitude = 39F;
		const float VerticalSliderAmplitude = 49F;
		const double SliderPositionSteepness = 20;

		const float Step = 0.002F;
	}
}
