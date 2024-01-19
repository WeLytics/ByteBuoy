

# Troubleshooting

`addMigration.cmd`

Problem: 
Could not execute because the specified command or file was not found.
Possible reasons for this include:
  * You misspelled a built-in dotnet command.
  * You intended to execute a .NET program, but dotnet-ef does not exist.
  * You intended to run a global tool, but a dotnet-prefixed executable with this name could not be found on the PATH.


Solution:
Install Entity Framework Core tools: https://learn.microsoft.com/en-us/ef/core/cli/  
  
 
`dotnet tool install --global dotnet-ef`