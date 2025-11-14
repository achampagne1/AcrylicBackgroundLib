AcrylicBackgroundLib

A tiny, simple WPF helper library that enables Windows 10/11 acrylic blur on any WPF window with just a few XAML attributes.

The library is designed to be minimal, flexible, and non-opinionated.
You keep full control over your UI — this library only applies the blur.

⭐ Features

✔️ Enable acrylic / blur behind a WPF window

✔️ Adjustable opacity

✔️ Custom RGB background color

✔️ Supports all Windows Accent states

✔️ Automatically sets WindowStyle=None so you are free to design your own chrome

✔️ Extremely simple to use — zero C# code required

✔️ Works on .NET 6/7/8 WPF

📥 Installation

You can install the library in two ways:

Option 1 — Clone and Build Yourself

Clone the repository

Open in Visual Studio

Build the project

The compiled DLL will be in:

AcrylicBackgroundLib/bin/Release/net8.0-windows/AcrylicBackgroundLib.dll

Option 2 — Download Precompiled DLL

Go to:
👉 Releases → Download AcrylicBackgroundLib.dll

🔧 Add the DLL to Your WPF Project

Edit your .csproj file and add:

```xml
<ItemGroup>
    <Reference Include="AcrylicBackgroundLib">
        <HintPath>path-to-your-dll/AcrylicBackgroundLib.dll</HintPath>
        <Private>true</Private>
    </Reference>
</ItemGroup>
```

🖼️ Enable Acrylic Blur in Your Window

In your MainWindow.xaml:

1. Add XML namespace
```xml
xmlns:acrylic="clr-namespace:AcrylicBackgroundLib;assembly=AcrylicBackgroundLib"
```
3. Apply blur settings to the window
```xml
acrylic:BlurEffect.IsEnabled="True"
acrylic:BlurEffect.AccentState="4"
acrylic:BlurEffect.BlurOpacity="20"
acrylic:BlurEffect.BackgroundColor="0x2600ff"
```

That's it.
The blur applies automatically when the window initializes.

⚠️ Important Notes
The library removes the default window chrome

The library sets:
```xml
WindowStyle=None
AllowsTransparency=True
```


This means:

You must implement your own Minimize, Maximize, and Close buttons

This is intentional — the library stays simple and does not force any UI design

All top-level children get a nearly-transparent background

This prevents the window from minimizing when clicking empty areas.

🎨 Accent States Explained

AccentState controls which effect is applied.
```xml
Value	Name	Description
0	ACCENT_DISABLED	No effect
1	ACCENT_ENABLE_GRADIENT	Solid color gradient
2	ACCENT_ENABLE_TRANSPARENTGRADIENT	Transparent gradient
3	ACCENT_ENABLE_BLURBEHIND	Older style blur (Windows 7-like)
4	ACCENT_ENABLE_ACRYLICBLURBEHIND	Fluent Acrylic — recommended
5	ACCENT_ENABLE_HOSTBACKDROP	Uses system backdrop
6	ACCENT_ENABLE_BLURBEHINDWITHBLURREGION	Blur with region support
7	ACCENT_ENABLE_ACRYLICBLURBEHINDBLACK	Acrylic but darker
```
Recommended:
```xml
AccentState = 4  // Acrylic blur
```

✨ Example Window
```xml
<Window x:Class="WpfApp1.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:acrylic="clr-namespace:AcrylicBackgroundLib;assembly=AcrylicBackgroundLib"
        xmlns:local="clr-namespace:WpfApp1"
        mc:Ignorable="d"
        acrylic:BlurEffect.IsEnabled="True" <!--Enables blur -->
        acrylic:BlurEffect.AccentState="4" <!--Sets Accent State -->
        acrylic:BlurEffect.BlurOpacity="20" <!--Sets Opacity -->
        acrylic:BlurEffect.BackgroundColor="0xffffff" <!--Sets Color -->
        Title="MainWindow" Height="450" Width="800"
        WindowStartupLocation="CenterScreen">
    <Grid>

    </Grid>
</Window>
```

📦 Library Source Code

The full library code is included in the repository and is intended to be readable and modifiable.
It contains:

Attached dependency properties

AccentPolicy interop

Alpha + RGB/BGR color handling

Window behavior fixes

❤️ Philosophy

This library is intentionally:

Small

Unopinionated

Easy to integrate

UI-agnostic

Zero external dependencies

You're free to build any custom chrome, any layout, any UI behavior — the library only gives you the blur.

📝 License

MIT License.
