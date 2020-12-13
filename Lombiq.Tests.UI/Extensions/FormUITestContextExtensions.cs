using Atata;
using Lombiq.Tests.UI.Helpers;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;

// Using the Atata namespace because that'll surely be among the using declarations of the test. OpenQA.Selenium not
// necessarily.
namespace Lombiq.Tests.UI.Extensions
{
    public static class FormUITestContextExtensions
    {
        public static void ClickAndFillInWithRetries(
            this UITestContext context,
            By by,
            string text,
            TimeSpan? timeout = null,
            TimeSpan? interval = null)
        {
            context.Get(by).Click();
            context.FillInWithRetries(by, text, timeout, interval);
        }

        public static void ClickAndClear(this UITestContext context, By by) =>
            context.ExecuteLogged(
                nameof(ClickAndClear),
                by,
                () =>
                {
                    var element = context.Get(by);
                    element.Click();
                    element.Clear();
                });

        /// <summary>
        /// Fills a form field with the given text, and retries if the value doesn't stick.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Even when the element is absolutely, positively there (Atata's Get() succeeds), Displayed == Enabled ==
        /// true, sometimes filling form fields still fails. Go figure...
        /// </para>
        /// </remarks>
        public static void FillInWithRetries(
            this UITestContext context,
            By by,
            string text,
            TimeSpan? timeout = null,
            TimeSpan? interval = null) =>
            context.ExecuteLogged(
                nameof(FillInWithRetries),
                $"{by} - \"{text}\"",
                () => ReliabilityHelper.DoWithRetries(
                    () =>
                    {
                        var element = context.Get(by);

                        if (text.Contains('@', StringComparison.OrdinalIgnoreCase))
                        {
                            element.Clear();

                            // On some platforms, probably due to keyboard settings, the @ character can be missing from the
                            // address when entered into a textfield so we need to use JS. The following solution doesn't
                            // work: https://stackoverflow.com/a/52202594/220230. This needs to be done in addition to the
                            // standard FillInWith() as without that some forms start to behave strange and not save values.
                            new Actions(context.Driver).SendKeys(element, text).Perform();
                        }
                        else
                        {
                            element.FillInWith(text);
                        }

                        return element.GetValue() == text;
                    },
                    timeout,
                    interval));
    }
}
