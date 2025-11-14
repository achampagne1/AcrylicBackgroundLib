using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace AcrylicBackgroundLib
{
    public static class BlurEffect
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        // --- Attached Dependency Properties ---
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(BlurEffect),
                new PropertyMetadata(false, OnBlurPropertyChanged));

        public static readonly DependencyProperty AccentStateProperty =
            DependencyProperty.RegisterAttached(
                "AccentState",
                typeof(int),
                typeof(BlurEffect),
                new PropertyMetadata(3, OnBlurPropertyChanged));

        public static readonly DependencyProperty BlurOpacityProperty =
            DependencyProperty.RegisterAttached(
                "BlurOpacity",
                typeof(int),
                typeof(BlurEffect),
                new PropertyMetadata(0x99, OnBlurPropertyChanged));

        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.RegisterAttached(
                "BackgroundColor",
                typeof(int),
                typeof(BlurEffect),
                new PropertyMetadata(0x000000, OnBlurPropertyChanged));

        // --- Getters/Setters ---
        public static bool GetIsEnabled(DependencyObject obj) => (bool)obj.GetValue(IsEnabledProperty);
        public static void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(IsEnabledProperty, value);

        public static int GetAccentState(DependencyObject obj) => (int)obj.GetValue(AccentStateProperty);
        public static void SetAccentState(DependencyObject obj, int value) => obj.SetValue(AccentStateProperty, value);

        public static int GetBlurOpacity(DependencyObject obj) => (int)obj.GetValue(BlurOpacityProperty);
        public static void SetBlurOpacity(DependencyObject obj, int value) => obj.SetValue(BlurOpacityProperty, value);

        public static int GetBackgroundColor(DependencyObject obj) => (int)obj.GetValue(BackgroundColorProperty);
        public static void SetBackgroundColor(DependencyObject obj, int value) => obj.SetValue(BackgroundColorProperty, value);

        // --- Property Changed Logic ---
        private static void OnBlurPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Window window)
                return;

            // Only hook once
            window.SourceInitialized -= sourceInitialized;
            window.WindowStyle = WindowStyle.None;
            if (GetIsEnabled(window))
                window.SourceInitialized += sourceInitialized;
        }

        private static void sourceInitialized(object? sender, EventArgs e)
        {
            if (sender is Window window)
            {
                applyBlur(window);
            }
        }

        private static void applyBlur(Window window)
        {
            window.Background = Brushes.Transparent;
            var accent = new AccentPolicy
            {
                AccentState = GetAccentState(window),
                GradientColor = ((uint)percentToByte(GetBlurOpacity(window)) << 24) | (uint)GetBackgroundColor(window) & 0xFFFFFF
            };

            int accentStructSize = Marshal.SizeOf(accent);
            IntPtr accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            var helper = new WindowInteropHelper(window);
            SetWindowCompositionAttribute(helper.Handle, ref data);
            Marshal.FreeHGlobal(accentPtr);

            //for preventing minimizing on empty space click
            if (window.Content is Panel panel)
            {
                foreach (UIElement child in panel.Children)
                {
                    switch (child)
                    {
                        case Panel p:
                            p.Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));
                            break;
                        case Control c:
                            c.Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));
                            break;
                        case Border b:
                            b.Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));
                            break;
                    }
                }
            }
        }

        private static byte percentToByte(int value)
        {
            if(value < 0) value = 0;   
            if(value > 100) value = 100;
            return (byte)(value * 255 / 100);
        }
    }
}
