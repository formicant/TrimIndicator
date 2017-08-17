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

			_trimPitch = new TrimLabel(gaugesObject, new Vector3(34F, -15.5F), true);
			_trimYaw = new TrimLabel(gaugesObject, new Vector3(-24F, -63.5F), false);
			_trimRoll = new TrimLabel(gaugesObject, new Vector3(-24F, -25F), false);
			//_trimWheelThrottle = new TrimLabel(gaugesObject, new Vector3(), true);
			//_trimWheelSteer = new TrimLabel(gaugesObject, new Vector3(), false);
		}

		void Update()
		{
			var ctrlState = FlightGlobals.ActiveVessel?.ctrlState;

			_trimPitch.SetValue(ctrlState?.pitchTrim ?? 0);
			_trimYaw.SetValue(ctrlState?.yawTrim ?? 0);
			_trimRoll.SetValue(ctrlState?.rollTrim ?? 0);
			//_trimWheelThrottle.SetValue(ctrlState?.wheelThrottleTrim ?? 0);
			//_trimWheelSteer.SetValue(ctrlState?.wheelSteerTrim ?? 0);
		}

		TrimLabel _trimPitch;
		TrimLabel _trimYaw;
		TrimLabel _trimRoll;
		//TrimLabel _trimWheelThrottle;
		//TrimLabel _trimWheelSteer;
	}
}
