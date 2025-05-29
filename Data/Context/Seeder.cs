using Data.Entities;
using Data.Entities.Enums;
using Tag = Data.Entities.Tag;

namespace Data.Context;

public class Seeder
{
    private readonly ELiteratureDbContext _dbContext;

    public Seeder(ELiteratureDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedDataAsync()
    {
        var romanticism = new LiteratureDirection
        {
            Title = "Романтизм",
            Description = "Романтизм – літературний напрям, що виник наприкінці XVIII століття і характеризується акцентом на емоції, індивідуалізм, природу та уяву. В українській літературі романтизм часто пов’язаний із фольклором та національною ідентичністю.",
            StartCentury = 18,
            EndCentury = 19
        };

        var futurism = new LiteratureDirection
        {
            Title = "Футуризм",
            Description = "Футуризм – авангардний напрям, що зародився на початку XX століття. Він відкидає традиції, прославляє сучасність, технології, динаміку та експерименти в мистецтві. В Україні футуризм асоціюється з творчістю Михайля Семенка та його кверофутуризму.",
            StartCentury = 20,
            EndCentury = 20
        };

        _dbContext.LiteratureDirections.AddRange(romanticism, futurism);

        var mykhailoSemenko = new Author
        {
            FirstName = "Михайло",
            LastName = "Васильович",
            MiddleName = "Семенко",
            Biography = "Семенко народився у селі Кибинці Миргородського району на Полтавщині. Його батько Василь Семенко був волосним писарем. Мати – письменниця Марія Проскурівна. Навчався Михайль у Хорольському реальному училищі, а в 1912 році вступив до Психоневрологічного інституту в Петербурзі.\nУ 1913-му Семенко звернувся до футуризму – авангардного напряму в літературі та мистецтві, що зародився в Італії. Поет зачинає окремий напрям \u00a0– так званий кверофутуризм або футуризму пошуку. Сутність мистецтва Семенко вбачав у динаміці та шуканні, а не в самому творі.\nДо когорти кверофутуристів, окрім Михайля Семенка, входили його брат Базиль Семенко та художник Павло Ковжун. Ця група стала відомою у 1914 році, коли її учасники виступили із закликом спалити Кобзар. Своєю заявою Семенко не мав на увазі буквальне спалення книги, а відмову від спадщини, яка, на його думку, затримувала розвиток мистецтва. Кверофутуристів за це розкритикувала значна частина української інтелігенції.\u2028З початком Першої світової війни Семенка мобілізували у військо. До 1917 року він служив у кріпосно-телеграфній роті у Владивостоці. Там познайомився зі своєю майбутньою дружиною Лідією Горенко. Написав сповнені любовної й ностальгійної лірики збірки «П’єро кохає» та «П’єро здається».\nПісля Лютневої революції 1917-го повернувся в Київ. Намагався створити нове футуристичне об’єднання, адже «Кверо» розпалося: Базиль Семенко загинув на фронті, Павло Ковжун переїхав у Львів.\u2028Певний час Михайль Семенко був близький із групою «Флямінго», згодом створив «Ударну групу поетів-футуристів», яка у 1922-му переросла в Асоціацію панфутуристів – Аспанфут. Сутність панфутуризму полягала в тому, що всі мистецтва є синтетичними й перетікають з одного в інше. Відповідно футуризм має об’єднувати різних митців: літераторів, художників, театралів, кінематографістів тощо.\nЗ особливим пієтетом Михайль ставився до кінематографа. У 1925–1927 роках працював редактором на Одеській кіностудії. Запропонував письменникам Гео Шкурупію, Юрію Яновському та Миколі Бажану стати сценаристами й зробити свій внесок в українське кіно. Під впливом Семенка та роботи в кіностудії Бажан почав займатися кінокритикою, став редактором журналу «Кіно».\nВ Одесі Семенко познайомився з акторкою драмтеатру Наталією Ужвій, переконав зніматися в кіно. Вона стала його другою дружиною.",
            Occupations = new List<Occupation>
            {
                new Occupation { Title = "Поет" },
                new Occupation { Title = "Літературний критик" },
                new Occupation { Title = "Редактор" }
            },
            DateOfBirth = new DateOnly(1892, 12, 31),
            DateOfDeath = new DateOnly(1937, 10, 24),
            Photos = new List<AuthorPhoto>
            {
                new AuthorPhoto()
                {
                    Type = PhotoType.MainPhoto,
                    PhotoUrl = "https://res.cloudinary.com/dldm4ojsk/image/upload/v1747287219/fvh7da0xyr8pkzyc9zmm.jpg",
                },
                new AuthorPhoto
                {
                    Type = PhotoType.SliderPhoto,
                    PhotoUrl = "https://res.cloudinary.com/dldm4ojsk/image/upload/v1747287908/mf1je5jcbeydgyljmcvg.jpg",
                    Quote = "Я хтів би знать — що є\u00a0життя?\u2028Хто засвітив на небі зорі?\u2028В натхненному бурхливоморі\u2028Сумує людське почуття..."
                },
                new AuthorPhoto
                {
                    Type = PhotoType.SliderPhoto,
                    PhotoUrl = "https://res.cloudinary.com/dldm4ojsk/image/upload/v1747287908/tnatwpybnepifympgv6e.jpg",
                    Quote = "Я не умру від смерти —\nя умру від життя.\nУмиратиму — життя буде мерти,\nне маятиме стяг."
                },
                new AuthorPhoto
                {
                    Type = PhotoType.SliderPhoto,
                    PhotoUrl = "https://res.cloudinary.com/dldm4ojsk/image/upload/v1747287908/ao5cbrmdebl1ecvyn8p0.jpg",
                    Quote = "\t\nЯ не хочу з тобою говорити. Ти підносиш мені засмальцьованого «Кобзаря» й кажеш: ось моє мистецтво. Чоловіче, мені за тебе соромно... Ти підносиш мені заялозені мистецькі «ідеї», й мене канудить"
                }
            },
            LiteratureDirection = new List<LiteratureDirection> { romanticism, futurism }
        };

        var vasylSemenko = new Author
        {
            FirstName = "Василь",
            LastName = "Семенко",
            MiddleName = "Васильович",
            Biography = "Василь Семенко – молодший брат Михайля Семенка, також поет і учасник кверофутуристичного руху. Народився у селі Кибинці, навчався у Хорольському реальному училищі. Брав активну участь у діяльності кверофутуристів, але його кар’єра обірвалася через загибель на фронті під час Першої світової війни.",
            Occupations = new List<Occupation>
            {
                new Occupation { Title = "Поет" }
            },
            DateOfBirth = new DateOnly(1895, 5, 10),
            DateOfDeath = new DateOnly(1917, 3, 15),
            Photos = new List<AuthorPhoto>()
            {
                new AuthorPhoto()
                {
                    PhotoUrl = "https://res.cloudinary.com/dldm4ojsk/image/upload/v1747337631/authors/p1jchu2ximpkvucennmq.jpg",
                    Type = PhotoType.MainPhoto
                }
            },
            LiteratureDirection = new List<LiteratureDirection> { futurism }
        };

        var pavloKovzhun = new Author
        {
            FirstName = "Павло",
            LastName = "Ковжун",
            MiddleName = "Іванович",
            Biography = "Павло Ковжун – український художник, графік і мистецтвознавець, учасник кверофутуристичного руху. Народився у Києві, навчався у Київській художній школі. Після розпаду «Кверо» переїхав до Львова, де продовжив свою діяльність як художник і критик.",
            Occupations = new List<Occupation>
            {
                new Occupation { Title = "Художник" },
                new Occupation { Title = "Мистецтвознавець" }
            },
            DateOfBirth = new DateOnly(1896, 7, 22),
            DateOfDeath = new DateOnly(1939, 5, 15),
            Photos = new List<AuthorPhoto>()
            {
                new AuthorPhoto()
                {
                    PhotoUrl = "https://res.cloudinary.com/dldm4ojsk/image/upload/v1747337560/authors/xnjon9isgubdwv0j544r.jpg",
                    Type = PhotoType.MainPhoto,
                }
            },
            LiteratureDirection = new List<LiteratureDirection> { futurism },
        };

        var kveroFuturists = new Organization
        {
            Title = "Кверофутуристи",
            Description = "Кверофутуристи – авангардна група, заснована Михайлем Семенком у 1914 році. До її складу входили Михайль Семенко, його брат Василь Семенко та художник Павло Ковжун. Група виступала за радикальні зміни в мистецтві, зокрема закликала «спалити Кобзар» як символ відмови від застарілих традицій.",
            StartDate = new DateOnly(1914, 1, 1),
            EndDate = new DateOnly(1917, 12, 31),
            Authors = new List<Author> { mykhailoSemenko, vasylSemenko, pavloKovzhun }
        };

        var aspanfut = new Organization
        {
            Title = "Аспанфут",
            Description = "Асоціація панфутуристів (Аспанфут) – об’єднання, створене Михайлем Семенком у 1922 році. Група виступала за синтез усіх мистецтв (літератури, живопису, театру, кіно) у рамках футуристичного руху.",
            StartDate = new DateOnly(1922, 1, 1),
            EndDate = new DateOnly(1925, 12, 31),
            Authors = new List<Author> { mykhailoSemenko }
        };
        
        _dbContext.Authors.AddRange(mykhailoSemenko, vasylSemenko, pavloKovzhun);
        _dbContext.Organizations.AddRange(kveroFuturists, aspanfut);

        await SeedSymonenkoAsync();
        await _dbContext.SaveChangesAsync();
    }

    private async Task SeedSymonenkoAsync()
    {
        var literatureDirection = new LiteratureDirection
        {
            Title = "Реалізм",
            Description = "Художній метод у мистецтві, що ґрунтується на відтворенні дійсності"
        };

        _dbContext.LiteratureDirections.Add(literatureDirection);

        var occupations = new List<Occupation>
        {
            new Occupation { Title = "Поет" },
            new Occupation { Title = "Журналіст" }
        };

        _dbContext.Occupations.AddRange(occupations);

        var organizations = new List<Organization>
        {
            new Organization { Title = "Спілка письменників України", Description = "Добровільна творча організація професійних літераторів України: поетів, прозаїків, драматургів, критиків і перекладачів." }
        };

        _dbContext.Organizations.AddRange(organizations);

        var tags = new List<Tag>
        {
            new Tag { Title = "Поезія" },
            new Tag { Title = "Українська література" },
            new Tag { Title = "Патріотизм" },
            new Tag { Title = "Філософія" }
        };

        _dbContext.Tags.AddRange(tags);

        var publications = new List<Publication>
        {
            new Publication
            {
                Title = "Ну скажи, хіба не фантастично",
                Description = "Вірш про красу природи та фантастичність світу.",
                PublicationYear = new DateOnly(1962, 9, 24),
                Type = PublicationType.Poem,
                Text = "Ну скажи, хіба не фантастично,\nЩо у світі тім, де все продажне,\nЩе є душі, що живуть щасливо\nІ сміються дзвінко і ласкаво...",
                LiteratureDirection = new List<LiteratureDirection> { literatureDirection },
                Tags = new List<Tag> { tags[0], tags[1], tags[2] },
                Photos = new List<PublicationPhoto>()
                {
                    new PublicationPhoto()
                    {
                        PhotoUrl = "https://res.cloudinary.com/dldm4ojsk/image/upload/v1747336699/mzxipslcetrwkjpbgqix.jpg",
                        Type = PhotoType.MainPhoto,
                    }
                }
            },
            new Publication
            {
                Title = "Ти знаєш, що ти — людина",
                Description = "Філософський вірш про цінність людського життя.",
                PublicationYear = new DateOnly(1962, 1, 1),
                Type = PublicationType.Poem,
                Text = "Ти знаєш, що ти — людина?\nТи знаєш про це чи ні?\nУсмішка твоя — єдина,\nМука твоя — єдина,\nОчі твої — одні...",
                LiteratureDirection = new List<LiteratureDirection> { literatureDirection },
                Tags = new List<Tag> { tags[0], tags[1], tags[3] },
                Photos = new List<PublicationPhoto>()
                {
                    new PublicationPhoto()
                    {
                        PhotoUrl = "https://res.cloudinary.com/dldm4ojsk/image/upload/v1747336699/piyfo3bphpbi4eemx8vl.jpg",
                        Type = PhotoType.MainPhoto,
                    }
                }
            }
        };

        var writer = new Author
        {
            FirstName = "Василь",
            LastName = "Симоненко",
            MiddleName = "Андрійович",
            DateOfBirth = new DateOnly(1935, 1, 8),
            DateOfDeath = new DateOnly(1963, 12, 13),
            Biography = "Василь Симоненко — видатний український поет, журналіст, представник шістдесятників. Народився 8 січня 1935 року в селі Біївці на Полтавщині. Закінчив факультет журналістики Київського університету. Працював у газетах, писав поезію, що вирізнялася патріотизмом, щирістю та філософськими роздумами про людське життя. Його творчість стала символом боротьби за свободу і правду в умовах радянського режиму. Помер передчасно у 1963 році від важкої хвороби.",
            Publications = publications,
            LiteratureDirection = new List<LiteratureDirection> { literatureDirection },
            Occupations = occupations,
            Organizations = organizations,
            Photos = new List<AuthorPhoto>()
            {
                new AuthorPhoto() { Type = PhotoType.MainPhoto, PhotoUrl = "https://res.cloudinary.com/dldm4ojsk/image/upload/v1747335032/wkxbzxedtzfa8ngtzgl5.jpg" },
                new AuthorPhoto() { Type = PhotoType.SliderPhoto, PhotoUrl = "https://res.cloudinary.com/dldm4ojsk/image/upload/v1747336430/ux3ajdhqe0queuu1oy0v.jpg", Quote = "Україна — це не просто територія, це доля, це життя, це вічне джерело, з якого п’ють усі, хто любить свободу"},
                new AuthorPhoto() { Type = PhotoType.SliderPhoto, PhotoUrl = "https://res.cloudinary.com/dldm4ojsk/image/upload/v1747337040/aktxj1mfp0fv4txcg5r1.jpg", Quote = "Людина живе не для того, щоб їсти, пити і одягатися, а для того, щоб залишити по собі слід на землі — добрий, світлий, вічний"},
                new AuthorPhoto() { Type = PhotoType.SliderPhoto, PhotoUrl = "https://res.cloudinary.com/dldm4ojsk/image/upload/v1747337252/authors/merqm9wrstqmu8ujw2f9.jpg", Quote = "Моя Україна — це калина, що цвіте на камені, це пісня, що ллється крізь сльози, це сила, що не зламати ні в’язницями, ні часом"}
            },
        };

        publications[0].Authors = new List<Author> { writer };
        publications[1].Authors = new List<Author> { writer };

        _dbContext.Publications.AddRange(publications);
        _dbContext.Authors.Add(writer);
    }
}