---
title: "Api Calls"
description: "Check our Bruh requests"
summary: ""
date: 2023-09-07T16:04:48+02:00
lastmod: 2023-09-07T16:04:48+02:00
draft: false
menu:
  docs:
    parent: ""
    identifier: "starthere"
weight: 810
toc: true
---
# API Call Examples

This documentation provides a streamlined guide to help you integrate and test API requests using the Bruno format. For additional details on the Bruno format, please refer to the [https://www.usebruno.com/bru](Bruno documentation).

## Getting Started with Bruno

To begin using Bruno with your API:

   1) Load the Collection:
        Import the ByteBuoy collection located in the `scripts/requests` directory. This collection contains pre-configured requests tailored for testing.

    2= Set Up Your Environment:
        Define a working environment, such as Dev, to manage settings specific to your development process.
        Create an environment variable `BYTEBUOY_SERVER` and set it to your local server's URL, for example,`http://localhost:5000`. This variable will be used by the Bruno collection to route requests to your server.