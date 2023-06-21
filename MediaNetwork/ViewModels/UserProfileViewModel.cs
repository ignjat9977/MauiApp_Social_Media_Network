using MediaNetwork.Business;
using MediaNetwork.Business.Dto;
using MediaNetwork.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaNetwork.ViewModels
{
    public class UserProfileViewModel : BaseViewModel
    {
        public MProp<UserDto> UserToDisplay { get; set; } = new MProp<UserDto>();
        public PageResponse<PostDto> Posts { get; set; } = new PageResponse<PostDto>();
        public ObservableCollection<Pagination> Pagination { get; set; } = new ObservableCollection<Pagination>();
        public MProp<Pagination> SelectedItem { get; set; } = new MProp<Pagination>();
        public MProp<bool> AnyItems { get; set; } = new MProp<bool>();
        public MProp<string> Keyword { get; set; } = new MProp<string>();
        public ICommand GetPostsCommand { get; private set; }
        public ICommand LogOutCommand { get; private set; }
        public ICommand GoToUpdatePageCommand { get; private set; }
        public ICommand GoToCreatePageCommand { get; private set; }
        public UserProfileViewModel()
        {
            // Pozovite asinkronu metodu
            InitializeAsync();
            GetPostsCommand = new Command(async () => await GetPosts());
            LogOutCommand = new Command(LogOut);
            GoToUpdatePageCommand = new Command(GoToUpdatePage);
            GoToCreatePageCommand = new Command(GoToCreatePage);
        }
        private void GoToUpdatePage(object obj)
        {
            PostDto post = obj as PostDto;
            if(post != null)
            {
                Application.Current.MainPage = new UpdatePostPage(post);
            }
            
        }
        private void GoToCreatePage(object obj)
        {
            Application.Current.MainPage = new CreatePostPage();

        }
        private async void InitializeAsync()
        {

            await GetPosts();

            var userService = new UserService();
            UserToDisplay.Value = await userService.GetUserInfo(null);



            NotifyPropertyChanged(nameof(Posts));
            NotifyPropertyChanged(nameof(UserToDisplay));
        }
       
        public async Task GetPosts()
        {
            SearchDto dto = new SearchDto();

            if (!string.IsNullOrEmpty(Keyword.Value))
            {
                dto.Keyword = Keyword.Value;
            }

            if(SelectedItem.Value != null)
                dto.Page = SelectedItem.Value.CurrentPage;

            var postsService = new PostsService();
            Posts = await postsService.GetSearchedPosts(dto, null);
            Pagination = new ObservableCollection<Pagination>();
            for(int i=1; i<= Posts.PagesCount; i++) 
            { 
                Pagination.Add(new Business.Dto.Pagination { CurrentPage = i});
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
        public async void LogOut()
        {
            Application.Current.MainPage = new MainPage();
            await SecureStorage.Default.SetAsync("user", "");
        }
       
    }
}
