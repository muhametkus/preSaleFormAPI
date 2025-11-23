using Microsoft.AspNetCore.Hosting;
using PreSaleForm.Application.Common.Interfaces;
using PreSaleForm.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;

namespace PreSaleForm.Infrastructure.Services;

public class PdfService : IPdfService
{
    private readonly IWebHostEnvironment _env;

    public PdfService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public Task<string> GeneratePreSaleFormPdfAsync(
        PreSaleFormEntity form,
        CancellationToken cancellationToken)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var folder = Path.Combine(_env.WebRootPath ?? "wwwroot", "pdf", "presale");
        Directory.CreateDirectory(folder);

        var fileName = $"PreSale_{form.Id}.pdf";
        var filePath = Path.Combine(folder, fileName);

        var logoPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "logo.png");
        bool hasLogo = File.Exists(logoPath);

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(11).FontColor("#374151"));

                page.Content().Column(col =>
                {
                    // ---------------- HEADER ----------------
                    col.Item().BorderBottom(2).BorderColor("#E5E7EB").PaddingBottom(10).Row(row =>
                    {
                        // Sol taraf - Firma Bilgileri
                        row.RelativeItem().Column(c =>
                        {
                            if (hasLogo)
                            {
                                c.Item().Height(75).Image(logoPath);
                            }
                            else
                            {
                                c.Item().Text("MODERN KAPI SİSTEMLERİ")
                                    .FontSize(20)
                                    .Bold()
                                    .FontColor("#1D4ED8")
                                    .LetterSpacing(0.5f);

                                c.Item().Text("Kapı • Kasa • Montaj • İç Oda Tasarımı")
                                    .FontSize(9)
                                    .FontColor("#6B7280");
                            }
                        });

                        // Sağ taraf - İletişim Bilgileri
                        row.ConstantItem(200).AlignRight().Column(c =>
                        {
                            c.Item().DefaultTextStyle(x => x.FontSize(9).FontColor("#374151"))
                                .Text(txt =>
                                {
                                    txt.Span("Telefon: ").SemiBold();
                                    txt.Span("0 216 397 54 01");
                                });

                            c.Item().DefaultTextStyle(x => x.FontSize(9).FontColor("#374151"))
                                .Text(txt =>
                                {
                                    txt.Span("Adres: ").SemiBold();
                                    txt.Line("Fevziçakmak mh. Mustafa Kemal cd. 4/A");
                                    txt.Line("İstanbul / Pendik");
                                });


                            c.Item().DefaultTextStyle(x => x.FontSize(9).FontColor("#374151"))
                                .Text(txt =>
                                {
                                    txt.Span("E-Posta: ").SemiBold();
                                    txt.Span("info@hebilogluahsap.com");
                                });
                        });
                    });

                    // ---------------- FORM TITLE ----------------
                    col.Item().PaddingTop(15).Row(r =>
                    {
                        r.RelativeItem().Text("ÖN SATIŞ FORMU")
                            .Bold()
                            .FontSize(18)
                            .FontColor("#1F2937");

                        r.ConstantItem(150).AlignRight().Column(rc =>
                        {
                            rc.Item().DefaultTextStyle(x => x.FontSize(10))
                                .Text(txt =>
                                {
                                    txt.Span("Belge No: ").SemiBold();
                                    txt.Span(form.Id.ToString().PadLeft(6, '0')).SemiBold().FontColor("#DC2626");
                                });

                            rc.Item().DefaultTextStyle(x => x.FontSize(10))
                                .Text(txt =>
                                {
                                    txt.Span("Tarih: ").SemiBold();
                                    txt.Span($"{form.CreatedAt:dd.MM.yyyy}");
                                });
                        });
                    });

                    // ---------------- MÜŞTERİ BİLGİLERİ ----------------
                    col.Item().PaddingTop(15).Border(1).BorderColor("#9CA3AF").Column(box =>
                    {
                        // Başlık
                        box.Item()
                            .Background("#F3F4F6")
                            .BorderBottom(1)
                            .BorderColor("#9CA3AF")
                            .Padding(8)
                            .Text("MÜŞTERİ BİLGİLERİ")
                            .SemiBold()
                            .FontSize(11)
                            .FontColor("#374151");

                        // İçerik - 2 Sütunlu Grid
                        box.Item().Padding(10).Row(contentRow =>
                        {
                            contentRow.RelativeItem().Column(leftCol =>
                            {
                                leftCol.Item().DefaultTextStyle(x => x.FontSize(10))
                                    .Text(txt =>
                                    {
                                        txt.Span("Ad Soyad: ").SemiBold();
                                        txt.Span(form.CustomerFullName);
                                    });
                            });

                            contentRow.RelativeItem().Column(rightCol =>
                            {
                                rightCol.Item().DefaultTextStyle(x => x.FontSize(10))
                                    .Text(txt =>
                                    {
                                        txt.Span("Telefon: ").SemiBold();
                                        txt.Span(form.CustomerPhone);
                                    });
                            });
                        });

                        // Not (tam genişlik)
                        if (!string.IsNullOrWhiteSpace(form.Note))
                        {
                            box.Item().PaddingHorizontal(10).PaddingBottom(10)
                                .DefaultTextStyle(x => x.FontSize(10))
                                .Text(txt =>
                                {
                                    txt.Span("Not: ").SemiBold();
                                    txt.Span(form.Note);
                                });
                        }
                    });

                    // ---------------- ÜRÜN TABLOSU ----------------
                    col.Item().PaddingTop(15).Border(1).BorderColor("#9CA3AF").Column(box =>
                    {
                        // Başlık
                        box.Item()
                            .Background("#F3F4F6")
                            .BorderBottom(1)
                            .BorderColor("#9CA3AF")
                            .Padding(8)
                            .Text("ÜRÜN BİLGİLERİ")
                            .SemiBold()
                            .FontSize(11)
                            .FontColor("#374151");

                        // Tablo
                        box.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.RelativeColumn(3); // Model
                                cols.RelativeColumn(2); // Yüzey
                                cols.RelativeColumn(1); // En
                                cols.RelativeColumn(1); // Boy
                                cols.RelativeColumn(1); // Kasa
                                cols.RelativeColumn(1); // Adet
                                cols.RelativeColumn(1); // Cam
                                cols.RelativeColumn(2); // Renk
                                cols.RelativeColumn(2); // Tutar
                            });

                            // HEADER
                            table.Header(header =>
                            {
                                header.Cell()
                                    .Background("#E5E7EB")
                                    .BorderBottom(1)
                                    .BorderRight(1)
                                    .BorderColor("#9CA3AF")
                                    .Padding(5)
                                    .Text("Model")
                                    .SemiBold()
                                    .FontSize(9)
                                    .FontColor("#374151");

                                header.Cell()
                                    .Background("#E5E7EB")
                                    .BorderBottom(1)
                                    .BorderRight(1)
                                    .BorderColor("#9CA3AF")
                                    .Padding(5)
                                    .Text("Yüzey")
                                    .SemiBold()
                                    .FontSize(9)
                                    .FontColor("#374151");

                                header.Cell()
                                    .Background("#E5E7EB")
                                    .BorderBottom(1)
                                    .BorderRight(1)
                                    .BorderColor("#9CA3AF")
                                    .Padding(5)
                                    .Text("En")
                                    .SemiBold()
                                    .FontSize(9)
                                    .FontColor("#374151");

                                header.Cell()
                                    .Background("#E5E7EB")
                                    .BorderBottom(1)
                                    .BorderRight(1)
                                    .BorderColor("#9CA3AF")
                                    .Padding(5)
                                    .Text("Boy")
                                    .SemiBold()
                                    .FontSize(9)
                                    .FontColor("#374151");

                                header.Cell()
                                    .Background("#E5E7EB")
                                    .BorderBottom(1)
                                    .BorderRight(1)
                                    .BorderColor("#9CA3AF")
                                    .Padding(5)
                                    .Text("Kasa")
                                    .SemiBold()
                                    .FontSize(9)
                                    .FontColor("#374151");

                                header.Cell()
                                    .Background("#E5E7EB")
                                    .BorderBottom(1)
                                    .BorderRight(1)
                                    .BorderColor("#9CA3AF")
                                    .Padding(5)
                                    .Text("Adet")
                                    .SemiBold()
                                    .FontSize(9)
                                    .FontColor("#374151");

                                header.Cell()
                                    .Background("#E5E7EB")
                                    .BorderBottom(1)
                                    .BorderRight(1)
                                    .BorderColor("#9CA3AF")
                                    .Padding(5)
                                    .Text("Cam")
                                    .SemiBold()
                                    .FontSize(9)
                                    .FontColor("#374151");

                                header.Cell()
                                    .Background("#E5E7EB")
                                    .BorderBottom(1)
                                    .BorderRight(1)
                                    .BorderColor("#9CA3AF")
                                    .Padding(5)
                                    .Text("Renk")
                                    .SemiBold()
                                    .FontSize(9)
                                    .FontColor("#374151");

                                header.Cell()
                                    .Background("#E5E7EB")
                                    .BorderBottom(1)
                                    .BorderRight(1)
                                    .BorderColor("#9CA3AF")
                                    .Padding(5)
                                    .Text("Tutar")
                                    .SemiBold()
                                    .FontSize(9)
                                    .FontColor("#374151");
                            });

                            // ROWS
                            foreach (var p in form.Products)
                            {
                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text(p.DoorModel)
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text(p.DoorSurfaceType)
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text(p.DoorLeafWidth.ToString())
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text(p.DoorLeafHeight.ToString())
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text(p.DoorFrameWidth.ToString())
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text(p.DoorQuantity.ToString())
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text(p.IsWithGlass ? "Var" : "Yok")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text(p.Color)
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text($"{p.Amount:N0} TL")
                                    .FontSize(9)
                                    .FontColor("#1F2937");
                            }

                            // Aksesuar ücreti varsa ek satır ekle
                            if (form.AksesuarUcreti.HasValue && form.AksesuarUcreti.Value > 0)
                            {
                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text(form.SecilenAksesuar ?? "Aksesuar")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text("-")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text("-")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text("-")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text("-")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text("-")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text("-")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text("-")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text($"{form.AksesuarUcreti.Value:N0} TL")
                                    .FontSize(9)
                                    .FontColor("#1F2937");
                            }

                            // Söküm ücreti varsa ek satır ekle
                            if (form.TotalDismantlingPrice.HasValue && form.TotalDismantlingPrice.Value > 0)
                            {
                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text($"Söküm ({form.OldDoorCount ?? 0} adet)")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text("-")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text("-")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text("-")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text("-")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text(form.OldDoorCount?.ToString() ?? "-")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text("-")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text("-")
                                    .FontSize(9)
                                    .FontColor("#1F2937");

                                table.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(5)
                                    .Text($"{form.TotalDismantlingPrice.Value:N0} TL")
                                    .FontSize(9)
                                    .FontColor("#1F2937");
                            }
                        });
                    });

                    // ---------------- ÖDEME BİLGİLERİ ----------------
                    col.Item().PaddingTop(15).Border(1).BorderColor("#9CA3AF").Column(box =>
                    {
                        // Başlık
                        box.Item()
                            .Background("#F3F4F6")
                            .BorderBottom(1)
                            .BorderColor("#9CA3AF")
                            .Padding(8)
                            .Text("ÖDEME BİLGİLERİ")
                            .SemiBold()
                            .FontSize(11)
                            .FontColor("#374151");

                        // Ödeme Tablosu
                        box.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                            });

                            // Toplam Tutar
                            t.Cell()
                                .Border(1)
                                .BorderColor("#D1D5DB")
                                .Background("#F9FAFB")
                                .Padding(10)
                                .Text("Toplam Tutar")
                                .SemiBold()
                                .FontSize(12);

                            t.Cell()
                                .Border(1)
                                .BorderColor("#D1D5DB")
                                .Padding(10)
                                .AlignRight()
                                .Text($"{form.TotalAmount:N0} TL")
                                .Bold()
                                .FontSize(12);

                            // İndirim Tutarı (varsa göster)
                            if (form.DiscountAmount.HasValue && form.DiscountAmount.Value > 0)
                            {
                                t.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Background("#F9FAFB")
                                    .Padding(10)
                                    .Text("İndirim Tutarı")
                                    .SemiBold()
                                    .FontSize(12);

                                t.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(10)
                                    .AlignRight()
                                    .Text($"-{form.DiscountAmount.Value:N0} TL")
                                    .Bold()
                                    .FontSize(12)
                                    .FontColor("#DC2626");
                            }

                            // İndirimli Son Fiyat (varsa göster)
                            if (form.DiscountedAmount.HasValue && form.DiscountedAmount.Value > 0)
                            {
                                t.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Background("#F9FAFB")
                                    .Padding(10)
                                    .Text("İndirimli Son Fiyat")
                                    .SemiBold()
                                    .FontSize(12)
                                    .FontColor("#059669");

                                t.Cell()
                                    .Border(1)
                                    .BorderColor("#D1D5DB")
                                    .Padding(10)
                                    .AlignRight()
                                    .Text($"{form.DiscountedAmount.Value:N0} TL")
                                    .Bold()
                                    .FontSize(12)
                                    .FontColor("#059669");
                            }

                            // Ödenen Tutar
                            t.Cell()
                                .Border(1)
                                .BorderColor("#D1D5DB")
                                .Background("#F9FAFB")
                                .Padding(10)
                                .Text("Ödenen Tutar")
                                .SemiBold()
                                .FontSize(12);

                            t.Cell()
                                .Border(1)
                                .BorderColor("#D1D5DB")
                                .Padding(10)
                                .AlignRight()
                                .Text($"{form.PaidAmount:N0} TL")
                                .Bold()
                                .FontSize(12);

                            // Kalan Tutar
                            t.Cell()
                                .Border(1)
                                .BorderColor("#D1D5DB")
                                .Background("#F9FAFB")
                                .Padding(10)
                                .Text("Kalan Tutar")
                                .SemiBold()
                                .FontSize(12);

                            t.Cell()
                                .Border(1)
                                .BorderColor("#D1D5DB")
                                .Padding(10)
                                .AlignRight()
                                .Text($"{form.RemainingAmount:N0} TL")
                                .Bold()
                                .FontSize(12)
                                .FontColor("#DC2626");
                        });
                    });

                    // ---------------- SÖZLEŞME VE ŞARTLAR ----------------
                    col.Item().PaddingTop(25).Column(x =>
                    {
                        x.Item().Text("Sözleşme ve Şartlar")
                            .SemiBold()
                            .FontSize(13)
                            .FontColor("#374151");

                        // Dinamik şartlar metni
                        var sartlarMetni = "Bu form, müşteri tarafından onaylanan ön satış koşullarını içermektedir. " +
                            "Ölçü, renk ve diğer tüm detayların doğruluğu müşteriye aittir. " +
                            "Üretim sürecinde bu forma uyulacaktır.";

                        if (form.MontajDahilMi == true)
                        {
                            sartlarMetni += "\n\nAnlaşılan şartlara montaj hizmeti dahildir.";
                        }
                        else
                        {
                            sartlarMetni += "\n\nAnlaşılan şartlara montaj hizmeti dahil değildir. Demonte olarak teslim edilecektir.";

                            // Montaj dahil değilse nakliye ve teslim bilgilerini ekle
                            if (form.NakliyeDahilMi == true)
                            {
                                sartlarMetni += " Hizmete nakliye dahildir.";
                            }
                            else if (form.NakliyeDahilMi == false)
                            {
                                sartlarMetni += " Hizmete nakliye dahil değildir.";
                            }

                            if (form.FabrikaTeslimMi == true)
                            {
                                sartlarMetni += " Fabrikadan teslim edilecektir.";
                            }

                            // Aksesuar bilgilerini ekle
                            if (form.AksesuarDahilMi == true)
                            {
                                sartlarMetni += " Aksesuar (Kapı Kolu, Kilit, Menteşe, Fitil) dahildir.";
                            }
                            else if (form.AksesuarDahilMi == false)
                            {
                                sartlarMetni += " Aksesuar (Kapı Kolu, Kilit, Menteşe, Fitil) hariçtir.";
                            }
                        }

                        x.Item().PaddingTop(5).Text(sartlarMetni)
                            .FontSize(9)
                            .FontColor("#6B7280")
                            .LineHeight(1.5f);
                    });

                    // ---------------- İMZA ALANLARI ----------------
                    col.Item().PaddingTop(20).PaddingHorizontal(30).Row(r =>
                    {
                        // Müşteri İmzası
                        r.RelativeItem().Column(c =>
                        {
                            c.Item().Height(20).Width(180).BorderBottom(1).BorderColor("#6B7280");
                            c.Item().PaddingTop(5).AlignCenter().Text("Müşteri İmzası")
                                .FontSize(9)
                                .SemiBold()
                                .FontColor("#374151");
                        });

                        // Boşluk
                        r.ConstantItem(40);

                        // Firma Yetkilisi
                        r.RelativeItem().Column(c =>
                        {
                            c.Item().Height(20).Width(180).BorderBottom(1).BorderColor("#6B7280");
                            c.Item().PaddingTop(5).AlignCenter().Text("Firma Yetkilisi")
                                .FontSize(9)
                                .SemiBold()
                                .FontColor("#374151");
                        });
                    });
                });
            });
        });

        document.GeneratePdf(filePath);

        return Task.FromResult($"/pdf/presale/{fileName}");
    }
}
