---
title: "Run Agent and ByteBuoy.yml"
description: "Shows you the steps to use ByteBuoy.Agent to send Metrics to the API"
---

# Introduction to ByteBuoy Agent

The ByteBuoy Agent is a versatile command-line application designed for automating file operations. These operations include but are not limited to copying, moving files, verifying the presence of files that match specific patterns, and uploading them to an API server. The application utilizes a YAML-formatted file to define the sequence of operations to be performed.

Upon initialization, the ByteBuoy Agent searches for a configuration file named `bytebuoy.yml`. If this file is found, the application proceeds to execute the defined operations sequentially.

# Command-line Arguments

## Specifying Configuration File

- **Syntax:** `-file [file name]` or `-f [file name]`
- **Description:** This argument allows users to specify a different configuration file than the default `bytebuoy.yml`. This is useful for running the application with various sets of operations without modifying the default configuration file.
- **Example:** To use a custom YAML file named `myfile.yml`, the command would be `ByteBuoy.Agent.exe -f myfile.yml`. This tells the ByteBuoy Agent to load and execute operations defined in `myfile.yml` instead of `bytebuoy.yml`.

By allowing the specification of different configuration files, the ByteBuoy Agent provides flexibility in managing file operations across different projects or scenarios.



# YML configuration file 
An example file

```yml
version: 1
host: http://localhost:5000/
apiKey: bb_DEV123
page: 1
description: my new job description
ignoreFiles: .gitkeep
tasks:
  # - name: Copies all files from sources to targets
    # action: filesCopy@v1
    # sources: 
         # - c:\SourceFolder\ByteBuoys\myFiles\v1\*.txt
    # targets:
      # - c:\TargetFolder\Target1\
      # - C:\StagingFolder\ByteBuoysTarget2\
    # labels:
      # customer: customer1
      # customer2: someRandom Value

  # - name: Run some command Line commands
    # action: commandLine@v1
    # commands:
      # - ls -la
    # workingDirectory: c:\SourceFolder\ByteBuoy   
  
  # - name: Move Files Job
    # action: filesMove@v1
    # overwrite: true
    # sources: 
      # - c:\SourceFolder\ByteBuoy\myFiles\v1\Customer2*.xml
    # targets:
      # - c:\TargetFolder\TargetFolder2
    # labels:
      # customer: customer2

  # - name: Just checking file hashes (Customer 2)
    # action: filesHashes@v1
    # hashAlgorithm: MD5
    # paths:
      # - c:\SourceFolder\ByteBuoys\myFiles\v1\Customer2*.xml
    # labels:
      # customer: customer2

  - name: Just checking file exists (Customer 3)
    action: filesExists@v1
    paths: 
      - c:\Temp\ByteBuoys\sources\v1\MediScan{yyyyMMdd}*.*
    labels:
       customer: customer3

  # - name: Upload Files to SSH Server
    # action: sshUpload@v1
    # host: myserver.local
    # port: 22
    # username: myuser
    # password: mypassword
    # sources: 
      # - c:\SourceFolder\ByteBuoys\myFiles\v1\Customer3*.xml
    # targets:
      # - /targetfolder/
    # labels:
       # customer: customer3

```

This YAML file defines a configuration for the ByteBuoy Agent, a command-line application designed to automate a variety of file operations. Each section and property of the file serves a specific purpose in guiding the application's behavior.
Top-Level Properties

    version: Specifies the version of the configuration format. This helps ensure compatibility between different versions of the ByteBuoy Agent.
    host: The base URL of the API server where files may be sent or operations reported. It's essential for tasks that involve interaction with a web service.
    apiKey: An authentication key used for secure communication with the API server.
    page: Generally used for pagination in API calls. This could specify a starting point or context for operations that interact with the server.
    description: Provides a brief description of the job or series of tasks being defined in the YAML file.
    ignoreFiles: Specifies file patterns to be ignored during operations. For example, .gitkeep files are often used to keep empty directories in version control and might not need to be processed.

Task Properties

Each task is defined as a list item under the tasks property. While most tasks are commented out in this example, they can be activated by removing the leading #.
Common Properties for Tasks

    name: A human-readable identifier for the task, aiding in logs and debugging.
    action: Specifies the type of operation to be performed, such as copying files, moving files, executing command-line commands, checking file hashes, verifying file existence, or uploading files via SSH.
    labels: Key-value pairs that can be used to tag tasks with additional metadata, such as customer identifiers.

Task-Specific Properties

    sources: For tasks involving file operations, this property lists the source file paths or patterns.
    targets: For file operations, this specifies the destination paths.
    overwrite: A boolean flag (true/false) used in file-moving tasks to indicate whether existing files at the target should be overwritten.
    commands: For tasks that run command-line commands, this lists the commands to be executed.
    workingDirectory: Specifies the directory in which command-line commands should be executed.
    hashAlgorithm: In tasks that compute file hashes, this specifies the algorithm to use (e.g., MD5).
    paths: Used in tasks that need to operate on specific file paths, like checking for existence or computing hashes.
    host, port, username, password: For SSH upload tasks, these properties define the SSH server details and authentication credentials.

Example Task: Checking File Existence

```yaml

- name: Just checking file exists (Customer 3)
  action: filesExists@v1
  paths: 
    - c:\Temp\ByteBuoys\sources\v1\ExportFile{yyyyMMdd}*.*
  labels:
     customer: customer3
```

This task checks for the existence of files matching a specific pattern in a given directory. It's named "Just checking file exists (Customer 3)" and tagged with a label indicating it's associated with "customer3". The action filesExists@v1 tells the ByteBuoy Agent to perform a file existence check. The paths property lists the patterns of files to be checked, supporting dynamic date-based file selection.

This YAML configuration structure allows for a high degree of flexibility and customization in defining automated file operations, making it a powerful tool for streamlining workflows that involve file management and interaction with external APIs or services.