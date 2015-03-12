using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Opentok;


namespace OpenTokForms
{
	public partial class ConferencePage : ContentPage
	{
		
		IOpentokFactory _otFactory = OpentokFactoryImpl.GetInstance();
		IOpentok _opentok;

		ISession _session;
		IPublisher _publisher;
		ISubscriber _subscriber;

		public ConferencePage ()
		{
			InitializeComponent ();
			_opentok = _otFactory.Opentok;

			DoConnect ();
		}

		private void DoConnect()
		{
			_session = _opentok.CreateSession (Configuration.Config.API_KEY, Configuration.Config.SESSION_ID);

			_session.Connected += (object sender, EventArgs e) => DoPublish();
			_session.StreamAdded += (object sender, StreamAddedEventArgs e) => DoSubscribe(e.Stream);

			_session.Connect (Configuration.Config.TOKEN);
		}

		public void DoPublish()
		{
			_publisher = _opentok.CreatePublisher ("XAMARIN FORMS", 320, 240);

			_publisher.RenderFrame += (object sender, RenderFrameEventArgs e) => {
				/* Render Frame */
			};
			_session.Publish (_publisher);
		}

		private void DoSubscribe(IStream stream)
		{
			_subscriber = _opentok.CreateSubscriber (stream);
			_subscriber.RenderFrame += (object sender, RenderFrameEventArgs e) => {
				/* Render Frame */
				;
			};
			_session.Subscribe (_subscriber);
		}
	}
}

