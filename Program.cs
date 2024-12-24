using System;
using System.Diagnostics;

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
                Console.WriteLine("\t\t[5] Sorting Barang");
                Console.WriteLine("\t\t[6] Lihat Semua Barang");
                Console.WriteLine("\t\t[7] Keluar");
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
                        SortingBarang();
                        break;
                    case 6:
                        TampilkanBarang();
                        break;
                    case 7:
                        Console.WriteLine("Keluar dari program...");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        break;
                }

                if (pilihMenu != 7)
                {
                    Console.WriteLine("Tekan Enter untuk kembali ke menu utama...");
                    Console.ReadLine();
                }
            } while (pilihMenu != 7);
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

            while (true)
            {
               Console.Write("Masukkan Harga Barang: ");
               string inputHarga = Console.ReadLine();
                try
                {
                    int hargaBaru = int.Parse(inputHarga);
                    if (hargaBaru == barangBaru.HargaBarang)
                    {
                    Console.WriteLine("Harga masih sama, masukkan harga terbaru");
                    continue;
                    }
                    barangBaru.HargaBarang = hargaBaru;
                    break; 
                }
                catch  (FormatException)
                {
                    Console.WriteLine("Input yang dimasukkan tidak sesuai, silahkan masukkan angka yang benar");
                }

            }

            while (true)
            {
                Console.Write("Masukkan Stok Barang: ");
                string inputStok = Console.ReadLine();
                try
                {
                   barangBaru.Stok = int.Parse(inputStok);
                   break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input yang dimasukkan tidak sesuai, silahkan masukkan angka yang benar");
                }
            }

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
                        while (true)
                        {
                            Console.WriteLine("Masukkan Harga Baru");
                            string inputHarga = Console.ReadLine();
                            try
                            {
                               int hargaBaru = int.Parse(inputHarga);
                               if (hargaBaru == daftarBarang[i].HargaBarang)
                               {
                                Console.WriteLine("Harga masih sama, masukkan harga terbaru");
                                continue;
                               }
                               daftarBarang[i].HargaBarang = hargaBaru;
                               Console.WriteLine("Harga berhasil diubah");
                               break;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Input yang dimasukkan tidak sesuai, silahkan masukkan angka yang benar");
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Harga masih sama, masukkan harga terbaru");
                            }
                        }
                       
                    }
                    else if (pilihan == 2)
                    {
                        while (true)
                        {
                           Console.WriteLine("Masukkan stok terbaru");
                           string inputStok = Console.ReadLine();
                           try
                           {
                            int stokBaru = int.Parse(inputStok);
                            if (stokBaru == daftarBarang[i].Stok)
                            {
                                Console.WriteLine("Stok masih sama, masukkan stok terbaru");
                                continue;
                            }
                            daftarBarang[i].Stok = int.Parse(inputStok);
                            Console.WriteLine("Stok berhasil diperbarui");
                            break;
                           }
                            catch (FormatException)
                            {
                                Console.WriteLine("Input tidak sesuai, silahkan masukkan angka yang benar");
                            }
                        }
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

            //bubble sort
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
            while (true)
            {
            Console.Write("Masukkan batas harga maksimum: ");
            string inputMaxPrice = Console.ReadLine();
            try
            {
                int maxPrice = int.Parse(inputMaxPrice);
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
            break;
            }
        
        catch (FormatException)
        {
            Console.WriteLine("Input tidak benar, harap masukkan angka yang benar");
        }
    }
 }
        
        else
        {
            Console.WriteLine("Pilihan tidak valid.");
        }
    }

    static void SortingBarang()
    {
        if (jumlahBarang == 0)
        {
            Console.WriteLine("Tidak ada barang yang dapat diurutkan");
            return;
        }
        Console.WriteLine("Pilih cara pengurutan:");
        Console.WriteLine("1. Urutkan dari harga terendah");
        Console.WriteLine("2. Urutkan dari harga tertinggi");
        Console.WriteLine("Masukkan pilihan: ");

        int pilihan = int.Parse(Console.ReadLine());

            for (int i = 0; i < jumlahBarang - 1; i++)
            {
                for(int j = 0; j < jumlahBarang -  i - 1; j++)
                {
                    bool swapCondition = false;
                    
                    if (pilihan == 1)
                    {
                        swapCondition = daftarBarang[j].HargaBarang > daftarBarang[j + 1].HargaBarang;
                    }
                    else if (pilihan == 2)
                    {
                        swapCondition = daftarBarang[j].HargaBarang < daftarBarang[j + 1].HargaBarang;
                    }
                    else
                    {
                        Console.WriteLine("Pilihan salah");
                        return;
                    }
                    
                    if (swapCondition)
                    {
                        Barang temp = daftarBarang[j];
                        daftarBarang[j] = daftarBarang[j + 1];
                        daftarBarang[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("Barang berhasil diurutkan");
            TampilkanBarang();
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
        public int HargaBarang { get; set; }
        public int Stok { get; set; }
    }
}
