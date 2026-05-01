using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Win81StoreRevival.Common
{
	// Token: 0x02000063 RID: 99
	internal sealed class SuspensionManager
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0001D8B3 File Offset: 0x0001BAB3
		public static Dictionary<string, object> SessionState
		{
			get
			{
				return SuspensionManager._sessionState;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0001D8BA File Offset: 0x0001BABA
		public static List<Type> KnownTypes
		{
			get
			{
				return SuspensionManager._knownTypes;
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001D8C4 File Offset: 0x0001BAC4
		public static Task SaveAsync()
		{
			SuspensionManager.<SaveAsync>d__7 <SaveAsync>d__;
			<SaveAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SaveAsync>d__.<>t__builder;
			<>t__builder.Start<SuspensionManager.<SaveAsync>d__7>(ref <SaveAsync>d__);
			return <SaveAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001D904 File Offset: 0x0001BB04
		public static Task RestoreAsync(string sessionBaseKey = null)
		{
			SuspensionManager.<RestoreAsync>d__8 <RestoreAsync>d__;
			<RestoreAsync>d__.sessionBaseKey = sessionBaseKey;
			<RestoreAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RestoreAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RestoreAsync>d__.<>t__builder;
			<>t__builder.Start<SuspensionManager.<RestoreAsync>d__8>(ref <RestoreAsync>d__);
			return <RestoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001D94C File Offset: 0x0001BB4C
		public static void RegisterFrame(Frame frame, string sessionStateKey, string sessionBaseKey = null)
		{
			if (frame.GetValue(SuspensionManager.FrameSessionStateKeyProperty) != null)
			{
				throw new InvalidOperationException("Frames can only be registered to one session state key");
			}
			if (frame.GetValue(SuspensionManager.FrameSessionStateProperty) != null)
			{
				throw new InvalidOperationException("Frames must be either be registered before accessing frame session state, or not registered at all");
			}
			if (!string.IsNullOrEmpty(sessionBaseKey))
			{
				frame.SetValue(SuspensionManager.FrameSessionBaseKeyProperty, sessionBaseKey);
				sessionStateKey = sessionBaseKey + "_" + sessionStateKey;
			}
			frame.SetValue(SuspensionManager.FrameSessionStateKeyProperty, sessionStateKey);
			SuspensionManager._registeredFrames.Add(new WeakReference<Frame>(frame));
			SuspensionManager.RestoreFrameNavigationState(frame);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001D9D0 File Offset: 0x0001BBD0
		public static void UnregisterFrame(Frame frame)
		{
			SuspensionManager.SessionState.Remove((string)frame.GetValue(SuspensionManager.FrameSessionStateKeyProperty));
			SuspensionManager._registeredFrames.RemoveAll(delegate(WeakReference<Frame> weakFrameReference)
			{
				Frame frame2;
				return !weakFrameReference.TryGetTarget(ref frame2) || frame2 == frame;
			});
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001DA24 File Offset: 0x0001BC24
		public static Dictionary<string, object> SessionStateForFrame(Frame frame)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)frame.GetValue(SuspensionManager.FrameSessionStateProperty);
			if (dictionary == null)
			{
				string text = (string)frame.GetValue(SuspensionManager.FrameSessionStateKeyProperty);
				if (text != null)
				{
					if (!SuspensionManager._sessionState.ContainsKey(text))
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

		// Token: 0x06000652 RID: 1618 RVA: 0x0001DA9C File Offset: 0x0001BC9C
		private static void RestoreFrameNavigationState(Frame frame)
		{
			Dictionary<string, object> dictionary = SuspensionManager.SessionStateForFrame(frame);
			if (dictionary.ContainsKey("Navigation"))
			{
				frame.SetNavigationState((string)dictionary["Navigation"]);
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001DAD3 File Offset: 0x0001BCD3
		private static void SaveFrameNavigationState(Frame frame)
		{
			SuspensionManager.SessionStateForFrame(frame)["Navigation"] = frame.GetNavigationState();
		}

		// Token: 0x04000266 RID: 614
		private static Dictionary<string, object> _sessionState = new Dictionary<string, object>();

		// Token: 0x04000267 RID: 615
		private static List<Type> _knownTypes = new List<Type>();

		// Token: 0x04000268 RID: 616
		private const string sessionStateFilename = "_sessionState.xml";

		// Token: 0x04000269 RID: 617
		private static DependencyProperty FrameSessionStateKeyProperty = DependencyProperty.RegisterAttached("_FrameSessionStateKey", typeof(string), typeof(SuspensionManager), null);

		// Token: 0x0400026A RID: 618
		private static DependencyProperty FrameSessionBaseKeyProperty = DependencyProperty.RegisterAttached("_FrameSessionBaseKeyParams", typeof(string), typeof(SuspensionManager), null);

		// Token: 0x0400026B RID: 619
		private static DependencyProperty FrameSessionStateProperty = DependencyProperty.RegisterAttached("_FrameSessionState", typeof(Dictionary<string, object>), typeof(SuspensionManager), null);

		// Token: 0x0400026C RID: 620
		private static List<WeakReference<Frame>> _registeredFrames = new List<WeakReference<Frame>>();
	}
}
