using MediatR;
using System.Collections.Generic;

namespace Sample.Api.Queries
{
    public class WeatherForecastRequest : IRequest<IEnumerable<WeatherForecast>> { }
}
