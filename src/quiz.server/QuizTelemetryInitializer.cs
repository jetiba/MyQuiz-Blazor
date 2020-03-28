using System;
using System.Web;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;

namespace quiz.server
{
  public class QuizTelemetryInitializer : ITelemetryInitializer
  {
        private IHttpContextAccessor _httpContextAccessor;

        public QuizTelemetryInitializer(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException("httpContextAccessor");
        }
        public void Initialize(ITelemetry telemetry)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.User?.Identity?.Name != null && httpContext.User.Identity.IsAuthenticated == true)
            {
                telemetry.Context.User.AuthenticatedUserId = httpContext.User.Identity.Name;
            }
        }
  }
}