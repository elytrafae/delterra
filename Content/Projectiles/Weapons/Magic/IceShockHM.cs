using Delterra.Content.Gores;
using Delterra.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Delterra.Content.Projectiles.Weapons.Magic
{
    public class IceShockHM : IceShock {

        public override int DebuffToInflict => BuffID.Frostburn2;

    }
}
