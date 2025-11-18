# Coolify Deployment Guide

Bu proje Coolify ile deploy edilebilir. Aşağıdaki adımları takip edin:

## 1. Coolify'da Yeni Bir Resource Oluşturun

- **Resource Type**: Docker Image
- **Source**: GitHub Repository
- **Dockerfile Location**: `/Dockerfile` (root dizinde)

## 2. Environment Variables (Ortam Değişkenleri)

Coolify'da aşağıdaki environment variables'ları ayarlayın:

```bash
# Database Connection
ConnectionStrings__DefaultConnection=Host=your-postgres-host;Database=presaleform_db;Username=your-username;Password=your-password;Port=5432

# ASP.NET Core Settings
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:8080

# QuestPDF License (Community)
QUESTPDF_LICENSE=Community
```

## 3. Port Configuration

- **Container Port**: `8080`
- **Public Port**: Coolify otomatik atayacak

## 4. Volume Mounts (Persistent Storage)

PDF dosyalarını kalıcı hale getirmek için Coolify'da bir volume mount ekleyin:

```
Container Path: /app/wwwroot
Host Path: [Coolify tarafından yönetilir]
```

Bu sayede PDF dosyaları container yeniden başlatıldığında kaybolmaz.

## 5. Health Check

Dockerfile'da zaten health check yapılandırılmış:
- **Endpoint**: `http://localhost:8080/api/presaleforms/ping`
- **Interval**: 30 saniye
- **Timeout**: 3 saniye

## 6. Database Setup

### PostgreSQL Container (Coolify üzerinde)

Coolify'da ayrı bir PostgreSQL database service oluşturun:

1. **New Resource** > **Database** > **PostgreSQL**
2. Database bilgilerini not alın
3. Bu bilgileri yukarıdaki `ConnectionStrings__DefaultConnection` değişkenine ekleyin

### Migration

İlk deploy sonrası database migration'ları çalıştırın:

```bash
# Coolify terminal'de
dotnet ef database update --project PreSaleForm.Infrastructure
```

Veya API ilk çalıştığında otomatik migration için `Program.cs`'e eklenebilir:

```csharp
// Program.cs (opsiyonel)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}
```

## 7. Build Arguments (Opsiyonel)

```
BUILD_CONFIGURATION=Release
```

## 8. Resource Limits (Önerilen)

- **Memory Limit**: 512MB - 1GB
- **CPU Limit**: 0.5 - 1.0 CPU

## 9. Coolify Deployment Checklist

- [ ] GitHub repository bağlandı
- [ ] Environment variables ayarlandı
- [ ] PostgreSQL database oluşturuldu
- [ ] Volume mount yapılandırıldı
- [ ] Port mapping doğru (8080)
- [ ] Health check aktif
- [ ] İlk deploy başarılı
- [ ] Database migrations çalıştırıldı
- [ ] PDF oluşturma test edildi

## 10. Logs ve Debugging

Coolify'da logs'u görüntülemek için:
1. Resource sayfasına gidin
2. **Logs** sekmesini açın
3. Real-time logları izleyin

## 11. Backup

PDF dosyaları için düzenli backup ayarlayın:
- Coolify'ın backup özelliğini kullanın
- Volume: `/app/wwwroot` dizinini backup'layın

## 12. SSL/HTTPS

Coolify otomatik olarak Let's Encrypt ile SSL certificate sağlar. Sadece custom domain ayarlayın.

## Test

Deploy sonrası test edin:

```bash
# Health check
curl https://your-domain.com/api/presaleforms/ping

# API test
curl https://your-domain.com/swagger
```

## Troubleshooting

### PDF oluşturulmuyor
- `/app/wwwroot/pdf/presale` dizininin yazma izinleri kontrol edin
- Volume mount'un doğru yapılandırıldığından emin olun

### Database bağlantı hatası
- Connection string'i kontrol edin
- PostgreSQL container'ın çalıştığından emin olun
- Network ayarlarını kontrol edin

### Memory hatası
- Resource limits'i artırın
- Logs'u kontrol edin

## Notlar

- Container her restart'ta `/app/wwwroot` dışındaki dosyalar silinir
- PDF'ler için mutlaka volume mount kullanın
- Environment variables değiştiğinde container'ı yeniden başlatın
- Database backup'ları düzenli alın
- **Docker image'a sadece logo dosyaları (`logo*.png`) dahil edilir**
- **PDF klasörü (`wwwroot/pdf/`) image'a dahil edilmez, runtime'da oluşturulur**
- Bu sayede image boyutu küçük kalır ve güvenlik artar

