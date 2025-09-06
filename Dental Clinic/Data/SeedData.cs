using System.Numerics;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        // ✅ إضافة الاختصاصات الخاصة بطب الأسنان
        if (!context.Specializations.Any())
        {
            context.Specializations.AddRange(new Specialization[]
            {
                new Specialization { Name = "تقويم الأسنان" },
                new Specialization { Name = "جراحة الفم والوجه والفكين" },
                new Specialization { Name = "علاج جذور الأسنان" },
                new Specialization { Name = "طب أسنان الأطفال" },
                new Specialization { Name = "التركيبات السنية" },
                new Specialization { Name = "زراعة الأسنان" },
                new Specialization { Name = "طب الأسنان الوقائي" },
                new Specialization { Name = "طب الأسنان التجميلي" },
                new Specialization { Name = "أمراض الفم واللثة" },
                new Specialization { Name = "أشعة الأسنان" }
            });
            context.SaveChanges();
        }

        var specializationIds = context.Specializations.Select(s => s.Id).ToList();

        // ✅ إضافة المرضى
        if (!context.Patients.Any())
        {
            context.Patients.AddRange(new Patient[]
            {
                new Patient { Name = "أحمد علي", DateOfBirth = new DateTime(1990, 5, 12), Gender = "ذكر", PhoneNumber = "0123456789", Email = "ahmed@example.com", Address = "دمشق، سوريا", HealthStatus = "جيد" },
                new Patient { Name = "سارة خالد", DateOfBirth = new DateTime(1985, 8, 24), Gender = "أنثى", PhoneNumber = "0987654321", Email = "sara@example.com", Address = "حلب، سوريا", HealthStatus = "مستقر" },
                new Patient { Name = "محمد يوسف", DateOfBirth = new DateTime(2000, 1, 10), Gender = "ذكر", PhoneNumber = "0112233445", Email = "mohamed@example.com", Address = "اللاذقية، سوريا", HealthStatus = "حرج" },
                new Patient { Name = "لينا فواز", DateOfBirth = new DateTime(1995, 12, 5), Gender = "أنثى", PhoneNumber = "0223344556", Email = "lina@example.com", Address = "طرطوس، سوريا", HealthStatus = "جيد جدًا" },
                new Patient { Name = "خالد عمران", DateOfBirth = new DateTime(1988, 7, 19), Gender = "ذكر", PhoneNumber = "0334455667", Email = "khaled@example.com", Address = "حمص، سوريا", HealthStatus = "متوسط" },
                new Patient { Name = "مها يونس", DateOfBirth = new DateTime(1993, 9, 8), Gender = "أنثى", PhoneNumber = "0445566778", Email = "maha@example.com", Address = "إدلب، سوريا", HealthStatus = "حرج جدًا" },
                new Patient { Name = "زياد حسن", DateOfBirth = new DateTime(1977, 3, 21), Gender = "ذكر", PhoneNumber = "0556677889", Email = "ziad@example.com", Address = "دير الزور، سوريا", HealthStatus = "جيد" },
                new Patient { Name = "منى جابر", DateOfBirth = new DateTime(1982, 11, 14), Gender = "أنثى", PhoneNumber = "0667788990", Email = "mona@example.com", Address = "السويداء، سوريا", HealthStatus = "مستقر" },
                new Patient { Name = "فراس سالم", DateOfBirth = new DateTime(1999, 6, 30), Gender = "ذكر", PhoneNumber = "0778899001", Email = "firas@example.com", Address = "الحسكة، سوريا", HealthStatus = "متوسط" },
                new Patient { Name = "هدى عبد الرحمن", DateOfBirth = new DateTime(1991, 4, 17), Gender = "أنثى", PhoneNumber = "0889900112", Email = "huda@example.com", Address = "الرقة، سوريا", HealthStatus = "حرج" }
            });
        }

        // ✅ إضافة الأطباء مع ربطهم بتخصصات طب الأسنان
        if (!context.Doctors.Any())
        {
            context.Doctors.AddRange(new Doctor[]
            {
                new Doctor { Name = "د. أحمد علي", DateOfBirth = new DateTime(1975, 5, 12), Gender = "ذكر", PhoneNumber = "0123456789", Email = "ahmed@example.com", Address = "دمشق، سوريا", SpecializationId = specializationIds[0], LicenseNumber = "LIC001" },
                new Doctor { Name = "د. سارة خالد", DateOfBirth = new DateTime(1980, 8, 24), Gender = "أنثى", PhoneNumber = "0987654321", Email = "sara@example.com", Address = "حلب، سوريا", SpecializationId = specializationIds[1], LicenseNumber = "LIC002" },
                new Doctor { Name = "د. محمد يوسف", DateOfBirth = new DateTime(1985, 1, 10), Gender = "ذكر", PhoneNumber = "0112233445", Email = "mohamed@example.com", Address = "اللاذقية، سوريا", SpecializationId = specializationIds[2], LicenseNumber = "LIC003" },
                new Doctor { Name = "د. لينا فواز", DateOfBirth = new DateTime(1990, 12, 5), Gender = "أنثى", PhoneNumber = "0223344556", Email = "lina@example.com", Address = "طرطوس، سوريا", SpecializationId = specializationIds[3], LicenseNumber = "LIC004" },
                new Doctor { Name = "د. خالد عمران", DateOfBirth = new DateTime(1978, 7, 19), Gender = "ذكر", PhoneNumber = "0334455667", Email = "khaled@example.com", Address = "حمص، سوريا", SpecializationId = specializationIds[4], LicenseNumber = "LIC005" },
                new Doctor { Name = "د. مها يونس", DateOfBirth = new DateTime(1983, 9, 8), Gender = "أنثى", PhoneNumber = "0445566778", Email = "maha@example.com", Address = "إدلب، سوريا", SpecializationId = specializationIds[5], LicenseNumber = "LIC006" },
                new Doctor { Name = "د. زياد حسن", DateOfBirth = new DateTime(1967, 3, 21), Gender = "ذكر", PhoneNumber = "0556677889", Email = "ziad@example.com", Address = "دير الزور، سوريا", SpecializationId = specializationIds[6], LicenseNumber = "LIC007" },
                new Doctor { Name = "د. منى جابر", DateOfBirth = new DateTime(1972, 11, 14), Gender = "أنثى", PhoneNumber = "0667788990", Email = "mona@example.com", Address = "السويداء، سوريا", SpecializationId = specializationIds[7], LicenseNumber = "LIC008" },
                new Doctor { Name = "د. فراس سالم", DateOfBirth = new DateTime(1995, 6, 30), Gender = "ذكر", PhoneNumber = "0778899001", Email = "firas@example.com", Address = "الحسكة، سوريا", SpecializationId = specializationIds[8], LicenseNumber = "LIC009" },
                new Doctor { Name = "د. هدى عبد الرحمن", DateOfBirth = new DateTime(1989, 4, 17), Gender = "أنثى", PhoneNumber = "0889900112", Email = "huda@example.com", Address = "الرقة، سوريا", SpecializationId = specializationIds[9], LicenseNumber = "LIC010" }
            });
        }

        context.SaveChanges();

        if (!context.Appointments.Any())
        {
            var appointments = new List<Appointment>
    {
        new Appointment {
            Start = new DateTime(2025, 5, 20, 14, 30, 0),
            End   = new DateTime(2025, 5, 20, 15, 0, 0),
            Status = "مؤكد", DoctorId = 1, PatientId = 3
        },
        new Appointment {
            Start = new DateTime(2025, 5, 21, 10, 0, 0),
            End   = new DateTime(2025, 5, 21, 10, 30, 0),
            Status = "ملغي", DoctorId = 2, PatientId = 5
        },
        new Appointment {
            Start = new DateTime(2025, 5, 22, 16, 45, 0),
            End   = new DateTime(2025, 5, 22, 17, 15, 0),
            Status = "مكتمل", DoctorId = 3, PatientId = 7
        },
        new Appointment {
            Start = new DateTime(2025, 5, 23, 11, 15, 0),
            End   = new DateTime(2025, 5, 23, 11, 45, 0),
            Status = "مؤكد", DoctorId = 4, PatientId = 8
        },
        new Appointment {
            Start = new DateTime(2025, 5, 24, 9, 30, 0),
            End   = new DateTime(2025, 5, 24, 10, 0, 0),
            Status = "مؤكد", DoctorId = 5, PatientId = 10
        },
        // أضف المزيد بنفس النمط
    };

            context.Appointments.AddRange(appointments);
            context.SaveChanges();
        }
        if (!context.PaymentMethods.Any())
        {
            context.PaymentMethods.AddRange(new PaymentMethod[]
            {
                new PaymentMethod { MethodName = "نقدي" },
                new PaymentMethod { MethodName = "بطاقة ائتمان" },
                new PaymentMethod { MethodName = "تحويل بنكي" }
            });
        }

        context.SaveChanges();

        if (!context.Invoices.Any())
        {
            context.Invoices.AddRange(new Invoice[]
            {
                new Invoice
                {
                    PatientId = 1,
                    TotalAmount = 500,
                    IssueDate = new DateTime(2025, 6, 10),
                    DueDate = new DateTime(2025, 6, 17),
                    Status = InvoiceStatus.Pending,
                    AppointmentId = 1
                },
                new Invoice
                {

                    PatientId = 2,
                    TotalAmount = 300,
                    IssueDate = new DateTime(2025, 6, 5),
                    DueDate = new DateTime(2025, 6, 12),
                    Status = InvoiceStatus.Pending,
                    AppointmentId = 2
                },
                new Invoice
                {

                    PatientId = 3,
                    TotalAmount = 400,
                    IssueDate = new DateTime(2025, 6, 8),
                    DueDate = new DateTime(2025, 6, 15),
                    Status = InvoiceStatus.Pending,
                    AppointmentId = 3
                },
        });



        }
        context.SaveChanges();

        /*if (!context.Payments.Any())
        {
           // context.Payments.AddRange(new Payment[]
            {
                new Payment
        {

            Amount = 100,
            Status = PaymentStatus.Paid,
            PaymentDate = new DateTime(2025, 6, 1),
            PatientId = 1,
            InvoiceId = 1,
            PaymentType = PaymentType.Cash
        },
                new Payment
        {

            Amount = 200,
            Status = PaymentStatus.Pending,
            PaymentDate = new DateTime(2025, 6, 5),
            PatientId = 2,
            InvoiceId = 2,
            PaymentType = PaymentType.Online
        },
                new Payment
        {


            Amount = 0,
            Status = PaymentStatus.Paid,
            PaymentDate = new DateTime(2025, 6, 7),
            PatientId = 3,
            InvoiceId = 3,
            PaymentType = PaymentType.Free
        }
            });
        }*/



        context.MedicalRecords.AddRange(new MedicalRecord[]
            {
                new MedicalRecord { RecordDate = DateTime.UtcNow.AddMonths(-3), Diagnosis = "تسوس الأسنان الحاد", Medications = "فلوريد الصوديوم، مسكنات الألم", Notes = "ضرورة حشو الأسنان وإجراء تنظيف شامل", PatientId = 1 },
                new MedicalRecord { RecordDate = DateTime.UtcNow.AddMonths(-6), Diagnosis = "التهاب اللثة", Medications = "غسول الفم المضاد للبكتيريا، مضادات الالتهاب", Notes = "ينصح باستخدام فرشاة ناعمة ومعجون خاص باللثة", PatientId = 2 },
                new MedicalRecord { RecordDate = DateTime.UtcNow.AddMonths(-1), Diagnosis = "خلع ضرس العقل", Medications = "مسكنات الألم، مضاد حيوي للوقاية من العدوى", Notes = "يجب متابعة التئام الجرح وتجنب الأطعمة الصلبة", PatientId = 3 }
            });
        context.SaveChanges();
        context.MedicalImages.AddRange(new MedicalImage[]
            {
                new MedicalImage { ImageUrl = "https://example.com/images/cavity.jpg", Description = "أشعة سينية تُظهر تسوس الأسنان الحاد", MedicalRecordId = 1},
                new MedicalImage { ImageUrl = "https://example.com/images/gum_infection.jpg", Description = "صورة توضح التهاب اللثة الحاد", MedicalRecordId = 2 },
                new MedicalImage { ImageUrl = "https://example.com/images/wisdom_tooth.jpg", Description = "صورة لمكان ضرس العقل بعد الجراحة", MedicalRecordId = 3 }
            });
        context.SaveChanges();

        if (!context.DentalModels.Any())
        {
            context.DentalModels.AddRange(
                new DentalModel
                {

                    ToothNumber = 11,
                    Condition = "سليم",
                    PatientId = 1,
                    MedicalRecordId = 1,

                },
                new DentalModel
                {
                    ToothNumber = 26,
                    Condition = "مسوس",
                    PatientId = 2,
                    MedicalRecordId = 2,

                }
            );
            context.SaveChanges();
        }

        if (!context.DentalTreatments.Any())
        {
            var treatments = new List<DentalTreatment>
        {
            new DentalTreatment
            {
                TreatmentType = "تنظيف",
                TreatmentDate = DateTime.Now.AddDays(-7),
                Notes = "تنظيف الأسنان بشكل عام",
                DoctorId = 1,
                DentalModelId = 1
            },
            new DentalTreatment
            {
                TreatmentType = "علاج عصب",
                TreatmentDate = DateTime.Now.AddDays(-3),
                Notes = "جلسة أولى لعلاج العصب",
                DoctorId = 1,
                DentalModelId = 1
            }
        };

            context.DentalTreatments.AddRange(treatments);
            context.SaveChanges();
        }
        if (!context.Users.Any())
        {

            CreatePasswordHash("admin123", out var hash1, out var salt1);
            CreatePasswordHash("user123", out var hash2, out var salt2);

            context.Users.AddRange(
                new User
                {
                    Username = "admin",
                    PasswordHash = hash1,
                    PasswordSalt = salt1,
                    Role = "Admin",
                    DateofBirth = new DateTime(2025, 5, 20, 14, 30, 0)
                },
                new User
                {
                    Username = "user",
                    PasswordHash = hash2,
                    PasswordSalt = salt2,
                    Role = "User",
                    DateofBirth = new DateTime(2025, 5, 20, 14, 30, 0)
                }
            );
            context.SaveChanges();


        }

    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
}




public static class ModelBuilderExtensions
{
    public static void SeedUsers(this ModelBuilder modelBuilder)
    {

    }


}