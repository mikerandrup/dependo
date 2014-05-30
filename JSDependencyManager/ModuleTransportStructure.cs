using System;

namespace ClientAssetDependencyManager
{
    public class TransportModel
    {
        public string name { get; set; }
        public string version { get; set; }
        public string content { get; set; }
        public int sequence { get; set; }
    }
}
