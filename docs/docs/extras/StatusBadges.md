---
title: "Status Badges"
description: "Generate Status Badges"
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
# Page Metric Status Badge

This API endpoint generates and retrieves a status badge for a specific page identified by either its ID or slug. The badge provides a visual representation of the page's metrics, such as performance or availability status.
Endpoint

GET /api/v1/pages/[pageIdOrSlug]/badge
## Parameters

    pageIdOrSlug: A unique identifier for the page. This can be either the numeric ID or a human-readable slug.

## Responses

Content-Type: image/svg+xml

    200 OK: The request was successful, and the badge is returned as an SVG image.
    404 Not Found: No page found corresponding to the provided identifier.
    500 Internal Server Error: An error occurred on the server while processing the request.

## Example Request

```http

`GET /api/v1/pages/123/badge`
```

### Curl Example

```bash

curl -X GET "https://[yourUrl]/api/v1/pages/123/badge"
```

### Usage in Markdown

You can embed the badge directly in your markdown documents with the following syntax:

````markdown

![Page Status Badge](https://[yourUrl]/api/v1/pages/123/badge)

```

Replace 123 with the actual page ID or slug to refer to the specific page.