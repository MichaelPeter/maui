﻿using System;

namespace Microsoft.Maui.Handlers
{
	public partial class WebViewHandler : ViewHandler<IWebView, object>
	{
		protected override object CreateNativeView() => throw new NotImplementedException();

		public static void MapSource(IViewHandler handler, IWebView webView) { }

		public static void MapEval(IViewHandler handler, IWebView webView, object? arg) { }
	}
}