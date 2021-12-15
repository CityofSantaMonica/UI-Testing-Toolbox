using Lombiq.Tests.UI.MonkeyTesting;
using Lombiq.Tests.UI.Services;

namespace Lombiq.Tests.UI.Extensions
{
    /// <summary>
    /// Provides a set of extension methods for monkey testing.
    /// </summary>
    public static class MonkeyTestingUITestContextExtensions
    {
        /// <summary>
        /// Tests the current page as monkey. Test finishes by timeout or when the current page is left during testing.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="randomSeed">The random seed.</param>
        /// <returns>The same <see cref="UITestContext"/> instance.</returns>
        public static UITestContext TestCurrentPageAsMonkey(
            this UITestContext context,
            MonkeyTestingOptions options = null,
            int? randomSeed = null)
        {
            var monkeyTester = new MonkeyTester(context, options);
            monkeyTester.TestOnePage(randomSeed);

            return context;
        }

        /// <summary>
        /// Tests the current page as monkey recursively. When current page is left during test, continues to test the
        /// other page and so on. Each page is tested for <see cref="MonkeyTestingOptions.PageTestTime"/> value of
        /// <paramref name="options"/>.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>The same <see cref="UITestContext"/> instance.</returns>
        public static UITestContext TestCurrentPageAsMonkeyRecursively(
            this UITestContext context,
            MonkeyTestingOptions options = null)
        {
            var monkeyTester = new MonkeyTester(context, options);
            monkeyTester.TestRecursively();

            return context;
        }
    }
}
