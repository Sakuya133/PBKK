# Blue Calculator — Simple Calculator C# .NET

**Nama:** Ida Bagus Gde Dimas Sutha Maha  
**NRP:** 5025241218  
**Mata Kuliah:** Framework Programming

Blue Calculator adalah kalkulator desktop sederhana dengan desain dark blue yang bersih. Aplikasi dibuat menggunakan **C#**, **.NET 8**, dan **Avalonia UI** agar berjalan native di Linux, sekaligus tetap dapat dijalankan di Windows dan macOS.

## Fitur

- Operasi penjumlahan, pengurangan, perkalian, dan pembagian
- Input bilangan desimal
- Clear dan backspace dari keyboard
- Error handling untuk pembagian dengan nol dan input tidak valid
- Kontrol dengan mouse maupun keyboard
- Tampilan dark blue dengan susunan kalkulator klasik

## Menjalankan di Ubuntu 24.04

Pastikan .NET 8 SDK sudah terpasang:

```bash
sudo apt update
sudo apt install dotnet-sdk-8.0
```

Masuk ke folder proyek lalu jalankan:

```bash
chmod +x run-linux.sh
./run-linux.sh
```

Atau secara manual:

```bash
dotnet restore
dotnet run
```

## Pengujian logika

Proyek menyediakan self-test tanpa framework tambahan:

```bash
dotnet run -- --self-test
```

Jika semua operasi benar, terminal akan menampilkan `Semua self-test berhasil.`

## Publish aplikasi Linux

```bash
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true
```

Hasil publish berada di:

```text
bin/Release/net8.0/linux-x64/publish/
```

## Struktur proyek

```text
LinuxCalculator/
├── App.axaml                     # Tema dan entry UI
├── App.axaml.cs                  # Membuka MainWindow
├── CalculatorEngine.cs           # Logika/OOP kalkulator
├── CalculatorEngineSelfTest.cs   # Pengujian operasi dasar
├── MainWindow.axaml              # Desain GUI Linux
├── MainWindow.axaml.cs           # Event dan handler tombol
├── Program.cs                    # Entry point aplikasi
├── LinuxCalculator.csproj        # Konfigurasi .NET/Avalonia
└── run-linux.sh                  # Shortcut menjalankan aplikasi
```

Detail hubungan kode dengan petunjuk tugas tersedia di `PENJELASAN-TUGAS.md`.
