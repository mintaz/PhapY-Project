using System.Collections.Generic;
using System.Linq;
using Abp.Localization;
using PhapY.EntityFramework;

namespace PhapY.Migrations.SeedData
{
    public class DefaultLanguagesCreator
    {
        public static List<ApplicationLanguage> InitialLanguages { get; private set; }

        private readonly PhapYDbContext _context;

        static DefaultLanguagesCreator()
        {
            InitialLanguages = new List<ApplicationLanguage>
            {
                //new ApplicationLanguage(null, "en", "English", "famfamfam-flag-gb"),
                new ApplicationLanguage(null, "vi-VN", "Việt Nam", "famfamfam-flag-vn")
            };
        }

        public DefaultLanguagesCreator(PhapYDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            foreach (var language in InitialLanguages)
            {
                AddLanguageIfNotExists(language);
            }
        }

        private void AddLanguageIfNotExists(ApplicationLanguage language)
        {
            if (_context.Languages.Any(l => l.TenantId == language.TenantId && l.Name == language.Name))
            {
                return;
            }

            _context.Languages.Add(language);

            _context.SaveChanges();
        }
    }
}
