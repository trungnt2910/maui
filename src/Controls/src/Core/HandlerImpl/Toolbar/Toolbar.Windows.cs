﻿#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using Microsoft.UI.Xaml.Controls;
using WImage = Microsoft.UI.Xaml.Controls.Image;
using NativeAutomationProperties = Microsoft.UI.Xaml.Automation.AutomationProperties;

namespace Microsoft.Maui.Controls
{
	public partial class Toolbar
	{
		readonly ImageConverter _imageConverter = new ImageConverter();
		readonly ImageSourceIconElementConverter _imageSourceIconElementConverter = new ImageSourceIconElementConverter();

		NavigationRootManager? NavigationRootManager =>
			MauiContext?.GetNavigationRootManager();

		internal void UpdateMenu()
		{
			if (Handler.PlatformView is not MauiToolbar wh)
				return;

			var commandBar = wh.CommandBar;

			if (commandBar == null)
			{
				return;
			}

			commandBar.PrimaryCommands.Clear();
			commandBar.SecondaryCommands.Clear();

			List<ToolbarItem> toolbarItems = new List<ToolbarItem>(ToolbarItems ?? Array.Empty<ToolbarItem>());

			foreach (ToolbarItem item in toolbarItems)
			{
				var button = new AppBarButton();
				button.SetBinding(AppBarButton.LabelProperty, "Text");

				if (commandBar.IsDynamicOverflowEnabled && item.Order == ToolbarItemOrder.Secondary)
				{
					button.SetBinding(AppBarButton.IconProperty, "IconImageSource", _imageSourceIconElementConverter);
				}
				else if (!item.IconImageSource.IsNullOrEmpty())
				{
					var img = new WImage();
					img.SetBinding(WImage.SourceProperty, "Value");
					img.SetBinding(WImage.DataContextProperty, "IconImageSource", _imageConverter);
					button.Content = img;
				}

				button.Command = new MenuItemCommand(item);
				button.DataContext = item;
				button.SetValue(NativeAutomationProperties.AutomationIdProperty, item.AutomationId);
				button.SetAutomationPropertiesName(item);
				button.SetAutomationPropertiesAccessibilityView(item);
				button.SetAutomationPropertiesHelpText(item);
				button.UpdateTextColor(BarTextColor);

				button.SetAutomationPropertiesLabeledBy(item, null);

				ToolbarItemOrder order = item.Order == ToolbarItemOrder.Default ? ToolbarItemOrder.Primary : item.Order;
				if (order == ToolbarItemOrder.Primary)
				{
					commandBar.PrimaryCommands.Add(button);
				}
				else
				{
					commandBar.SecondaryCommands.Add(button);
				}
			}
		}

		public static void MapToolbarPlacement(IToolbarHandler arg1, Toolbar arg2)
		{
		}

		public static void MapToolbarDynamicOverflowEnabled(IToolbarHandler arg1, Toolbar arg2)
		{
			arg1.PlatformView.UpdateToolbarDynamicOverflowEnabled(arg2);
		}

		public static void MapBarTextColor(IToolbarHandler arg1, Toolbar arg2)
		{
			arg1.PlatformView.UpdateBarTextColor(arg2);
		}

		public static void MapBarBackground(IToolbarHandler arg1, Toolbar arg2)
		{
			arg1.PlatformView.UpdateBarBackground(arg2);
		}

		public static void MapBackButtonTitle(IToolbarHandler arg1, Toolbar arg2)
		{
			arg1.PlatformView.UpdateBackButton(arg2);
		}

		public static void MapToolbarItems(IToolbarHandler arg1, Toolbar arg2)
		{
			arg2.UpdateMenu();
		}

		public static void MapIconColor(IToolbarHandler arg1, Toolbar arg2)
		{
			arg1.PlatformView.UpdateIconColor(arg2);
		}

		public static void MapIcon(IToolbarHandler arg1, Toolbar arg2)
		{
		}

		public static void MapTitleView(IToolbarHandler arg1, Toolbar arg2)
		{
			arg1.PlatformView.UpdateTitleView(arg2);
		}

		public static void MapTitleIcon(IToolbarHandler arg1, Toolbar arg2)
		{
			arg1.PlatformView.UpdateTitleIcon(arg2);
		}

		public static void MapBackButtonVisible(IToolbarHandler arg1, Toolbar arg2)
		{
			arg1.PlatformView.UpdateBackButton(arg2);
		}

		public static void MapBackButtonEnabled(IToolbarHandler arg1, Toolbar arg2)
		{
			arg1.PlatformView.UpdateBackButton(arg2);
		}

		public static void MapIsVisible(IToolbarHandler arg1, Toolbar arg2)
		{
			arg1.PlatformView.UpdateIsVisible(arg2);
		}
	}
}
