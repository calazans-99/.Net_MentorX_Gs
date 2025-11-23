# Mentorax (.NET 7) - Projeto criado do zero

Projeto criado do zero para o tema **Mentorax – Gerador de planos de mentoria corporativa com IA**.
Target framework: **.NET 7**

## Como executar localmente

Requisitos:
- .NET 7 SDK
- dotnet-ef (ferramenta) para migrations, se for usar migrations locais

1. Restaurar pacotes:
```bash
dotnet restore src/Mentorax.Api/Mentorax.Api.csproj
```

2. Instalar dotnet-ef (se necessário):
```bash
dotnet tool install --global dotnet-ef --version 7.*
```

3. Criar migration e atualizar o banco:
```bash
dotnet ef migrations add InitialMentorax -p src/Mentorax.Api/Mentorax.Api.csproj -s src/Mentorax.Api/Mentorax.Api.csproj
dotnet ef database update -p src/Mentorax.Api/Mentorax.Api.csproj -s src/Mentorax.Api/Mentorax.Api.csproj
```

4. Executar a API:
```bash
dotnet run --project src/Mentorax.Api/Mentorax.Api.csproj
```

## Endpoints principais (v1)

- `POST /api/v1/mentorados` — criar mentorado (body JSON)
- `POST /api/v1/mentores` — criar mentor (body JSON)
- `POST /api/v1/questionarios` — salvar questionário (body JSON)
- `POST /api/v1/planosmentoria/generate` — gerar plano de mentoria a partir do questionário
- `GET  /api/v1/planosmentoria?page=1&pageSize=10` — listar planos com paginação e HATEOAS
- `GET  /health` — health check




## Enhancements added (automated)
- Serilog logging (console)
- HealthChecks endpoints: /health/ready and /health/live
- API Versioning (URL segment): e.g. /api/v1/[controller]
- Swagger documentation
- EF Core SqlServer package references added (run migrations as instructed)
- ApiKey middleware scaffolded (uses X-Api-Key header and configuration key 'ApiKey')
- Helpers: PagedResult<T>, Resource<T> (for HATEOAS)
- Test project scaffold (Mentorax.Tests) with xUnit and WebApplicationFactory example

### How to run locally
1. Configure connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MentoraxDb;Trusted_Connection=True;"
}
```
2. Set ApiKey in config (appsettings.json):
```json
"ApiKey": "your-secret-key"
```
3. Restore and build:
```
dotnet restore
dotnet build
```
4. Create migrations and update DB (from project folder containing the csproj):
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```
5. Run:
```
dotnet run
```

### Tests
A test project `Mentorax.Tests` was added. Run:
```
dotnet test
```

