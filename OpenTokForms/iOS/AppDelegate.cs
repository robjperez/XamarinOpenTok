using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using Opentok;

namespace OpenTokForms.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			IOpentokFactory otFactory = OpentokFactoryImpl.GetInstance ();	
			otFactory.Opentok = new Opentok.Opentok ();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}

