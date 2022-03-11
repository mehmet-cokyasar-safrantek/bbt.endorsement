﻿using Serilog.Events;

namespace Worker.App.Infrastructure.Logging.Options;

public class ElasticsearchOptions
{
    public bool IsEnabled { get; set; }

    public string Host { get; set; }

    public string IndexFormat { get; set; }

    public LogEventLevel MinimumLogEventLevel { get; set; }
}