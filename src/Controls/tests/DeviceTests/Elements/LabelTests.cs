﻿using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using Xunit;

namespace Microsoft.Maui.DeviceTests
{
	[Category(TestCategory.Label)]
	public partial class LabelTests : HandlerTestBase
	{
		[Theory]
		[InlineData("Hello There", TextTransform.None, "Hello There")]
		[InlineData("Hello There", TextTransform.Uppercase, "HELLO THERE")]
		[InlineData("Hello There", TextTransform.Lowercase, "hello there")]
		public async Task TextTransformApplied(string text, TextTransform transform, string expected)
		{
			var label = new Label() { Text = text, TextTransform = transform };

			var handler = await CreateHandlerAsync<LabelHandler>(label);

			var nativeText = await InvokeOnMainThreadAsync(() =>
			{
				return handler.NativeView.Text;
			});

			Assert.Equal(expected, nativeText);
		}


		[Theory]
		[InlineData("There", TextTransform.None, "There")]
		[InlineData("There", TextTransform.Uppercase, "THERE")]
		[InlineData("There", TextTransform.Lowercase, "there")]
		public async Task FormattedStringSpanTextTransformApplied(string text, TextTransform transform, string expected)
		{
			var formattedString = new FormattedString();
			formattedString.Spans.Add(new Span { Text = "Hello" });
			formattedString.Spans.Add(new Span { Text = text, TextTransform = transform });

			var label = new Label { FormattedText = formattedString };

			var handler = await CreateHandlerAsync<LabelHandler>(label);

			var nativeText = await InvokeOnMainThreadAsync(() =>
			{
				return handler.NativeView.AttributedText?.Value;
			});

			Assert.Equal("Hello" + expected, nativeText);
		}

		[Fact]
		public async Task FormattedStringSpanTextTransformOverridesLabelTextTransform()
		{
			var formattedString = new FormattedString();
			formattedString.Spans.Add(new Span { Text = "HELLO" });
			formattedString.Spans.Add(new Span { Text = "WORLD", TextTransform = TextTransform.None });
			formattedString.Spans.Add(new Span { Text = "new", TextTransform = TextTransform.Uppercase });

			var label = new Label { FormattedText = formattedString, TextTransform = TextTransform.Lowercase };

			var handler = await CreateHandlerAsync<LabelHandler>(label);

			var nativeText = await InvokeOnMainThreadAsync(() =>
			{
				return handler.NativeView.AttributedText?.Value;
			});

			Assert.Equal("helloWORLDNEW", nativeText);
		}
	}
}