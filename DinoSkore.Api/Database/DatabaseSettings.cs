﻿namespace DinoSkore.Api.Database;

public class DatabaseSettings
{
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 5432;
    public string Username { get; set; } = "postgres";
    public string Password { get; set; } = "postgres";
    public string Database { get; set; } = "dinoskore";
    public bool UseSsl { get; set; } = true;
}
