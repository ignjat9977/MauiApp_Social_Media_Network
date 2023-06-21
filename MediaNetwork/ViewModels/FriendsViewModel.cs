
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Extensions;
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
using CommunityToolkit.Maui.Alerts;
using Newtonsoft.Json;

namespace MediaNetwork.ViewModels
{
    public class FriendsViewModel : BaseViewModel
    {

        public ObservableCollection<UserDto> Friends { get; set; } = new ObservableCollection<UserDto>();
        private FriendsService _friendService;
        public FriendsViewModel()
        {
            _friendService = new FriendsService();
            DeleteFriendCommand = new Command(DeleteFriend);
            GoToFriendPageCommand = new Command(GoToFriendPage);
            GetServices();
        }


        public async Task GetServices()
        {
            var friendsAndSuggestedFriends = await _friendService.GetFriendsAndSuggestedFriends(null);

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
                await SecureStorage.SetAsync("friend", JsonConvert.SerializeObject(friendObj));
                Application.Current.MainPage = new FriendHomePage(friendObj);
            }
        }
        public async void DeleteFriend(object item)
        {
            UserDto friend = item as UserDto;
            if(friend != null)
            {
                var isDeleted = await _friendService.DeleteFriend(friend);

                if(isDeleted)
                {
                    Friends.Remove(item as UserDto);
                    NotifyPropertyChanged(nameof(Friends));
                }
                else
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

                    string text = "Friend is cant be deleted";
                    string actionButtonText = "Click Here to Dismiss";
                    TimeSpan duration = TimeSpan.FromSeconds(3);

                    var snackbar = Snackbar.Make(text, null, actionButtonText, duration, snackbarOptions);

                    await snackbar.Show(cancellationTokenSource.Token);
                }
            }
            
        }
        public ICommand DeleteFriendCommand { get; private set; }
        public ICommand GoToFriendPageCommand { get; private set; }
    }
}
