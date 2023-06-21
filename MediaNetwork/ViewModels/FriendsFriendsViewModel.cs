using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MediaNetwork.Business;
using MediaNetwork.Business.Dto;
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
    public class FriendsFriendsViewModel
    {
        public ObservableCollection<UserDto> Friends { get; set; } = new ObservableCollection<UserDto>();
        private FriendsService _friendService;
        public FriendsFriendsViewModel()
        {
            _friendService = new FriendsService();
            AddFriendCommand = new Command(AddFriend);
            GoToFriendPageCommand = new Command(GoToFriendPage);
            BackToHomeCommand = new Command(BackToHome);
            GetServices();
        }


        public async Task GetServices()
        {
            var textObj = await SecureStorage.Default.GetAsync("friend");
            var friendF = JsonConvert.DeserializeObject<UserDto>(textObj);
            var friendsAndSuggestedFriends = await _friendService.GetFriendsAndSuggestedFriends(friendF.Id);

            Friends.Clear();
            foreach (var friend in friendsAndSuggestedFriends.Friends)
            {
                Friends.Add(friend);
            }
        }
        public async void GoToFriendPage(object friend)
        {
            UserDto friendObj = friend as UserDto;
            if (friendObj != null)
            {

                var text = await SecureStorage.Default.GetAsync("user");
                var user = JsonConvert.DeserializeObject<UserDto> (text);

                if(user.Id == friendObj.Id)
                {
                    Application.Current.MainPage = new HomePage();
                    return;
                }

                await SecureStorage.SetAsync("friend", JsonConvert.SerializeObject(friendObj));
                Application.Current.MainPage = new FriendHomePage(friendObj);
            }
        }
        public async void AddFriend(object item)
        {
            var user = item as UserDto;
            if (user != null)
            {

                var isAdded = await _friendService.AddFriend(new AddFriendDto
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
        public async void BackToHome()
        {
            await SecureStorage.SetAsync("friend", "");
            Application.Current.MainPage = new HomePage();
        }
        public ICommand AddFriendCommand { get; private set; }
        public ICommand GoToFriendPageCommand { get; private set; }
        public ICommand BackToHomeCommand { get; private set; }
    }
}
