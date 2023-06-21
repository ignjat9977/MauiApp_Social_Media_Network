using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Extensions;
using MediaNetwork.Business;
using MediaNetwork.Business.Dto;
using MediaNetwork.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaNetwork.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        public PageResponse<UserDto> PRUsers { get; set; } = new PageResponse<UserDto>();
        public ObservableCollection<UserDto> Users { get; set; } = new ObservableCollection<UserDto>();
        public MProp<string> Keyword { get; set; } = new MProp<string>();
        public MProp<Pagination> SelectedItem { get; set; } = new MProp<Pagination>();
        public ObservableCollection<Pagination> Pagination { get; set; } = new ObservableCollection<Pagination>();
        public ICommand SearchUsers { get; set; }
        public ICommand AddFriendCommand { get; set; }
        public ICommand GoToFriendsPageCommand { get; set; }

        public MProp<bool> AnyItems { get; set; } = new MProp<bool>();
        public SearchPageViewModel()
        {
            GetUsers();
            SearchUsers = new Command(GetUsers);
            AddFriendCommand = new Command(AddFriend);
            GoToFriendsPageCommand = new Command(GoToFriendPage);
        }

        public async void GetUsers()
        {
            var keyw = "";
            var userService = new UserService();
            if(Keyword.Value != null)
            {
                keyw = Keyword.Value;
            }
            if (SelectedItem.Value != null)
            {
                keyw = keyw + "&Page=" + SelectedItem.Value.CurrentPage;

            }
            PRUsers = await userService.GetUsers(keyw);
            Pagination = new ObservableCollection<Pagination>();
            for(int i = 1; i<= PRUsers.PagesCount; i++) 
            {
                Pagination.Add(new Business.Dto.Pagination { CurrentPage = i});
            }
            AnyItems.Value = true;
            if (PRUsers.Items.Any())
            {
                AnyItems.Value = false;
            }

            NotifyPropertyChanged(nameof(Pagination));
            NotifyPropertyChanged(nameof(PRUsers));
        }
        public async void GoToFriendPage(object friend)
        {
            UserDto friendObj = friend as UserDto;
            if(friendObj != null)
            {
                await SecureStorage.SetAsync("friend", JsonConvert.SerializeObject(friendObj));
                Application.Current.MainPage = new FriendHomePage(friendObj);
            }
        }
        public async void AddFriend(object item)
        {
            var user = item as UserDto;
            if (user != null)
            {
                var friendService = new FriendsService();

                var isAdded= await friendService.AddFriend(new AddFriendDto
                {
                    UserId = 0,
                    FriendId = (int)user.Id
                });

                if (!isAdded)
                {
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                    var snackbarOptions = new SnackbarOptions
                    {
                        BackgroundColor = Colors.Red,
                        TextColor = Colors.Green,
                        ActionButtonTextColor = Colors.Yellow,
                        CornerRadius = new CornerRadius(10),
                        Font = Microsoft.Maui.Font.SystemFontOfSize(14),
                        ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(14),
                        CharacterSpacing = 0
                    };

                    string text = "User is already your friend!";
                    string actionButtonText = "Click Here to Dismiss";
                    TimeSpan duration = TimeSpan.FromSeconds(3);

                    var snackbar = Snackbar.Make(text, null, actionButtonText, duration, snackbarOptions);

                    await snackbar.Show(cancellationTokenSource.Token);
                }
            }
        }

    }
}
