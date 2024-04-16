@echo off
dotnet ef migrations remove --project ./ByteBuoy.Infrastructure.Data/ --context ByteBuoyDbContext --startup-project ./ByteBuoy.API/ByteBuoy.API.csproj
