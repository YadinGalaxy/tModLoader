using ExampleMod.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExampleMod.Projectiles.Minions
{
	public class ExampleSentry : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Example Sentry"); //The name for the sentry, note that minions and sentries are counted as projectiles
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true; //Makes the sentry target enemies, note that it's required to use Terraria.ID for the projectile ID
			//There is one more thing that can come with the minion/sentry as well:
			//ProjectileID.Sets.MinionSacrificable[projectile.type] = true - this makes the minion sacrifice for you
		}

		public override void SetDefaults()
		{
			projectile.sentry = true; //This will tell that this is a sentry minion
			projectile.width = 59; //The width of the sentry
			projectile.height = 59; //The height of the sentry
			projectile.ignoreWater = true; //This will make the sentry not move slow when it touches water
			projectile.timeLeft = 30000; //The amount of time the sentry will be protecting you until it despawns
			projectile.penetrate = -1; //Amount of enemies that the sentry can hit each time before the sentry is destroyed,
			projectile.friendly = true; //The sentry will not harm the player and friendly NPCs if this is true
			projectile.tileCollide = true; //This will make the sentry go through tiles/blocks if this is true
			projectile.minionSlots = 5f; //How much minion slots this sentry will use to summon it
		}

		public override void AI()
		{
			for (int i = 0; i < 1; i++) {
				int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Blood, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 120, default(Color), 1.50f); //This is for the dust particles to spawn to the sentry
				Main.dust[dust].noGravity = true; //This will make the dust have no gravity
				Main.dust[dust].velocity *= 0.9f; //Velocity of the dust
			}
			Player player = Main.player[projectile.owner]; //This sets up a variable and it also makes the player the projectile owner
			player.AddBuff(ModContent.BuffType<SentryBuff>(), 5000); //The buff of the sentry minion
			if (player.dead || !player.active) //This will get rid of the sentry buff when the player dies
			{
				player.ClearBuff(ModContent.BuffType<SentryBuff>());
			}

			if (player.HasBuff(ModContent.BuffType<SentryBuff>())) //This is just for the sentry buff for the sentry minion
			{
				projectile.timeLeft = 2;
			}

			for (int i = 0; i < 200; i++) {
				NPC target = Main.npc[i];

				//This code will make the sentry shoot projectiles at enemies
				float x = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
				float y = target.position.Y + (float)target.height * 0.5f - projectile.Center.Y;
				float distance = (float)System.Math.Sqrt((double)(x * x + y * y));
				if (distance < 450f && !target.friendly && target.active) //This is when the target is hostile and the sentry is ready to attack the hostile enemy
				{
					if (projectile.ai[0] > 240f) //This will make the projectile shooting of the sentry each time will be every 4 seconds, every number that is 60 will be a second
					{
						distance = 3f / distance; //Distance velocity

						x *= distance * 4;
						y *= distance * 4;
						int damage = 75; //The damage of the sentry's projectile

						Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, x, y, ProjectileID.UFOLaser, damage, 0, Main.myPlayer, 0f, 0f); //The UFO laser is a vanilla projectile ID, switch this to mod.ProjectileType("ProjectileName") if you want a custom projectile from your mod
						projectile.ai[0] = 0f; //This will reset the sentry's AI and it will do the repeat the attacks when seeing enemies
					}
				}
			}
			projectile.ai[0] += 1f; //When resetting the sentry's AI, this is very useful to repeat the sentry's attacks
		}

		public override bool? CanCutTiles() //It returns false which means it will not destroy a boss summon tile such as the queen bee larva
		{
			return false;
		}

		public override bool MinionContactDamage() //This is returned true which also means that it will do contact damage to enemies
		{
			return true;
		}
	}
}
