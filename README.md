# 🍳 ChefVN — Ứng dụng chia sẻ công thức nấu ăn

ASP.NET Core MVC (.NET 8) · SQL Server LocalDB · Bootstrap 5.3

---

## ⚡ Chạy ngay (không cần cấu hình gì thêm)

```bash
git clone https://github.com/nh2192679-art/mt3.git
cd mt3/MT3
dotnet run
```

Mở trình duyệt: `https://localhost:xxxx`

> Database **tự động tạo & seed dữ liệu** khi chạy lần đầu.

---

## 🔑 Tài khoản mặc định

| Role  | Email              | Mật khẩu  |
|-------|--------------------|-----------|
| Admin | admin@chefvn.com   | Admin@123 |
| User  | chef@chefvn.com    | Chef@123  |

---

## 📋 Yêu cầu hệ thống

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server LocalDB (có sẵn khi cài Visual Studio)

> Nếu dùng SQL Server Express riêng, sửa chuỗi kết nối trong `appsettings.json`.

---

## 🛠 Cấu trúc project

```
MT3/
├── Controllers/       # MVC Controllers
├── Data/
│   ├── ApplicationDbContext.cs
│   └── SeedData.cs    # Tự động seed dữ liệu lần đầu
├── Migrations/        # EF Core migrations
├── Models/            # Entity models + ViewModels
├── Services/          # Business logic
├── Views/             # Razor Views
└── wwwroot/           # Static files (CSS, JS, images)
```

---

## ✨ Tính năng

- 🔐 Đăng nhập / Đăng ký / Quản lý hồ sơ
- 📖 Duyệt & tìm kiếm công thức nấu ăn
- ✍️ Tạo / Chỉnh sửa / Xóa công thức
- ⭐ Đánh giá & bình luận công thức
- ❤️ Lưu công thức yêu thích
- 📅 Lập lịch ăn hàng tuần
- 🛒 Danh sách mua sắm
- 🛡️ Bảng quản trị Admin
