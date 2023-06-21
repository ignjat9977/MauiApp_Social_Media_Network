using MediaNetwork.Business;
using MediaNetwork.Business.Dto;
using MediaNetwork.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaNetwork.ViewModels
{
    public class GroupPageViewModel : BaseViewModel
    {

        private readonly GroupService _groupService;
        public PageResponse<GroupDto> Groups { get; set; }
        public MProp<string> Keyword { get; set; } = new MProp<string>();
        public MProp<Pagination> SelectedItem { get; set; } = new MProp<Pagination>();
        public ObservableCollection<Pagination> Pagination { get; set; } = new ObservableCollection<Pagination>();
        public ICommand SearchCommand { get; set; }
        public ICommand GoToGroupPageCommand { get; set; }
        public MProp<bool> AnyItems { get; set; } = new MProp<bool>();
        public GroupPageViewModel()
        {
            _groupService = new GroupService();


            InitialMessages();
            SearchCommand = new Command(async () => await InitialMessages());
            GoToGroupPageCommand = new Command(GoToGroupPage);
        }
        public void GoToGroupPage(object item)
        {
            GroupDto group = item as GroupDto;
            if(group != null)
            {

                Application.Current.MainPage = new SingleGroupPage(group);
            }
        }
        public async Task InitialMessages()
        {

            var keyw = "";
            if (Keyword.Value != null)
            {
                keyw = Keyword.Value;
            }
            if (SelectedItem.Value != null)
            {
                keyw = keyw + "&Page=" + SelectedItem.Value.CurrentPage;

            }

            Groups = await _groupService.GetAllGroups(keyw);
            Pagination = new ObservableCollection<Pagination>();
            for (int i = 1; i <= Groups.PagesCount; i++)
            {
                Pagination.Add(new Business.Dto.Pagination { CurrentPage = i });
            }
            AnyItems.Value = true;
            if (Groups.Items.Any())
            {
                AnyItems.Value = false;
            }


            NotifyPropertyChanged(nameof(Groups));
            NotifyPropertyChanged(nameof(Pagination));
        }
    }
}
