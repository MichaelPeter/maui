using ObjCRuntime;
using UIKit;
using NativeView = UIKit.UIView;

namespace Microsoft.Maui.Controls.Platform
{
	public static class AccessibilityExtensions
	{
		public static void SetAccessibilityProperties(this NativeView nativeViewElement, Element element)
		{
			if (element == null)
				return;

			nativeViewElement.AccessibilityIdentifier = element?.AutomationId;
			SetAccessibilityLabel(nativeViewElement, element);
			SetAccessibilityHint(nativeViewElement, element);
			SetIsAccessibilityElement(nativeViewElement, element);
			SetAccessibilityElementsHidden(nativeViewElement, element);
		}

		public static string SetAccessibilityHint(this NativeView Control, Element Element, string _defaultAccessibilityHint = null)
		{
			if (Element == null || Control == null)
				return _defaultAccessibilityHint;

			if (_defaultAccessibilityHint == null)
				_defaultAccessibilityHint = Control.AccessibilityHint;

			Control.AccessibilityHint = (string)Element.GetValue(AutomationProperties.HelpTextProperty) ?? _defaultAccessibilityHint;

			return _defaultAccessibilityHint;
		}

		public static string SetAccessibilityLabel(this NativeView Control, Element Element, string _defaultAccessibilityLabel = null)
		{
			if (Element == null || Control == null)
				return _defaultAccessibilityLabel;

			if (_defaultAccessibilityLabel == null)
				_defaultAccessibilityLabel = Control.AccessibilityLabel;

			Control.AccessibilityLabel = (string)Element.GetValue(AutomationProperties.NameProperty) ?? _defaultAccessibilityLabel;

			return _defaultAccessibilityLabel;
		}

		public static string SetAccessibilityHint(this UIBarItem Control, Element Element, string _defaultAccessibilityHint = null)
		{
			if (Element == null || Control == null)
				return _defaultAccessibilityHint;

			if (_defaultAccessibilityHint == null)
				_defaultAccessibilityHint = Control.AccessibilityHint;

			Control.AccessibilityHint = (string)Element.GetValue(AutomationProperties.HelpTextProperty) ?? _defaultAccessibilityHint;

			return _defaultAccessibilityHint;

		}

		public static string SetAccessibilityLabel(this UIBarItem Control, Element Element, string _defaultAccessibilityLabel = null)
		{
			if (Element == null || Control == null)
				return _defaultAccessibilityLabel;

			if (_defaultAccessibilityLabel == null)
				_defaultAccessibilityLabel = Control.AccessibilityLabel;

			Control.AccessibilityLabel = (string)Element.GetValue(AutomationProperties.NameProperty) ?? _defaultAccessibilityLabel;

			return _defaultAccessibilityLabel;
		}

		public static bool? SetIsAccessibilityElement(this NativeView Control, Element Element, bool? _defaultIsAccessibilityElement = null)
		{
			if (Element == null || Control == null)
				return _defaultIsAccessibilityElement;

			// If the user hasn't set IsInAccessibleTree then just don't do anything
			if (!Element.IsSet(AutomationProperties.IsInAccessibleTreeProperty))
				return null;

			if (!_defaultIsAccessibilityElement.HasValue)
			{
				// iOS sets the default value for IsAccessibilityElement late in the layout cycle
				// But if we set it to false ourselves then that causes it to act like it's false

				// from the docs:
				// https://developer.apple.com/documentation/objectivec/nsobject/1615141-isaccessibilityelement
				// The default value for this property is false unless the receiver is a standard UIKit control,
				// in which case the value is true.
				//
				// So we just base the default on that logic				
				_defaultIsAccessibilityElement = Control.IsAccessibilityElement || Control is UIControl;
			}

			Control.IsAccessibilityElement = (bool)((bool?)Element.GetValue(AutomationProperties.IsInAccessibleTreeProperty) ?? _defaultIsAccessibilityElement);


			return _defaultIsAccessibilityElement;
		}

		public static bool? SetAccessibilityElementsHidden(this NativeView Control, Element Element, bool? _defaultAccessibilityElementsHidden = null)
		{
			if (Element == null || Control == null)
				return _defaultAccessibilityElementsHidden;

			if (!Element.IsSet(AutomationProperties.ExcludedWithChildrenProperty))
				return null;

			if (!_defaultAccessibilityElementsHidden.HasValue)
			{
				_defaultAccessibilityElementsHidden = Control.AccessibilityElementsHidden || Control is UIControl;
			}

			return _defaultAccessibilityElementsHidden;
		}
	}
}
