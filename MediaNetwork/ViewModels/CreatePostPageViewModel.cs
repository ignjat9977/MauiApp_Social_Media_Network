using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using FluentValidation.Results;
using MediaNetwork.Business;
using MediaNetwork.Business.Dto;
using MediaNetwork.Common;
using MediaNetwork.Validators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaNetwork.ViewModels
{
    public class CreatePostPageViewModel : BaseViewModel
    {
        public MProp<string> Title { get; set; } = new MProp<string>();
        public MProp<string> Content { get; set; } = new MProp<string>();
        private PostsService _postService;
        public int Id { get; set; }
        public ICommand CreatePostCommand { get; set; }
        public ICommand BackToHomeCommand { get; set; }
        public CreatePostPageViewModel()
        {
            _postService = new PostsService();
            Title.OnValueChanged = Validate;
            Content.OnValueChanged = Validate;

            Title.Value = "";
            Content.Value = "";

            CreatePostCommand = new Command(async () => await CreatePost());
            BackToHomeCommand = new Command(BackToHome);
        }
        private async Task CreatePost()
        {
            var user = await SecureStorage.Default.GetAsync("user");
            var userObj = JsonConvert.DeserializeObject<User>(user);

            CreatePostDto post = new CreatePostDto
            {
                Id = Id,
                Title = Title.Value,
                Content = Content.Value,
                PrivacyId = 12,
                UserId = userObj.Id
            };
            var isUpdated = _postService.CreatePost(post);

            if (!await isUpdated)
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

                string text = "Server error please try again later!";
                string actionButtonText = "Click Here to Dismiss";
                TimeSpan duration = TimeSpan.FromSeconds(3);

                var snackbar = Snackbar.Make(text, null, actionButtonText, duration, snackbarOptions);

                await snackbar.Show(cancellationTokenSource.Token);


            }
            else
            {
                Application.Current.MainPage = new HomePage();
            }

        }
        public void BackToHome()
        {
            Application.Current.MainPage = new HomePage();
        }
        private void Validate()
        {
            CreatePostViewModelValidator validator = new CreatePostViewModelValidator();

            ValidationResult valid = validator.Validate(this);

            if (!valid.IsValid)
            {
                var titleError = valid.Errors.FirstOrDefault(x => x.PropertyName.Contains("Title"));

                if (titleError != null)
                {
                    Title.Error = titleError.ErrorMessage;
                }
                else
                {
                    Title.Error = "";
                }

                var contentError = valid.Errors.FirstOrDefault(x => x.PropertyName.Contains("Content"));

                if (contentError != null)
                {
                    Content.Error = contentError.ErrorMessage;
                }
                else
                {
                    Content.Error = "";
                }
            }
            else
            {
                Title.Error = "";
                Content.Error = "";
            }
            NotifyPropertyChanged(nameof(IsUpdateButtonEnabled));
        }
        public bool IsUpdateButtonEnabled => !Title.HasError && !Content.HasError;
    }
}
