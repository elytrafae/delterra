using Delterra.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Delterra.Content.Gores 
{
    public class IceShockBigSnowflake : ModGore {

        public override void OnSpawn(Gore gore, IEntitySource source) {
            gore.timeLeft = 1;
        }

        public override bool Update(Gore gore) {
            gore.timeLeft--;
            if (gore.timeLeft > 0) {
                Lighting.AddLight(gore.position + new Vector2(gore.Width / 2, gore.Height / 2), IceShock.LIGHT * 0.8f);
            } else if (gore.timeLeft == 0) {
                for (int i = 0; i < 4; i++) {
                    Gore.NewGore(new GoreEntitySource(gore), gore.position + new Vector2(10, 10), new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f)), ModContent.GoreType<IceShockSmallSnowflake>(), 0.5f);
                }
            }
            return gore.timeLeft <= 0;
        }

        public override Color? GetAlpha(Gore gore, Color lightColor) {
            return gore.timeLeft <= 0 ? Color.Transparent : lightColor;
        }

    }

    public class GoreEntitySource : IEntitySource {

        string context = "";
        public Gore gore;

        public string Context => context;
        public GoreEntitySource(Gore gore, string context = "") {
            this.context = context;
            this.gore = gore;
        }

    }
}
