using System;
using System.Drawing;
using ObjCRuntime;
using Foundation;
using UIKit;

namespace Opentok
{
	[BaseType(typeof(UIView))]
	interface OTGLVideoRender {
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF frame);

		[Export ("renderVideoFrame:")]
		void RenderVideoFrame(IntPtr frame);
	}
}

