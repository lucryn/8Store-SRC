using System;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Win81StoreRevival
{
	// Token: 0x0200003D RID: 61
	public static class SoundManager
	{
		// Token: 0x060003AA RID: 938 RVA: 0x000161C8 File Offset: 0x000143C8
		public static bool IsSoundEnabled()
		{
			bool flag = SoundManager.localSettings.Values.ContainsKey("SoundEffectsEnabled");
			return !flag || (bool)SoundManager.localSettings.Values["SoundEffectsEnabled"];
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00016210 File Offset: 0x00014410
		public static void PlaySound(string soundFileName)
		{
			try
			{
				bool flag = !SoundManager.IsSoundEnabled();
				if (flag)
				{
					Debug.WriteLine("Sound effects are disabled");
				}
				else
				{
					Debug.WriteLine(string.Format("Playing sound: {0}", new object[]
					{
						soundFileName
					}));
					MediaElement soundPlayer = new MediaElement();
					soundPlayer.put_AutoPlay(true);
					soundPlayer.put_Volume(0.5);
					soundPlayer.put_Visibility(1);
					UIElement content = Window.Current.Content;
					bool flag2 = content is Frame;
					if (flag2)
					{
						Frame frame = (Frame)content;
						bool flag3 = frame.Content is Page;
						if (flag3)
						{
							Page page = (Page)frame.Content;
							Grid grid = page.Content as Grid;
							bool flag4 = grid != null;
							if (flag4)
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
						Debug.WriteLine("Sound playback completed");
						SoundManager.CleanupSoundPlayer(soundPlayer);
					});
					soundPlayer2 = soundPlayer;
					WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(soundPlayer2.add_MediaFailed), new Action<EventRegistrationToken>(soundPlayer2.remove_MediaFailed), delegate(object sender, ExceptionRoutedEventArgs e)
					{
						Debug.WriteLine(string.Format("Sound playback failed: {0}", new object[]
						{
							e.ErrorMessage
						}));
						SoundManager.CleanupSoundPlayer(soundPlayer);
					});
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error playing sound {0}: {1}", new object[]
				{
					soundFileName,
					ex.Message
				}));
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000163E8 File Offset: 0x000145E8
		private static void CleanupSoundPlayer(MediaElement soundPlayer)
		{
			try
			{
				UIElement content = Window.Current.Content;
				bool flag = content is Frame;
				if (flag)
				{
					Frame frame = (Frame)content;
					bool flag2 = frame.Content is Page;
					if (flag2)
					{
						Page page = (Page)frame.Content;
						Grid grid = page.Content as Grid;
						bool flag3 = grid != null && grid.Children.Contains(soundPlayer);
						if (flag3)
						{
							grid.Children.Remove(soundPlayer);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error cleaning up sound player: " + ex.Message);
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000164A4 File Offset: 0x000146A4
		public static void PlayUpdateSound()
		{
			SoundManager.PlaySound("exclaim.wav");
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000164B2 File Offset: 0x000146B2
		public static void PlayDownloadSound()
		{
			SoundManager.PlaySound("notify1.wav");
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000164C0 File Offset: 0x000146C0
		public static void PlayStartupSound()
		{
			SoundManager.PlaySound("startup.wav");
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000164B2 File Offset: 0x000146B2
		public static void PlayNotificationSound()
		{
			SoundManager.PlaySound("notify1.wav");
		}

		// Token: 0x040001BD RID: 445
		private static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
	}
}
