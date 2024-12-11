using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using wpflab33.Models;
using wpflab33.View;

namespace wpflab33.ViewModels
{
    internal class DataViewModel: INotifyPropertyChanged
    {
        private string jwtToken;
        public string JwtToken
        {
            get { return jwtToken; }
            set
            {
                jwtToken = value;
                OnPropertyChanged("JwtToken");
            }
        }

        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        private string password;
        public string LoginPassword
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("LoginPassword");
            }
        }

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand = new RelayCommand(async obj =>
                {
                    PasswordBox? passwordBox = obj as PasswordBox;
                    HttpClient client = new HttpClient();
                    User user = new User { Login = Login, Password = passwordBox!.Password };
                    JsonContent content = JsonContent.Create(user);

                    // Отправка запроса для получения токена
                    using var response = await client.PostAsync("http://localhost:5055/login", content);
                    string responseText = await response.Content.ReadAsStringAsync();
                    UserResponse? resp = JsonSerializer.Deserialize<UserResponse>(responseText);

                    // Проверка успешности получения токена
                    if (resp != null && resp.access_token != null)
                    {
                        JwtToken = resp.access_token; // Сохраняем токен
                        OpenDataWindow(JwtToken); // Открываем окно с данными
                    }
                    else
                    {
                        // Обработка ошибки (например, неверные данные)
                        JwtToken = string.Empty;
                    }
                });
            }
        }

        private void OpenDataWindow(string token)
        {
            // Создаем окно с отчетом, передавая токен
            DataWindow window = new DataWindow(token);
            window.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

