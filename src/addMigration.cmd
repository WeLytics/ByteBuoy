@echo off
set /p CommitMessage=Enter commit message: 
dotnet ef migrations add %CommitMessage% --project ./ByteBuoy.Infrastructure.Data/ --context ByteBuoyDbContext -o Migrations/ --startup-project ./ByteBuoy.API/ByteBuoy.API.csproj
