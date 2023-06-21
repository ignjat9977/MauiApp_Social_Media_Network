using MediaNetwork.Business;
using MediaNetwork.Business.Dto;
using MediaNetwork.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.ViewModels
{
    public class MessagesPageViewModel : BaseViewModel
    {
        private readonly MessagesService _messageService;
        public PageResponse<MessagesDto> Messages { get; set; }
        public MessagesPageViewModel()
        {
            _messageService = new MessagesService();


            InitialMessages();
        }

        public async void InitialMessages()
        {
            var messages = await _messageService.GetAllMessages("");
            messages.Items.Distinct();
            
            Messages = messages;

            NotifyPropertyChanged(nameof(Messages));
        }
    }
}
