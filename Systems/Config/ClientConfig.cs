using Steamworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Delterra.Systems.Config {
    public class ClientConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("TPBar")]
        [DefaultValue(true)]
        public bool ShowGrazeArea;

        [DefaultValue(true)]
        public bool PlayGrazeSound;

        [DefaultValue(true)]
        public bool AlwaysDisplayTPBar;




        public static ClientConfig Get() { 
            return ModContent.GetInstance<ClientConfig>();
        }
    }
}
