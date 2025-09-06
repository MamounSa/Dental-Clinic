
var builder = WebApplication.CreateBuilder(args);

// -------------------------
// 1) تحميل الإعدادات
// -------------------------
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// -------------------------
// 2) إضافة خدمات الـ Controllers و Swagger
// -------------------------
builder.Services.AddControllers();

// لتمكين استكشاف الـ Endpoints في Swagger
builder.Services.AddEndpointsApiExplorer();

// إعداد Swagger + قراءة ملف الـ XML الخاص بالتعليقات لعرضها في الوثائق
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// -------------------------
// 3) تسجيل خدمات التطبيق الخاصة (Custom Services)
// -------------------------
builder.Services.RegisterServices();

// -------------------------
// 4) تفعيل سياسة CORS (السماح لأي مصدر بالاتصال بالـ API)
// -------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// -------------------------
// 5) AutoMapper + Database Context
// -------------------------
builder.Services.AddAutoMapper(typeof(AssemblyMarker));
builder.Services.AddDatabaseContext(builder.Configuration);

// -------------------------
// 6) إعدادات JWT للمصادقة
// -------------------------
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtSettings = jwtSection.Get<JwtSettings>();

// تسجيل خدمة المصادقة باستخدام JWT Bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,                          // التحقق من الـ Issuer
            ValidateAudience = true,                        // التحقق من الـ Audience
            ValidateLifetime = true,                        // التحقق من صلاحية التوكن
            ValidateIssuerSigningKey = true,                // التحقق من مفتاح التوقيع
            ValidIssuer = jwtSettings.Issuer,               // المصدر الصحيح للتوكن
            ValidAudience = jwtSettings.Audience,           // الجمهور الصحيح
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key))    // المفتاح السري للتوقيع
        };
    });

// -------------------------
// 7) تعريف سياسة تفويض مخصصة (Minimum Age)
// -------------------------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AtLeast18", policy =>
        policy.Requirements.Add(new MinimumAgeRequirement(18)));
});

// تسجيل الـ Handler المسؤول عن التحقق من الشرط
builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
// -------------------------
// 8) فرض المصادقة بشكل افتراضي على كل Controllers
// -------------------------
/*builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});*/

// -------------------------
// 9) إعداد Hangfire (للـ Background Jobs)
// -------------------------
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

// تفعيل خادم Hangfire داخل التطبيق
builder.Services.AddHangfireServer();
// -------------------------
// 10) تسجيل خدمات Scoped (تنشأ لكل طلب جديد)
// -------------------------
builder.Services.AddScoped<IAppointmentStatusUpdater, AppointmentStatusUpdater>();
builder.Services.AddScoped<IInvoiceValidator, InvoiceValidator>();

// -------------------------
// 11) بناء التطبيق
// -------------------------
var app = builder.Build();

// -------------------------
// 12) Middlewares
// -------------------------

// Middleware لتعامل مع الاستثناءات وتحويلها إلى ردود مناسبة
app.UseMiddleware<ExceptionHandlingMiddleware>();

// تفعيل Swagger فقط في بيئة التطوير
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// إعادة التوجيه من HTTP إلى HTTPS
app.UseHttpsRedirection();

// تفعيل CORS (مهم: لازم ينكتب قبل MapControllers)
app.UseCors("AllowAll");

// تفعيل المصادقة والتفويض
app.UseAuthentication();
app.UseAuthorization();

// ربط الـ Controllers بالـ Routing
app.MapControllers();

// تفعيل لوحة Hangfire لمراقبة الـ Jobs (افتراضياً على /hangfire)
app.UseHangfireDashboard();

// -------------------------
// 13) أمثلة لإضافة Jobs
// -------------------------

// مثال: مهمة متكررة كل دقيقة لتحديث المواعيد المنتهية
RecurringJob.AddOrUpdate<IAppointmentStatusUpdater>(
    "update-missed-appointments",
    updater => updater.UpdateExpiredAppointmentsAsync(),
    Cron.Minutely);
// -------------------------
// 14) تشغيل التطبيق
// -------------------------
app.Run();