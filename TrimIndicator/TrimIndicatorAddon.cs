using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TrimIndicator
{
	[KSPAddon(KSPAddon.Startup.Flight, once: false)]
	public class TrimIndicatorAddon : MonoBehaviour
	{
		void Start()
		{
			LoadSettings();

			var gaugesObject = FindObjectOfType<KSP.UI.Screens.Flight.LinearControlGauges>().gameObject;

			_pitchTrimLabel = new TrimLabel(gaugesObject, new Vector3(34F, -15.5F), isVertical: true);
			_yawTrimLabel = new TrimLabel(gaugesObject, new Vector3(-24F, -63.5F), isVertical: false);
			_rollTrimLabel = new TrimLabel(gaugesObject, new Vector3(-24F, -25F), isVertical: false);

			if(_showWheelTrim)
			{
				_wheelThrottleTrimLabel = new TrimLabel(gaugesObject, new Vector3(62F, -15.5F), isVertical: true);
				_wheelSteerTrimLabel = new TrimLabel(gaugesObject, new Vector3(-24F, -76F), isVertical: false);
			}
		}

		void Update()
		{
			var ctrlState = FlightInputHandler.state;

			_pitchTrimLabel?.SetValue(ctrlState?.pitchTrim ?? 0);
			_yawTrimLabel?.SetValue(ctrlState?.yawTrim ?? 0);
			_rollTrimLabel?.SetValue(ctrlState?.rollTrim ?? 0);

			if(_showWheelTrim)
			{
				_wheelThrottleTrimLabel?.SetValue(ctrlState?.wheelThrottleTrim ?? 0);
				_wheelSteerTrimLabel?.SetValue(-(ctrlState?.wheelSteerTrim ?? 0));
			}
		}

		void LoadSettings()
		{
			try
			{
				var settings = ConfigNode.Load(SettingsFilePath);
				settings.TryGetValue("ShowWheelTrim", ref _showWheelTrim);
			}
			catch(Exception exception)
			{
				print($"{nameof(TrimIndicator)}: Cannot load settings. {exception}");
			}
		}

		bool _showWheelTrim;

		TrimLabel _pitchTrimLabel;
		TrimLabel _yawTrimLabel;
		TrimLabel _rollTrimLabel;
		TrimLabel _wheelThrottleTrimLabel;
		TrimLabel _wheelSteerTrimLabel;

		static readonly string SettingsFilePath = $"{KSPUtil.ApplicationRootPath}GameData/{nameof(TrimIndicator)}/{nameof(TrimIndicator)}.settings";
	}
}
