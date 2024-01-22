@echo off
set /p CommitMessage=Enter migration name (e.g. "AddedMetricsTable")): 
dotnet ef migrations add %CommitMessage% --project ./ByteBuoy.Infrastructure.Data/ --context ByteBuoyDbContext -o Migrations/ --startup-project ./ByteBuoy.API/ByteBuoy.API.csproj
