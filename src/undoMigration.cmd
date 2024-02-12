@echo off
dotnet ef migrations remove --project ./ByteBuoy.Infrastructure.Data/ --context ByteBuoyDbContext -o Migrations/ --startup-project ./ByteBuoy.API/ByteBuoy.API.csproj
