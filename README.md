# Hotel
Добрый день, уважаемый ревьюер!

База данных -- (localdb)\MSSQLLocalDB, ее можно установить в VS Instaler, но с большей вероятностью, LocalDB у вас уже стоит.

Миграция бд создана, нужно только написать dotnet ef database update, и можно запускать приложение.

На старте приложения создается админская учетка, ее нужно использовать для входа.

Login: admin@gmail.com
Password: secret

К сожалению не успел написать тесты, логгирование, и поиск.
Так же возникли некоторые трудности с ef во время разработки методов добавления/удаления комнат и посетителей, пришлось выкручиваться.
