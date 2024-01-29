<h1 align="center">Welcome to ByteBuoy 👋</h1>
<p>
  <img alt="Version" src="https://img.shields.io/badge/version-0.0.1-blue.svg?cacheSeconds=2592000" />
  <a href="https://welytics.github.io/ByteBuoy/" target="_blank">
    <img alt="Documentation" src="https://img.shields.io/badge/documentation-yes-brightgreen.svg" />
  </a>
  <a href="https://github.com/WeLytics/ByteBuoy/blob/main/LICENSE" target="_blank">
    <img alt="License: Apache--2.0 license" src="https://img.shields.io/badge/License-Apache--2.0 license-green.svg" />
  </a>
</p>


## Introduction
Welcome to ByteBuoy, an open-source Artefact & File Monitoring solution. ByteBuoy is designed to streamline the process of monitoring, copying, and moving files and artefacts, offering an intuitive Web application to track the status of these operations. 

## Why ByteBuoy
We move a large number of files on a daily basis, particularly for the machine learning pipelines. As part of this process, we need a solution to monitor file transactions, such as whether all customers have received their export files, or whether the correct data has been made available for further processing by the machine learning system. After trying a number of monitoring solutions, we decided to build something ourselves. We wanted to share it with everyone. After all, how hard can it be? :D


## Features
- **File Monitoring:** Keep an eye on your critical files and artefacts with real-time monitoring.
- **Agent for File Operations:** A robust agent that can copy or move files according to your configurations.
- **Web Application:** A user-friendly web interface to view the status of file operations and monitoring alerts.


## Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

To initiate ByteBuoy, ensure you have a functional Docker installation on your system. This prerequisite is essential for running ByteBuoy effectively.


## Usage from Sources

To execute ByteBuoy, navigate to the root directory of the source repository and run the command docker compose up. This will initiate the Docker Compose process, which orchestrates the setup and starts ByteBuoy.

```sh
git clone https://github.com/WeLytics/ByteBuoy.git
cd ByteBuoy
docker compose up
```

Launch your default web browser and navigate to [`http://localhost:3000/`](http://localhost:3000/) to access the application.

## Versioning
We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/welytics/bytebuoy/tags).


## 🤝 Contributing

Contributions, issues and feature requests are welcome!<br />Feel free to check [issues page](https://github.com/WeLytics/ByteBuoy/issues). You can also take a look at the [contributing guide](https://github.com/WeLytics/ByteBuoy/blob/main/CONTRIBUTING.md).

## Show your support

Give a ⭐️ if this project helped you!

## Author

👤 **WeLytics**

* Website: https://www.welytics.ai
* Github: [@welytics](https://github.com/welytics)

## 📝 License

Copyright © 2024 [WeLytics](https://github.com/welytics).<br />
This project is [Apache--2.0 license](https://github.com/WeLytics/ByteBuoy/blob/main/LICENSE) licensed.

