using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;

namespace TelegramBotWebhook.Controllers
{
    [ApiController]
    [Route("api/bot")]
    public class BotController : ControllerBase
    {
        private readonly ITelegramBotClient _botClient;

        public BotController(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            if (update == null || update.Message == null)
                return BadRequest();

            var message = update.Message;

            // ���� �������� ������� /start
            if (message.Text != null && message.Text.ToLower() == "/start")
            {
                var keyboard = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] { "/Rules", "/Author", "/Play" }
                })
                {
                    ResizeKeyboard = true,   // �������������� ��������� ������� ����������
                    OneTimeKeyboard = true   // ������ ���������� ����� �������
                };

                await _botClient.SendTextMessageAsync(message.Chat.Id, "Another fool came here to test his luck. Well, uh. Name's Jack. " +
                    "Look around, if you don't know the rules, they're simple, just type /Rules. " +
                    "If you want to know who taught me so well, type /Author. And if you're ready to meet your luck, type /Play. Good luck, mate.", replyMarkup: keyboard);
            }
            else if (message.Text == "/Rules")
            {
                // ������������ �������� ��������� "Info" ��� "/info"
                // ���������� ����� � �����������
                await _botClient.SendTextMessageAsync(message.Chat.Id,
                    "���� ���� ������: ����� ������� ����� ���������� ����, ����� �� ����� ���� ����� 21, �� �� � ���� ������ �� ������. " +
                    "���� ���������, �� ����������. " +
                    "������ ����������� �� ����� �����, � ������ ������ � ��� ���, ��� ������ ����� � ��������� ������.\n" +
                    "\n�����: ������ ����� ����� ��� ��������. �� ������ �� ������� ����� ����� ������� ��, ������� �� ��� ��������." +
                    " �����, ���� � ������ � ��� �� 10 ����� ������." +
                    " � ��� ��� � ��� ������ �����: �� ����� ���� ��� 1, ��� � 11, � ����������� �� ����, ��� �������� ����.\n" +
                    "\n������ ����: ������ ������ ������� ������ �� ��� �����. ���� �� ����� ���� ������ ����� ����� �����," +
                    " ����� � ����� ������ ��� ������.\n" +
                    "\n��� ����: �� �������� �� ���� ����� � �� �������� ����� ������. ���� ����������, ��� ���� �� ������� �����, " +
                    "�� ������ ��������� ��� �����, ��� ���������� \"hit\". �� ���� ���������: ���� � ���� ����� ������ 21 ����, " +
                    "�� ����������. ���� �� ���� ���������� �����, ����� \"stand\", � ����� ���� ������� � ������.\n" +
                    "\n������: �� ������ ����� �����, ���� � ���� ������ 17 �����. " +
                    "��� ������ �� ��������� ��� �������� 17, �� ���������������. " +
                    "���� ������ � �������� ������, ������ ������ �����, �� �� ������ 21.\n" +
                    "\n�������� ������:\r\nBlackjack: ���� ���� ��� ����� � ��� ��� � ����� �� 10 ����� (10, �����, ���� ��� ������), ����������! " +
                    "��� ���������� ��������, � �� ����� ����������, ���� ������ � ������ ���� ��� ���������." +
                    "\r\n������� ������: ���� ���� ���� ����� � 21, ��� � ������, �� �������." +
                    "\r\n��������� ������: ���� ������ ���������� ������ 21, �� ������������� �����������, ���� ���� � ���� �� ��������� ����������." +
                    "");
            }
            else if (message.Text == "/Author")
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, "��� ������ � �������������� C# � ASP.NET.");
            }

            return Ok();
        }
    }
}



