using MLAPI.Serialization;

namespace Chuzaman.Entities {

    public struct WinData : INetworkSerializable {

        public int ChuzaCoins;
        public int DoggaCoins;

        public void NetworkSerialize(NetworkSerializer serializer) {
            serializer.Serialize(ref ChuzaCoins);
            serializer.Serialize(ref DoggaCoins);
        }

    }

}