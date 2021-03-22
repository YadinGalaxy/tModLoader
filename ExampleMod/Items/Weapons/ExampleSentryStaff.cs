using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExampleMod.Items.Weapons
{
	public class ExampleSentryStaff : ModItem //If you want to find the sentry itself, go to ExampleMod/Projectiles/Minions and look for ExampleSentry
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Example Sentry Staff");
			Tooltip.SetDefault("This is a example sentry summon.");
		}

		public override void SetDefaults()
		{
			item.damage = 75;
			item.width = 35;
			item.height = 45;
			item.autoReuse = true; //Auto reuse is when you hold on to a keybind/mouse click and when holding on it without having to click multiple times
			item.mana = 10; //How much mana will this summon will consume
			item.useAnimation = 15; //How long is the weapon is going to be used
			item.useStyle = ItemUseStyleID.SwingThrow; //The ID of the animation when using this item
			item.noMelee = true; //When the item's animation does no damage because this is a summon weapon
			item.value = Item.buyPrice(0, 20, 0, 0); //0 is platinum coins, 20 is gold coins, the other 0s are silver and copper coins
			item.sentry = true;
			item.knockBack = 5f; //The knockback to enemies for your weapon
			item.summon = true; //This will be a summon weapon
			item.rare = ItemRarityID.Cyan; //The color rarity of the weapon, which is cyan-colored
			item.shoot = mod.ProjectileType("ExampleSentry"); //This is mandatory, since this is a sentry summon
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 mousePos = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY); //This will make the sentry summon at the mouse cursor's position
			position = mousePos;

			return true;
		}
	}
}