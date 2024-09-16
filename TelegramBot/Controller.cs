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

            // Если получена команда /start
            if (message.Text != null && message.Text.ToLower() == "/start")
            {
                var keyboard = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton[] { "/Rules", "/Author", "/Play" }
                })
                {
                    ResizeKeyboard = true,   // Автоматическое изменение размера клавиатуры
                    OneTimeKeyboard = true   // Скрыть клавиатуру после нажатия
                };

                await _botClient.SendTextMessageAsync(message.Chat.Id, "Another fool came here to test his luck. Well, uh. Name's Jack. " +
                    "Look around, if you don't know the rules, they're simple, just type /Rules. " +
                    "If you want to know who taught me so well, type /Author. And if you're ready to meet your luck, type /Play. Good luck, mate.", replyMarkup: keyboard);
            }
            else if (message.Text == "/Rules")
            {
                // Пользователь отправил сообщение "Info" или "/info"
                // Отправляем ответ с информацией
                await _botClient.SendTextMessageAsync(message.Chat.Id,
                    "Суть игры проста: нужно собрать такую комбинацию карт, чтобы их сумма была равна 21, но ни в коем случае не больше. " +
                    "Если переберёшь, ты проиграешь. " +
                    "Игроки соревнуются не между собой, а против крупье — это тот, кто раздаёт карты и управляет столом.\n" +
                    "\nКарты: Каждая карта имеет своё значение. От двойки до десятки карты стоят столько же, сколько на них написано." +
                    " Валет, дама и король — это по 10 очков каждая." +
                    " А вот туз — это хитрая карта: он может быть как 1, так и 11, в зависимости от того, что выгоднее тебе.\n" +
                    "\nНачало игры: Крупье раздаёт каждому игроку по две карты. Одну из своих карт крупье кладёт лицом вверх," +
                    " чтобы её могли видеть все игроки.\n" +
                    "\nХод игры: Ты смотришь на свои карты и на открытую карту крупье. Если чувствуешь, что тебе не хватает очков, " +
                    "ты можешь попросить ещё карту, это называется \"hit\". Но будь осторожен: если у тебя будет больше 21 очка, " +
                    "ты проиграешь. Если же тебе достаточно очков, скажи \"stand\", и тогда игра перейдёт к крупье.\n" +
                    "\nКрупье: Он должен взять карту, если у него меньше 17 очков. " +
                    "Как только он достигнет или превысит 17, он останавливается. " +
                    "Ваша задача — обыграть крупье, набрав больше очков, но не больше 21.\n" +
                    "\nВарианты победы:\r\nBlackjack: Если твои две карты — это туз и карта на 10 очков (10, валет, дама или король), поздравляю! " +
                    "Это называется блэкджек, и ты сразу побеждаешь, если только у крупье тоже нет блэкджека." +
                    "\r\nОбычная победа: Если твои очки ближе к 21, чем у крупье, ты победил." +
                    "\r\nПоражение крупье: Если крупье перебирает больше 21, ты автоматически выигрываешь, даже если у тебя не идеальная комбинация." +
                    "");
            }
            else if (message.Text == "/Author")
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, "Бот создан с использованием C# и ASP.NET.");
            }

            return Ok();
        }
    }
}



