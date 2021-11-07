using System;
using System.Text;
using System.Reflection;
using System.Configuration;
using System.Collections.Generic;

namespace Game.Settings
{
	/// <summary>
	/// Description of Manager.
	/// </summary>
	public class Manager
	{
		Dictionary<string, string> _settings;
		
		public Manager(Dictionary<string, string> settings)
		{
			_settings = settings;
			ConfigSettings();
		}
		
		void ConfigSettings()
		{
			string exePath = Assembly.GetEntryAssembly().Location;
			string configPath = String.Concat(exePath, ".config");
			if (System.IO.File.Exists(configPath)){
				List<string> keys = new List<string>(_settings.Keys);
				foreach(string key in keys){
					_settings[key] = ConfigurationManager.AppSettings[key];
				}
			}else{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
				sb.AppendLine("<configuration>");
				sb.AppendLine("<startup>");
				sb.AppendLine("<supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.5.1\" />");
				sb.AppendLine("</startup>");
				sb.AppendLine("<appSettings>");
				foreach(var item in _settings){
					sb.AppendLine("<add key=\"" + item.Key + "\" value=\"" + item.Value + "\"/>");
				}
				sb.AppendLine("</appSettings>");
				sb.AppendLine("</configuration>");
				System.IO.File.WriteAllText(configPath, sb.ToString());
			}
		}
		
		public string GetSettings(string id)
		{
			return _settings[id];
		}
		
		public void SaveSettings(string id, string val)
		{
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			configuration.AppSettings.Settings[id].Value = val;
			configuration.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection("appSettings");
		}
	}
}
