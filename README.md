# Tsunami

## Uruchamianie

#### Budowanie solucji
```bash
dotnet build
```

#### Uruchamianie kontenerów bazy danych i usług SOAP
```
docker-compose up
```

Usługi zostają wystawione na adresach:
- Trend API - http://localhost:7779/trend
- Category API - http://localhost:7789/category

#### Uruchamianie aplikacji
W katalogu `Assets` należy umieścić plik `client_secrets.json` usługi OAuth 2.0 z Google Cloud.

Przed uruchomieniem należy dostarczyć zmienne środowiskowe:
- `YOUTUBEAPIKEY` - klucz API usługi Google Cloud,
- `DB_PASSWORD` - hasło do bazy danych (dla bazy danych uruchomionej według powyższej instrukcji - `tsunami`).

```bash
export YOUTUBEAPIKEY=XXXX
export DB_PASSWORD=tsunami
```

```bash
cd DesktopApp/bin/Debug/net5.0
./DesktopApp
```

#### Uruchamianie usługi API Gateway
```bash
./ApiGateway/bin/Debug/net5.0/ApiGateway
```

Usługi SOAP stają się dostępne poprzez API Gateway na adresach:
- Trend API - http://localhost:7070/ws/trend
- Category API - http://localhost:7070/ws/category

