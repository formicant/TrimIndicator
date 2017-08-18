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
			var gaugesObject = FindObjectOfType<KSP.UI.Screens.Flight.LinearControlGauges>().gameObject;

			_pitchTrimLabel = new TrimLabel(gaugesObject, new Vector3(34F, -15.5F), isVertical: true);
			_yawTrimLabel = new TrimLabel(gaugesObject, new Vector3(-24F, -63.5F), isVertical: false);
			_rollTrimLabel = new TrimLabel(gaugesObject, new Vector3(-24F, -25F), isVertical: false);
			//_wheelThrottleTrimLabel = new TrimLabel(gaugesObject, new Vector3(), isVertical: true);
			//_wheelSteerTrimLabel = new TrimLabel(gaugesObject, new Vector3(), isVertical: false);
		}

		void Update()
		{
			var ctrlState = FlightGlobals.ActiveVessel?.ctrlState;

			_pitchTrimLabel?.SetValue(ctrlState?.pitchTrim ?? 0);
			_yawTrimLabel?.SetValue(ctrlState?.yawTrim ?? 0);
			_rollTrimLabel?.SetValue(ctrlState?.rollTrim ?? 0);
			//_wheelThrottleTrimLabel?.SetValue(ctrlState?.wheelThrottleTrim ?? 0);
			//_wheelSteerTrimLabel?.SetValue(ctrlState?.wheelSteerTrim ?? 0);
		}

		TrimLabel _pitchTrimLabel;
		TrimLabel _yawTrimLabel;
		TrimLabel _rollTrimLabel;
		//TrimLabel _wheelThrottleTrimLabel;
		//TrimLabel _wheelSteerTrimLabel;
	}
}
