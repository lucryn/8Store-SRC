using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.UI.Notifications;

namespace Win81StoreRevival
{
	// Token: 0x02000026 RID: 38
	public static class DownloadManager
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000213 RID: 531 RVA: 0x0000C550 File Offset: 0x0000A750
		// (remove) Token: 0x06000214 RID: 532 RVA: 0x0000C584 File Offset: 0x0000A784
		[DebuggerBrowsable(0)]
		public static event EventHandler<DownloadManager.DownloadProgressEventArgs> DownloadProgress;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000215 RID: 533 RVA: 0x0000C5B8 File Offset: 0x0000A7B8
		// (remove) Token: 0x06000216 RID: 534 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		[DebuggerBrowsable(0)]
		public static event EventHandler<DownloadManager.DownloadCompletedEventArgs> DownloadCompleted;

		// Token: 0x06000217 RID: 535 RVA: 0x0000C620 File Offset: 0x0000A820
		[DebuggerStepThrough]
		public static Task<DownloadItem> StartDownloadWithFolderPickerAsync(string downloadUrl, string appName, string iconUrl)
		{
			DownloadManager.<StartDownloadWithFolderPickerAsync>d__11 <StartDownloadWithFolderPickerAsync>d__ = new DownloadManager.<StartDownloadWithFolderPickerAsync>d__11();
			<StartDownloadWithFolderPickerAsync>d__.downloadUrl = downloadUrl;
			<StartDownloadWithFolderPickerAsync>d__.appName = appName;
			<StartDownloadWithFolderPickerAsync>d__.iconUrl = iconUrl;
			<StartDownloadWithFolderPickerAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DownloadItem>.Create();
			<StartDownloadWithFolderPickerAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<DownloadItem> <>t__builder = <StartDownloadWithFolderPickerAsync>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<StartDownloadWithFolderPickerAsync>d__11>(ref <StartDownloadWithFolderPickerAsync>d__);
			return <StartDownloadWithFolderPickerAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000C678 File Offset: 0x0000A878
		[DebuggerStepThrough]
		public static void HandleLaunch(string args)
		{
			DownloadManager.<HandleLaunch>d__12 <HandleLaunch>d__ = new DownloadManager.<HandleLaunch>d__12();
			<HandleLaunch>d__.args = args;
			<HandleLaunch>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<HandleLaunch>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <HandleLaunch>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<HandleLaunch>d__12>(ref <HandleLaunch>d__);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		[DebuggerStepThrough]
		private static void StartDownloadProcessing(DownloadItem item, string iconUrl, string appName)
		{
			DownloadManager.<StartDownloadProcessing>d__13 <StartDownloadProcessing>d__ = new DownloadManager.<StartDownloadProcessing>d__13();
			<StartDownloadProcessing>d__.item = item;
			<StartDownloadProcessing>d__.iconUrl = iconUrl;
			<StartDownloadProcessing>d__.appName = appName;
			<StartDownloadProcessing>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<StartDownloadProcessing>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <StartDownloadProcessing>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<StartDownloadProcessing>d__13>(ref <StartDownloadProcessing>d__);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C700 File Offset: 0x0000A900
		[DebuggerStepThrough]
		private static Task<bool> AutoSideloadPackage(DownloadItem item)
		{
			DownloadManager.<AutoSideloadPackage>d__14 <AutoSideloadPackage>d__ = new DownloadManager.<AutoSideloadPackage>d__14();
			<AutoSideloadPackage>d__.item = item;
			<AutoSideloadPackage>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<AutoSideloadPackage>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <AutoSideloadPackage>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<AutoSideloadPackage>d__14>(ref <AutoSideloadPackage>d__);
			return <AutoSideloadPackage>d__.<>t__builder.Task;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000C748 File Offset: 0x0000A948
		[DebuggerStepThrough]
		public static Task<bool> InstallCertificate(StorageFile certFile)
		{
			DownloadManager.<InstallCertificate>d__15 <InstallCertificate>d__ = new DownloadManager.<InstallCertificate>d__15();
			<InstallCertificate>d__.certFile = certFile;
			<InstallCertificate>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<InstallCertificate>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <InstallCertificate>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<InstallCertificate>d__15>(ref <InstallCertificate>d__);
			return <InstallCertificate>d__.<>t__builder.Task;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000C790 File Offset: 0x0000A990
		[DebuggerStepThrough]
		public static Task<bool> SideloadAppx(StorageFile appxFile)
		{
			DownloadManager.<SideloadAppx>d__16 <SideloadAppx>d__ = new DownloadManager.<SideloadAppx>d__16();
			<SideloadAppx>d__.appxFile = appxFile;
			<SideloadAppx>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<SideloadAppx>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <SideloadAppx>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<SideloadAppx>d__16>(ref <SideloadAppx>d__);
			return <SideloadAppx>d__.<>t__builder.Task;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000C7D8 File Offset: 0x0000A9D8
		[DebuggerStepThrough]
		public static Task SaveDownloadedApp(StoreApp app)
		{
			DownloadManager.<SaveDownloadedApp>d__17 <SaveDownloadedApp>d__ = new DownloadManager.<SaveDownloadedApp>d__17();
			<SaveDownloadedApp>d__.app = app;
			<SaveDownloadedApp>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveDownloadedApp>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SaveDownloadedApp>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<SaveDownloadedApp>d__17>(ref <SaveDownloadedApp>d__);
			return <SaveDownloadedApp>d__.<>t__builder.Task;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000C820 File Offset: 0x0000AA20
		[DebuggerStepThrough]
		public static Task UpdateInstalledAppStatus(string appName, bool isInstalled)
		{
			DownloadManager.<UpdateInstalledAppStatus>d__18 <UpdateInstalledAppStatus>d__ = new DownloadManager.<UpdateInstalledAppStatus>d__18();
			<UpdateInstalledAppStatus>d__.appName = appName;
			<UpdateInstalledAppStatus>d__.isInstalled = isInstalled;
			<UpdateInstalledAppStatus>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateInstalledAppStatus>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateInstalledAppStatus>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<UpdateInstalledAppStatus>d__18>(ref <UpdateInstalledAppStatus>d__);
			return <UpdateInstalledAppStatus>d__.<>t__builder.Task;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000C870 File Offset: 0x0000AA70
		private static void UpdateDownloadProgress(DownloadItem item, BackgroundDownloadProgress progress)
		{
			double num = 0.0;
			bool flag = progress.TotalBytesToReceive > 0UL;
			if (flag)
			{
				num = progress.BytesReceived * 100.0 / progress.TotalBytesToReceive;
			}
			item.Progress = num;
			item.BytesReceived = progress.BytesReceived;
			item.TotalBytes = progress.TotalBytesToReceive;
			item.Status = DownloadManager.GetStatusText(progress.Status, num);
			EventHandler<DownloadManager.DownloadProgressEventArgs> downloadProgress = DownloadManager.DownloadProgress;
			if (downloadProgress != null)
			{
				downloadProgress.Invoke(null, new DownloadManager.DownloadProgressEventArgs
				{
					DownloadItem = item
				});
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000C908 File Offset: 0x0000AB08
		private static string GetStatusText(BackgroundTransferStatus status, double percent)
		{
			string result;
			switch (status)
			{
			case 1:
				result = "Downloading... " + percent.ToString("F1") + "%";
				break;
			case 2:
				result = "Paused";
				break;
			case 3:
				result = "Paused (metered network)";
				break;
			case 4:
				result = "Waiting for network...";
				break;
			case 5:
				result = "Completed";
				break;
			case 6:
				result = "Cancelled";
				break;
			case 7:
				result = "Error";
				break;
			default:
				result = "Unknown";
				break;
			}
			return result;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000C998 File Offset: 0x0000AB98
		private static void ShowToastNotification(string title, string content, string iconUrl)
		{
			try
			{
				ToastTemplateType toastTemplateType = 2;
				XmlDocument templateContent = ToastNotificationManager.GetTemplateContent(toastTemplateType);
				XmlNodeList elementsByTagName = templateContent.GetElementsByTagName("text");
				bool flag = elementsByTagName.Count >= 2;
				if (flag)
				{
					elementsByTagName[0].put_InnerText(title);
					elementsByTagName[1].put_InnerText(content);
				}
				bool flag2 = !string.IsNullOrEmpty(iconUrl);
				if (flag2)
				{
					XmlNodeList elementsByTagName2 = templateContent.GetElementsByTagName("image");
					bool flag3 = elementsByTagName2.Count > 0;
					if (flag3)
					{
						((XmlElement)elementsByTagName2[0]).SetAttribute("src", iconUrl);
					}
				}
				ToastNotification toastNotification = new ToastNotification(templateContent);
				ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[Toast] Error: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000CA84 File Offset: 0x0000AC84
		public static List<DownloadItem> GetActiveDownloads()
		{
			return new List<DownloadItem>(DownloadManager.activeDownloads);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000CAA0 File Offset: 0x0000ACA0
		public static List<DownloadItem> GetCompletedDownloads()
		{
			return new List<DownloadItem>(DownloadManager.completedDownloads);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000CABC File Offset: 0x0000ACBC
		[DebuggerStepThrough]
		private static Task RunOnUIThread(Action action)
		{
			DownloadManager.<RunOnUIThread>d__24 <RunOnUIThread>d__ = new DownloadManager.<RunOnUIThread>d__24();
			<RunOnUIThread>d__.action = action;
			<RunOnUIThread>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RunOnUIThread>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RunOnUIThread>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<RunOnUIThread>d__24>(ref <RunOnUIThread>d__);
			return <RunOnUIThread>d__.<>t__builder.Task;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000CB04 File Offset: 0x0000AD04
		public static void CancelDownload(string fileName)
		{
			bool flag = DownloadManager.downloadOperations.ContainsKey(fileName);
			if (flag)
			{
				DownloadManager.downloadOperations[fileName].AttachAsync().Cancel();
				DownloadManager.downloadOperations.Remove(fileName);
				DownloadItem downloadItem = Enumerable.FirstOrDefault<DownloadItem>(DownloadManager.activeDownloads, (DownloadItem item) => item.FileName == fileName);
				bool flag2 = downloadItem != null;
				if (flag2)
				{
					DownloadManager.activeDownloads.Remove(downloadItem);
				}
			}
		}

		// Token: 0x0400010C RID: 268
		private static List<DownloadItem> activeDownloads = new List<DownloadItem>();

		// Token: 0x0400010D RID: 269
		private static List<DownloadItem> completedDownloads = new List<DownloadItem>();

		// Token: 0x0400010E RID: 270
		private static Dictionary<string, DownloadOperation> downloadOperations = new Dictionary<string, DownloadOperation>();

		// Token: 0x020000B9 RID: 185
		public class DownloadProgressEventArgs : EventArgs
		{
			// Token: 0x170000FD RID: 253
			// (get) Token: 0x06000649 RID: 1609 RVA: 0x0002849C File Offset: 0x0002669C
			// (set) Token: 0x0600064A RID: 1610 RVA: 0x000284A4 File Offset: 0x000266A4
			public DownloadItem DownloadItem { get; set; }
		}

		// Token: 0x020000BA RID: 186
		public class DownloadCompletedEventArgs : EventArgs
		{
			// Token: 0x170000FE RID: 254
			// (get) Token: 0x0600064C RID: 1612 RVA: 0x000284B6 File Offset: 0x000266B6
			// (set) Token: 0x0600064D RID: 1613 RVA: 0x000284BE File Offset: 0x000266BE
			public DownloadItem DownloadItem { get; set; }

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x0600064E RID: 1614 RVA: 0x000284C7 File Offset: 0x000266C7
			// (set) Token: 0x0600064F RID: 1615 RVA: 0x000284CF File Offset: 0x000266CF
			public bool Success { get; set; }

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x06000650 RID: 1616 RVA: 0x000284D8 File Offset: 0x000266D8
			// (set) Token: 0x06000651 RID: 1617 RVA: 0x000284E0 File Offset: 0x000266E0
			public string Error { get; set; }
		}
	}
}
