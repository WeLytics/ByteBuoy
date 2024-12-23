# Welcome to ByteBuoy 👋

![Version](https://img.shields.io/badge/version-0.1.0-blue.svg?cacheSeconds=2592000)
[![Documentation](https://img.shields.io/badge/documentation-yes-brightgreen.svg)](https://welytics.github.io/ByteBuoy/)
[![License: Apache--2.0 license](https://img.shields.io/badge/License-Apache--2.0_license-green.svg)](https://github.com/WeLytics/ByteBuoy/blob/main/LICENSE)



## Introduction
Welcome to ByteBuoy, an open-source Artefact & File Monitoring solution. ByteBuoy is designed to streamline the process of monitoring, copying, and moving files and artefacts, offering an intuitive Web application to track the status of these operations. 

## Why ByteBuoy
We move a large number of files on a daily basis, particularly for machine learning pipelines. As part of this process, we need a solution to monitor file transactions, such as whether all customers have received their export files, or whether the correct data has been made available for further processing by the machine learning system. After trying a number of monitoring solutions, we decided to build something ourselves. How hard can it be? :D And we wanted to share it with the rest of you. 

![image](https://github.com/user-attachments/assets/efd81ee8-f903-441d-8b10-0ac32dd52e9c)
![image](https://github.com/user-attachments/assets/0e205627-55d4-42db-9167-8d20d79d4e9c)


## Features
- **File Monitoring:** Keep an eye on your critical files and artefacts with real-time monitoring.
- **Agent for File Operations:** Equip your systems with a powerful agent capable of performing sophisticated file operations. This agent supports copying, moving, and managing files both locally and remotely via SSH, based on user-defined settings. It's designed for flexibility and efficiency in handling various file management tasks.
- **Web Application:** A user-friendly web interface to view the status of file operations and monitoring alerts.
- **Status Badges:**  Seamlessly integrate dynamic status badges into your application or CI/CD pipeline to provide real-time insights into page metrics and operational health.

## ⚠️ Status
This project is still in its very early development days. Expect a few bugs.

## Demo

Check out our Demo page [https://bytebuoy.app](https://bytebuoy.app)


## Roadmap
- **Notifications & Subscribers:** Receive updates on failed jobs, missing files, and other critical alerts based on specific job conditions. Customize subscription options to target key stakeholders or groups.
- **Tracing:** Trace files and artifacts through your system with a visual overview, detailing where and when files interact with various systems.
- **Incidents:** Streamline incident communication with a dedicated system to quickly share and manage updates and resolutions.
- **Online Config Builder:** Simplify your YAML configuration management with an intuitive online builder, making it easier to create, edit, and manage your configurations.


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

Launch your default web browser and navigate to [`http://localhost:4200/`](http://localhost:4200/) to access the application.

## Versioning
We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/welytics/bytebuoy/tags).


## 🤝 Contributing

Contributions, issues and feature requests are welcome!<br />Feel free to check [issues page](https://github.com/WeLytics/ByteBuoy/issues). You can also take a look at the [contributing guide](https://github.com/WeLytics/ByteBuoy/blob/main/CONTRIBUTING.md).

## Discord

Join our [Discord](https://discord.gg/9ujA3fme)

## Show your support

Give a ⭐️ if this project helped you!

## Author

👤 **WeLytics**

* Website: https://www.welytics.ai
* Github: [@welytics](https://github.com/welytics)

## 📝 License

Copyright © 2024 [WeLytics](https://github.com/welytics).<br />
This project is [Apache--2.0 license](https://github.com/WeLytics/ByteBuoy/blob/main/LICENSE) licensed.

