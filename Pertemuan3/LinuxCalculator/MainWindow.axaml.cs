using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace LinuxCalculator;

public partial class MainWindow : Window
{
    private readonly CalculatorEngine mesinKalkulator = new();

    public MainWindow()
    {
        InitializeComponent();
        PerbaruiTampilan();
    }

    private void SaatAngkaDiklik(object? sender, RoutedEventArgs e)
    {
        if (sender is Button tombol && tombol.Content is string angka)
        {
            JalankanAman(() => mesinKalkulator.MasukkanAngka(angka));
        }
    }

    private void SaatOperatorDiklik(object? sender, RoutedEventArgs e)
    {
        if (sender is Button tombol && tombol.Content is string simbolOperator)
        {
            JalankanAman(() => mesinKalkulator.PilihOperator(simbolOperator));
        }
    }

    private void SaatDesimalDiklik(object? sender, RoutedEventArgs e)
    {
        JalankanAman(mesinKalkulator.MasukkanDesimal);
    }

    private void SaatSamaDenganDiklik(object? sender, RoutedEventArgs e)
    {
        JalankanAman(mesinKalkulator.HitungHasil);
    }

    private void SaatBersihkanDiklik(object? sender, RoutedEventArgs e)
    {
        JalankanAman(mesinKalkulator.Bersihkan);
    }

    private void SaatUbahTandaDiklik(object? sender, RoutedEventArgs e)
    {
        JalankanAman(mesinKalkulator.UbahTanda);
    }

    private void SaatPersenDiklik(object? sender, RoutedEventArgs e)
    {
        JalankanAman(mesinKalkulator.Persen);
    }

    private void SaatTombolKeyboardDitekan(object? sender, KeyEventArgs e)
    {
        string? angka = e.Key switch
        {
            Key.D0 or Key.NumPad0 => "0",
            Key.D1 or Key.NumPad1 => "1",
            Key.D2 or Key.NumPad2 => "2",
            Key.D3 or Key.NumPad3 => "3",
            Key.D4 or Key.NumPad4 => "4",
            Key.D5 or Key.NumPad5 => "5",
            Key.D6 or Key.NumPad6 => "6",
            Key.D7 or Key.NumPad7 => "7",
            Key.D8 or Key.NumPad8 => "8",
            Key.D9 or Key.NumPad9 => "9",
            _ => null
        };

        if (angka is not null)
        {
            JalankanAman(() => mesinKalkulator.MasukkanAngka(angka));
            e.Handled = true;
            return;
        }

        Action? aksi = e.Key switch
        {
            Key.Add => () => mesinKalkulator.PilihOperator("+"),
            Key.Subtract => () => mesinKalkulator.PilihOperator("−"),
            Key.Multiply => () => mesinKalkulator.PilihOperator("×"),
            Key.Divide => () => mesinKalkulator.PilihOperator("÷"),
            Key.Decimal => mesinKalkulator.MasukkanDesimal,
            Key.Enter => mesinKalkulator.HitungHasil,
            Key.Escape or Key.Delete => mesinKalkulator.Bersihkan,
            Key.Back => mesinKalkulator.HapusSatuKarakter,
            _ => null
        };

        if (aksi is not null)
        {
            JalankanAman(aksi);
            e.Handled = true;
        }
    }

    private void JalankanAman(Action aksi)
    {
        try
        {
            aksi();
        }
        catch (DivideByZeroException)
        {
            mesinKalkulator.TampilkanError("tidak dapat membagi dengan nol");
        }
        catch (Exception error)
        {
            mesinKalkulator.TampilkanError(error.Message);
        }
        finally
        {
            PerbaruiTampilan();
        }
    }

    private void PerbaruiTampilan()
    {
        TampilanText.Text = mesinKalkulator.Tampilan;
    }
}
