using Newtonsoft.Json;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    public class ChatTypeConverter : JsonConverter<ChatType>
    {
        public override void WriteJson(JsonWriter writer, ChatType value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString().ToLower());
        }

        public override ChatType ReadJson(JsonReader reader, Type objectType, ChatType existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var chatTypeValue = reader.Value?.ToString()?.ToLower();

            return chatTypeValue switch
            {
                "private" => ChatType.Private,
                "group" => ChatType.Group,
                "supergroup" => ChatType.Supergroup,
                "channel" => ChatType.Channel,
                _ => throw new JsonSerializationException($"Invalid chat type: {chatTypeValue}")
            };
        }
    }
}
