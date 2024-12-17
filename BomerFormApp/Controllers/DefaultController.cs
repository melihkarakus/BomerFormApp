using BomerFormApp.DataAccess;
using BomerFormApp.Entities;
using BomerFormApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BomerFormApp.Controllers
{
    public class DefaultController : Controller
    {
        private readonly BomerFormData _formData;

        public DefaultController(BomerFormData formData)
        {
            _formData = formData;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserBomerViewModel userBomerViewModel)
        {

            // Dekont'un yolu için bir değişken tanımlayın
            string dekontPath = null;

            // Dosya yükleme kontrolü
            if (userBomerViewModel.Dekont != null && userBomerViewModel.Dekont.Length > 0)
            {
                // Dosya adını al
                var fileName = Path.GetFileName(userBomerViewModel.Dekont.FileName);
                dekontPath = $"/DekontImage/{fileName}"; // Dosya yolunu oluştur

                // Dizin yolunu tanımlayın
                string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "DekontImage");

                // Dizin var mı kontrol edin, yoksa oluşturun
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = Path.Combine(directoryPath, fileName);

                // Dosyayı kaydet
                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await userBomerViewModel.Dekont.CopyToAsync(stream);
                    }
                }
                catch (Exception ex)
                {
                    // Hata durumunda kullanıcıya bir hata mesajı gösterin
                    ModelState.AddModelError("Dekont", $"Dosya kaydetme sırasında bir hata oluştu: {ex.Message}");
                    return View(userBomerViewModel); // Hata durumunda aynı görünümü geri döndür
                }
            }
            else
            {
                ModelState.AddModelError("Dekont", "Lütfen dekont dosyasını yükleyin.");
                return View(userBomerViewModel); // Hata durumunda aynı görünümü geri döndür
            }

            // Kullanıcı nesnesini oluştur
            User user = new User()
            {
                FullName = userBomerViewModel.FullName,
                StudentNumber = userBomerViewModel.StudentNumber,
                Department = userBomerViewModel.Department,
                Class = userBomerViewModel.Class,
                Email = userBomerViewModel.Email,
                PhoneNumber = userBomerViewModel.PhoneNumber,
                University = string.IsNullOrEmpty(userBomerViewModel.University) ? "Biruni Üniversite" : userBomerViewModel.University,
                Dekont = dekontPath // Dekont'un yolunu User nesnesine ekleyin
            };

            // Kullanıcıyı veritabanına ekle
            await _formData.Users.AddAsync(user); // Async ekleme
            await _formData.SaveChangesAsync(); // Asenkron olarak veritabanına kaydedin

            return RedirectToAction("Index");
        }


    }
}
