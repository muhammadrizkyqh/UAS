using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<User> users = new List<User>();
    static List<Film> films = new List<Film>();
    static List<Ticket> tickets = new List<Ticket>();
    static User currentUser = null;

    static void Main()
    {
        SeedData();
        while (true)
        {
            if (currentUser == null)
            {
                ShowLoginMenu();
            }
            else
            {
                ShowMainMenu();
            }
        }
    }

    static void SeedData()
    {
        users.Add(new User { Username = "admin", Password = "admin", Role = "Admin" });
        users.Add(new User { Username = "user", Password = "user", Role = "User" });
    }

    static void ShowLoginMenu()
    {
        Console.WriteLine("\nAplikasi Manajemen Bioskop");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Keluar");
        Console.Write("Pilih menu: ");
        string pilihan = Console.ReadLine();

        switch (pilihan)
        {
            case "1":
                Login();
                break;
            case "2":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Pilihan tidak valid.");
                break;
        }
    }

    static void Login()
    {
        Console.Write("Username: ");
        string username = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();

        User user = users.Find(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            currentUser = user;
            Console.WriteLine($"Selamat datang, {currentUser.Username}!");
        }
        else
        {
            Console.WriteLine("Username atau password salah.");
        }
    }

    static void ShowMainMenu()
    {
        Console.WriteLine("\nAplikasi Manajemen Bioskop");
        if (currentUser.Role == "Admin")
        {
            Console.WriteLine("1. Tambah Film");
            Console.WriteLine("2. Tampilkan Semua Film");
            Console.WriteLine("3. Update Film");
            Console.WriteLine("4. Hapus Film");
            Console.WriteLine("5. Cari Film");
            Console.WriteLine("6. Filter Film Berdasarkan Durasi");
            Console.WriteLine("7. Pilih Film dan Kursi");
            Console.WriteLine("8. Laporan Penjualan");
            Console.WriteLine("9. Backup Data");
            Console.WriteLine("10. Restore Data");
            Console.WriteLine("11. Logout");
        }
        else
        {
            Console.WriteLine("1. Tampilkan Semua Film");
            Console.WriteLine("2. Cari Film");
            Console.WriteLine("3. Filter Film Berdasarkan Durasi");
            Console.WriteLine("4. Pilih Film dan Kursi");
            Console.WriteLine("5. Logout");
        }
        Console.Write("Pilih menu: ");
        string pilihan = Console.ReadLine();

        try
        {
            switch (pilihan)
            {
                case "1":
                    if (currentUser.Role == "Admin") TambahFilm(); else TampilkanSemuaFilm();
                    break;
                case "2":
                    if (currentUser.Role == "Admin") TampilkanSemuaFilm(); else CariFilm();
                    break;
                case "3":
                    if (currentUser.Role == "Admin") UpdateFilm(); else FilterFilm();
                    break;
                case "4":
                    if (currentUser.Role == "Admin") HapusFilm(); else PilihFilmDanKursi();
                    break;
                case "5":
                    if (currentUser.Role == "Admin") CariFilm(); else Logout();
                    break;
                case "6":
                    if (currentUser.Role == "Admin") FilterFilm();
                    break;
                case "7":
                    if (currentUser.Role == "Admin") PilihFilmDanKursi();
                    break;
                case "8":
                    if (currentUser.Role == "Admin") LaporanPenjualan();
                    break;
                case "9":
                    if (currentUser.Role == "Admin") BackupData();
                    break;
                case "10":
                    if (currentUser.Role == "Admin") RestoreData();
                    break;
                case "11":
                    if (currentUser.Role == "Admin") Logout();
                    break;
                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
        }
    }

    static void Logout()
    {
        currentUser = null;
        Console.WriteLine("Anda telah logout.");
    }

    static void TambahFilm()
    {
        Console.Write("Masukkan judul film: ");
        string judul = Console.ReadLine();
        Console.Write("Masukkan durasi film (menit): ");
        int durasi = int.Parse(Console.ReadLine());

        films.Add(new Film { Judul = judul, Durasi = durasi });
        Console.WriteLine("Film berhasil ditambahkan.");
    }

    static void TampilkanSemuaFilm()
    {
        Console.WriteLine("\nDaftar Film:");
        for (int i = 0; i < films.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {films[i].Judul} - {films[i].Durasi} menit");
        }
    }

    static void UpdateFilm()
    {
        Console.Write("Masukkan nomor film yang ingin diupdate: ");
        int nomor = int.Parse(Console.ReadLine()) - 1;

        if (nomor < 0 || nomor >= films.Count)
        {
            Console.WriteLine("Nomor film tidak valid.");
            return;
        }

        Console.Write("Masukkan judul film baru: ");
        films[nomor].Judul = Console.ReadLine();
        Console.Write("Masukkan durasi film baru (menit): ");
        films[nomor].Durasi = int.Parse(Console.ReadLine());
        Console.WriteLine("Film berhasil diupdate.");
    }

    static void HapusFilm()
    {
        Console.Write("Masukkan nomor film yang ingin dihapus: ");
        int nomor = int.Parse(Console.ReadLine()) - 1;

        if (nomor < 0 || nomor >= films.Count)
        {
            Console.WriteLine("Nomor film tidak valid.");
            return;
        }

        films.RemoveAt(nomor);
        Console.WriteLine("Film berhasil dihapus.");
    }

    static void CariFilm()
    {
        Console.Write("Masukkan judul film yang ingin dicari: ");
        string judul = Console.ReadLine();
        bool ditemukan = false;

        for (int i = 0; i < films.Count; i++)
        {
            if (films[i].Judul.Contains(judul, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"{i + 1}. {films[i].Judul} - {films[i].Durasi} menit");
                ditemukan = true;
            }
        }

        if (!ditemukan)
        {
            Console.WriteLine("Film tidak ditemukan.");
        }
    }

    static void FilterFilm()
    {
        Console.Write("Masukkan durasi maksimal (menit): ");
        int durasi = int.Parse(Console.ReadLine());
        bool ditemukan = false;

        for (int i = 0; i < films.Count; i++)
        {
            if (films[i].Durasi <= durasi)
            {
                Console.WriteLine($"{i + 1}. {films[i].Judul} - {films[i].Durasi} menit");
                ditemukan = true;
            }
        }

        if (!ditemukan)
        {
            Console.WriteLine("Tidak ada film yang sesuai dengan durasi yang diberikan.");
        }
    }

    static void PilihFilmDanKursi()
    {
        Console.Write("Masukkan nomor film yang ingin dipilih: ");
        int nomorFilm = int.Parse(Console.ReadLine()) - 1;

        if (nomorFilm < 0 || nomorFilm >= films.Count)
        {
            Console.WriteLine("Nomor film tidak valid.");
            return;
        }

        Console.WriteLine($"Anda memilih film: {films[nomorFilm].Judul}");
        Console.WriteLine("Pilih kursi (1-10):");
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine($"Kursi {i + 1}: {(films[nomorFilm].Kursi[i] ? "Terisi" : "Kosong")}");
        }

        Console.Write("Masukkan nomor kursi yang ingin dipilih: ");
        int nomorKursi = int.Parse(Console.ReadLine()) - 1;

        if (nomorKursi < 0 || nomorKursi >= 10 || films[nomorFilm].Kursi[nomorKursi])
        {
            Console.WriteLine("Nomor kursi tidak valid atau sudah terisi.");
            return;
        }

        films[nomorFilm].Kursi[nomorKursi] = true;
        Console.WriteLine("Kursi berhasil dipilih.");

        Console.Write("Masukkan jumlah pembayaran: ");
        decimal pembayaran = decimal.Parse(Console.ReadLine());
        Console.WriteLine($"Pembayaran sebesar {pembayaran:C} berhasil dilakukan.");

        // Cetak tiket ke file teks
        string tiket = $"Tiket Bioskop\nFilm: {films[nomorFilm].Judul}\nKursi: {nomorKursi + 1}\nPembayaran: {pembayaran:C}\n";
        string filePath = $"Tiket_{films[nomorFilm].Judul.Replace(" ", "_")}_{nomorKursi + 1}.txt";
        File.WriteAllText(filePath, tiket);
        Console.WriteLine($"Tiket berhasil dicetak ke file: {filePath}");

        // Simpan tiket ke daftar tiket
        tickets.Add(new Ticket { Film = films[nomorFilm], Kursi = nomorKursi + 1, Pembayaran = pembayaran });
    }

    static void LaporanPenjualan()
    {
        Console.WriteLine("\nLaporan Penjualan:");
        foreach (var ticket in tickets)
        {
            Console.WriteLine($"Film: {ticket.Film.Judul}, Kursi: {ticket.Kursi}, Pembayaran: {ticket.Pembayaran:C}");
        }
    }

    static void BackupData()
    {
        string backupPath = "backup.txt";
        using (StreamWriter writer = new StreamWriter(backupPath))
        {
            writer.WriteLine("Users:");
            foreach (var user in users)
            {
                writer.WriteLine($"{user.Username},{user.Password},{user.Role}");
            }

            writer.WriteLine("Films:");
            foreach (var film in films)
            {
                writer.WriteLine($"{film.Judul},{film.Durasi}");
                for (int i = 0; i < 10; i++)
                {
                    writer.WriteLine(film.Kursi[i] ? "1" : "0");
                }
            }

            writer.WriteLine("Tickets:");
            foreach (var ticket in tickets)
            {
                writer.WriteLine($"{ticket.Film.Judul},{ticket.Kursi},{ticket.Pembayaran}");
            }
        }
        Console.WriteLine("Data berhasil di-backup ke file: backup.txt");
    }

    static void RestoreData()
    {
        string backupPath = "backup.txt";
        if (!File.Exists(backupPath))
        {
            Console.WriteLine("File backup tidak ditemukan.");
            return;
        }

        using (StreamReader reader = new StreamReader(backupPath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line == "Users:")
                {
                    users.Clear();
                    while ((line = reader.ReadLine()) != null && line != "Films:")
                    {
                        var parts = line.Split(',');
                        users.Add(new User { Username = parts[0], Password = parts[1], Role = parts[2] });
                    }
                }

                if (line == "Films:")
                {
                    films.Clear();
                    while ((line = reader.ReadLine()) != null && line != "Tickets:")
                    {
                        var parts = line.Split(',');
                        var film = new Film { Judul = parts[0], Durasi = int.Parse(parts[1]) };
                        for (int i = 0; i < 10; i++)
                        {
                            film.Kursi[i] = reader.ReadLine() == "1";
                        }
                        films.Add(film);
                    }
                }

                if (line == "Tickets:")
                {
                    tickets.Clear();
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        var film = films.Find(f => f.Judul == parts[0]);
                        tickets.Add(new Ticket { Film = film, Kursi = int.Parse(parts[1]), Pembayaran = decimal.Parse(parts[2]) });
                    }
                }
            }
        }
        Console.WriteLine("Data berhasil di-restore dari file: backup.txt");
    }
}

class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}

class Film
{
    public string Judul { get; set; }
    public int Durasi { get; set; }
    public bool[] Kursi { get; set; } = new bool[10];
}

class Ticket
{
    public Film Film { get; set; }
    public int Kursi { get; set; }
    public decimal Pembayaran { get; set; }
}