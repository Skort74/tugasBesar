using System;

namespace TugasBesar
{
    class Program
    {
        static Barang[] daftarBarang = new Barang[100];
        static int jumlahBarang = 0;

        static void Main(string[] args)
        {
            int pilihMenu = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Selamat Datang di Aplikasi Manajemen Warduro");
                Console.WriteLine("\t\t[1] Tambah Barang");
                Console.WriteLine("\t\t[2] Edit Barang");
                Console.WriteLine("\t\t[3] Hapus Barang");
                Console.WriteLine("\t\t[4] Cari Barang");
                Console.WriteLine("\t\t[5] Lihat Semua Barang");
                Console.WriteLine("\t\t[6] Keluar");
                Console.Write("\t\tMasukan pilihan: ");
                pilihMenu = int.Parse(Console.ReadLine());

                switch (pilihMenu)
                {
                    case 1:
                        TambahBarang();
                        break;
                    case 2:
                        EditBarang();
                        break;
                    case 3:
                        HapusBarang();
                        break;
                    case 4:
                        CariDanFilterBarang();
                        break;
                    case 5:
                        TampilkanBarang();
                        break;
                    case 6:
                        Console.WriteLine("Keluar dari program...");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        break;
                }

                if (pilihMenu != 6)
                {
                    Console.WriteLine("Tekan Enter untuk kembali ke menu utama...");
                    Console.ReadLine();
                }
            } while (pilihMenu != 6);
        }

        static void TambahBarang()
        {
            if (jumlahBarang >= daftarBarang.Length)
            {
                Console.WriteLine("Kapasitas penuh, tidak dapat menambahkan barang baru.");
                return;
            }

            Barang barangBaru = new Barang();

            Console.Write("Masukkan Jenis Barang: ");
            barangBaru.JenisBarang = Console.ReadLine();

            Console.Write("Masukkan Merk Barang: ");
            barangBaru.MerkBarang = Console.ReadLine();

            Console.Write("Masukkan Harga Barang: ");
            barangBaru.HargaBarang = decimal.Parse(Console.ReadLine());

            Console.Write("Masukkan Stok Barang: ");
            barangBaru.Stok = int.Parse(Console.ReadLine());

            daftarBarang[jumlahBarang] = barangBaru;
            jumlahBarang++;

            Console.WriteLine("Barang berhasil ditambahkan!");
        }

        static void EditBarang()
        {
            Console.Write("Masukkan Merk Barang yang ingin diubah: ");
            string merk = Console.ReadLine();

            for (int i = 0; i < jumlahBarang; i++)
            {
                if (daftarBarang[i].MerkBarang.Equals(merk, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Pilih opsi untuk mengubah");
                    Console.WriteLine("1. Ubah Harga Barang");
                    Console.WriteLine("2. Ubah Stok Barang");
                    Console.Write("Masukkan Pilihan: ");
                    int pilihan = int.Parse(Console.ReadLine());

                    if (pilihan == 1)
                    {
                        Console.Write("Masukkan Harga Baru Barang: ");
                        daftarBarang[i].HargaBarang = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Harga berhasil diperbarui");
                    }
                    else if (pilihan == 2)
                    {
                        Console.Write("Masukkan Stok Baru Barang: ");
                        daftarBarang[i].Stok = int.Parse(Console.ReadLine());
                        Console.WriteLine("Stok berhasil diperbarui");
                    }
                    else
                    {
                        Console.WriteLine("Pilihan tidak valid");
                    }

                    Console.WriteLine("Barang berhasil diubah!");
                    return;
                }
            }

            Console.WriteLine("Barang dengan merk tersebut tidak ditemukan.");
        }

        static void HapusBarang()
        {
            Console.Write("Masukkan merk Barang yang ingin dihapus: ");
            string merk = Console.ReadLine();

            for (int i = 0; i < jumlahBarang; i++)
            {
                if (daftarBarang[i].MerkBarang.Equals(merk, StringComparison.OrdinalIgnoreCase))
                {
                    for (int j = i; j < jumlahBarang - 1; j++)
                    {
                        daftarBarang[j] = daftarBarang[j + 1];
                    }

                    daftarBarang[jumlahBarang - 1] = null;
                    jumlahBarang--;
                    Console.WriteLine("Barang berhasil dihapus!");
                    return;
                }
            }

            Console.WriteLine("Barang dengan merk tersebut tidak ditemukan.");
        }

       static void CariDanFilterBarang()
{
    Console.WriteLine("Pilih Pencarian Berdasarkan:");
    Console.WriteLine("1. Berdasarkan Jenis Barang");
    Console.WriteLine("2. Berdasarkan Batas Harga");
    Console.Write("Masukkan pilihan: ");
    int pilihan = int.Parse(Console.ReadLine());

    if (pilihan == 1)
    {
        Console.Write("Masukkan jenis barang yang ingin dicari: ");
        string keyword = Console.ReadLine();

        bool found = false;
        Barang[] filteredBarang = new Barang[jumlahBarang];
        int countFiltered = 0;

        for (int i = 0; i < jumlahBarang; i++)
        {
            if (daftarBarang[i].JenisBarang.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                filteredBarang[countFiltered++] = daftarBarang[i];
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Barang tidak ditemukan.");
            return;
        }

        // Bubble Sort berdasarkan HargaBarang
        for (int i = 0; i < countFiltered - 1; i++)
        {
            for (int j = 0; j < countFiltered - i - 1; j++)
            {
                if (filteredBarang[j].HargaBarang > filteredBarang[j + 1].HargaBarang)
                {
                    Barang temp = filteredBarang[j];
                    filteredBarang[j] = filteredBarang[j + 1];
                    filteredBarang[j + 1] = temp;
                }
            }
        }

        Console.WriteLine("Barang yang ditemukan setelah diurutkan berdasarkan harga:");
        for (int i = 0; i < countFiltered; i++)
        {
            TampilkanDetailBarang(filteredBarang[i]);
        }
    }
    else if (pilihan == 2)
    {
        Console.Write("Masukkan batas harga maksimum: ");
        decimal maxPrice = decimal.Parse(Console.ReadLine());

        Barang[] filteredBarang = new Barang[jumlahBarang];
        int countFiltered = 0;

        for (int i = 0; i < jumlahBarang; i++)
        {
            if (daftarBarang[i].HargaBarang <= maxPrice)
            {
                filteredBarang[countFiltered++] = daftarBarang[i];
            }
        }

        if (countFiltered == 0)
        {
            Console.WriteLine("Tidak ada barang yang sesuai dengan filter.");
            return;
        }

        for (int i = 0; i < countFiltered - 1; i++)
        {
            for (int j = 0; j < countFiltered - i - 1; j++)
            {
                if (filteredBarang[j].HargaBarang > filteredBarang[j + 1].HargaBarang)
                {
                    Barang temp = filteredBarang[j];
                    filteredBarang[j] = filteredBarang[j + 1];
                    filteredBarang[j + 1] = temp;
                }
            }
        }

        Console.WriteLine("Barang setelah difilter dan diurutkan berdasarkan harga:");
        for (int i = 0; i < countFiltered; i++)
        {
            TampilkanDetailBarang(filteredBarang[i]);
        }
    }
    else
    {
        Console.WriteLine("Pilihan tidak valid.");
    }
}


        static void TampilkanBarang()
        {
            if (jumlahBarang == 0)
            {
                Console.WriteLine("Tidak ada barang untuk ditampilkan.");
                return;
            }

            for (int i = 0; i < jumlahBarang; i++)
            {
                TampilkanDetailBarang(daftarBarang[i]);
            }
        }

        static void TampilkanDetailBarang(Barang barang)
        {
            Console.WriteLine($"Jenis Barang: {barang.JenisBarang}");
            Console.WriteLine($"Merk Barang: {barang.MerkBarang}");
            Console.WriteLine($"Harga Barang: Rp {barang.HargaBarang:#,##0}");
            Console.WriteLine($"Stok Barang: {barang.Stok}");
            Console.WriteLine($"--------------------------------");
        }
    }

    class Barang
    {
        public string JenisBarang { get; set; }
        public string MerkBarang { get; set; }
        public decimal HargaBarang { get; set; }
        public int Stok { get; set; }
    }
}
