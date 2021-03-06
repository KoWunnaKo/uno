﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Private.Infrastructure;
using Windows.UI.Xaml.Controls;
#if __IOS__
using UIKit;
#elif __MACOS__
using AppKit;
#else
using Uno.UI.Extensions;
#endif

namespace Uno.UI.RuntimeTests.Tests.Windows_UI_Xaml_Controls
{
	[TestClass]
	public class Given_Native_View
	{
		[TestMethod]
		[RunsOnUIThread]
		public async Task When_Added_In_Xaml()
		{
			var page = new NativeView_Page();

			TestServices.WindowHelper.WindowContent = page;
			await TestServices.WindowHelper.WaitForIdle();

			var nativeInPanel = page.hostPanel.FindFirstChild<NativeView>();
			Assert.IsNotNull(nativeInPanel);

			var nativeInBorder = page.hostBorder.FindFirstChild<NativeView>();
			Assert.IsNotNull(nativeInBorder);

			var nativeInButton = page.hostButton.FindFirstChild<NativeView>();
			Assert.IsNotNull(nativeInButton);

			var nativeInSplitViewPane = page.hostSplitView.Pane?.FindFirstChild<NativeView>();
			Assert.IsNotNull(nativeInSplitViewPane);

			var nativeInSplitViewContent = page.hostSplitView.Content?.FindFirstChild<NativeView>();
			Assert.IsNotNull(nativeInSplitViewContent);

			page.hostPopup.IsOpen = true;

			await TestServices.WindowHelper.WaitForIdle();
			var nativeInPopup = page.hostPopup.Child?.FindFirstChild<NativeView>();
			Assert.IsNotNull(nativeInPopup);

			page.hostPopup.IsOpen = false;

			await TestServices.WindowHelper.WaitForIdle();

			page.flyoutHostButton.Flyout.ShowAt(page.flyoutHostButton);

			await TestServices.WindowHelper.WaitForIdle();

			var nativeInFlyout = (page.flyoutHostButton.Flyout as Flyout).Content?.FindFirstChild<NativeView>();
			Assert.IsNotNull(nativeInFlyout);
			page.flyoutHostButton.Flyout.Hide();
			TestServices.WindowHelper.WindowContent = null;
		}
	}
}
