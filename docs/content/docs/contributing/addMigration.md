---
title: "Database migrations"
description: "How to add Database Migrations & Trouble shooting"
summary: ""
date: 2023-09-07T16:04:48+02:00
lastmod: 2023-09-07T16:04:48+02:00
draft: false
menu:
  docs:
    parent: ""
    identifier: "build-notes"
weight: 810
toc: true
seo:
  title: "" # custom title (optional)
  description: "" # custom description (recommended)
  canonical: "" # custom canonical URL (optional)
  noindex: false # false (default) or true
---



# Adding a Database Migration

To add a new database migration, please run the script `src\addMigration.cmd`. When prompted, ensure that the naming convention follows PascalCase and accurately describes the changes you have made. For instance, if you have added a metrics table, you might name it AddedMetricsTable.

# Undoing or Removing the Last Database Migration

In case you need to undo or remove the most recent database migration during the development phase, especially if it was configured incorrectly or in error, execute the script `src\undoMigration.cmd`. This will help in reverting the changes made by the last migration.


## Troubleshooting


Problem: 
Could not execute because the specified command or file was not found.
Possible reasons for this include:
  * You misspelled a built-in dotnet command.
  * You intended to execute a .NET program, but dotnet-ef does not exist.
  * You intended to run a global tool, but a dotnet-prefixed executable with this name could not be found on the PATH.


## Solution:
Install Entity Framework Core tools: https://learn.microsoft.com/en-us/ef/core/cli/  
   
`dotnet tool install --global dotnet-ef`