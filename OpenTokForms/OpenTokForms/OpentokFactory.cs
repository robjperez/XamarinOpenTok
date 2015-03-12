using System;
using Opentok;

namespace OpenTokForms
{
	public class OpentokFactoryImpl : IOpentokFactory
	{
		public IOpentok Opentok { get; set; }

		private static IOpentokFactory _instance = null;

		public static IOpentokFactory GetInstance() {
			if (_instance == null) {
				_instance = new OpentokFactoryImpl ();
			}
			return _instance;
		}

		public OpentokFactoryImpl ()
		{
		}
	}
}

