namespace LinuxCalculator;

internal static class CalculatorEngineSelfTest
{
    public static int Run()
    {
        try
        {
            CekOperasi("2", "+", "3", "5");
            CekOperasi("9", "−", "4", "5");
            CekOperasi("6", "×", "7", "42");
            CekOperasi("8", "÷", "2", "4");
            CekDesimalDanPersen();
            CekPembagianNol();

            Console.WriteLine("Semua self-test berhasil.");
            return 0;
        }
        catch (Exception error)
        {
            Console.Error.WriteLine($"Self-test gagal: {error.Message}");
            return 1;
        }
    }

    private static void CekOperasi(string kiri, string simbol, string kanan, string hasilBenar)
    {
        var kalkulator = new CalculatorEngine();
        kalkulator.MasukkanAngka(kiri);
        kalkulator.PilihOperator(simbol);
        kalkulator.MasukkanAngka(kanan);
        kalkulator.HitungHasil();

        Pastikan(kalkulator.Tampilan == hasilBenar,
            $"{kiri} {simbol} {kanan} seharusnya {hasilBenar}, tetapi hasilnya {kalkulator.Tampilan}");
    }

    private static void CekDesimalDanPersen()
    {
        var kalkulator = new CalculatorEngine();
        kalkulator.MasukkanAngka("5");
        kalkulator.MasukkanDesimal();
        kalkulator.MasukkanAngka("5");
        kalkulator.Persen();

        Pastikan(kalkulator.Tampilan == "0.055", "fitur desimal/persen tidak sesuai");
    }

    private static void CekPembagianNol()
    {
        var kalkulator = new CalculatorEngine();
        kalkulator.MasukkanAngka("9");
        kalkulator.PilihOperator("÷");
        kalkulator.MasukkanAngka("0");

        bool errorDitemukan = false;
        try
        {
            kalkulator.HitungHasil();
        }
        catch (DivideByZeroException)
        {
            errorDitemukan = true;
        }

        Pastikan(errorDitemukan, "pembagian nol seharusnya menghasilkan exception");
    }

    private static void Pastikan(bool kondisi, string pesan)
    {
        if (!kondisi)
        {
            throw new InvalidOperationException(pesan);
        }
    }
}
