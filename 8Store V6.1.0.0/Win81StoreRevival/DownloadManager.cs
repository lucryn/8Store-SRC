using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.UI.Notifications;

namespace Win81StoreRevival
{
	// Token: 0x0200002A RID: 42
	public static class DownloadManager
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600029A RID: 666 RVA: 0x0000D758 File Offset: 0x0000B958
		// (remove) Token: 0x0600029B RID: 667 RVA: 0x0000D78C File Offset: 0x0000B98C
		public static event EventHandler<DownloadManager.DownloadProgressEventArgs> DownloadProgress;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600029C RID: 668 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		// (remove) Token: 0x0600029D RID: 669 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
		public static event EventHandler<DownloadManager.DownloadCompletedEventArgs> DownloadCompleted;

		// Token: 0x0600029E RID: 670 RVA: 0x0000D828 File Offset: 0x0000BA28
		private static Task<StorageFolder> GetLocalDownloadFolderAsync()
		{
			DownloadManager.<GetLocalDownloadFolderAsync>d__11 <GetLocalDownloadFolderAsync>d__;
			<GetLocalDownloadFolderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StorageFolder>.Create();
			<GetLocalDownloadFolderAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<StorageFolder> <>t__builder = <GetLocalDownloadFolderAsync>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<GetLocalDownloadFolderAsync>d__11>(ref <GetLocalDownloadFolderAsync>d__);
			return <GetLocalDownloadFolderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000D868 File Offset: 0x0000BA68
		private static Task<StorageFolder> GetPublicDownloadFolderAsync()
		{
			DownloadManager.<GetPublicDownloadFolderAsync>d__12 <GetPublicDownloadFolderAsync>d__;
			<GetPublicDownloadFolderAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StorageFolder>.Create();
			<GetPublicDownloadFolderAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<StorageFolder> <>t__builder = <GetPublicDownloadFolderAsync>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<GetPublicDownloadFolderAsync>d__12>(ref <GetPublicDownloadFolderAsync>d__);
			return <GetPublicDownloadFolderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		private static Task CopyDownloadedFilesToPublicFolderAsync(DownloadItem item)
		{
			DownloadManager.<CopyDownloadedFilesToPublicFolderAsync>d__13 <CopyDownloadedFilesToPublicFolderAsync>d__;
			<CopyDownloadedFilesToPublicFolderAsync>d__.item = item;
			<CopyDownloadedFilesToPublicFolderAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CopyDownloadedFilesToPublicFolderAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <CopyDownloadedFilesToPublicFolderAsync>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<CopyDownloadedFilesToPublicFolderAsync>d__13>(ref <CopyDownloadedFilesToPublicFolderAsync>d__);
			return <CopyDownloadedFilesToPublicFolderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000D8F0 File Offset: 0x0000BAF0
		public static Task<DownloadItem> StartDownloadWithFolderPickerAsync(string downloadUrl, string appId, string appName, string publisher, string version, string iconUrl)
		{
			DownloadManager.<StartDownloadWithFolderPickerAsync>d__14 <StartDownloadWithFolderPickerAsync>d__;
			<StartDownloadWithFolderPickerAsync>d__.downloadUrl = downloadUrl;
			<StartDownloadWithFolderPickerAsync>d__.appId = appId;
			<StartDownloadWithFolderPickerAsync>d__.appName = appName;
			<StartDownloadWithFolderPickerAsync>d__.publisher = publisher;
			<StartDownloadWithFolderPickerAsync>d__.version = version;
			<StartDownloadWithFolderPickerAsync>d__.iconUrl = iconUrl;
			<StartDownloadWithFolderPickerAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DownloadItem>.Create();
			<StartDownloadWithFolderPickerAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<DownloadItem> <>t__builder = <StartDownloadWithFolderPickerAsync>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<StartDownloadWithFolderPickerAsync>d__14>(ref <StartDownloadWithFolderPickerAsync>d__);
			return <StartDownloadWithFolderPickerAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000D960 File Offset: 0x0000BB60
		public static void HandleLaunch(string args)
		{
			DownloadManager.<HandleLaunch>d__15 <HandleLaunch>d__;
			<HandleLaunch>d__.args = args;
			<HandleLaunch>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<HandleLaunch>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <HandleLaunch>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<HandleLaunch>d__15>(ref <HandleLaunch>d__);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000D99C File Offset: 0x0000BB9C
		private static void StartDownloadProcessing(DownloadItem item, string iconUrl, string appName)
		{
			DownloadManager.<StartDownloadProcessing>d__16 <StartDownloadProcessing>d__;
			<StartDownloadProcessing>d__.item = item;
			<StartDownloadProcessing>d__.iconUrl = iconUrl;
			<StartDownloadProcessing>d__.appName = appName;
			<StartDownloadProcessing>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<StartDownloadProcessing>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <StartDownloadProcessing>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<StartDownloadProcessing>d__16>(ref <StartDownloadProcessing>d__);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
		private static Task<bool> AutoSideloadPackage(DownloadItem item)
		{
			DownloadManager.<AutoSideloadPackage>d__17 <AutoSideloadPackage>d__;
			<AutoSideloadPackage>d__.item = item;
			<AutoSideloadPackage>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<AutoSideloadPackage>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <AutoSideloadPackage>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<AutoSideloadPackage>d__17>(ref <AutoSideloadPackage>d__);
			return <AutoSideloadPackage>d__.<>t__builder.Task;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000DA30 File Offset: 0x0000BC30
		private static Task<StorageFile> PrepareInstallablePackageAsync(DownloadItem item)
		{
			DownloadManager.<PrepareInstallablePackageAsync>d__18 <PrepareInstallablePackageAsync>d__;
			<PrepareInstallablePackageAsync>d__.item = item;
			<PrepareInstallablePackageAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StorageFile>.Create();
			<PrepareInstallablePackageAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<StorageFile> <>t__builder = <PrepareInstallablePackageAsync>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<PrepareInstallablePackageAsync>d__18>(ref <PrepareInstallablePackageAsync>d__);
			return <PrepareInstallablePackageAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000DA78 File Offset: 0x0000BC78
		public static Task<bool> InstallDownloadedApp(DownloadItem item)
		{
			DownloadManager.<InstallDownloadedApp>d__19 <InstallDownloadedApp>d__;
			<InstallDownloadedApp>d__.item = item;
			<InstallDownloadedApp>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<InstallDownloadedApp>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <InstallDownloadedApp>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<InstallDownloadedApp>d__19>(ref <InstallDownloadedApp>d__);
			return <InstallDownloadedApp>d__.<>t__builder.Task;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000DAC0 File Offset: 0x0000BCC0
		public static Task<bool> InstallCertificate(StorageFile certFile)
		{
			DownloadManager.<InstallCertificate>d__20 <InstallCertificate>d__;
			<InstallCertificate>d__.certFile = certFile;
			<InstallCertificate>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<InstallCertificate>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <InstallCertificate>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<InstallCertificate>d__20>(ref <InstallCertificate>d__);
			return <InstallCertificate>d__.<>t__builder.Task;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000DB08 File Offset: 0x0000BD08
		public static Task<bool> SideloadAppx(StorageFile appxFile)
		{
			DownloadManager.<SideloadAppx>d__21 <SideloadAppx>d__;
			<SideloadAppx>d__.appxFile = appxFile;
			<SideloadAppx>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<SideloadAppx>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <SideloadAppx>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<SideloadAppx>d__21>(ref <SideloadAppx>d__);
			return <SideloadAppx>d__.<>t__builder.Task;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000DB50 File Offset: 0x0000BD50
		public static Task SaveDownloadedApp(StoreApp app)
		{
			DownloadManager.<SaveDownloadedApp>d__22 <SaveDownloadedApp>d__;
			<SaveDownloadedApp>d__.app = app;
			<SaveDownloadedApp>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveDownloadedApp>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SaveDownloadedApp>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<SaveDownloadedApp>d__22>(ref <SaveDownloadedApp>d__);
			return <SaveDownloadedApp>d__.<>t__builder.Task;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000DB98 File Offset: 0x0000BD98
		public static Task SaveDownloadedApp(DownloadItem item)
		{
			DownloadManager.<SaveDownloadedApp>d__23 <SaveDownloadedApp>d__;
			<SaveDownloadedApp>d__.item = item;
			<SaveDownloadedApp>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveDownloadedApp>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SaveDownloadedApp>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<SaveDownloadedApp>d__23>(ref <SaveDownloadedApp>d__);
			return <SaveDownloadedApp>d__.<>t__builder.Task;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		private static Task SaveDownloadedAppRecord(DownloadedAppRecord record)
		{
			DownloadManager.<SaveDownloadedAppRecord>d__24 <SaveDownloadedAppRecord>d__;
			<SaveDownloadedAppRecord>d__.record = record;
			<SaveDownloadedAppRecord>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveDownloadedAppRecord>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SaveDownloadedAppRecord>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<SaveDownloadedAppRecord>d__24>(ref <SaveDownloadedAppRecord>d__);
			return <SaveDownloadedAppRecord>d__.<>t__builder.Task;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000DC28 File Offset: 0x0000BE28
		public static Task UpdateInstalledAppStatus(string appName, bool isInstalled)
		{
			DownloadManager.<UpdateInstalledAppStatus>d__25 <UpdateInstalledAppStatus>d__;
			<UpdateInstalledAppStatus>d__.appName = appName;
			<UpdateInstalledAppStatus>d__.isInstalled = isInstalled;
			<UpdateInstalledAppStatus>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateInstalledAppStatus>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateInstalledAppStatus>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<UpdateInstalledAppStatus>d__25>(ref <UpdateInstalledAppStatus>d__);
			return <UpdateInstalledAppStatus>d__.<>t__builder.Task;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000DC78 File Offset: 0x0000BE78
		private static void UpdateDownloadProgress(DownloadItem item, BackgroundDownloadProgress progress)
		{
			double num = 0.0;
			if (progress.TotalBytesToReceive > 0UL)
			{
				num = progress.BytesReceived * 100.0 / progress.TotalBytesToReceive;
			}
			item.Progress = num;
			item.BytesReceived = progress.BytesReceived;
			item.TotalBytes = progress.TotalBytesToReceive;
			item.Status = DownloadManager.GetStatusText(progress.Status, num);
			EventHandler<DownloadManager.DownloadProgressEventArgs> downloadProgress = DownloadManager.DownloadProgress;
			if (downloadProgress == null)
			{
				return;
			}
			downloadProgress.Invoke(null, new DownloadManager.DownloadProgressEventArgs
			{
				DownloadItem = item
			});
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000DD04 File Offset: 0x0000BF04
		private static string GetStatusText(BackgroundTransferStatus status, double percent)
		{
			switch (status)
			{
			case 1:
				return "Downloading... " + percent.ToString("F1") + "%";
			case 2:
				return "Paused";
			case 3:
				return "Paused (metered network)";
			case 4:
				return "Waiting for network...";
			case 5:
				return "Completed";
			case 6:
				return "Cancelled";
			case 7:
				return "Error";
			default:
				return "Unknown";
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		private static void ShowToastNotification(string title, string content, string iconUrl)
		{
			try
			{
				XmlDocument templateContent = ToastNotificationManager.GetTemplateContent(2);
				XmlNodeList elementsByTagName = templateContent.GetElementsByTagName("text");
				if (elementsByTagName.Count >= 2)
				{
					elementsByTagName[0].put_InnerText(title);
					elementsByTagName[1].put_InnerText(content);
				}
				if (!string.IsNullOrEmpty(iconUrl))
				{
					XmlNodeList elementsByTagName2 = templateContent.GetElementsByTagName("image");
					if (elementsByTagName2.Count > 0)
					{
						((XmlElement)elementsByTagName2[0]).SetAttribute("src", iconUrl);
					}
				}
				ToastNotification toastNotification = new ToastNotification(templateContent);
				ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000DE1C File Offset: 0x0000C01C
		public static List<DownloadItem> GetActiveDownloads()
		{
			return new List<DownloadItem>(DownloadManager.activeDownloads);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000DE28 File Offset: 0x0000C028
		public static List<DownloadItem> GetCompletedDownloads()
		{
			return new List<DownloadItem>(DownloadManager.completedDownloads);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000DE34 File Offset: 0x0000C034
		private static Task RunOnUIThread(Action action)
		{
			DownloadManager.<RunOnUIThread>d__31 <RunOnUIThread>d__;
			<RunOnUIThread>d__.action = action;
			<RunOnUIThread>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RunOnUIThread>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RunOnUIThread>d__.<>t__builder;
			<>t__builder.Start<DownloadManager.<RunOnUIThread>d__31>(ref <RunOnUIThread>d__);
			return <RunOnUIThread>d__.<>t__builder.Task;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000DE7C File Offset: 0x0000C07C
		public static void CancelDownload(string fileName)
		{
			if (DownloadManager.downloadOperations.ContainsKey(fileName))
			{
				DownloadManager.downloadOperations[fileName].AttachAsync().Cancel();
				DownloadManager.downloadOperations.Remove(fileName);
				DownloadItem downloadItem = Enumerable.FirstOrDefault<DownloadItem>(DownloadManager.activeDownloads, (DownloadItem item) => item.FileName == fileName);
				if (downloadItem != null)
				{
					DownloadManager.activeDownloads.Remove(downloadItem);
				}
			}
		}

		// Token: 0x04000119 RID: 281
		private static List<DownloadItem> activeDownloads = new List<DownloadItem>();

		// Token: 0x0400011A RID: 282
		private static List<DownloadItem> completedDownloads = new List<DownloadItem>();

		// Token: 0x0400011B RID: 283
		private static Dictionary<string, DownloadOperation> downloadOperations = new Dictionary<string, DownloadOperation>();

		// Token: 0x020000DB RID: 219
		public class DownloadProgressEventArgs : EventArgs
		{
			// Token: 0x1700013F RID: 319
			// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00029E40 File Offset: 0x00028040
			// (set) Token: 0x060007C5 RID: 1989 RVA: 0x00029E48 File Offset: 0x00028048
			public DownloadItem DownloadItem { get; set; }
		}

		// Token: 0x020000DC RID: 220
		public class DownloadCompletedEventArgs : EventArgs
		{
			// Token: 0x17000140 RID: 320
			// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00029E59 File Offset: 0x00028059
			// (set) Token: 0x060007C8 RID: 1992 RVA: 0x00029E61 File Offset: 0x00028061
			public DownloadItem DownloadItem { get; set; }

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00029E6A File Offset: 0x0002806A
			// (set) Token: 0x060007CA RID: 1994 RVA: 0x00029E72 File Offset: 0x00028072
			public bool Success { get; set; }

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x060007CB RID: 1995 RVA: 0x00029E7B File Offset: 0x0002807B
			// (set) Token: 0x060007CC RID: 1996 RVA: 0x00029E83 File Offset: 0x00028083
			public string Error { get; set; }
		}
	}
}
