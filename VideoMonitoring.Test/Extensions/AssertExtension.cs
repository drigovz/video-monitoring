using System;

namespace Xunit
{
    public static class AssertExtension
    {
        public static void AssertThrowsWithMessage(this ArgumentException exception, string message)
        {
            if (exception.Message == message)
                Assert.True(true);
            else
                Assert.False(true, $"\nReceived message: '{message}'\n");
        }
    }
}
