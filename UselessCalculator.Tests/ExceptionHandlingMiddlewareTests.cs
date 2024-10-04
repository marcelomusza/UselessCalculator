using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using UselessCalculator.Api.Middleware;
namespace UselessCalculator.Tests
{
    public class ExceptionHandlingMiddlewareTests
    {
        private Mock<ILogger<GlobalExceptionHandler>> _mockLogger;
        private GlobalExceptionHandler _exceptionHandler;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<GlobalExceptionHandler>>();
            _exceptionHandler =  new GlobalExceptionHandler(_mockLogger.Object);
        }

        [Test]
        public async Task TryHandleAsync_Should_ReturnNotFound_WhenKeyNotFoundExceptionIsThrown()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new KeyNotFoundException("Test exception");

            // Act
            var result = await _exceptionHandler.TryHandleAsync(context, exception, CancellationToken.None);

            // Assert
            await AssertProblemDetailsResponse(context, StatusCodes.Status404NotFound, "Not Found", "Test exception");
            Assert.IsTrue(result); 
        }

        [Test]
        public async Task TryHandleAsync_Should_ReturnUnauthorized_WhenUnauthorizedAccessExceptionIsThrown()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new UnauthorizedAccessException("Test exception");

            // Act
            var result = await _exceptionHandler.TryHandleAsync(context, exception, CancellationToken.None);

            // Assert
            await AssertProblemDetailsResponse(context, StatusCodes.Status401Unauthorized, "Unauthorized", "Test exception");
            Assert.IsTrue(result);
        }

        [Test]
        public async Task TryHandleAsync_Should_ReturnBadRequest_WhenArgumentExceptionIsThrown()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new ArgumentException("Test exception");

            // Act
            var result = await _exceptionHandler.TryHandleAsync(context, exception, CancellationToken.None);

            // Assert
            await AssertProblemDetailsResponse(context, StatusCodes.Status400BadRequest, "Bad Request", "Test exception");
            Assert.IsTrue(result); 
        }

        [Test]
        public async Task TryHandleAsync_Should_ReturnInternalServerError_WhenGeneralExceptionIsThrown()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new InvalidOperationException("Test exception");

            // Act
            var result = await _exceptionHandler.TryHandleAsync(context, exception, CancellationToken.None);

            // Assert
            await AssertProblemDetailsResponse(context, StatusCodes.Status500InternalServerError, "Internal Server Error", "Test exception");
            Assert.IsTrue(result); 
        }

        private async Task AssertProblemDetailsResponse(HttpContext context, int expectedStatusCode, string expectedTitle, string expectedDetail)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var responseContent = await reader.ReadToEndAsync();

            var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(responseContent);

            Assert.That(context.Response.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(problemDetails!.Status, Is.EqualTo(expectedStatusCode));
            Assert.That(problemDetails.Title, Is.EqualTo(expectedTitle));
            Assert.That(problemDetails.Detail, Is.EqualTo(expectedDetail));
        }

    }
}
