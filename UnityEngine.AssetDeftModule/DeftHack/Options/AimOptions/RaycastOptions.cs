using System;
using System.Collections.Generic;
using DeftHack.Attributes;
using DeftHack.Misc.Enums;
using DeftHack.Misc.Serializables;
using SDG.Unturned;

namespace DeftHack.Options.AimOptions
{
	// Token: 0x0200005B RID: 91
	public static class RaycastOptions
	{
		// Token: 0x04000128 RID: 296
		[Save]
		public static bool Enabled = false;

		// Token: 0x04000129 RID: 297
		[Save]
		public static bool NoShootthroughthewalls = false;

		// Token: 0x0400012A RID: 298
		[Save]
		public static bool AlwaysHitHead = false;

		// Token: 0x0400012B RID: 299
		[Save]
		public static bool UseRandomLimb = false;

		// Token: 0x0400012C RID: 300
		[Save]
		public static bool UseCustomLimb = false;

		// Token: 0x0400012D RID: 301
		[Save]
		public static bool UseTargetMaterial = false;

		// Token: 0x0400012E RID: 302
		[Save]
		public static bool UseModifiedVector = false;

		// Token: 0x0400012F RID: 303
		[Save]
		public static bool EnablePlayerSelection = false;

		// Token: 0x04000130 RID: 304
		[Save]
		public static bool OnlyShootAtSelectedPlayer = false;

		// Token: 0x04000131 RID: 305
		[Save]
		public static float SelectedFOV = 10f;

		// Token: 0x04000132 RID: 306
		[Save]
		public static bool SilentAimUseFOV = false;

		// Token: 0x04000133 RID: 307
		[Save]
		public static bool ShowSilentAimUseFOV = false;

		// Token: 0x04000134 RID: 308
		[Save]
		public static bool ShowAimUseFOV = false;

		// Token: 0x04000135 RID: 309
		[Save]
		public static float SilentAimFOV = 10f;

		// Token: 0x04000136 RID: 310
		[Save]
		public static HashSet<TargetPriority> Targets = new HashSet<TargetPriority>();

		// Token: 0x04000137 RID: 311
		[Save]
		public static TargetPriority Target = TargetPriority.Players;

		// Token: 0x04000138 RID: 312
		[Save]
		public static EPhysicsMaterial TargetMaterial = EPhysicsMaterial.ALIEN_DYNAMIC;

		// Token: 0x04000139 RID: 313
		[Save]
		public static ELimb TargetLimb = ELimb.SKULL;

		// Token: 0x0400013A RID: 314
		[Save]
		public static SerializableVector TargetRagdoll = new SerializableVector(0f, 10f, 0f);
	}
}
