# IMPORTANT

This repository and sink is no longer actively maintained as I've moved its functionality back over to the core [serilog umbrella](https://github.com/serilog/), particularly [its own repository over there](https://github.com/serilog/serilog-sinks-applicationinsights).

The NuGet Package, while it's a pre-release at the moment is now [Serilog.Sinks.ApplicationInsights](https://www.nuget.org/packages/Serilog.Sinks.ApplicationInsights/).

*The following is for archiving purposes only*:


----------------------------------------------------------------------
# ~~Serilog.Sinks.AzureApplicationInsights~~ [![NuGet Version](http://img.shields.io/nuget/v/Serilog.Sinks.AzureApplicationInsights.svg?style=flat)](https://www.nuget.org/packages/Serilog.Sinks.AzureApplicationInsights/)

Provides a [serilog sink](https://github.com/serilog/serilog/wiki/Provided-Sinks) that writes log messages to the new, [Microsoft Azure Portal integrated version of Application Insights](http://azure.microsoft.com/en-us/services/application-insights/).

I also wrote the [original sink](https://www.nuget.org/packages/Serilog.Sinks.ApplicationInsights/) that writes / wrote to the [Visual Studio Online integrated version of AI](https:/msdn.microsoft.com/en-us/library/dn481095.aspx), however the later one is deprecated and longer actively developed.

However, the new Azure Portal based AI is still under development itself and the NuGet package this sink relies on is also considered a pre-release at this stage.. so things might change or break over time.


## Usage

Start of by installing the [Serilog.Sinks.AzureApplicationInsights](https://www.nuget.org/packages/Serilog.Sinks.AzureApplicationInsights/) NuGet package:

`PM> Install-Package Serilog.Sinks.AzureApplicationInsights -Pre`

Once installed, configure your serilog logger like this:

```
using Serilog.Sinks.AzureApplicationInsights;

// ...

var logger = new LoggerConfiguration()
    .WriteTo.AzureApplicationInsights(instrumentationKey)
    .CreateLogger();
```

.. the `instrumentationKey` variable used above is provided by the MS Azure Portal when creating or looking at a new or existing Application Insights instance's properties.
Everything else from there on works just like any other serilog sink, see https://github.com/serilog/serilog/wiki for details.

## Thanks

[Nicholas](https://github.com/nblumhardt) for creating [serilog](https://github.com/serilog/serilog/) and Microsoft for creating Application Insights.

## Copyright

Copyright © 2014 Jörg Battermann

## License

Serilog.Sinks.AzureApplicationInsights is licensed under [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form"). Refer to [LICENSE.md](https://github.com/jbattermann/Serilog.Sinks.AzureApplicationInsights/blob/master/LICENSE.md) for more information.
