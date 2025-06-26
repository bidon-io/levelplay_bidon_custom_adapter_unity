// ReSharper disable CheckNamespace

using System.Diagnostics.CodeAnalysis;

namespace Bidon.LevelPlayCustomAdapter
{
    [System.Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class BidonNetworkData
    {
        public bool BidonCA_GDPR;
        public bool BidonCA_CCPA;
        public bool BidonCA_COPPA;

        public string ToJsonString() => UnityEngine.JsonUtility.ToJson(this, false); 
    }
}
