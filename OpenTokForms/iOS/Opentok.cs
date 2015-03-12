using System;
using Opentok;
using ObjCRuntime;
using System.Runtime.InteropServices;

namespace Opentok
{
	public class Session : ISession
	{
		private IntPtr _session;
		private SessionNative.otc_session_cb _callbacks;

		public event EventHandler Connected;
		public event EventHandler<StreamAddedEventArgs> StreamAdded;

		public Session(string apiKey, string sessionId) 
		{			
			_callbacks = new SessionNative.otc_session_cb ();
			_callbacks.on_connected = on_connected_cb;
			_callbacks.on_stream_received = on_stream_received_cb;
			_callbacks.userData = (IntPtr) GCHandle.Alloc(this);

			_session = SessionNative.otc_session_new (apiKey, sessionId, ref _callbacks);
		}

		public void Connect(string token) 
		{
			SessionNative.otc_session_connect (_session, token);
		}

		public void Publish(IPublisher publisher)
		{
			SessionNative.otc_session_publish (_session, publisher.GetNativeReference ());
		}

		public void Subscribe(ISubscriber subscriber)
		{
			SessionNative.otc_session_subscribe (_session, subscriber.GetNativeReference ());
		}

		#region Callbacks and Events

		[MonoPInvokeCallback (typeof (SessionNative.on_stream_received_delegate))]
		static void on_stream_received_cb(IntPtr session, IntPtr userData, IntPtr stream) {			
			var handle = (GCHandle)userData;
			var refSession = (handle.Target as Session);
			refSession.OnStreamAdded (stream);
		}

		public void OnStreamAdded(IntPtr stream)
		{
			var args = new StreamAddedEventArgs ();
			args.Stream = new Stream (stream);

			if (StreamAdded != null) {
				StreamAdded (this, args);
			}
		}

		[MonoPInvokeCallback (typeof (SessionNative.on_connected_delegate))]
		static void on_connected_cb(IntPtr session, IntPtr userData)
		{
			var handle = (GCHandle)userData;
			var refSession = (handle.Target as Session);
			refSession.OnConnected ();
		}

		public void OnConnected() 
		{
			if (Connected != null) {
				Connected (this, new EventArgs());
			}
		}

		#endregion

		#region INativeRef implementation

		public IntPtr GetNativeReference()
		{
			return _session;
		}

		public void Dispose ()
		{
			((GCHandle)_callbacks.userData).Free ();
			SessionNative.otc_session_delete (_session);
		}

		#endregion
	}		

	public class Subscriber : ISubscriber
	{
		IntPtr _subscriber;
		SubscriberNative.otc_subscriber_cb _callbacks;

		public event EventHandler<RenderFrameEventArgs> RenderFrame;

		public Subscriber(IStream stream)
		{
			_callbacks = new SubscriberNative.otc_subscriber_cb ();
			_callbacks.userData = (IntPtr)GCHandle.Alloc (this);

			_subscriber = SubscriberNative.otc_subscriber_new (stream.GetNativeReference (), ref _callbacks);
		}

		#region Callbacks and Events

		[MonoPInvokeCallback (typeof (SubscriberNative.on_render_frame_delegate))]
		static void on_render_frame(IntPtr subscriber, IntPtr userdata, IntPtr frame)
		{
			GCHandle handle = (GCHandle)userdata;
			Subscriber refSub = (handle.Target as Subscriber);
			refSub.OnRenderFrame (frame);
		}

		public void OnRenderFrame(IntPtr frame)
		{
			int numPlanes = VideoFrameNative.otc_video_frame_get_num_planes (frame);

			var rfArgs = new RenderFrameEventArgs ();
			rfArgs.planes = new byte[numPlanes][];

			for (var i = 0; i < numPlanes; i++) {
				var plane = VideoFrameNative.otc_video_frame_get_plane (frame, i);
				var planelen = VideoFrameNative.otc_video_frame_get_plane_size (frame, i);
				rfArgs.planes [i] = new byte[planelen];
				Marshal.Copy (plane, rfArgs.planes [i], 0, planelen);
			}

			if (RenderFrame != null) {
				RenderFrame (this, rfArgs);
			}
		}

		#endregion

		#region INativeRef implementation

		public IntPtr GetNativeReference()
		{
			return _subscriber;
		}

		public void Dispose()
		{
			((GCHandle)_callbacks.userData).Free ();
			SubscriberNative.otc_subscriber_delete (_subscriber);
		}

		#endregion
	}
		
	public class Publisher : IPublisher
	{
		IntPtr _publisher;
		PublisherNative.otc_publisher_cb _callbacks;
		IntPtr _capturer;

		public event EventHandler<RenderFrameEventArgs> RenderFrame;

		public Publisher(string name, int width, int height)
		{
			_callbacks = new PublisherNative.otc_publisher_cb ();
			_callbacks.on_render_frame = on_render_frame;
			_callbacks.userData = (IntPtr) GCHandle.Alloc (this);
			_capturer = iOS.VideoCapturerNative.otc_video_capturer_create (width, height);

			_publisher = PublisherNative.otc_publisher_new (name, _capturer, ref _callbacks);
		}

		#region Callbacks and Events

		[MonoPInvokeCallback (typeof (PublisherNative.on_render_frame_delegate))]
		static void on_render_frame(IntPtr publisher, IntPtr userdata, IntPtr frame)
		{
			GCHandle handle = (GCHandle)userdata;
			Publisher refPub = (handle.Target as Publisher);
			refPub.OnRenderFrame (frame);
		}

		public void OnRenderFrame(IntPtr frame)
		{
			int numPlanes = VideoFrameNative.otc_video_frame_get_num_planes (frame);

			var rfArgs = new RenderFrameEventArgs ();
			rfArgs.planes = new byte[numPlanes][];

			for (var i = 0; i < numPlanes; i++) {
				var plane = VideoFrameNative.otc_video_frame_get_plane (frame, i);
				var planelen = VideoFrameNative.otc_video_frame_get_plane_size (frame, i);
				rfArgs.planes [i] = new byte[planelen];
				Marshal.Copy (plane, rfArgs.planes [i], 0, planelen);
			}

			if (RenderFrame != null) {
				RenderFrame (this, rfArgs);
			}
		}

		#endregion

		#region INativeRef implementation

		public IntPtr GetNativeReference()
		{
			return _publisher;
		}

		public void Dispose()
		{
			PublisherNative.otc_publisher_delete (_publisher);
			((GCHandle)_callbacks.userData).Free ();
		}

		#endregion
	}

	public class Stream : IStream
	{
		IntPtr _stream;

		public Stream(IntPtr stream)
		{
			_stream = stream;
		}

		#region INativeRef implementation

		public IntPtr GetNativeReference()
		{
			return _stream;
		}

		public void Dispose()
		{
			// Nothing to dispose here	
		}

		#endregion
	}

	public class Opentok : IOpentok
	{
		#region IOpentok implementation

		public ISession CreateSession (string apiKey, string sessionId)
		{
			return new Session (apiKey, sessionId);
		}
		public IPublisher CreatePublisher (string name, int width, int height)
		{
			return new Publisher (name, width, height);
		}

		public ISubscriber CreateSubscriber(IStream stream)
		{
			return new Subscriber (stream);
		}

		#endregion

		#region INativeRef implementation

		public IntPtr GetNativeReference()
		{
			throw new NotImplementedException ("It hasn't got any native reference");
		}

		public void Dispose()
		{
			Base.otc_destroy ();
		}

		#endregion

		public Opentok ()
		{
			Base.otc_init (IntPtr.Zero);
		}
	}


}