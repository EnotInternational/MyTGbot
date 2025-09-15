using System.Text.Json.Serialization;

namespace TgBotClassLibrary
{
    public class UserInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? lastSeen { get; set; }
    }
}
