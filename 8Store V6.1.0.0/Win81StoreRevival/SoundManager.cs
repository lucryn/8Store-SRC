using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Win81StoreRevival
{
	// Token: 0x02000043 RID: 67
	public static class SoundManager
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x00016E49 File Offset: 0x00015049
		public static bool IsSoundEnabled()
		{
			return !SoundManager.localSettings.Values.ContainsKey("SoundEffectsEnabled") || (bool)SoundManager.localSettings.Values["SoundEffectsEnabled"];
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00016E7C File Offset: 0x0001507C
		public static void PlaySound(string soundFileName)
		{
			try
			{
				if (SoundManager.IsSoundEnabled())
				{
					MediaElement soundPlayer = new MediaElement();
					soundPlayer.put_AutoPlay(true);
					soundPlayer.put_Volume(0.5);
					soundPlayer.put_Visibility(1);
					UIElement content = Window.Current.Content;
					if (content is Frame)
					{
						Frame frame = (Frame)content;
						if (frame.Content is Page)
						{
							Grid grid = ((Page)frame.Content).Content as Grid;
							if (grid != null)
							{
								grid.Children.Add(soundPlayer);
							}
						}
					}
					soundPlayer.put_Source(new Uri(string.Format("ms-appx:///Assets/{0}", new object[]
					{
						soundFileName
					})));
					MediaElement soundPlayer2 = soundPlayer;
					WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(soundPlayer2.add_MediaEnded), new Action<EventRegistrationToken>(soundPlayer2.remove_MediaEnded), delegate(object sender, RoutedEventArgs e)
					{
						SoundManager.CleanupSoundPlayer(soundPlayer);
					});
					soundPlayer2 = soundPlayer;
					WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(soundPlayer2.add_MediaFailed), new Action<EventRegistrationToken>(soundPlayer2.remove_MediaFailed), delegate(object sender, ExceptionRoutedEventArgs e)
					{
						SoundManager.CleanupSoundPlayer(soundPlayer);
					});
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00016FD4 File Offset: 0x000151D4
		private static void CleanupSoundPlayer(MediaElement soundPlayer)
		{
			try
			{
				UIElement content = Window.Current.Content;
				if (content is Frame)
				{
					Frame frame = (Frame)content;
					if (frame.Content is Page)
					{
						Grid grid = ((Page)frame.Content).Content as Grid;
						if (grid != null && grid.Children.Contains(soundPlayer))
						{
							grid.Children.Remove(soundPlayer);
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00017054 File Offset: 0x00015254
		public static void PlayUpdateSound()
		{
			SoundManager.PlaySound("exclaim.wav");
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00017060 File Offset: 0x00015260
		public static void PlayDownloadSound()
		{
			SoundManager.PlaySound("notify1.wav");
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001706C File Offset: 0x0001526C
		public static void PlayStartupSound()
		{
			SoundManager.PlaySound("startup.wav");
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00017060 File Offset: 0x00015260
		public static void PlayNotificationSound()
		{
			SoundManager.PlaySound("notify1.wav");
		}

		// Token: 0x040001FC RID: 508
		private static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
	}
}
