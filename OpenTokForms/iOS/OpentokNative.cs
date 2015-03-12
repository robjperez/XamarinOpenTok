using System;
using System.Runtime.InteropServices;

namespace Opentok.iOS
{
	public class VideoCapturerNative
	{
		[DllImport("__Internal", EntryPoint = "otc_video_capturer_create", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr otc_video_capturer_create(int width, int height);

		[DllImport("__Internal", EntryPoint = "otc_video_capturer_toggle_camera_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern void otc_video_capturer_toggle_camera_position (IntPtr videocapturer);
	}
}

namespace Opentok
{
	class PublisherNative
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void on_stream_created_delegate(IntPtr publisher, IntPtr userData, IntPtr stream);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void on_stream_destroyed_delegate(IntPtr publisher, IntPtr userData, IntPtr stream);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void on_render_frame_delegate(IntPtr publisher, IntPtr userData, IntPtr frame);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void on_destroyed_delegate(IntPtr publisher, IntPtr userData);

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct otc_publisher_cb
		{
			public IntPtr userData;
			public on_stream_created_delegate on_stream_created;
			public on_stream_destroyed_delegate on_stream_destroyed;
			public on_render_frame_delegate on_render_frame;
			public on_destroyed_delegate on_destroyed;
		}

		[DllImport("__Internal", EntryPoint = "otc_publisher_new", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr otc_publisher_new(string name, IntPtr capturer, ref otc_publisher_cb callbacks);

		[DllImport("__Internal", EntryPoint = "otc_publisher_delete", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_publisher_delete(IntPtr publisher);

		[DllImport("__Internal", EntryPoint = "otc_publisher_get_stream", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr otc_publisher_get_stream(IntPtr publisher);

		[DllImport("__Internal", EntryPoint = "otc_publisher_set_publish_video", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_publisher_set_publish_video(IntPtr publisher, bool publish_video);

		[DllImport("__Internal", EntryPoint = "otc_publisher_set_publish_audio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_publisher_set_publish_audio(IntPtr publisher, bool publish_audio);

		[DllImport("__Internal", EntryPoint = "otc_publisher_get_publish_video", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern bool otc_publisher_get_publish_video(IntPtr publisher);

		[DllImport("__Internal", EntryPoint = "otc_publisher_get_publish_audio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern bool otc_publisher_get_publish_audio(IntPtr publisher);

		[DllImport("__Internal", EntryPoint = "otc_publisher_get_session", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr otc_publisher_get_session(IntPtr publisher);       
	}

	class VideoFrameNative
	{
		[DllImport("__Internal", EntryPoint = "otc_video_frame_get_width", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_video_frame_get_width(IntPtr frame);

		[DllImport("__Internal", EntryPoint = "otc_video_frame_get_height", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_video_frame_get_height(IntPtr frame);

		[DllImport("__Internal", EntryPoint = "otc_video_frame_get_num_planes", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_video_frame_get_num_planes(IntPtr frame);

		[DllImport("__Internal", EntryPoint = "otc_video_frame_get_format", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_video_frame_get_format(IntPtr frame);

		[DllImport("__Internal", EntryPoint = "otc_video_frame_get_plane", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr otc_video_frame_get_plane(IntPtr frame, int plane);

		[DllImport("__Internal", EntryPoint = "otc_video_frame_get_plane_size", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_video_frame_get_plane_size(IntPtr frame, int plane);

		[DllImport("__Internal", EntryPoint = "otc_video_frame_get_plane_width", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_video_frame_get_plane_width(IntPtr frame, int plane);

		[DllImport("__Internal", EntryPoint = "otc_video_frame_get_plane_height", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_video_frame_get_plane_height(IntPtr frame, int plane);

		[DllImport("__Internal", EntryPoint = "otc_video_frame_copy", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr otc_video_frame_copy(int format, IntPtr frame);

		[DllImport("__Internal", EntryPoint = "otc_video_frame_get_plane_height", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_video_frame_destroy(IntPtr frame);
	}

	public class VideoCapturerNative
	{
		public struct otc_video_capturer_settings
		{
			public int format;
			public int width;
			public int height;
			public int fps;
			public int expected_capture_delay;
			public bool mirror_on_local_render;
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool init_delegate(IntPtr capturer, IntPtr userData);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool destroy_delegate(IntPtr capturer, IntPtr userData);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool start_delegate(IntPtr capturer, IntPtr userData);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool stop_delegate(IntPtr capturer, IntPtr userData);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool capture_settings_delegate(IntPtr capturer, IntPtr userData, otc_video_capturer_settings settings);

		public struct otc_video_capturer
		{
			public IntPtr userData;
			public init_delegate init;
			public destroy_delegate destroy;
			public start_delegate start;
			public stop_delegate stop;
			public capture_settings_delegate capture_settings;
		}

		[DllImport("__Internal", EntryPoint = "otc_video_capturer_provide_frame", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_video_capturer_provide_frame(ref otc_video_capturer capturer,
			int format, int width, int height,
			int rotation, byte[] buffer);
	}

	class SubscriberNative
	{        
		public enum otc_video_reason
		{
			OTC_VIDEO_REASON_PUBLISH_VIDEO = 1,
			OTC_VIDEO_REASON_SUBSCRIBE_TO_VIDEO = 2,
			OTC_VIDEO_REASON_QUALITY = 3
		};

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void on_connected_delegate(IntPtr subscriber, IntPtr user_data, IntPtr stream);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void on_disconnected_delegate(IntPtr subscriber, IntPtr user_data, IntPtr stream);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void on_render_frame_delegate(IntPtr subscriber, IntPtr user_data, IntPtr frame);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void on_video_disabled_delegate(IntPtr subscriber, otc_video_reason reason);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void on_video_enabled_delegate(IntPtr subscriber, otc_video_reason reason);

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct otc_subscriber_cb
		{
			public IntPtr userData;
			public on_connected_delegate on_connected;
			public on_disconnected_delegate on_disconnected;
			public on_render_frame_delegate on_render_frame;
			public on_video_disabled_delegate on_video_disabled;
			public on_video_enabled_delegate on_video_enabled;
		}

		[DllImport("__Internal", EntryPoint = "otc_subscriber_new", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr otc_subscriber_new(IntPtr stream, ref otc_subscriber_cb callbacks);

		[DllImport("__Internal", EntryPoint = "otc_subscriber_delete", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_subscriber_delete(IntPtr subscriber);
	}

	class SessionNative
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void on_connected_delegate(IntPtr session, IntPtr userData);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void on_disconnected_delegate(IntPtr session, IntPtr user_data);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void on_stream_received_delegate(IntPtr session, IntPtr user_data, IntPtr stream);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void on_stream_dropped_delegate(IntPtr session, IntPtr user_data, IntPtr stream);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void on_error_delegate(IntPtr session, IntPtr user_data, int error_code);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void on_signal_received_delegate(IntPtr session,
			IntPtr user_data,
			string type,
			string signal,
			IntPtr connection);

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct otc_session_cb
		{
			public IntPtr userData;
			public on_connected_delegate on_connected;
			public on_disconnected_delegate on_disconnected;
			public on_stream_received_delegate on_stream_received;
			public on_stream_dropped_delegate on_stream_dropped;
			public on_error_delegate on_error;
			public on_signal_received_delegate on_signal_received;
		};

		[DllImport("__Internal", EntryPoint = "otc_session_new", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr otc_session_new(string apiKey, string sessionId, ref otc_session_cb callbacks);

		[DllImport("__Internal", EntryPoint = "otc_session_connect", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_session_connect(IntPtr session, string token);

		[DllImport("__Internal", EntryPoint = "otc_session_subscribe", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_session_subscribe(IntPtr session, IntPtr subscribler);

		[DllImport("__Internal", EntryPoint = "otc_session_disconnect", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_session_disconnect(IntPtr session);

		[DllImport("__Internal", EntryPoint = "otc_session_delete", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_session_delete(IntPtr session);

		[DllImport("__Internal", EntryPoint = "otc_session_publish", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_session_publish(IntPtr session, IntPtr publisher);

		[DllImport("__Internal", EntryPoint = "otc_session_unpublish", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_session_unpublish(IntPtr session, IntPtr publisher);
	}

	public class Base
	{		
		public enum otc_constants {
			OTC_SUCCESS = 0,
			OTC_FALSE = 0,
			OTC_TRUE = 1,
			OTC_ERR_INVALID_PARAM = 1,
			OTC_ERR_FATAL = 2
		};

		[DllImport("__Internal", EntryPoint = "otc_init")]
		public static extern int otc_init(IntPtr context);        

		[DllImport("__Internal", EntryPoint = "otc_destroy", CallingConvention = CallingConvention.Cdecl)]
		public static extern int otc_destroy();        

		[DllImport("__Internal", EntryPoint = "otc_log_enable", CallingConvention = CallingConvention.Cdecl)]
		public static extern void otc_log_enable(bool enable);        
	}
}

