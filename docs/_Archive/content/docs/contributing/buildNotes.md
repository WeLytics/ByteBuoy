---
title: "Build Notes"
description: "Trouble shooting"
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



# Troubleshooting Builds: ByteBuoy.Agent Error

# Issue

When attempting to run the ByteBuoy.Agent, you encounter the following error message:

`Error reading config: Line: 1, Col: 1, Idx: 0Failed to create an instance of type 'ByteBuoy.Infrastructure.Config.JobDto`

# Cause

This error typically indicates that the Trim Code option was enabled during the build process of the Agent. The YAML reader heavily relies on reflection, and the code trimming feature, which removes unused code, can lead to issues by inadvertently removing necessary code paths that the YAML reader depends on.

# Solution

To resolve this issue, please follow these steps:

1)    Disable the Trim Code Option: Navigate to your build configuration settings and locate the Trim Code option. Ensure that this option is deactivated or turned off.

1)    Rebuild the Agent: After disabling the Trim Code option, rebuild the ByteBuoy.Agent.

1) Run the Agent Again: With the Trim Code option disabled, run the ByteBuoy.Agent once more. The error should no longer occur, and the Agent should function as expected.

By following these steps, you should be able to successfully troubleshoot and resolve the build error encountered with ByteBuoy.Agent.