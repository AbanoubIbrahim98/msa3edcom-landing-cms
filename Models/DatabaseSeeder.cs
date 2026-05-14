namespace Msa3edcomAdmin.Models;

public static class DatabaseSeeder
{
    public const string DefaultLogoUrl    = "/img/msa3edcom-logo.svg";
    public const string DefaultFaviconUrl = "/img/msa3edcom-logo.svg";

    public static void Seed(ApplicationDbContext db)
    {
        SeedSiteContent(db);
        SeedSiteSettings(db);
        BackfillBrandAssets(db);
        SeedServices(db);
        SeedPortfolio(db);
        SeedTestimonials(db);
        SeedFaqs(db);
        SeedTechStack(db);
        SeedStats(db);
        SeedProcess(db);
        SeedClients(db);

        db.SaveChanges();
    }

    private static void BackfillBrandAssets(ApplicationDbContext db)
    {
        var content = db.SiteContents.FirstOrDefault();
        if (content is not null)
        {
            if (string.IsNullOrWhiteSpace(content.LogoUrl))    content.LogoUrl    = DefaultLogoUrl;
            if (string.IsNullOrWhiteSpace(content.FaviconUrl)) content.FaviconUrl = DefaultFaviconUrl;
        }

        var settings = db.SiteSettings.FirstOrDefault();
        if (settings is not null && string.IsNullOrWhiteSpace(settings.FaviconUrl))
        {
            settings.FaviconUrl = DefaultFaviconUrl;
        }
    }

    private static void SeedSiteContent(ApplicationDbContext db)
    {
        if (db.SiteContents.Any()) return;

        db.SiteContents.Add(new SiteContent
        {
            LogoUrl    = DefaultLogoUrl,
            FaviconUrl = DefaultFaviconUrl,

            Phone    = "+96561001089",
            WhatsApp = "+96561001089",
            Email    = "Abanoubhabak98@gmail.com",
            Address  = "Kuwait City, Kuwait",

            HeroEyebrowEn = "Enterprise Software Engineering",
            HeroEyebrowAr = "هندسة برمجيات على مستوى المؤسسات",

            HeroTitleEn = "We build software that runs serious businesses and governments.",
            HeroTitleAr = "نبني أنظمة برمجية تدير مؤسسات حقيقية وجهات حكومية بثقة.",

            HeroSubtitleEn = "From secure government platforms to scalable enterprise systems and polished mobile apps — msa3edcom delivers production-grade digital solutions engineered for the Gulf market.",
            HeroSubtitleAr = "من الأنظمة الحكومية الآمنة إلى المنصات المؤسسية القابلة للتوسّع وتطبيقات الهاتف الاحترافية — تقدّم msa3edcom حلولًا رقمية بمعايير الإنتاج الحقيقية، مصمّمة خصيصًا لسوق الخليج.",

            HeroPrimaryCtaTextEn = "Start a Project",
            HeroPrimaryCtaTextAr = "ابدأ مشروعك",
            HeroPrimaryCtaUrl    = "#contact",

            HeroSecondaryCtaTextEn = "Our Work",
            HeroSecondaryCtaTextAr = "أعمالنا",
            HeroSecondaryCtaUrl    = "#portfolio",

            AboutTitleEn = "An engineering team building software that lasts.",
            AboutTitleAr = "فريق هندسي يبني برمجيات تدوم طويلًا.",
            AboutTextEn  = "We're a Kuwait-based software house specialized in enterprise systems, government platforms, and modern digital products. We combine strong engineering discipline with a deep understanding of the regional market.",
            AboutTextAr  = "نحن شركة برمجيات مقرّها الكويت، متخصّصون في الأنظمة المؤسسية والمنصات الحكومية والمنتجات الرقمية الحديثة. نجمع بين انضباط هندسي قوي وفهم عميق لسوق المنطقة.",

            ServicesTitleEn = "End-to-end software solutions, engineered to production standard.",
            ServicesTitleAr = "حلول برمجية متكاملة، مبنية وفق معايير الإنتاج الحقيقية.",
            ServicesLeadEn  = "Web, mobile, government, and enterprise systems — built with .NET, modern frameworks, and decades of combined experience.",
            ServicesLeadAr  = "أنظمة ويب وموبايل وحكومية ومؤسسية — مبنية بـ .NET وتقنيات حديثة وخبرات هندسية مجتمعة.",

            PortfolioTitleEn = "Selected work from production deployments.",
            PortfolioTitleAr = "نماذج مختارة من أعمالنا في بيئات الإنتاج.",
            PortfolioLeadEn  = "Real systems serving real users — government platforms, enterprise tools, mobile apps.",
            PortfolioLeadAr  = "أنظمة حقيقية تخدم مستخدمين حقيقيين — منصات حكومية وأدوات مؤسسية وتطبيقات هاتف.",

            CtaBannerTitleEn    = "Ready to build something solid?",
            CtaBannerTitleAr    = "جاهز لبناء شيء متين؟",
            CtaBannerSubtitleEn = "Tell us about your project. We'll come back with a clear scope, timeline, and budget within 48 hours.",
            CtaBannerSubtitleAr = "أخبرنا عن مشروعك. سنعود إليك بنطاق واضح وجدول زمني وميزانية خلال 48 ساعة.",
            CtaBannerButtonTextEn = "Schedule a Call",
            CtaBannerButtonTextAr = "احجز اتصالًا",
            CtaBannerButtonUrl    = "#contact",

            FooterTextEn = "© msa3edcom. All rights reserved.",
            FooterTextAr = "© msa3edcom. جميع الحقوق محفوظة.",

            LinkedInUrl = "#",
            TwitterUrl  = "#",
            GitHubUrl   = "#",
            UpdatedAt   = DateTime.UtcNow
        });
    }

    private static void SeedSiteSettings(ApplicationDbContext db)
    {
        if (db.SiteSettings.Any()) return;

        db.SiteSettings.Add(new SiteSettings
        {
            FaviconUrl        = DefaultFaviconUrl,
            MetaTitleEn       = "msa3edcom — Enterprise Software & Digital Solutions",
            MetaTitleAr       = "msa3edcom — حلول البرمجيات والتحوّل الرقمي",
            MetaDescriptionEn = "We build enterprise-grade web platforms, mobile apps, and government systems for Kuwait and the Gulf.",
            MetaDescriptionAr = "نبني منصات ويب وتطبيقات وأنظمة حكومية بمستوى المؤسسات للكويت ومنطقة الخليج.",
            MetaKeywordsEn    = "software, kuwait, gulf, enterprise, government, mobile apps, .net, flutter",
            MetaKeywordsAr    = "برمجيات, الكويت, الخليج, أنظمة حكومية, تطبيقات",
            PrimaryColor      = "#6366F1",
            SecondaryColor    = "#A855F7",
            UpdatedAt         = DateTime.UtcNow
        });
    }

    private static void SeedServices(ApplicationDbContext db)
    {
        if (db.ServiceItems.Any()) return;

        db.ServiceItems.AddRange(
            new ServiceItem { TitleEn = "Web Development",       TitleAr = "تطوير الويب",        DescriptionEn = "Fast, secure, and scalable web platforms built with .NET and modern frameworks.", DescriptionAr = "منصات ويب سريعة وآمنة وقابلة للتوسّع، مبنية على .NET وتقنيات حديثة.",       IconClass = "bi-code-slash",   DisplayOrder = 1 },
            new ServiceItem { TitleEn = "Mobile Applications",   TitleAr = "تطبيقات الهاتف",     DescriptionEn = "Cross-platform Flutter apps with native-grade performance and full Arabic RTL support.", DescriptionAr = "تطبيقات Flutter متعددة المنصات بأداء قريب من النيتيف ودعم كامل للعربية.", IconClass = "bi-phone",        DisplayOrder = 2 },
            new ServiceItem { TitleEn = "Government Systems",    TitleAr = "الأنظمة الحكومية",   DescriptionEn = "Compliant, secure, and auditable platforms tailored to public-sector requirements.",      DescriptionAr = "منصات آمنة ومتوافقة وقابلة للتدقيق، مصمّمة وفق متطلبات القطاع الحكومي.",      IconClass = "bi-bank",         DisplayOrder = 3 },
            new ServiceItem { TitleEn = "Enterprise Solutions",  TitleAr = "الحلول المؤسسية",    DescriptionEn = "ERP-grade internal systems and operational tools that run the core of your business.",   DescriptionAr = "أنظمة داخلية بجودة ERP وأدوات تشغيلية تدير جوهر أعمالك.",                  IconClass = "bi-building-gear",DisplayOrder = 4 },
            new ServiceItem { TitleEn = "API Integration",       TitleAr = "تكامل واجهات API",   DescriptionEn = "Clean REST APIs, third-party integrations, and system-to-system automation.",          DescriptionAr = "REST APIs نظيفة، تكاملات خارجية، بوابات دفع وأتمتة بين الأنظمة.",            IconClass = "bi-plug",         DisplayOrder = 5 },
            new ServiceItem { TitleEn = "Digital Transformation",TitleAr = "التحول الرقمي",     DescriptionEn = "Modernize legacy systems, digitize workflows, and move to scalable cloud platforms.",   DescriptionAr = "تحديث الأنظمة القديمة، رقمنة العمليات، والانتقال إلى السحابة.",            IconClass = "bi-arrow-repeat", DisplayOrder = 6 }
        );
    }

    private static void SeedPortfolio(ApplicationDbContext db)
    {
        if (db.PortfolioItems.Any()) return;

        db.PortfolioItems.AddRange(
            new PortfolioItem
            {
                TitleEn = "Government Services Portal",
                TitleAr = "بوابة الخدمات الحكومية",
                DescriptionEn = "Citizen-facing portal with secure authentication, e-payment, and document workflows.",
                DescriptionAr = "بوابة خدمات للمواطنين مع مصادقة آمنة ودفع إلكتروني وسير عمل المستندات.",
                Technologies  = ".NET 8, SQL Server, Azure, OAuth2",
                IsFeatured    = true,
                DisplayOrder  = 1
            },
            new PortfolioItem
            {
                TitleEn = "Enterprise HR System",
                TitleAr = "نظام الموارد البشرية المؤسسي",
                DescriptionEn = "End-to-end HR platform with payroll, leave management, and analytics dashboards.",
                DescriptionAr = "منصة موارد بشرية شاملة مع الرواتب وإدارة الإجازات ولوحات تحليلية.",
                Technologies  = ".NET, EF Core, React, PostgreSQL",
                DisplayOrder  = 2
            },
            new PortfolioItem
            {
                TitleEn = "Field Operations Mobile App",
                TitleAr = "تطبيق العمليات الميدانية",
                DescriptionEn = "Offline-first Flutter app for field technicians with sync, GPS tagging, and reports.",
                DescriptionAr = "تطبيق Flutter يعمل دون اتصال للفنيين الميدانيين مع مزامنة وتتبع GPS وتقارير.",
                Technologies  = "Flutter, Firebase, REST APIs",
                DisplayOrder  = 3
            }
        );
    }

    private static void SeedTestimonials(ApplicationDbContext db)
    {
        if (db.Testimonials.Any()) return;

        db.Testimonials.AddRange(
            new TestimonialItem
            {
                NameEn = "Mohammed Al-Sabah", NameAr = "محمد الصباح",
                RoleEn = "IT Director",       RoleAr = "مدير تقنية المعلومات",
                CompanyEn = "Public Sector",  CompanyAr = "القطاع العام",
                QuoteEn = "msa3edcom delivered a critical platform on time and on spec. Their engineering discipline shows in every release.",
                QuoteAr = "نفّذت msa3edcom منصة حيوية في الوقت المحدد وبالمواصفات المطلوبة. انضباطهم الهندسي يظهر في كل إصدار.",
                Rating = 5, DisplayOrder = 1
            },
            new TestimonialItem
            {
                NameEn = "Sarah Al-Mutairi", NameAr = "سارة المطيري",
                RoleEn = "Product Manager",  RoleAr = "مديرة منتج",
                CompanyEn = "FinTech",       CompanyAr = "تقنية مالية",
                QuoteEn = "Smart team, clean code, real production sense. They understand the Gulf market and build for it.",
                QuoteAr = "فريق ذكي، كود نظيف، وحسّ إنتاجي حقيقي. يفهمون سوق الخليج ويبنون لأجله.",
                Rating = 5, DisplayOrder = 2
            },
            new TestimonialItem
            {
                NameEn = "Khaled Al-Otaibi", NameAr = "خالد العتيبي",
                RoleEn = "CTO",              RoleAr = "المدير التقني",
                CompanyEn = "Logistics",     CompanyAr = "خدمات لوجستية",
                QuoteEn = "From discovery to launch they kept us informed and shipped weekly. Top-tier partner.",
                QuoteAr = "من الاكتشاف وحتى الإطلاق أبقونا على اطّلاع وكانوا يطلقون أسبوعيًا. شريك من الطراز الأول.",
                Rating = 5, DisplayOrder = 3
            }
        );
    }

    private static void SeedFaqs(ApplicationDbContext db)
    {
        if (db.FaqItems.Any()) return;

        db.FaqItems.AddRange(
            new FaqItem
            {
                QuestionEn = "How long does a typical project take?",
                QuestionAr = "كم يستغرق المشروع النموذجي؟",
                AnswerEn = "It depends on scope, but most engagements run between 6 and 16 weeks from discovery to launch. We share weekly demos throughout.",
                AnswerAr = "يعتمد على النطاق، لكن معظم المشاريع تستغرق بين 6 و16 أسبوعًا من الاكتشاف حتى الإطلاق، مع عرض أسبوعي للتقدم.",
                DisplayOrder = 1
            },
            new FaqItem
            {
                QuestionEn = "Do you sign NDAs and security agreements?",
                QuestionAr = "هل توقّعون اتفاقيات السرية والأمان؟",
                AnswerEn = "Yes. NDAs are standard before discovery, and we follow security guidelines compatible with regulated industries.",
                AnswerAr = "نعم. اتفاقيات السرية معيارية قبل البدء، ونلتزم بمعايير أمان تتوافق مع القطاعات المنظّمة.",
                DisplayOrder = 2
            },
            new FaqItem
            {
                QuestionEn = "What technologies do you specialize in?",
                QuestionAr = "ما التقنيات التي تتخصصون فيها؟",
                AnswerEn = ".NET 8, SQL Server, Flutter, React, Azure, and a deep stack for government-grade and enterprise workloads.",
                AnswerAr = ".NET 8 وSQL Server وFlutter وReact وAzure، بالإضافة إلى تقنيات للعمل الحكومي والمؤسسي عالي الحساسية.",
                DisplayOrder = 3
            },
            new FaqItem
            {
                QuestionEn = "Do you provide post-launch support?",
                QuestionAr = "هل تقدمون دعمًا ما بعد الإطلاق؟",
                AnswerEn = "Yes. We offer SLA-backed support packages: bug fixes, monitoring, feature iterations, and security patches.",
                AnswerAr = "نعم. نقدّم باقات دعم مرتبطة بمعدلات SLA: إصلاح الأخطاء والمراقبة وتطوير المزايا وتحديثات الأمان.",
                DisplayOrder = 4
            }
        );
    }

    private static void SeedTechStack(ApplicationDbContext db)
    {
        if (db.TechStackItems.Any()) return;

        db.TechStackItems.AddRange(
            new TechStackItem { Name = ".NET 8",     IconClass = "bi-microsoft", DisplayOrder = 1 },
            new TechStackItem { Name = "SQL Server", IconClass = "bi-database",  DisplayOrder = 2 },
            new TechStackItem { Name = "Flutter",    IconClass = "bi-phone",     DisplayOrder = 3 },
            new TechStackItem { Name = "React",      IconClass = "bi-code-slash",DisplayOrder = 4 },
            new TechStackItem { Name = "Azure",      IconClass = "bi-cloud",     DisplayOrder = 5 },
            new TechStackItem { Name = "Docker",     IconClass = "bi-box-seam",  DisplayOrder = 6 },
            new TechStackItem { Name = "PostgreSQL", IconClass = "bi-server",    DisplayOrder = 7 },
            new TechStackItem { Name = "Redis",      IconClass = "bi-lightning", DisplayOrder = 8 }
        );
    }

    private static void SeedStats(ApplicationDbContext db)
    {
        if (db.StatItems.Any()) return;

        db.StatItems.AddRange(
            new StatItem { LabelEn = "Projects Delivered", LabelAr = "مشاريع منفّذة",       Value = "120", Suffix = "+",  IconClass = "bi-rocket-takeoff", DisplayOrder = 1 },
            new StatItem { LabelEn = "Enterprise Clients", LabelAr = "عملاء مؤسسيون",      Value = "40",  Suffix = "+",  IconClass = "bi-building",       DisplayOrder = 2 },
            new StatItem { LabelEn = "Years of Experience",LabelAr = "سنوات الخبرة",        Value = "10",  Suffix = "+",  IconClass = "bi-award",          DisplayOrder = 3 },
            new StatItem { LabelEn = "Engineers on Staff", LabelAr = "مهندسون في الفريق",   Value = "25",  Suffix = "+",  IconClass = "bi-people",         DisplayOrder = 4 }
        );
    }

    private static void SeedClients(ApplicationDbContext db)
    {
        var defaults = new[]
        {
            new ClientItem { Name = "Beauty Center",    LogoUrl = "/img/Beauty-center.png",     DisplayOrder = 1 },
            new ClientItem { Name = "Gam3ya",           LogoUrl = "/img/Gam3ya.png",            DisplayOrder = 2 },
            new ClientItem { Name = "Lulu Hypermarket", LogoUrl = "/img/luluHypermarket.png",   DisplayOrder = 3 }
        };

        var existingNames = db.ClientItems
            .Select(c => c.Name.ToLower())
            .ToHashSet();

        var maxOrder = db.ClientItems.Any()
            ? db.ClientItems.Max(c => c.DisplayOrder)
            : 0;

        foreach (var seed in defaults)
        {
            if (existingNames.Contains(seed.Name.ToLower())) continue;

            maxOrder++;
            db.ClientItems.Add(new ClientItem
            {
                Name         = seed.Name,
                LogoUrl      = seed.LogoUrl,
                DisplayOrder = maxOrder,
                CreatedAt    = DateTime.UtcNow
            });
        }
    }

    private static void SeedProcess(ApplicationDbContext db)
    {
        if (db.ProcessSteps.Any()) return;

        db.ProcessSteps.AddRange(
            new ProcessStep { TitleEn = "Discovery",  TitleAr = "الاكتشاف",  DescriptionEn = "We listen, map the problem, and define a clear scope.",     DescriptionAr = "نستمع ونوثّق المشكلة ونحدّد نطاقًا واضحًا.",       IconClass = "bi-search",       DisplayOrder = 1 },
            new ProcessStep { TitleEn = "Design",     TitleAr = "التصميم",   DescriptionEn = "Architecture, UI, and a prototype your users can validate.", DescriptionAr = "هندسة وبنية وواجهات ونموذج أوّلي يمكن للمستخدمين تجربته.", IconClass = "bi-vector-pen",   DisplayOrder = 2 },
            new ProcessStep { TitleEn = "Build",      TitleAr = "التنفيذ",   DescriptionEn = "Weekly demos, clean code, real production engineering.",     DescriptionAr = "عروض أسبوعية وكود نظيف وهندسة إنتاج حقيقية.",      IconClass = "bi-tools",        DisplayOrder = 3 },
            new ProcessStep { TitleEn = "Launch",     TitleAr = "الإطلاق",   DescriptionEn = "We deploy, monitor, and support — with you, not behind a ticket queue.", DescriptionAr = "نطلق ونراقب وندعم — معك مباشرة، لا خلف نظام تذاكر.", IconClass = "bi-rocket",       DisplayOrder = 4 }
        );
    }
}
