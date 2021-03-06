﻿using System;
using System.Drawing;
using ObjCRuntime;
using Foundation;
using UIKit;
using AVFoundation;

namespace OpenTok.Binding.Ios
{
    [BaseType (typeof (NSObject)), Protocol]
    public partial interface OTConnection {

        [Export ("connectionId")]
        string ConnectionId { get; }

        [Export ("creationTime")]
        NSDate CreationTime { get; }

        [Export ("data")]
        string Data { get; }
    }

    [BaseType (typeof (NSObject), Delegates=new string [] {"Delegate"}, Events=new Type [] { typeof (OTSessionDelegate) }), Protocol]
    public partial interface OTSession {

        [Export ("sessionConnectionStatus")]
        OTSessionConnectionStatus SessionConnectionStatus { get; }

        [Export ("sessionId", ArgumentSemantic.Copy)]
        string SessionId { get; set; }

        [Export ("streams")]
        NSDictionary Streams { get; }

        [Export ("connection")]
        OTConnection Connection { get; }

        [Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
        OTSessionDelegate Delegate { get; set; }

        [Export ("initWithApiKey:sessionId:delegate:")]
        IntPtr Constructor (string apiKey, string sessionId, [NullAllowed] OTSessionDelegate SessionDelegate);

        [Export ("connectWithToken:error:")]
        void ConnectWithToken (string token, out OTError error);

        [Export ("disconnect")]
        void Disconnect ();

        [Export ("publish:error:")]
        void Publish (OTPublisher publisher, out OTError error);

        [Export ("unpublish:error:")]
        void Unpublish (OTPublisher publisher, out OTError error);

        [Export ("subscribe:error:")]
        void Subscribe (OTSubscriber subscriber, out OTError error);

        [Export ("unsubscribe:error:")]
        void Unsubscribe (OTSubscriber subscriber, out OTError error);

        [Export ("signalWithType:string:connection:error:")]
        void SignalWithType (string type, string data, [NullAllowed] OTConnection connection, OTError error);
    }

    [Protocol, Model, BaseType (typeof (NSObject))]
    public partial interface OTSessionDelegate {

        [Export ("sessionDidConnect:"), EventArgs ("OTSessionDelegateSession")]
        void DidConnect (OTSession session);

        [Export ("sessionDidDisconnect:"), EventArgs ("OTSessionDelegateSession")]
        void DidDisconnect (OTSession session);

        [Export ("session:didFailWithError:"), EventArgs ("OTSessionDelegateError")]
        void DidFailWithError (OTSession session, OTError error);

        [Export ("session:streamCreated:"), EventArgs ("OTSessionDelegateStream")]
        void StreamCreated (OTSession session, OTStream stream);

        [Export ("session:streamDestroyed:"), EventArgs ("OTSessionDelegateStream")]
        void StreamDestroyed (OTSession session, OTStream stream);

        [Export ("session:connectionCreated:"), EventArgs ("OTSessionDelegateConnection")]
        void ConnectionCreated (OTSession session, OTConnection connection);

        [Export ("session:connectionDestroyed:"), EventArgs ("OTSessionDelegateConnection")]
        void ConnectionDestroyed (OTSession session, OTConnection connection);

        [Export ("session:receivedSignalType:fromConnection:withString:"), EventArgs("OTSessionDelegateSignal")]
        void ReceivedSignalType (OTSession session, string type, OTConnection connection, string data);
    }

    [BaseType (typeof (NSObject), Delegates=new string [] {"Delegate"}, Events=new Type [] { typeof (OTPublisherKitDelegate) }), Protocol]
    public partial interface OTPublisher {

        [Export ("initWithDelegate:")]
        IntPtr Constructor ([NullAllowed] OTPublisherKitDelegate publisherDelegate);

        [Export ("initWithDelegate:name:")]
        IntPtr Constructor ([NullAllowed] OTPublisherKitDelegate publisherDelegate, string name);

        [Export ("delegate", ArgumentSemantic.Assign)] [NullAllowed]
        OTPublisherKitDelegate Delegate { get; set; }

        [Export ("session")]
        OTSession Session { get; }

        [Export ("view")]
        UIView View { get; }

        [Export ("name", ArgumentSemantic.Copy)]
        string Name { get; set; }

        [Export ("publishAudio")]
        bool PublishAudio { get; set; }

        [Export ("publishVideo")]
        bool PublishVideo { get; set; }

        [Export ("cameraPosition")]
        AVCaptureDevicePosition CameraPosition { get; set; }
    }

    [Protocol, Model, BaseType (typeof (NSObject))]
    public partial interface OTPublisherKitDelegate {

        [Export ("publisher:didFailWithError:"), EventArgs ("OTPublisherDelegateError")]
        void DidFailWithError (OTPublisher publisher, OTError error);

        [Export ("publisher:streamCreated:"), EventArgs ("OTPublisherDelegatePublisher")]
        void StreamCreated (OTPublisher publisher, OTStream stream);

        [Export ("publisher:streamDestroyed:"), EventArgs ("OTPublisherDelegatePublisher")]
        void StreamDestroyed (OTPublisher publisher, OTStream stream);

        [Export ("publisher:didChangeCameraPosition:"), EventArgs ("OTPublisherDelegatePosition")]
        void DidChangeCameraPosition (OTPublisher publisher, AVCaptureDevicePosition position);
    }

    [BaseType (typeof (NSObject), Delegates=new string [] {"Delegate"}, Events=new Type [] { typeof (OTSubscriberKitDelegate) }), Protocol]
    public partial interface OTSubscriber {

        [Export ("session")]
        OTSession Session { get; }

        [Export ("stream")]
        OTStream Stream { get; }

        [Export ("view")]
        UIView View { get; }

        [Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
        OTSubscriberKitDelegate Delegate { get; set; }

        [Export ("subscribeToAudio")]
        bool SubscribeToAudio { get; set; }

        [Export ("subscribeToVideo")]
        bool SubscribeToVideo { get; set; }

        [Export ("initWithStream:delegate:")]
        IntPtr Constructor (OTStream stream, [NullAllowed] OTSubscriberKitDelegate subscriberDelegate);

        [Export ("close")]
        void Close ();
    }

    [Protocol, Model, BaseType (typeof (NSObject))]
    public partial interface OTSubscriberKitDelegate {

        [Export ("subscriberDidConnectToStream:")]
        void DidConnectToStream (OTSubscriber subscriber);

        [Export ("subscriber:didFailWithError:"), EventArgs ("OTSubscriberDelegateError")]
        void DidFailWithError (OTSubscriber subscriber, OTError error);

        [Export ("subscriberVideoDataReceived:"), EventArgs ("OTSubscriberDelegateSubscriber")]
        void VideoDataReceived (OTSubscriber subscriber);

        [Export ("stream:didChangeVideoDimensions:"), EventArgs ("OTSubscriberDelegateDimensions")]
        void DidChangeVideoDimensions (OTStream stream, SizeF dimensions);

        [Export ("subscriberVideoDisabled:"), EventArgs ("OTSubscriberDelegateSubscriber")]
        void VideoDisabled (OTSubscriber subscriber);
    }

    [BaseType (typeof (NSObject)), Protocol]
    public partial interface OTStream {

        [Export ("connection", ArgumentSemantic.Retain)]
        OTConnection Connection { get; }

        [Export ("session")]
        OTSession Session { get; }

        [Export ("streamId", ArgumentSemantic.Retain)]
        string StreamId { get; }

        [Export ("creationTime", ArgumentSemantic.Retain)]
        NSDate CreationTime { get; }

        [Export ("name")]
        string Name { get; }

        [Export ("hasAudio")]
        bool HasAudio { get; }

        [Export ("hasVideo")]
        bool HasVideo { get; }

        [Export ("videoDimensions")]
        SizeF VideoDimensions { get; }
    }

    [BaseType (typeof (NSError)), Protocol]
    public partial interface OTError {

    }
}