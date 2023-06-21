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
    public class FriendsProfileViewModel : BaseViewModel
    {

        public MProp<UserDto> Friend { get; set; } = new MProp<UserDto>();
        public PageResponse<PostDto> Posts { get; set; } = new PageResponse<PostDto>();
        public ObservableCollection<Pagination> Pagination { get; set; } = new ObservableCollection<Pagination>();
        public MProp<Pagination> SelectedItem { get; set; } = new MProp<Pagination>();
        public MProp<bool> AnyItems { get; set; } = new MProp<bool>();
        public MProp<string> Keyword { get; set; } = new MProp<string>();
        public ICommand GetPostsCommand { get; private set; }
        private int FRIEND_ID { get; set; }

        public FriendsProfileViewModel()
        {
            GetFriendInfo();
            GetPosts();
            BackToHomeCommand = new Command(BackToHome);
            GetPostsCommand = new Command(async () => await GetPosts());
        }

        public async void GetFriendInfo()
        {

            var friend = await SecureStorage.GetAsync("friend");
            UserDto friendObj = JsonConvert.DeserializeObject<UserDto>(friend);
            var userService = new UserService();
            Friend.Value = await userService.GetUserInfo(friendObj.Id);
        }
        public async Task GetPosts()
        {
            SearchDto dto = new SearchDto();

            if (!string.IsNullOrEmpty(Keyword.Value))
            {
                dto.Keyword = Keyword.Value;
            }

            if (SelectedItem.Value != null)
                dto.Page = SelectedItem.Value.CurrentPage;

            var postsService = new PostsService();
            var friend = await SecureStorage.GetAsync("friend");
            UserDto friendObj = JsonConvert.DeserializeObject<UserDto>(friend);
            Posts = await postsService.GetSearchedPosts(dto, friendObj.Id);
            Pagination = new ObservableCollection<Pagination>();
            for (int i = 1; i <= Posts.PagesCount; i++)
            {
                Pagination.Add(new Business.Dto.Pagination { CurrentPage = i });
            }
            SelectedItem.Value = Pagination.ElementAt(Posts.CurrentPage - 1);
            AnyItems.Value = true;
            if (Posts.Items.Any())
            {
                AnyItems.Value = false;
            }

            NotifyPropertyChanged(nameof(Posts));
            NotifyPropertyChanged(nameof(Pagination));
        }
        public async void BackToHome()
        {
            await SecureStorage.SetAsync("friend", "");
            Application.Current.MainPage = new HomePage();
        }
        public ICommand BackToHomeCommand { get; set; }
    }
}
