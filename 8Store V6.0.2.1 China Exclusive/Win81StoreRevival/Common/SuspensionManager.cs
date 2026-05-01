using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Win81StoreRevival.Common
{
	// Token: 0x02000057 RID: 87
	internal sealed class SuspensionManager
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0001BD88 File Offset: 0x00019F88
		public static Dictionary<string, object> SessionState
		{
			get
			{
				return SuspensionManager._sessionState;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0001BDA0 File Offset: 0x00019FA0
		public static List<Type> KnownTypes
		{
			get
			{
				return SuspensionManager._knownTypes;
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001BDB8 File Offset: 0x00019FB8
		[DebuggerStepThrough]
		public static Task SaveAsync()
		{
			SuspensionManager.<SaveAsync>d__7 <SaveAsync>d__ = new SuspensionManager.<SaveAsync>d__7();
			<SaveAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SaveAsync>d__.<>t__builder;
			<>t__builder.Start<SuspensionManager.<SaveAsync>d__7>(ref <SaveAsync>d__);
			return <SaveAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001BDF8 File Offset: 0x00019FF8
		[DebuggerStepThrough]
		public static Task RestoreAsync(string sessionBaseKey = null)
		{
			SuspensionManager.<RestoreAsync>d__8 <RestoreAsync>d__ = new SuspensionManager.<RestoreAsync>d__8();
			<RestoreAsync>d__.sessionBaseKey = sessionBaseKey;
			<RestoreAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RestoreAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RestoreAsync>d__.<>t__builder;
			<>t__builder.Start<SuspensionManager.<RestoreAsync>d__8>(ref <RestoreAsync>d__);
			return <RestoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001BE40 File Offset: 0x0001A040
		public static void RegisterFrame(Frame frame, string sessionStateKey, string sessionBaseKey = null)
		{
			bool flag = frame.GetValue(SuspensionManager.FrameSessionStateKeyProperty) != null;
			if (flag)
			{
				throw new InvalidOperationException("Frames can only be registered to one session state key");
			}
			bool flag2 = frame.GetValue(SuspensionManager.FrameSessionStateProperty) != null;
			if (flag2)
			{
				throw new InvalidOperationException("Frames must be either be registered before accessing frame session state, or not registered at all");
			}
			bool flag3 = !string.IsNullOrEmpty(sessionBaseKey);
			if (flag3)
			{
				frame.SetValue(SuspensionManager.FrameSessionBaseKeyProperty, sessionBaseKey);
				sessionStateKey = sessionBaseKey + "_" + sessionStateKey;
			}
			frame.SetValue(SuspensionManager.FrameSessionStateKeyProperty, sessionStateKey);
			SuspensionManager._registeredFrames.Add(new WeakReference<Frame>(frame));
			SuspensionManager.RestoreFrameNavigationState(frame);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001BEDC File Offset: 0x0001A0DC
		public static void UnregisterFrame(Frame frame)
		{
			SuspensionManager.SessionState.Remove((string)frame.GetValue(SuspensionManager.FrameSessionStateKeyProperty));
			SuspensionManager._registeredFrames.RemoveAll(delegate(WeakReference<Frame> weakFrameReference)
			{
				Frame frame2;
				return !weakFrameReference.TryGetTarget(ref frame2) || frame2 == frame;
			});
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001BF30 File Offset: 0x0001A130
		public static Dictionary<string, object> SessionStateForFrame(Frame frame)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)frame.GetValue(SuspensionManager.FrameSessionStateProperty);
			bool flag = dictionary == null;
			if (flag)
			{
				string text = (string)frame.GetValue(SuspensionManager.FrameSessionStateKeyProperty);
				bool flag2 = text != null;
				if (flag2)
				{
					bool flag3 = !SuspensionManager._sessionState.ContainsKey(text);
					if (flag3)
					{
						SuspensionManager._sessionState[text] = new Dictionary<string, object>();
					}
					dictionary = (Dictionary<string, object>)SuspensionManager._sessionState[text];
				}
				else
				{
					dictionary = new Dictionary<string, object>();
				}
				frame.SetValue(SuspensionManager.FrameSessionStateProperty, dictionary);
			}
			return dictionary;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001BFCC File Offset: 0x0001A1CC
		private static void RestoreFrameNavigationState(Frame frame)
		{
			Dictionary<string, object> dictionary = SuspensionManager.SessionStateForFrame(frame);
			bool flag = dictionary.ContainsKey("Navigation");
			if (flag)
			{
				frame.SetNavigationState((string)dictionary["Navigation"]);
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001C00C File Offset: 0x0001A20C
		private static void SaveFrameNavigationState(Frame frame)
		{
			Dictionary<string, object> dictionary = SuspensionManager.SessionStateForFrame(frame);
			dictionary["Navigation"] = frame.GetNavigationState();
		}

		// Token: 0x0400022F RID: 559
		private static Dictionary<string, object> _sessionState = new Dictionary<string, object>();

		// Token: 0x04000230 RID: 560
		private static List<Type> _knownTypes = new List<Type>();

		// Token: 0x04000231 RID: 561
		private const string sessionStateFilename = "_sessionState.xml";

		// Token: 0x04000232 RID: 562
		private static DependencyProperty FrameSessionStateKeyProperty = DependencyProperty.RegisterAttached("_FrameSessionStateKey", typeof(string), typeof(SuspensionManager), null);

		// Token: 0x04000233 RID: 563
		private static DependencyProperty FrameSessionBaseKeyProperty = DependencyProperty.RegisterAttached("_FrameSessionBaseKeyParams", typeof(string), typeof(SuspensionManager), null);

		// Token: 0x04000234 RID: 564
		private static DependencyProperty FrameSessionStateProperty = DependencyProperty.RegisterAttached("_FrameSessionState", typeof(Dictionary<string, object>), typeof(SuspensionManager), null);

		// Token: 0x04000235 RID: 565
		private static List<WeakReference<Frame>> _registeredFrames = new List<WeakReference<Frame>>();
	}
}
