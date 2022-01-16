// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebView.WebView2;
using Microsoft.Web.WebView2.Core;
using WebView2Control = Microsoft.Web.WebView2.Wpf.WebView2;

namespace Microsoft.AspNetCore.Components.WebView.Wpf
{
	internal class WpfWebView2Wrapper : IWebView2Wrapper
	{
		private readonly WpfCoreWebView2Wrapper _coreWebView2Wrapper;
		private readonly BlazorWebView _blazorWebView;

		public WpfWebView2Wrapper(WebView2Control webView2, BlazorWebView blazorWebView)
		{
			if (webView2 is null)
			{
				throw new ArgumentNullException(nameof(webView2));
			}

			WebView2 = webView2;
			_blazorWebView = blazorWebView;
			_coreWebView2Wrapper = new WpfCoreWebView2Wrapper(this);
		}

		public ICoreWebView2Wrapper CoreWebView2 => _coreWebView2Wrapper;

		public Uri Source
		{
			get => WebView2.Source;
			set => WebView2.Source = value;
		}

		public WebView2Control WebView2 { get; }

		public CoreWebView2Environment Environment { get; set; }

		public async Task CreateEnvironmentAsync()
		{
			CoreWebView2EnvironmentOptions options = new CoreWebView2EnvironmentOptions();
			var args = new WebViewInitEventArgs(options);

			_blazorWebView.RaiseInitializingWebViewEvent(args);
			Environment = await CoreWebView2Environment.CreateAsync(
				args.CoreWebView2BrowserExecutableFolder, args.CoreWebView2UserDataFolder, args.CoreWebView2EnvironmentOptions);
		}

		public Task EnsureCoreWebView2Async()
		{
			return WebView2.EnsureCoreWebView2Async(Environment);
		}
	}
}
