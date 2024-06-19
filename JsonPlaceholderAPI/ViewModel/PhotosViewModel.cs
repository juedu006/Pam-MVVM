using CommunityToolkit.Mvvm.ComponentModel;
using JsonPlaceholderAPI.Model;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonPlaceholderAPI.ViewModel
{
    public partial class PhotosViewModel : ObservableObject
    {
        private ObservableCollection<PhotoModel> photos;
        public ObservableCollection<PhotoModel> Photos
        {
            get => photos;
            set => SetProperty(ref photos, value);
        }

        public PhotosViewModel()
        {
            Photos = new ObservableCollection<PhotoModel>();
            LoadPhotos();
        }

        private async void LoadPhotos()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://jsonplaceholder.typicode.com/photos");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var photoList = JsonSerializer.Deserialize<List<PhotoModel>>(json);

                    // Limpar a coleção existente antes de adicionar novos itens
                    Photos.Clear();

                    // Adicionar as fotos na ObservableCollection
                    foreach (var photo in photoList.Take(10)) // Adicionando apenas as primeiras 10 fotos
                    {
                        Photos.Add(photo);
                    }
                }
            }
        }
    }
}