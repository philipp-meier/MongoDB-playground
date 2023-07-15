# MongoDB playground

[ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet) and [MongoDB](https://www.mongodb.com/) playground for experimenting with [time series collections](https://www.mongodb.com/docs/manual/core/timeseries-collections/) and getting familiar with the [MongoDB C# Driver](https://www.mongodb.com/docs/drivers/csharp/current/) and its performance for handling millions of data entries.

## Required tools

- [Docker Compose](https://docs.docker.com/compose/)
- [MongoDB Compass](https://www.mongodb.com/products/compass) or similar.
- [.NET CLI](https://dotnet.microsoft.com/en-us/download)
- Optional: [Python 3.10](https://www.python.org/downloads/release/python-3100/)

## Getting started

- Run `docker compose up -d` in the root folder.
- Run `dotnet run` in the Server folder.
- Open the Swagger UI page (`http://localhost:5109/swagger/index.html`).
- Import data with (for instance) `python3 client.py` in the Client folder.
