using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace W80StoreRevival
{
	// Token: 0x02000009 RID: 9
	public class LiveTileManager
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00009560 File Offset: 0x00007760
		[DebuggerStepThrough]
		public Task UpdateLiveTileAsync()
		{
			LiveTileManager.<UpdateLiveTileAsync>d__0 <UpdateLiveTileAsync>d__;
			<UpdateLiveTileAsync>d__.<>4__this = this;
			<UpdateLiveTileAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateLiveTileAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateLiveTileAsync>d__.<>t__builder;
			<>t__builder.Start<LiveTileManager.<UpdateLiveTileAsync>d__0>(ref <UpdateLiveTileAsync>d__);
			return <UpdateLiveTileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000095AC File Offset: 0x000077AC
		private void UpdateTileNotifications(JsonObject jsonObject)
		{
			try
			{
				ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
				int num = 1;
				if (localSettings.Values.ContainsKey("TileIndex"))
				{
					num = (int)localSettings.Values["TileIndex"];
					num = num % 5 + 1;
				}
				localSettings.Values["TileIndex"] = num;
				string text = "app" + num;
				Debug.WriteLine("[LIVETILE] Using tile index: " + text);
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement("tile");
				xmlDocument.AppendChild(xmlElement);
				XmlElement xmlElement2 = xmlDocument.CreateElement("visual");
				xmlElement2.SetAttribute("version", "2");
				xmlElement.AppendChild(xmlElement2);
				this.AddSquareTileBinding(xmlDocument, xmlElement2, jsonObject, text);
				this.AddWideTileBinding(xmlDocument, xmlElement2, jsonObject, text);
				this.AddLargeTileBinding(xmlDocument, xmlElement2, jsonObject, text);
				TileNotification tileNotification = new TileNotification(xmlDocument);
				TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
				Debug.WriteLine("[LIVETILE] Tile notification sent");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[LIVETILE] Error creating tile notification: " + ex.Message);
				this.CreateDefaultTileNotification();
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00009708 File Offset: 0x00007908
		private void AddSquareTileBinding(XmlDocument xmlDoc, XmlElement visualElement, JsonObject jsonObject, string appKey)
		{
			try
			{
				if (jsonObject.ContainsKey("squareTile"))
				{
					JsonObject @object = jsonObject["squareTile"].GetObject();
					if (@object.ContainsKey(appKey))
					{
						JsonObject object2 = @object[appKey].GetObject();
						string @string = object2["imgUrl"].GetString();
						string string2 = object2["title"].GetString();
						XmlElement xmlElement = xmlDoc.CreateElement("binding");
						xmlElement.SetAttribute("template", "TileSquare150x150PeekImageAndText01");
						xmlElement.SetAttribute("fallback", "TileSquarePeekImageAndText01");
						XmlElement xmlElement2 = xmlDoc.CreateElement("image");
						xmlElement2.SetAttribute("id", "1");
						xmlElement2.SetAttribute("src", @string);
						xmlElement2.SetAttribute("alt", string2);
						xmlElement.AppendChild(xmlElement2);
						XmlElement xmlElement3 = xmlDoc.CreateElement("text");
						xmlElement3.SetAttribute("id", "1");
						xmlElement3.put_InnerText(string2);
						xmlElement.AppendChild(xmlElement3);
						visualElement.AppendChild(xmlElement);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[LIVETILE] Error adding square tile binding: " + ex.Message);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00009878 File Offset: 0x00007A78
		private void AddWideTileBinding(XmlDocument xmlDoc, XmlElement visualElement, JsonObject jsonObject, string appKey)
		{
			try
			{
				if (jsonObject.ContainsKey("wideTile"))
				{
					JsonObject @object = jsonObject["wideTile"].GetObject();
					if (@object.ContainsKey(appKey))
					{
						JsonObject object2 = @object[appKey].GetObject();
						string @string = object2["imgUrl"].GetString();
						string string2 = object2["title"].GetString();
						string string3 = object2["description"].GetString();
						XmlElement xmlElement = xmlDoc.CreateElement("binding");
						xmlElement.SetAttribute("template", "TileWide310x150ImageAndText01");
						xmlElement.SetAttribute("fallback", "TileWideImageAndText01");
						XmlElement xmlElement2 = xmlDoc.CreateElement("image");
						xmlElement2.SetAttribute("id", "1");
						xmlElement2.SetAttribute("src", @string);
						xmlElement2.SetAttribute("alt", string2);
						xmlElement.AppendChild(xmlElement2);
						XmlElement xmlElement3 = xmlDoc.CreateElement("text");
						xmlElement3.SetAttribute("id", "1");
						xmlElement3.put_InnerText(string2);
						xmlElement.AppendChild(xmlElement3);
						XmlElement xmlElement4 = xmlDoc.CreateElement("text");
						xmlElement4.SetAttribute("id", "2");
						xmlElement4.put_InnerText(string3);
						xmlElement.AppendChild(xmlElement4);
						visualElement.AppendChild(xmlElement);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[LIVETILE] Error adding wide tile binding: " + ex.Message);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00009A2C File Offset: 0x00007C2C
		private void AddLargeTileBinding(XmlDocument xmlDoc, XmlElement visualElement, JsonObject jsonObject, string appKey)
		{
			try
			{
				if (jsonObject.ContainsKey("hugeTile"))
				{
					JsonObject @object = jsonObject["hugeTile"].GetObject();
					if (@object.ContainsKey(appKey))
					{
						JsonObject object2 = @object[appKey].GetObject();
						string @string = object2["imgUrl"].GetString();
						string string2 = object2["title"].GetString();
						string string3 = object2["description"].GetString();
						XmlElement xmlElement = xmlDoc.CreateElement("binding");
						xmlElement.SetAttribute("template", "TileSquare310x310ImageAndText01");
						xmlElement.SetAttribute("fallback", "TileSquareText01");
						XmlElement xmlElement2 = xmlDoc.CreateElement("image");
						xmlElement2.SetAttribute("id", "1");
						xmlElement2.SetAttribute("src", @string);
						xmlElement2.SetAttribute("alt", string2);
						xmlElement.AppendChild(xmlElement2);
						XmlElement xmlElement3 = xmlDoc.CreateElement("text");
						xmlElement3.SetAttribute("id", "1");
						xmlElement3.put_InnerText(string2);
						xmlElement.AppendChild(xmlElement3);
						XmlElement xmlElement4 = xmlDoc.CreateElement("text");
						xmlElement4.SetAttribute("id", "2");
						xmlElement4.put_InnerText(string3);
						xmlElement.AppendChild(xmlElement4);
						visualElement.AppendChild(xmlElement);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[LIVETILE] Error adding large tile binding: " + ex.Message);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00009BE0 File Offset: 0x00007DE0
		private void CreateDefaultTileNotification()
		{
			try
			{
				string text = "<?xml version='1.0' encoding='utf-8'?>\n                    <tile>\n                      <visual version='2'>\n                        <binding template='TileSquare150x150PeekImageAndText01'>\n                          <image id='1' src='Assets/StoreLogo.png' alt='8Store'/>\n                          <text id='1'>8Store</text>\n                        </binding>\n                        <binding template='TileWide310x150ImageAndText01'>\n                          <image id='1' src='Assets/StoreLogo.png' alt='8Store'/>\n                          <text id='1'>8Store Revival</text>\n                          <text id='2'>Windows 8.0 App Store</text>\n                        </binding>\n                      </visual>\n                    </tile>";
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(text);
				TileNotification tileNotification = new TileNotification(xmlDocument);
				TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
				Debug.WriteLine("[LIVETILE] Default tile notification sent");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[LIVETILE] Error creating default tile: " + ex.Message);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00009C50 File Offset: 0x00007E50
		public void ClearLiveTile()
		{
			try
			{
				TileUpdateManager.CreateTileUpdaterForApplication().Clear();
				Debug.WriteLine("[LIVETILE] Live tile cleared");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[LIVETILE] Error clearing live tile: " + ex.Message);
			}
		}

		// Token: 0x04000046 RID: 70
		private const string LiveTileJsonUrl = "http://8store.dankassassin368.com/data/newsservice/livetile.json";
	}
}
