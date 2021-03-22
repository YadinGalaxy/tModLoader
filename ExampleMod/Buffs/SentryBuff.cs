using Terraria;
using Terraria.ModLoader;

namespace ExampleMod.Buffs
{
	public class SentryBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Example Sentry");
			Description.SetDefault("The sentry will protect you.");
			Main.buffNoTimeDisplay[Type] = true; //This will make the buff have no time display showing
			Main.buffNoSave[Type] = true; //This will make the buff not save
		}
	}
}