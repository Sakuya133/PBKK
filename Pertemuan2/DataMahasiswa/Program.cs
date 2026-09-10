using System;
using System.Collections.Generic;
using System.Text;

namespace DataMahasiswa
{
    
    class Mahasiswa
    {
        public string NIM { get; set; }
        public string Nama { get; set; }
        public string Prodi { get; set; }
        public double IPK { get; set; }

        
        public Mahasiswa(string nim, string nama, string prodi, double ipk)
        {
            NIM = nim;
            Nama = nama;
            Prodi = prodi;
            IPK = ipk;
        }
    }

    class Program
    {
        
        static List<Mahasiswa> daftarMahasiswa = new List<Mahasiswa>();

      
        const ConsoleColor WarnaBingkai = ConsoleColor.Cyan;
        const ConsoleColor WarnaTambah = ConsoleColor.Green;
        const ConsoleColor WarnaDaftar = ConsoleColor.Blue;
        const ConsoleColor WarnaCari = ConsoleColor.Yellow;
        const ConsoleColor WarnaHapus = ConsoleColor.Red;
        const ConsoleColor WarnaKeluar = ConsoleColor.Gray;
        const int LebarPanel = 62;

        static void Main(string[] args)
        {
            Console.OutputEncoding = new UTF8Encoding(false);

            
            try
            {
                if (!Console.IsOutputRedirected)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                int pilihan;

                do
                {
                    TampilkanMenu();
                    string input = BacaInput("Pilihan", WarnaBingkai);

                   
                    if (input == null)
                    {
                        break;
                    }

                    if (!int.TryParse(input, out pilihan))
                    {
                        pilihan = 0;
                    }

                    Console.WriteLine();

                    switch (pilihan)
                    {
                        case 1:
                            TambahMahasiswa();
                            break;

                        case 2:
                            TampilkanMahasiswa();
                            break;

                        case 3:
                            CariMahasiswa();
                            break;

                        case 4:
                            HapusMahasiswa();
                            break;

                        case 5:
                            TulisBarisWarna("  [>] Terima kasih telah menggunakan program.", WarnaKeluar);
                            break;

                        default:
                            TulisBarisWarna("  [!] Pilihan tidak tersedia! Pilih angka 1 - 5.", ConsoleColor.Red);
                            break;
                    }

                    if (pilihan != 5)
                    {
                        Console.WriteLine();
                        TulisWarna("  Tekan ENTER untuk melanjutkan...", ConsoleColor.Gray);
                        if (Console.ReadLine() == null)
                        {
                            break;
                        }
                    }
                } while (pilihan != 5);
            }
            finally
            {
                if (!Console.IsOutputRedirected)
                {
                    Console.ResetColor();
                }
            }
        }

        
        static void TampilkanMenu()
        {
            BersihkanLayar();

            TulisBatasPanel('╔', '═', '╗', WarnaBingkai);
            TulisBarisPanel("", ConsoleColor.White, WarnaBingkai);
            TulisBarisPanel("SISTEM DATA MAHASISWA", ConsoleColor.Cyan, WarnaBingkai, true);
            TulisBarisPanel("KELOLA DATA MAHASISWA DENGAN MUDAH", ConsoleColor.Gray, WarnaBingkai, true);
            TulisBarisPanel("", ConsoleColor.White, WarnaBingkai);
            TulisBatasPanel('╠', '═', '╣', WarnaBingkai);
            TulisBarisPanel("", ConsoleColor.White, WarnaBingkai);

            TulisPilihanMenu(1, "[+]", "Tambah Mahasiswa", WarnaTambah);
            TulisPilihanMenu(2, "[=]", "Tampilkan Mahasiswa", WarnaDaftar);
            TulisPilihanMenu(3, "[?]", "Cari Mahasiswa", WarnaCari);
            TulisPilihanMenu(4, "[-]", "Hapus Mahasiswa", WarnaHapus);
            TulisPilihanMenu(5, "[>]", "Keluar", WarnaKeluar);

            TulisBarisPanel("", ConsoleColor.White, WarnaBingkai);
            TulisBatasPanel('╟', '─', '╢', WarnaBingkai);
            TulisBarisPanel("Total data: " + daftarMahasiswa.Count + " mahasiswa", ConsoleColor.Gray, WarnaBingkai);
            TulisBatasPanel('╚', '═', '╝', WarnaBingkai);
            Console.WriteLine();
        }

        
        static void TambahMahasiswa()
        {
            TampilkanHeader("[+] TAMBAH MAHASISWA", "Masukkan identitas dan IPK mahasiswa.", WarnaTambah);

            string nim = BacaInput("NIM", WarnaTambah);
            if (nim == null) return;

            string nama = BacaInput("Nama", WarnaTambah);
            if (nama == null) return;

            string prodi = BacaInput("Program Studi", WarnaTambah);
            if (prodi == null) return;

            double ipk;

            while (true)
            {
                string inputIpk = BacaInput("IPK (0 - 4)", WarnaTambah);
                if (inputIpk == null) return;

                if (double.TryParse(inputIpk, out ipk))
                {
                    if (ipk >= 0 && ipk <= 4)
                    {
                        break;
                    }
                }

                TulisBarisWarna("  [!] IPK harus berupa angka 0 - 4.", ConsoleColor.Red);
            }

            Mahasiswa mahasiswa = new Mahasiswa(nim, nama, prodi, ipk);
            daftarMahasiswa.Add(mahasiswa);

            Console.WriteLine();
            TulisBarisWarna("  [OK] Data mahasiswa berhasil ditambahkan.", ConsoleColor.Green);
        }

        
        static void TampilkanMahasiswa()
        {
            TampilkanHeader("[=] DAFTAR MAHASISWA", "Total data: " + daftarMahasiswa.Count + " mahasiswa", WarnaDaftar);

            if (daftarMahasiswa.Count == 0)
            {
                TulisBarisWarna("  [!] Belum ada data mahasiswa.", ConsoleColor.Yellow);
                return;
            }

            TulisBarisWarna("  " + string.Format("{0,-12} {1,-20} {2,-20} {3,5}", "NIM", "Nama", "Prodi", "IPK"), ConsoleColor.Cyan);
            TulisBarisWarna("  " + new string('─', 60), WarnaDaftar);

            foreach (Mahasiswa m in daftarMahasiswa)
            {
                Console.Write("  ");
                TulisWarna(Kolom(m.NIM, 12) + " ", ConsoleColor.Cyan);
                TulisWarna(Kolom(m.Nama, 20) + " ", ConsoleColor.White);
                TulisWarna(Kolom(m.Prodi, 20) + " ", ConsoleColor.Gray);
                TulisBarisWarna(m.IPK.ToString("F2").PadLeft(5), ConsoleColor.Cyan);
            }

            TulisBarisWarna("  " + new string('─', 60), WarnaDaftar);
            TulisBarisWarna("  Gunakan menu Cari untuk melihat data lengkap.", ConsoleColor.Gray);
        }

        
        static void CariMahasiswa()
        {
            TampilkanHeader("[?] CARI MAHASISWA", "Temukan mahasiswa berdasarkan NIM.", WarnaCari);

            string nimCari = BacaInput("Masukkan NIM", WarnaCari);
            if (nimCari == null) return;

            Mahasiswa mahasiswaDitemukan = null;

            foreach (Mahasiswa m in daftarMahasiswa)
            {
                if (m.NIM.Equals(nimCari, StringComparison.OrdinalIgnoreCase))
                {
                    mahasiswaDitemukan = m;
                    break;
                }
            }

            Console.WriteLine();

            if (mahasiswaDitemukan != null)
            {
                TulisBarisWarna("  [OK] Data ditemukan!", ConsoleColor.Green);
                Console.WriteLine();
                TulisDetail("NIM", mahasiswaDitemukan.NIM, WarnaCari);
                TulisDetail("Nama", mahasiswaDitemukan.Nama, WarnaCari);
                TulisDetail("Prodi", mahasiswaDitemukan.Prodi, WarnaCari);
                TulisDetail("IPK", mahasiswaDitemukan.IPK.ToString("F2"), WarnaCari);
            }
            else
            {
                TulisBarisWarna("  [!] Mahasiswa dengan NIM tersebut tidak ditemukan.", ConsoleColor.Yellow);
            }
        }

        
        static void HapusMahasiswa()
        {
            TampilkanHeader("[-] HAPUS MAHASISWA", "Hapus data mahasiswa berdasarkan NIM.", WarnaHapus);

            string nimHapus = BacaInput("Masukkan NIM", WarnaHapus);
            if (nimHapus == null) return;

            Mahasiswa mahasiswaDitemukan = null;

            foreach (Mahasiswa m in daftarMahasiswa)
            {
                if (m.NIM.Equals(nimHapus, StringComparison.OrdinalIgnoreCase))
                {
                    mahasiswaDitemukan = m;
                    break;
                }
            }

            if (mahasiswaDitemukan != null)
            {
                daftarMahasiswa.Remove(mahasiswaDitemukan);

                Console.WriteLine();
                TulisBarisWarna("  [OK] Data mahasiswa berhasil dihapus.", ConsoleColor.Green);
            }
            else
            {
                Console.WriteLine();
                TulisBarisWarna("  [!] Data mahasiswa tidak ditemukan.", ConsoleColor.Yellow);
            }
        }

       
        static void TulisWarna(string teks, ConsoleColor warna)
        {
            if (Console.IsOutputRedirected)
            {
                Console.Write(teks);
                return;
            }

            ConsoleColor warnaSebelumnya = Console.ForegroundColor;
            Console.ForegroundColor = warna;
            Console.Write(teks);
            Console.ForegroundColor = warnaSebelumnya;
        }

        static void TulisBarisWarna(string teks, ConsoleColor warna)
        {
            TulisWarna(teks, warna);
            Console.WriteLine();
        }

        static void BersihkanLayar()
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }
            Console.WriteLine();
        }

        static void TulisBatasPanel(char kiri, char garis, char kanan, ConsoleColor warna)
        {
            TulisBarisWarna("  " + kiri + new string(garis, LebarPanel) + kanan, warna);
        }

        static void TulisBarisPanel(string teks, ConsoleColor warnaTeks, ConsoleColor warnaBingkai, bool rataTengah = false)
        {
            if (teks.Length > LebarPanel - 4)
            {
                teks = teks.Substring(0, LebarPanel - 7) + "...";
            }

            int kiri = rataTengah ? (LebarPanel - teks.Length) / 2 : 2;
            int kanan = LebarPanel - kiri - teks.Length;

            TulisWarna("  ║", warnaBingkai);
            Console.Write(new string(' ', kiri));
            TulisWarna(teks, warnaTeks);
            Console.Write(new string(' ', kanan));
            TulisBarisWarna("║", warnaBingkai);
        }

        static void TulisPilihanMenu(int nomor, string simbol, string label, ConsoleColor warna)
        {
            TulisWarna("  ║", WarnaBingkai);
            Console.Write("  ");

           
            if (!Console.IsOutputRedirected)
            {
                Console.BackgroundColor = warna;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.Write(" " + nomor + " ");
            if (!Console.IsOutputRedirected)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Write("  ");
            TulisWarna(simbol, warna);
            Console.Write("  ");
            TulisWarna(label, ConsoleColor.White);

            int panjangIsi = 2 + 3 + 2 + simbol.Length + 2 + label.Length;
            Console.Write(new string(' ', LebarPanel - panjangIsi));
            TulisBarisWarna("║", WarnaBingkai);
        }

        static void TampilkanHeader(string judul, string keterangan, ConsoleColor warna)
        {
            BersihkanLayar();
            TulisBatasPanel('╔', '═', '╗', warna);
            TulisBarisPanel(judul, warna, warna);
            TulisBarisPanel(keterangan, ConsoleColor.Gray, warna);
            TulisBatasPanel('╚', '═', '╝', warna);
            Console.WriteLine();
        }

        static string BacaInput(string label, ConsoleColor warna)
        {
            TulisWarna("  " + label.PadRight(16), warna);
            TulisWarna(": ", ConsoleColor.White);
            return Console.ReadLine();
        }

        static void TulisDetail(string label, string nilai, ConsoleColor warna)
        {
            TulisWarna("  " + label.PadRight(8), warna);
            TulisBarisWarna(": " + nilai, ConsoleColor.White);
        }

       
        static string Kolom(string nilai, int lebar)
        {
            nilai = nilai ?? "";
            if (nilai.Length > lebar)
            {
                nilai = nilai.Substring(0, lebar - 3) + "...";
            }
            return nilai.PadRight(lebar);
        }
    }
}
