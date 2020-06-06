using ImageFiltersWPF.ViewModels.Enums;
using ImageFiltersWPF.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;

namespace ImageFiltersWPF.ViewModels.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        private readonly ILogger<MessageBoxService> logger;

        public MessageBoxService(ILogger<MessageBoxService> logger)
        {
            this.logger = logger;
        }

        public void ShowMessage(MessageBoxEnum messageType, string message)
        {
            logger.LogInformation($"Show({messageType}, {message})");
        }
    }
}
