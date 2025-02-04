---
# https://vitepress.dev/reference/default-theme-home-page
layout: home

hero:
  name: "ResultifyCore"
  text: "It is a .NET library providing Result, Option, Outcome, OneOf and Guard patterns to simplify error handling, optional values, and type discrimination."
  tagline: My great project tagline
  actions:
    - theme: brand
      text: Markdown Examples
      link: /markdown-examples
    - theme: alt
      text: API Examples
      link: /api-examples

features:
  - title: Result Pattern
    details: The Result pattern represents the outcome of an operation that can either succeed or fail. It allows for better error handling and makes the flow of success or failure explicit.
  - title: Option Pattern
    details: The Option pattern represents an optional value that may or may not be present. It allows you to avoid null references and explicitly handle the absence of values.
  - title: OneOf Pattern
    details: The OneOf pattern allows you to encapsulate multiple possible types for a single value. This is useful for cases where a value can belong to one of several types.
---

