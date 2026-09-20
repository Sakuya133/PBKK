using System.Globalization;

namespace LinuxCalculator;

public class CalculatorEngine
{
    private string inputSaatIni = "0";
    private decimal angkaSebelumnya;
    private string? operatorAktif;
    private bool mulaiAngkaBaru;

    public string Tampilan => inputSaatIni;
    public string Riwayat { get; private set; } = "terminal siap menerima input";

    public void MasukkanAngka(string angka)
    {
        if (mulaiAngkaBaru || inputSaatIni == "0" || inputSaatIni == "Error")
        {
            inputSaatIni = angka;
            mulaiAngkaBaru = false;
            return;
        }

        if (inputSaatIni.Replace("-", string.Empty).Replace(".", string.Empty).Length < 15)
        {
            inputSaatIni += angka;
        }
    }

    public void MasukkanDesimal()
    {
        if (mulaiAngkaBaru || inputSaatIni == "Error")
        {
            inputSaatIni = "0.";
            mulaiAngkaBaru = false;
        }
        else if (!inputSaatIni.Contains('.'))
        {
            inputSaatIni += ".";
        }
    }

    public void PilihOperator(string simbolOperator)
    {
        decimal angkaSekarang = AmbilNilaiTampilan();

        if (operatorAktif is not null && !mulaiAngkaBaru)
        {
            angkaSekarang = Hitung(angkaSebelumnya, angkaSekarang, operatorAktif);
            inputSaatIni = FormatAngka(angkaSekarang);
        }

        angkaSebelumnya = angkaSekarang;
        operatorAktif = simbolOperator;
        Riwayat = $"{FormatAngka(angkaSebelumnya)} {operatorAktif}";
        mulaiAngkaBaru = true;
    }

    public void HitungHasil()
    {
        if (operatorAktif is null)
        {
            return;
        }

        decimal angkaSekarang = AmbilNilaiTampilan();
        decimal hasil = Hitung(angkaSebelumnya, angkaSekarang, operatorAktif);

        Riwayat = $"{FormatAngka(angkaSebelumnya)} {operatorAktif} {FormatAngka(angkaSekarang)} =";
        inputSaatIni = FormatAngka(hasil);
        angkaSebelumnya = hasil;
        operatorAktif = null;
        mulaiAngkaBaru = true;
    }

    public void UbahTanda()
    {
        decimal nilai = AmbilNilaiTampilan();
        inputSaatIni = FormatAngka(-nilai);
    }

    public void Persen()
    {
        decimal nilai = AmbilNilaiTampilan();
        Riwayat = $"{FormatAngka(nilai)}%";
        inputSaatIni = FormatAngka(nilai / 100m);
        mulaiAngkaBaru = true;
    }

    public void HapusSatuKarakter()
    {
        if (mulaiAngkaBaru || inputSaatIni is "0" or "Error")
        {
            return;
        }

        inputSaatIni = inputSaatIni.Length > 1
            ? inputSaatIni[..^1]
            : "0";

        if (inputSaatIni == "-")
        {
            inputSaatIni = "0";
        }
    }

    public void Bersihkan()
    {
        inputSaatIni = "0";
        angkaSebelumnya = 0;
        operatorAktif = null;
        mulaiAngkaBaru = false;
        Riwayat = "terminal siap menerima input";
    }

    public void TampilkanError(string pesan)
    {
        inputSaatIni = "Error";
        Riwayat = $"error: {pesan}";
        operatorAktif = null;
        mulaiAngkaBaru = true;
    }

    private decimal AmbilNilaiTampilan()
    {
        return decimal.Parse(inputSaatIni, CultureInfo.InvariantCulture);
    }

    private static decimal Hitung(decimal angkaPertama, decimal angkaKedua, string simbolOperator)
    {
        return simbolOperator switch
        {
            "+" => angkaPertama + angkaKedua,
            "−" => angkaPertama - angkaKedua,
            "×" => angkaPertama * angkaKedua,
            "÷" when angkaKedua == 0 => throw new DivideByZeroException("tidak dapat membagi dengan nol"),
            "÷" => angkaPertama / angkaKedua,
            _ => throw new InvalidOperationException("operator tidak dikenali")
        };
    }

    private static string FormatAngka(decimal nilai)
    {
        return nilai.ToString("0.###############", CultureInfo.InvariantCulture);
    }
}
