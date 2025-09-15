using System.Text.Json.Serialization;

namespace TgBotClassLibrary
{
    public class UserInfo
    {
        [JsonIgnore]public int Id { get; set; }
        public string Name { get; set; }
        public string TelegramID { get; set; }
        public DateTime? lastSeen { get; set; }
    }
}
