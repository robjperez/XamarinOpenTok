using System;

namespace Opentok
{
	public interface IOpentokFactory
	{
		IOpentok Opentok { get; set; }	
	}

	public interface IOpentok : INativeRef
	{				
		ISession CreateSession(string apiKey, string sessionId);
		IPublisher CreatePublisher(string name, int width, int height);
		ISubscriber CreateSubscriber(IStream stream);
	}

	public interface INativeRef : IDisposable
	{
		IntPtr GetNativeReference();
	}

	public interface IPublisher : INativeRef
	{
		event EventHandler<RenderFrameEventArgs> RenderFrame;
	}

	public interface ISubscriber : INativeRef
	{
		event EventHandler<RenderFrameEventArgs> RenderFrame;
	}

	public interface IStream : INativeRef
	{
	}

	public class RenderFrameEventArgs : EventArgs
	{
		public byte[][] planes;
	}

	public interface ISession : INativeRef
	{
		void Connect(string token);
		void Publish(IPublisher publisher);
		void Subscribe(ISubscriber subscriber);

		event EventHandler Connected;
		event EventHandler<StreamAddedEventArgs> StreamAdded;
	}

	public class StreamAddedEventArgs : EventArgs
	{
		public IStream Stream { get; set; }
	}
}

