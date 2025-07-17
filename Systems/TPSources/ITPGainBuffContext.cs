using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delterra.Systems.TPSources {
    public interface ITPGainBuffContext : ITPGainContext {

        int buffID { get; }

    }
}
