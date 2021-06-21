using Chuzaman.Entities;
using Chuzaman.Managers;

namespace Chuzaman.Net {

    public class PlayerSession {

        public ulong ID { get; set; }
        public Character Character { get; set; }
        public int Coins { get; set; }
        
    }

}