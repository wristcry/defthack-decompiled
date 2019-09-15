using System;
using System.Collections;
using System.IO;
using DeftHack.Components.UI;
using DeftHack.Components.UI.Menu;
using DeftHack.Utilities;
using DeftHack.Variables;
using UnityEngine;

namespace DeftHack.Coroutines
{
	// Token: 0x0200008A RID: 138
	public static class LoaderCoroutines
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00002C34 File Offset: 0x00000E34
		public static IEnumerator LoadAssets()
		{
			string mylink = "https://unturnedhake.000webhostapp.com/thonk/1/assets";
			bool flag2 = !File.Exists(LoaderCoroutines.AssetPath);
			if (flag2)
			{
				WWW loader = new WWW(mylink);
				yield return loader;
				File.WriteAllBytes(LoaderCoroutines.AssetPath, loader.bytes);
				byte[] array23 = loader.bytes;
				loader = null;
				loader = null;
			}
			else
			{
				byte[] array23 = File.ReadAllBytes(LoaderCoroutines.AssetPath);
				bool flag3 = HashUtilities.GetSHA2HashString(array23) != "1f0c81293065ceadadb8e7f84f2aa77cc28c2e03c9b2c23b4d8b03e654e0455c";
				if (flag3)
				{
					WWW loader2 = new WWW(mylink);
					yield return loader2;
					File.WriteAllBytes(LoaderCoroutines.AssetPath, loader2.bytes);
					array23 = loader2.bytes;
					loader2 = null;
					loader2 = null;
				}
			}
			yield return new WaitForSeconds(1f);
			byte[] Loader = File.ReadAllBytes(LoaderCoroutines.AssetPath);
			Console.WriteLine(LoaderCoroutines.AssetPath);
			AssetBundle bundle = AssetBundle.LoadFromMemory(Loader);
			AssetVariables.ABundle = bundle;
			foreach (Shader s in bundle.LoadAllAssets<Shader>())
			{
				AssetVariables.Materials.Add(s.name, new Material(s)
				{
					hideFlags = HideFlags.HideAndDontSave
				});
				s = null;
			}
			Shader[] array24 = null;
			foreach (Shader s2 in bundle.LoadAllAssets<Shader>())
			{
				AssetVariables.Shaders.Add(s2.name, s2);
				s2 = null;
			}
			Shader[] array25 = null;
			foreach (Font f in bundle.LoadAllAssets<Font>())
			{
				AssetVariables.Fonts.Add(f.name, f);
				f = null;
			}
			Font[] array26 = null;
			foreach (AudioClip ac in bundle.LoadAllAssets<AudioClip>())
			{
				AssetVariables.Audio.Add(ac.name, ac);
				ac = null;
			}
			AudioClip[] array27 = null;
			foreach (Texture2D t in bundle.LoadAllAssets<Texture2D>())
			{
				bool flag = t.name != "Font Texture";
				bool flag4 = flag;
				if (flag4)
				{
					AssetVariables.Textures.Add(t.name, t);
				}
				t = null;
			}
			Texture2D[] array28 = null;
			ESPComponent.GLMat = AssetVariables.Materials["ESP"];
			ESPComponent.ESPFont = AssetVariables.Fonts["Roboto-Light"];
			MenuComponent._TabFont = AssetVariables.Fonts["Anton-Regular"];
			MenuComponent._TextFont = AssetVariables.Fonts["CALIBRI"];
			MenuComponent._LogoTexLarge = AssetVariables.Textures["thanking_logo_large"];
			ESPCoroutines.Normal = Shader.Find("Standard");
			ESPCoroutines.LitChams = AssetVariables.Shaders["chamsLit"];
			ESPCoroutines.UnlitChams = AssetVariables.Shaders["chamsUnlit"];
			LoaderCoroutines.IsLoaded = true;
			yield break;
		}

		// Token: 0x04000299 RID: 665
		public static bool IsLoaded;

		// Token: 0x0400029A RID: 666
		public static string AssetPath = string.Format("{0}/assets", Application.dataPath);
	}
}
