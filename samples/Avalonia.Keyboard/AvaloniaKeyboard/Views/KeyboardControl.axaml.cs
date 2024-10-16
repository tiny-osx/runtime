using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using Avalonia.VisualTree;

namespace AvaloniaKeyboard.Views;

public partial class KeyboardControl : TemplatedControl
{
    public static readonly StyledProperty<int> KeySizeProperty =
            AvaloniaProperty.Register<KeyboardControl, int>(nameof(KeySize), 50);
    public static readonly StyledProperty<bool> IsCapsProperty =
           AvaloniaProperty.Register<KeyboardControl, bool>(nameof(IsCaps), false);
    public static readonly StyledProperty<bool> IsShiftProperty =
           AvaloniaProperty.Register<KeyboardControl, bool>(nameof(IsShift), false);
    public static readonly StyledProperty<TextBox?> TextBoxProperty =
           AvaloniaProperty.Register<KeyboardControl, TextBox?>(nameof(TextBox));

    public int KeySize
    {
        get { return GetValue(KeySizeProperty); }
        set { SetValue(KeySizeProperty, value); }
    }
    public bool IsCaps
    {
        get { return GetValue(IsCapsProperty); }
        set { SetValue(IsCapsProperty, value); }
    }
    public bool IsShift
    {
        get { return GetValue(IsShiftProperty); }
        set { SetValue(IsShiftProperty, value); }
    }

    public TextBox? TextBox
    {
        get { return GetValue(TextBoxProperty); }
        set { SetValue(TextBoxProperty, value); }
    }

    private readonly ITransform _matrix = new MatrixTransform(Matrix.CreateScale(0.95, 0.95));

    internal TextBlock? Input;
    internal TextBlock? InputPage;
    internal WrapPanel? InputSelect;

    internal Border Pad01 = new Border();
    internal Border Pad02 = new Border();
    internal Border Pad03 = new Border();
    internal Border Pad04 = new Border();
    internal Border Pad05 = new Border();
    internal Border Pad06 = new Border();
    internal Border Pad07 = new Border();
    internal Border Pad08 = new Border();
    internal Border Pad09 = new Border();
    internal Border Pad10 = new Border();
    internal Border Pad11 = new Border();
    internal Border Pad12 = new Border();
    internal Border Pad13 = new Border();
    internal Border Pad14 = new Border();
    internal Border Pad15 = new Border();
    internal Border Pad16 = new Border();
    internal Border Pad17 = new Border();
    internal Border Pad18 = new Border();
    internal Border Pad19 = new Border();
    internal Border Pad20 = new Border();
    internal Border Pad21 = new Border();
    internal Border Pad22 = new Border();
    internal Border Pad23 = new Border();
    internal Border Pad24 = new Border();
    internal Border Pad25 = new Border();
    internal Border Pad26 = new Border();

    internal Border Caps = new Border();
    internal Border Back = new Border();
    internal Border Alt = new Border();
    internal Border Shift = new Border();
    internal Border Space = new Border();
    internal Border Return = new Border();
    internal Border Exit = new Border();

    protected override void OnApplyTemplate(TemplateAppliedEventArgs args)
    {
        Input = args.NameScope.Find<TextBlock>("Input")!;
        InputPage = args.NameScope.Find<TextBlock>("InputPage")!;
        InputSelect = args.NameScope.Find<WrapPanel>("InputSelect")!;

        Pad01 = args.NameScope.Find<Border>("Pad01") ?? throw new ArgumentNullException();
        Pad01.PointerPressed += OnKeyPressed;
        Pad01.PointerReleased += OnKeyReleased;

        Pad02 = args.NameScope.Find<Border>("Pad02") ?? throw new ArgumentNullException();
        Pad02.PointerPressed += OnKeyPressed;
        Pad02.PointerReleased += OnKeyReleased;

        Pad03 = args.NameScope.Find<Border>("Pad03") ?? throw new ArgumentNullException();
        Pad03.PointerPressed += OnKeyPressed;
        Pad03.PointerReleased += OnKeyReleased;

        Pad04 = args.NameScope.Find<Border>("Pad04") ?? throw new ArgumentNullException();
        Pad04.PointerPressed += OnKeyPressed;
        Pad04.PointerReleased += OnKeyReleased;

        Pad05 = args.NameScope.Find<Border>("Pad05") ?? throw new ArgumentNullException();
        Pad05.PointerPressed += OnKeyPressed;
        Pad05.PointerReleased += OnKeyReleased;

        Pad06 = args.NameScope.Find<Border>("Pad06") ?? throw new ArgumentNullException();
        Pad06.PointerPressed += OnKeyPressed;
        Pad06.PointerReleased += OnKeyReleased;

        Pad07 = args.NameScope.Find<Border>("Pad07") ?? throw new ArgumentNullException();
        Pad07.PointerPressed += OnKeyPressed;
        Pad07.PointerReleased += OnKeyReleased;

        Pad08 = args.NameScope.Find<Border>("Pad08") ?? throw new ArgumentNullException();
        Pad08.PointerPressed += OnKeyPressed;
        Pad08.PointerReleased += OnKeyReleased;

        Pad09 = args.NameScope.Find<Border>("Pad09") ?? throw new ArgumentNullException();
        Pad09.PointerPressed += OnKeyPressed;
        Pad09.PointerReleased += OnKeyReleased;

        Pad10 = args.NameScope.Find<Border>("Pad10") ?? throw new ArgumentNullException();
        Pad10.PointerPressed += OnKeyPressed;
        Pad10.PointerReleased += OnKeyReleased;

        Pad11 = args.NameScope.Find<Border>("Pad11") ?? throw new ArgumentNullException();
        Pad11.PointerPressed += OnKeyPressed;
        Pad11.PointerReleased += OnKeyReleased;

        Pad12 = args.NameScope.Find<Border>("Pad12") ?? throw new ArgumentNullException();
        Pad12.PointerPressed += OnKeyPressed;
        Pad12.PointerReleased += OnKeyReleased;

        Pad13 = args.NameScope.Find<Border>("Pad13") ?? throw new ArgumentNullException();
        Pad13.PointerPressed += OnKeyPressed;
        Pad13.PointerReleased += OnKeyReleased;

        Pad14 = args.NameScope.Find<Border>("Pad14") ?? throw new ArgumentNullException();
        Pad14.PointerPressed += OnKeyPressed;
        Pad14.PointerReleased += OnKeyReleased;

        Pad15 = args.NameScope.Find<Border>("Pad15") ?? throw new ArgumentNullException();
        Pad15.PointerPressed += OnKeyPressed;
        Pad15.PointerReleased += OnKeyReleased;

        Pad16 = args.NameScope.Find<Border>("Pad16") ?? throw new ArgumentNullException();
        Pad16.PointerPressed += OnKeyPressed;
        Pad16.PointerReleased += OnKeyReleased;

        Pad17 = args.NameScope.Find<Border>("Pad17") ?? throw new ArgumentNullException();
        Pad17.PointerPressed += OnKeyPressed;
        Pad17.PointerReleased += OnKeyReleased;

        Pad18 = args.NameScope.Find<Border>("Pad18") ?? throw new ArgumentNullException();
        Pad18.PointerPressed += OnKeyPressed;
        Pad18.PointerReleased += OnKeyReleased;

        Pad19 = args.NameScope.Find<Border>("Pad19") ?? throw new ArgumentNullException();
        Pad19.PointerPressed += OnKeyPressed;
        Pad19.PointerReleased += OnKeyReleased;

        Pad20 = args.NameScope.Find<Border>("Pad20") ?? throw new ArgumentNullException();
        Pad20.PointerPressed += OnKeyPressed;
        Pad20.PointerReleased += OnKeyReleased;

        Pad21 = args.NameScope.Find<Border>("Pad21") ?? throw new ArgumentNullException();
        Pad21.PointerPressed += OnKeyPressed;
        Pad21.PointerReleased += OnKeyReleased;

        Pad22 = args.NameScope.Find<Border>("Pad22") ?? throw new ArgumentNullException();
        Pad22.PointerPressed += OnKeyPressed;
        Pad22.PointerReleased += OnKeyReleased;

        Pad23 = args.NameScope.Find<Border>("Pad23") ?? throw new ArgumentNullException();
        Pad23.PointerPressed += OnKeyPressed;
        Pad23.PointerReleased += OnKeyReleased;

        Pad24 = args.NameScope.Find<Border>("Pad24") ?? throw new ArgumentNullException();
        Pad24.PointerPressed += OnKeyPressed;
        Pad24.PointerReleased += OnKeyReleased;

        Pad25 = args.NameScope.Find<Border>("Pad25") ?? throw new ArgumentNullException();
        Pad25.PointerPressed += OnKeyPressed;
        Pad25.PointerReleased += OnKeyReleased;

        Pad26 = args.NameScope.Find<Border>("Pad26") ?? throw new ArgumentNullException();
        Pad26.PointerPressed += OnKeyPressed;
        Pad26.PointerReleased += OnKeyReleased;

        Caps = args.NameScope.Find<Border>("Caps") ?? throw new ArgumentNullException();
        Caps.PointerPressed += OnCapsPressed;
        Caps.PointerReleased += OnKeyReleased;

        Shift = args.NameScope.Find<Border>("Shift") ?? throw new ArgumentNullException();
        Shift.PointerPressed += OnShiftPressed;
        Shift.PointerReleased += OnKeyReleased;

        Space = args.NameScope.Find<Border>("Space") ?? throw new ArgumentNullException();
        Space.PointerPressed += OnSpacePressed;
        Space.PointerReleased += OnKeyReleased;

        Back = args.NameScope.Find<Border>("Back") ?? throw new ArgumentNullException();
        Back.PointerPressed += OnBackPressed;
        Back.PointerReleased += OnKeyReleased;

        Return = args.NameScope.Find<Border>("Return") ?? throw new ArgumentNullException();
        Return.PointerPressed += OnReturnPressed;
        Return.PointerReleased += OnKeyReleased;
    }

    private void OnKeyPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border border)
        {
            border.RenderTransform = _matrix;
        }

        if (TextBox is { } box)
        {
            var obj = (Border)sender!;
            if (obj.Child != null)
            {
                var child = (TextBlock)obj.Child;
                if (child.Text != null)
                {
                    InsertText(box, child.Text);
                }
            }
        }
    }

    private void OnCapsPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border border)
        {
            border.RenderTransform = _matrix;
        }

        if (Caps == null || Caps.Child == null)
            return;

        if (Shift == null || Shift.Child == null)
            return;

        if (IsShift)
        {
            if (IsCaps)
            {
                IsCaps = false;
                ((TextBlock)Caps.Child).Text = "#+=";
                ((TextBlock)Pad01.Child!).Text = "1";
                ((TextBlock)Pad02.Child!).Text = "2";
                ((TextBlock)Pad03.Child!).Text = "3";
                ((TextBlock)Pad04.Child!).Text = "4";
                ((TextBlock)Pad05.Child!).Text = "5";
                ((TextBlock)Pad06.Child!).Text = "6";
                ((TextBlock)Pad07.Child!).Text = "7";
                ((TextBlock)Pad08.Child!).Text = "8";
                ((TextBlock)Pad09.Child!).Text = "9";
                ((TextBlock)Pad10.Child!).Text = "0";
                ((TextBlock)Pad11.Child!).Text = "-";
                ((TextBlock)Pad12.Child!).Text = "/";
                ((TextBlock)Pad13.Child!).Text = ":";
                ((TextBlock)Pad14.Child!).Text = ";";
                ((TextBlock)Pad15.Child!).Text = "(";
                ((TextBlock)Pad16.Child!).Text = ")";
                ((TextBlock)Pad17.Child!).Text = "$";
                ((TextBlock)Pad18.Child!).Text = "&";
                ((TextBlock)Pad19.Child!).Text = "@";
                ((TextBlock)Pad20.Child!).Text = "\"";
                ((TextBlock)Pad21.Child!).Text = ".";
                ((TextBlock)Pad22.Child!).Text = ",";
                ((TextBlock)Pad23.Child!).Text = "?";
                ((TextBlock)Pad24.Child!).Text = "!";
                ((TextBlock)Pad25.Child!).Text = ",";
                ((TextBlock)Pad26.Child!).Text = "`";
            }
            else
            {
                IsCaps = true;
                ((TextBlock)Caps.Child).Text = "123";
                ((TextBlock)Pad01.Child!).Text = "[";
                ((TextBlock)Pad02.Child!).Text = "]";
                ((TextBlock)Pad03.Child!).Text = "{";
                ((TextBlock)Pad04.Child!).Text = "}";
                ((TextBlock)Pad05.Child!).Text = "#";
                ((TextBlock)Pad06.Child!).Text = "%";
                ((TextBlock)Pad07.Child!).Text = "^";
                ((TextBlock)Pad08.Child!).Text = "*";
                ((TextBlock)Pad09.Child!).Text = "+";
                ((TextBlock)Pad10.Child!).Text = "=";
                ((TextBlock)Pad11.Child!).Text = "_";
                ((TextBlock)Pad12.Child!).Text = "\\";
                ((TextBlock)Pad13.Child!).Text = "|";
                ((TextBlock)Pad14.Child!).Text = "~";
                ((TextBlock)Pad15.Child!).Text = "<";
                ((TextBlock)Pad16.Child!).Text = ">";
                ((TextBlock)Pad17.Child!).Text = "£";
                ((TextBlock)Pad18.Child!).Text = "¢";
                ((TextBlock)Pad19.Child!).Text = "€";
                ((TextBlock)Pad20.Child!).Text = "º";
                ((TextBlock)Pad21.Child!).Text = "™";
                ((TextBlock)Pad22.Child!).Text = "®";
                ((TextBlock)Pad23.Child!).Text = "©";
                ((TextBlock)Pad24.Child!).Text = "¶";
                ((TextBlock)Pad25.Child!).Text = "•";
                ((TextBlock)Pad26.Child!).Text = "√";
            }
        }
        else
        {
            if (IsCaps)
            {
                IsCaps = false;
                Caps.Classes.Remove("k2");
                foreach (var item in this.GetVisualDescendants().OfType<Border>())
                {
                    try
                    {
                        TextBlock value = (TextBlock)item.Child!;
                        if (value.Text != null)
                        {
                            if ("azertyuiopqsdfghjklmwxcvbn".Contains(value.Text, StringComparison.OrdinalIgnoreCase))
                                value.Text = value.Text.ToLower();
                        }
                    }
                    catch (Exception) { }
                }
            }
            else
            {
                IsCaps = true;
                Caps.Classes.Add("k2");
                foreach (var item in this.GetVisualDescendants().OfType<Border>())
                {
                    try
                    {
                        TextBlock value = (TextBlock)item.Child!;
                        if (value.Text != null)
                        {
                            if ("azertyuiopqsdfghjklmwxcvbn".Contains(value.Text, StringComparison.OrdinalIgnoreCase))
                                value.Text = value.Text.ToUpper();
                        }
                    }
                    catch (Exception) { }
                }
            }
        }
    }

    private void OnShiftPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border border)
        {
            border.RenderTransform = _matrix;
        }

        if (Shift.Child == null)
            return;

        if (Caps.Child == null)
            return;

        Caps.Classes.Remove("k2");

        if (IsShift)
        {
            IsShift = false;
            ((TextBlock)Shift.Child).Text = "123";
            ((TextBlock)Caps.Child).Text = "Caps";
            ((TextBlock)Pad01.Child!).Text = "q";
            ((TextBlock)Pad02.Child!).Text = "w";
            ((TextBlock)Pad03.Child!).Text = "e";
            ((TextBlock)Pad04.Child!).Text = "r";
            ((TextBlock)Pad05.Child!).Text = "t";
            ((TextBlock)Pad06.Child!).Text = "y";
            ((TextBlock)Pad07.Child!).Text = "u";
            ((TextBlock)Pad08.Child!).Text = "i";
            ((TextBlock)Pad09.Child!).Text = "o";
            ((TextBlock)Pad10.Child!).Text = "p";
            ((TextBlock)Pad11.Child!).Text = "a";
            ((TextBlock)Pad12.Child!).Text = "s";
            ((TextBlock)Pad13.Child!).Text = "d";
            ((TextBlock)Pad14.Child!).Text = "f";
            ((TextBlock)Pad15.Child!).Text = "g";
            ((TextBlock)Pad16.Child!).Text = "h";
            ((TextBlock)Pad17.Child!).Text = "j";
            ((TextBlock)Pad18.Child!).Text = "k";
            ((TextBlock)Pad19.Child!).Text = "l";
            ((TextBlock)Pad20.Child!).Text = "z";
            ((TextBlock)Pad21.Child!).Text = "x";
            ((TextBlock)Pad22.Child!).Text = "c";
            ((TextBlock)Pad23.Child!).Text = "v";
            ((TextBlock)Pad24.Child!).Text = "b";
            ((TextBlock)Pad25.Child!).Text = "n";
            ((TextBlock)Pad26.Child!).Text = "m";
        }
        else
        {
            IsShift = true;
            ((TextBlock)Shift.Child).Text = "ABC";
            ((TextBlock)Caps.Child).Text = "#+=";
            ((TextBlock)Pad01.Child!).Text = "1";
            ((TextBlock)Pad02.Child!).Text = "2";
            ((TextBlock)Pad03.Child!).Text = "3";
            ((TextBlock)Pad04.Child!).Text = "4";
            ((TextBlock)Pad05.Child!).Text = "5";
            ((TextBlock)Pad06.Child!).Text = "6";
            ((TextBlock)Pad07.Child!).Text = "7";
            ((TextBlock)Pad08.Child!).Text = "8";
            ((TextBlock)Pad09.Child!).Text = "9";
            ((TextBlock)Pad10.Child!).Text = "0";
            ((TextBlock)Pad11.Child!).Text = "-";
            ((TextBlock)Pad12.Child!).Text = "/";
            ((TextBlock)Pad13.Child!).Text = ":";
            ((TextBlock)Pad14.Child!).Text = ";";
            ((TextBlock)Pad15.Child!).Text = "(";
            ((TextBlock)Pad16.Child!).Text = ")";
            ((TextBlock)Pad17.Child!).Text = "$";
            ((TextBlock)Pad18.Child!).Text = "&";
            ((TextBlock)Pad19.Child!).Text = "@";
            ((TextBlock)Pad20.Child!).Text = "\"";
            ((TextBlock)Pad21.Child!).Text = ".";
            ((TextBlock)Pad22.Child!).Text = ",";
            ((TextBlock)Pad23.Child!).Text = "?";
            ((TextBlock)Pad24.Child!).Text = "!";
            ((TextBlock)Pad25.Child!).Text = ",";
            ((TextBlock)Pad26.Child!).Text = "`";
        }
    }

    private void OnSpacePressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border border)
        {
            border.RenderTransform = _matrix;
        }

        if (TextBox is { } box)
        {
           InsertText(box, " "); 
        }
    }

    private void OnBackPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border border)
        {
            border.RenderTransform = _matrix;
        }

        if (TextBox is { } box)
        {
            DeleteText(box);
        }
    }

    private void OnKeyReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (sender is Border border)
        {
            border.RenderTransform = null;
        }
    }

    private void OnReturnPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border border)
        {
            border.RenderTransform = _matrix;
        }

        if (TextBox is { } box)
        {
            InsertText(box, "\r\n");
        }
    }

    private static void InsertText(TextBox textBox, string text)
    {
        int insertionIndex = textBox.SelectionStart;

        if (textBox.Text == null)
        {
            textBox.Text = text;
        }
        else
        {
            int selectionLength = textBox.SelectionEnd - textBox.SelectionStart;
            if (selectionLength > 0)
            {
                textBox.Text = textBox.Text.Remove(insertionIndex, selectionLength);
            }

            textBox.Text = textBox.Text.Insert(insertionIndex, text);
        }

        textBox.SelectionStart = textBox.SelectionEnd = insertionIndex + text.Length;
    }

    private static void DeleteTextBeforeCursor(TextBox textBox)
    {
        int cursorPosition = textBox.SelectionStart;
        if (cursorPosition > 0 && textBox.Text != null)
        {
            textBox.Text = textBox.Text.Remove(cursorPosition - 1, 1);
            textBox.SelectionEnd = textBox.SelectionStart = cursorPosition - 1;
        }
    }

    private static void DeleteSelectedText(TextBox textBox)
    {
        if (textBox.Text != null)
        {
            int selectionStart = textBox.SelectionStart;
            int selectionLength = textBox.SelectionEnd - textBox.SelectionStart;

            if (selectionLength > 0)
            {
                textBox.Text = textBox.Text.Remove(selectionStart, selectionLength);
                textBox.SelectionEnd = textBox.SelectionStart = selectionStart;
            }
        }
    }

    private static void DeleteText(TextBox textBox)
    {
        if (textBox.SelectionStart > 0 && textBox.SelectionEnd > 0
            && textBox.SelectionStart != textBox.SelectionEnd)
        {
            DeleteSelectedText(textBox);
        }
        else
        {
            DeleteTextBeforeCursor(textBox);
        }
    }

    //private static readonly Dictionary<string, char[]> PadChar = new Dictionary<string, char[]>
    //{
    //        { "Pad01", new char[] { 'q','1','[' } },
    //        { "Pad02", new char[] { 'w','2',']' } },
    //        { "Pad03", new char[] { 'e','3','{' } },
    //        { "Pad04", new char[] { 'r','4','}' } },
    //        { "Pad05", new char[] { 't','5','#' } },
    //        { "Pad06", new char[] { 'y','6','%' } },
    //        { "Pad07", new char[] { 'q','7','^' } },
    //        { "Pad08", new char[] { 'i','8','*' } },
    //        { "Pad09", new char[] { 'o','9','+' } },
    //        { "Pad10", new char[] { 'p','0','=' } },
    //        { "Pad11", new char[] { 'a','-','-' } },
    //        { "Pad12", new char[] { 's','/','/' } },
    //        { "Pad13", new char[] { 'd',':','|' } },
    //        { "Pad14", new char[] { 'f','(','~' } },
    //        { "Pad15", new char[] { 'g',')','<' } },
    //        { "Pad16", new char[] { 'h','$','>' } },
    //        { "Pad17", new char[] { 'j','&','[' } },
    //        { "Pad18", new char[] { 'k','@','[' } },
    //        { "Pad19", new char[] { 'l','\"','\\' } },
    //        { "Pad20", new char[] { 'z','.','.' } },
    //        { "Pad21", new char[] { 'x',',',',' } },
    //        { "Pad22", new char[] { 'c','?','?' } },
    //        { "Pad23", new char[] { 'v','!','!' } },
    //        { "Pad24", new char[] { 'b',',',',' } },
    //        { "Pad25", new char[] { 'n','`','`' } },
    //        { "Pad26", new char[] { 'm','~','~' } },
    //};
}
