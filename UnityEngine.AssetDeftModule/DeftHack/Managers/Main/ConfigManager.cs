using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DeftHack.Attributes;
using DeftHack.Options;
using DeftHack.Options.VisualOptions;
using DeftHack.Variables;
using DeftHack.Variables.UIVariables;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace DeftHack.Managers.Main
{
	// Token: 0x0200007D RID: 125
	public class ConfigManager
	{
		// Token: 0x0600015C RID: 348 RVA: 0x000029FD File Offset: 0x00000BFD
		public static void Init()
		{
			ConfigManager.LoadConfig(ConfigManager.GetConfig());
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000C890 File Offset: 0x0000AA90
		public static Dictionary<string, object> CollectConfig()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{
					"Version",
					ConfigManager.ConfigVersion
				}
			};
			foreach (Type type in (from T in Assembly.GetExecutingAssembly().GetTypes()
			where T.IsClass
			select T).ToArray<Type>())
			{
				foreach (FieldInfo fieldInfo in (from F in type.GetFields()
				where F.IsDefined(typeof(SaveAttribute), false)
				select F).ToArray<FieldInfo>())
				{
					dictionary.Add(type.Name + "_" + fieldInfo.Name, fieldInfo.GetValue(null));
				}
			}
			return dictionary;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000C97C File Offset: 0x0000AB7C
		public static Dictionary<string, object> GetConfig()
		{
			bool flag = !File.Exists(ConfigManager.ConfigPath);
			bool flag2 = flag;
			if (flag2)
			{
				ConfigManager.SaveConfig(ConfigManager.CollectConfig());
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			try
			{
				dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(ConfigManager.ConfigPath), new JsonSerializerSettings
				{
					Formatting = Formatting.Indented
				});
			}
			catch
			{
				dictionary = ConfigManager.CollectConfig();
				ConfigManager.SaveConfig(dictionary);
			}
			return dictionary;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00002A0B File Offset: 0x00000C0B
		public static void SaveConfig(Dictionary<string, object> Config)
		{
			File.WriteAllText(ConfigManager.ConfigPath, JsonConvert.SerializeObject(Config, Formatting.Indented));
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		public static void LoadConfig(Dictionary<string, object> Config)
		{
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				foreach (FieldInfo fieldInfo in from f in type.GetFields()
				where Attribute.IsDefined(f, typeof(SaveAttribute))
				select f)
				{
					string key = type.Name + "_" + fieldInfo.Name;
					Type fieldType = fieldInfo.FieldType;
					object value = fieldInfo.GetValue(null);
					bool flag = !Config.ContainsKey(key);
					bool flag2 = flag;
					if (flag2)
					{
						Config.Add(key, value);
					}
					try
					{
						bool flag3 = Config[key].GetType() == typeof(JArray);
						bool flag4 = flag3;
						if (flag4)
						{
							Config[key] = ((JArray)Config[key]).ToObject(fieldInfo.FieldType);
						}
						bool flag5 = Config[key].GetType() == typeof(JObject);
						bool flag6 = flag5;
						if (flag6)
						{
							Config[key] = ((JObject)Config[key]).ToObject(fieldInfo.FieldType);
						}
						fieldInfo.SetValue(null, fieldInfo.FieldType.IsEnum ? Enum.ToObject(fieldInfo.FieldType, Config[key]) : Convert.ChangeType(Config[key], fieldInfo.FieldType));
					}
					catch
					{
						Config[key] = value;
					}
				}
			}
			foreach (KeyValuePair<string, ColorVariable> keyValuePair in ColorOptions.DefaultColorDict)
			{
				bool flag7 = !ColorOptions.ColorDict.ContainsKey(keyValuePair.Key);
				bool flag8 = flag7;
				if (flag8)
				{
					ColorOptions.ColorDict.Add(keyValuePair.Key, new ColorVariable(keyValuePair.Value));
				}
			}
			using (List<KeyValuePair<string, Hotkey>>.Enumerator enumerator3 = HotkeyOptions.UnorganizedHotkeys.ToList<KeyValuePair<string, Hotkey>>().GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					KeyValuePair<string, Hotkey> str = enumerator3.Current;
					bool flag9 = HotkeyOptions.HotkeyDict.All((KeyValuePair<string, Dictionary<string, Hotkey>> kvp) => !kvp.Value.ContainsKey(str.Key));
					bool flag10 = flag9;
					if (flag10)
					{
						HotkeyOptions.UnorganizedHotkeys.Remove(str.Key);
					}
				}
			}
			ConfigManager.SaveConfig(Config);
		}

		// Token: 0x040001FB RID: 507
		public static string ConfigPath = Application.dataPath + "/sharedassets4.assets";

		// Token: 0x040001FC RID: 508
		public static string ConfigVersion = "1.0.1";
	}
}
