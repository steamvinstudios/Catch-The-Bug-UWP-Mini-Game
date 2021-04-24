using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace uwp_game
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class GameScene : Page
    {
        public GameScene()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            health.Text = "100";
            counter.Text = "0";
            Bug.counter = 0;
            AddBug();
        }

        private async void AddBug()
        {
            var randomSpawnTime = new Random();
            while (true)
            {
                await Task.Delay(randomSpawnTime.Next(200, 500));
                rootCanvas.Children.Add(new Bug(rootCanvas, counter, health).Button);
                health.Text = (Convert.ToInt32(health.Text) - 15).ToString();
                if (Convert.ToInt32(health.Text) <= 0)
                {
                    var cd = new ContentDialog
                    {
                        Title = "Вы проиграли",
                        Content = new TextBlock
                        {
                            Text = $"Ваш результат: {Bug.counter} 🪲🪲🪲",
                            FontSize = 32
                        },
                        CloseButtonText = "Закрыть",
                        CloseButtonCommand = new RelayCommand(() => Frame.Navigate(typeof(MainPage)))
                    };
                    var result = await cd.ShowAsync();
                    break;
                }
            }
        }
    }
}
