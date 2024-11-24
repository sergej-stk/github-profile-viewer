using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace github_profile_viewer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly HttpClient _httpClient = new();

    public async Task<GitHubProfile?> GetGitHubProfile(string username)
    {
        var url = $"https://api.github.com/users/{username}";
        _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

        var response = await _httpClient.GetStringAsync(url);
        var profile = JsonConvert.DeserializeObject<GitHubProfile>(response);
        return profile;
    }

    [ObservableProperty] private string _username = String.Empty;
    [ObservableProperty] private Bitmap? _profileImage;
    [ObservableProperty] private GitHubProfile? _githubProfile;
    
    [RelayCommand]
    private async Task Search()
    {
        GithubProfile = await GetGitHubProfile(Username);
        Console.Write(GitHubProfile.AvatarUrl);

    }   
}

public class GitHubProfile
{
    public string? Login { get; set; }
    public string? Name { get; set; }
    public static string? AvatarUrl { get; set; }
    public string? Bio { get; set; }
    public int? PublicRepos { get; set; }
    public int? Followers { get; set; }
}