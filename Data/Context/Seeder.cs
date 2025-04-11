/*using Data.Entities;
using Data.Entities.Enums;

namespace Data.Context;

public class Seeder
{
    private readonly ELiteratureDbContext _dbContext;

    public Seeder(
        ELiteratureDbContext dbContext)
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
        var mykhailoSemenko = new Writer()
        {
            FirstName = "Михайло",
            LastName = "Васильович",
            MiddleName = "Семенко",
            Biography =
                "Семенко народився у селі Кибинці Миргородського району на Полтавщині. Його батько Василь Семенко був волосним писарем. Мати – письменниця Марія Проскурівна. Навчався Михайль у Хорольському реальному училищі, а в 1912 році вступив до Психоневрологічного інституту в Петербурзі.\nУ 1913-му Семенко звернувся до футуризму – авангардного напряму в літературі та мистецтві, що зародився в Італії. Поет зачинає окремий напрям \u00a0– так званий кверофутуризм або футуризму пошуку. Сутність мистецтва Семенко вбачав у динаміці та шуканні, а не в самому творі.\nДо когорти кверофутуристів, окрім Михайля Семенка, входили його брат Базиль Семенко та художник Павло Ковжун. Ця група стала відомою у 1914 році, коли її учасники виступили із закликом спалити Кобзар. Своєю заявою Семенко не мав на увазі буквальне спалення книги, а відмову від спадщини, яка, на його думку, затримувала розвиток мистецтва. Кверофутуристів за це розкритикувала значна частина української інтелігенції.\u2028З початком Першої світової війни Семенка мобілізували у військо. До 1917 року він служив у кріпосно-телеграфній роті у Владивостоці. Там познайомився зі своєю майбутньою дружиною Лідією Горенко. Написав сповнені любовної й ностальгійної лірики збірки «П’єро кохає» та «П’єро здається».\nПісля Лютневої революції 1917-го повернувся в Київ. Намагався створити нове футуристичне об’єднання, адже «Кверо» розпалося: Базиль Семенко загинув на фронті, Павло Ковжун переїхав у Львів.\u2028Певний час Михайль Семенко був близький із групою «Флямінго», згодом створив «Ударну групу поетів-футуристів», яка у 1922-му переросла в Асоціацію панфутуристів – Аспанфут. Суть панфутуризму полягала в тому, що всі мистецтва є синтетичними й перетікають з одного в інше. Відповідно футуризм має об’єднувати різних митців: літераторів, художників, театралів, кінематографістів тощо.\nЗ особливим пієтетом Михайль ставився до кінематографа. У 1925–1927 роках працював редактором на Одеській кіностудії. Запропонував письменникам Гео Шкурупію, Юрію Яновському та Миколі Бажану стати сценаристами й зробити свій внесок в українське кіно. Під впливом Семенка та роботи в кіностудії Бажан почав займатися кінокритикою, став редактором журналу «Кіно».\nВ Одесі Семенко познайомився з акторкою драмтеатру Наталією Ужвій, переконав зніматися в кіно. Вона стала його другою дружиною.",
            Occupations = new List<Occupation>()
            {
                new Occupation()
                {
                    Title = "Поет"
                },
                new Occupation()
                {
                    Title = "Літературний критик",
                },
                new Occupation()
                {
                    Title = "Редактор",
                }
            },
            DateOfBirth = new DateOnly(1892, 12, 31),
            DateOfDeath = new DateOnly(1937, 10, 24),
            Photos = new List<WriterPhoto>()
            {
                new WriterPhoto()
                {
                    PhotoUrl =
                        "https://www.google.com/url?sa=i&url=https%3A%2F%2Fuk.wikiquote.org%2Fwiki%2F%25D0%25A1%25D0%25B5%25D0%25BC%25D0%25B5%25D0%25BD%25D0%25BA%25D0%25BE_%25D0%259C%25D0%25B8%25D1%2585%25D0%25B0%25D0%25B9%25D0%25BB%25D1%258C_%25D0%2592%25D0%25B0%25D1%2581%25D0%25B8%25D0%25BB%25D1%258C%25D0%25BE%25D0%25B2%25D0%25B8%25D1%2587&psig=AOvVaw3Nda-0uVF4JS5yQwALtrDm&ust=1744027944041000&source=images&cd=vfe&opi=89978449&ved=0CBUQjRxqFwoTCNCM78Kww4wDFQAAAAAdAAAAABAE",
                    Type = PhotoType.MainPhoto,
                },
                new WriterPhoto()
                {
                    PhotoUrl =
                        "https://uk.wikipedia.org/wiki/%D0%9C%D0%B8%D1%85%D0%B0%D0%B9%D0%BB%D1%8C_%D0%A1%D0%B5%D0%BC%D0%B5%D0%BD%D0%BA%D0%BE#/media/%D0%A4%D0%B0%D0%B9%D0%BB:%D0%A1%D0%B5%D0%BC%D0%B5%D0%BD%D0%BA%D0%BE_%D0%9C3.jpg",
                    Type = PhotoType.SliderPhoto,
                    Quote =
                        "Я хтів би знать — що є\u00a0життя?\u2028Хто засвітив на небі зорі?\u2028В натхненному бурхливоморі\u2028Сумує людське почуття...",
                },
                new WriterPhoto()
                {
                    PhotoUrl =
                        "https://uk.wikipedia.org/wiki/%D0%9C%D0%B8%D1%85%D0%B0%D0%B9%D0%BB%D1%8C_%D0%A1%D0%B5%D0%BC%D0%B5%D0%BD%D0%BA%D0%BE#/media/%D0%A4%D0%B0%D0%B9%D0%BB:%D0%A1%D0%B5%D0%BC%D0%B5%D0%BD%D0%BA%D0%BE_%D0%9C5.jpg",
                    Type = PhotoType.SliderPhoto,
                    Quote =
                        "Я не умру від смерти —\nя умру від життя.\nУмиратиму — життя буде мерти,\nне маятиме стяг.",
                },
                new WriterPhoto()
                {
                    PhotoUrl =
                        "https://uk.wikipedia.org/wiki/%D0%9C%D0%B8%D1%85%D0%B0%D0%B9%D0%BB%D1%8C_%D0%A1%D0%B5%D0%BC%D0%B5%D0%BD%D0%BA%D0%BE#/media/%D0%A4%D0%B0%D0%B9%D0%BB:%D0%A1%D0%B5%D0%BC%D0%B5%D0%BD%D0%BA%D0%BE_%D0%9C.JPG",
                    Type = PhotoType.SliderPhoto,
                    Quote =
                        "\t\nЯ не хочу з тобою говорити. Ти підносиш мені засмальцьованого «Кобзаря» й кажеш: ось моє мистецтво. Чоловіче, мені за тебе соромно... Ти підносиш мені заялозені мистецькі «ідеї», й мене канудить",
                },
            },
            LiteratureDirection = new List<LiteratureDirection>() { romanticism, futurism }
        };

        await _dbContext.Writers.AddAsync(mykhailoSemenko);
        await _dbContext.SaveChangesAsync();
        
        var vasylSemenko = new Writer
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
            Photos = new List<WriterPhoto>
            {
                new WriterPhoto()
                {
                    PhotoUrl = "https://korydor.in.ua/wp-content/uploads/2019/06/1.Vasil-Semenko-na-tli-vlasnoi-kartini-Misto-1913-14-2-e1561114910579.jpg",
                    Type = PhotoType.MainPhoto
                },
            },
            LiteratureDirection = new List<LiteratureDirection> { futurism }
        };
        
        await _dbContext.Writers.AddAsync(vasylSemenko);
        await _dbContext.SaveChangesAsync();
        
        var pavloKovzhun = new Writer
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
            Photos = new List<WriterPhoto>
            {
                new WriterPhoto()
                {
                    PhotoUrl = "https://esu.com.ua/images/article_images/K/Kovghun%20Pavlo%20Maksimovich.jpg",
                    Type = PhotoType.MainPhoto
                },
                new WriterPhoto
                {
                    PhotoUrl = "https://vikna.if.ua/assets/gallery/2017/01/16/66368/kovgun15.jpg",
                    Type = PhotoType.SliderPhoto,
                    Quote = "Мистецтво має бути сміливим і новаторським."
                },
            },
            LiteratureDirection = new List<LiteratureDirection> { futurism }
        };
        
        await _dbContext.Writers.AddAsync(pavloKovzhun);
        await _dbContext.SaveChangesAsync();
        
        var kveroFuturists = new Organization
        {
            Title = "Кверофутуристи",
            Description = "Кверофутуристи – авангардна група, заснована Михайлем Семенком у 1914 році. До її складу входили Михайль Семенко, його брат Василь Семенко та художник Павло Ковжун. Група виступала за радикальні зміни в мистецтві, зокрема закликала «спалити Кобзар» як символ відмови від застарілих традицій.",
            StartDate = new DateOnly(1914, 1, 1),
            EndDate = new DateOnly(1917, 12, 31),
            Writers = new List<Writer?> { mykhailoSemenko, vasylSemenko, pavloKovzhun }
        };

        var aspanfut = new Organization
        {
            Title = "Аспанфут",
            Description = "Асоціація панфутуристів (Аспанфут) – об’єднання, створене Михайлем Семенком у 1922 році. Група виступала за синтез усіх мистецтв (літератури, живопису, театру, кіно) у рамках футуристичного руху.",
            StartDate = new DateOnly(1922, 1, 1),
            EndDate = new DateOnly(1925, 12, 31),
            Writers = new List<Writer?> { mykhailoSemenko }
        };

        _dbContext.Organizations.AddRange(kveroFuturists, aspanfut);
        await _dbContext.SaveChangesAsync();
        
        Seed_Symonenko();
    }

    private void Seed_Symonenko()
    {
        var literatureDirection = new LiteratureDirection
        {
            Title = "Реалізм",
            Description = "Художній метод у мистецтві, що ґрунтується на відтворенні дійсності",
        };
        
        _dbContext.LiteratureDirections.AddAsync(literatureDirection);

        var occupations = new List<Occupation>
        {
            new Occupation { Title = "Поет" },
            new Occupation { Title = "Журналіст" }
        };
        
        _dbContext.Occupations.AddRange(occupations);

        var organizations = new List<Organization>
        {
            new Organization { Title = "Спілка письменників України", Description = "Добровільна творча організація професійних літераторів України: поетів, прозаїків, драматургів, критиків і перекладачів."}
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
        _dbContext.SaveChangesAsync();
        
        var publications = new List<Publication>
        {
            new Publication
            {
                Title = "Ну скажи, хіба не фантастично",
                Description = "Вірш про красу природи та фантастичність світу.",
                PublicationYear = new DateOnly(1960, 1, 1),
                Type = PublicationType.Poem,
                Text = "Ну скажи, хіба не фантастично,\nЩо у світі тім, де все продажне,\nЩе є душі, що живуть щасливо\nІ сміються дзвінко і ласкаво...",
                LiteratureDirection = new List<LiteratureDirection> { literatureDirection },
                Tags = new List<Tag> { tags[0], tags[1], tags[2] },
                Photos = new List<PublicationPhoto>
                {
                    new PublicationPhoto { PhotoUrl = "https://i.pinimg.com/736x/fd/48/04/fd48047d564dd0a5770e529da0fdfbfe.jpg", Type = PhotoType.MainPhoto},
                    new PublicationPhoto { PhotoUrl = "https://i.pinimg.com/736x/fd/48/04/fd48047d564dd0a5770e529da0fdfbfe.jpg", Type = PhotoType.SidePhoto},
                },
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
            }
        };
        
        var writer = new Writer
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
            Photos = new List<WriterPhoto>
            {
                new WriterPhoto { PhotoUrl = "https://upload.wikimedia.org/wikipedia/uk/e/ee/%D0%A1%D0%B8%D0%BC%D0%BE%D0%BD%D0%B5%D0%BD%D0%BA%D0%BE.jpg", Type = PhotoType.MainPhoto},
            },
        };

        _dbContext.Writers.AddAsync(writer);
        _dbContext.SaveChangesAsync();

        publications[0].Writers = new List<Writer> { writer };
        publications[1].Writers = new List<Writer> { writer };
        
        _dbContext.Publications.AddRange(publications);
        _dbContext.SaveChangesAsync();
    }
}*/
using Data.Entities;
using Data.Entities.Enums;

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

        var mykhailoSemenko = new Writer
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
            Photos = new List<WriterPhoto>
            {
                new WriterPhoto
                {
                    PhotoUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fuk.wikiquote.org%2Fwiki%2F%25D0%25A1%25D0%25B5%25D0%25BC%25D0%25B5%25D0%25BD%25D0%25BA%25D0%25BE_%25D0%259C%25D0%25B8%25D1%2585%25D0%25B0%25D0%25B9%25D0%25BB%25D1%258C_%25D0%2592%25D0%25B0%25D1%2581%25D0%25B8%25D0%25BB%25D1%258C%25D0%25BE%25D0%25B2%25D0%25B8%25D1%2587&psig=AOvVaw3Nda-0uVF4JS5yQwALtrDm&ust=1744027944041000&source=images&cd=vfe&opi=89978449&ved=0CBUQjRxqFwoTCNCM78Kww4wDFQAAAAAdAAAAABAE",
                    Type = PhotoType.MainPhoto
                },
                new WriterPhoto
                {
                    PhotoUrl = "https://uk.wikipedia.org/wiki/%D0%9C%D0%B8%D1%85%D0%B0%D0%B9%D0%BB%D1%8C_%D0%A1%D0%B5%D0%BC%D0%B5%D0%BD%D0%BA%D0%BE#/media/%D0%A4%D0%B0%D0%B9%D0%BB:%D0%A1%D0%B5%D0%BC%D0%B5%D0%BD%D0%BA%D0%BE_%D0%9C3.jpg",
                    Type = PhotoType.SliderPhoto,
                    Quote = "Я хтів би знать — що є\u00a0життя?\u2028Хто засвітив на небі зорі?\u2028В натхненному бурхливоморі\u2028Сумує людське почуття..."
                },
                new WriterPhoto
                {
                    PhotoUrl = "https://uk.wikipedia.org/wiki/%D0%9C%D0%B8%D1%85%D0%B0%D0%B9%D0%BB%D1%8C_%D0%A1%D0%B5%D0%BC%D0%B5%D0%BD%D0%BA%D0%BE#/media/%D0%A4%D0%B0%D0%B9%D0%BB:%D0%A1%D0%B5%D0%BC%D0%B5%D0%BD%D0%BA%D0%BE_%D0%9C5.jpg",
                    Type = PhotoType.SliderPhoto,
                    Quote = "Я не умру від смерти —\nя умру від життя.\nУмиратиму — життя буде мерти,\nне маятиме стяг."
                },
                new WriterPhoto
                {
                    PhotoUrl = "https://uk.wikipedia.org/wiki/%D0%9C%D0%B8%D1%85%D0%B0%D0%B9%D0%BB%D1%8C_%D0%A1%D0%B5%D0%BC%D0%B5%D0%BD%D0%BA%D0%BE#/media/%D0%A4%D0%B0%D0%B9%D0%BB:%D0%A1%D0%B5%D0%BC%D0%B5%D0%BD%D0%BA%D0%BE_%D0%9C.JPG",
                    Type = PhotoType.SliderPhoto,
                    Quote = "\t\nЯ не хочу з тобою говорити. Ти підносиш мені засмальцьованого «Кобзаря» й кажеш: ось моє мистецтво. Чоловіче, мені за тебе соромно... Ти підносиш мені заялозені мистецькі «ідеї», й мене канудить"
                }
            },
            LiteratureDirection = new List<LiteratureDirection> { romanticism, futurism }
        };

        var vasylSemenko = new Writer
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
            Photos = new List<WriterPhoto>
            {
                new WriterPhoto
                {
                    PhotoUrl = "https://korydor.in.ua/wp-content/uploads/2019/06/1.Vasil-Semenko-na-tli-vlasnoi-kartini-Misto-1913-14-2-e1561114910579.jpg",
                    Type = PhotoType.MainPhoto
                }
            },
            LiteratureDirection = new List<LiteratureDirection> { futurism }
        };

        var pavloKovzhun = new Writer
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
            Photos = new List<WriterPhoto>
            {
                new WriterPhoto
                {
                    PhotoUrl = "https://esu.com.ua/images/article_images/K/Kovghun%20Pavlo%20Maksimovich.jpg",
                    Type = PhotoType.MainPhoto
                },
                new WriterPhoto
                {
                    PhotoUrl = "https://vikna.if.ua/assets/gallery/2017/01/16/66368/kovgun15.jpg",
                    Type = PhotoType.SliderPhoto,
                    Quote = "Мистецтво має бути сміливим і новаторським."
                }
            },
            LiteratureDirection = new List<LiteratureDirection> { futurism }
        };

        var kveroFuturists = new Organization
        {
            Title = "Кверофутуристи",
            Description = "Кверофутуристи – авангардна група, заснована Михайлем Семенком у 1914 році. До її складу входили Михайль Семенко, його брат Василь Семенко та художник Павло Ковжун. Група виступала за радикальні зміни в мистецтві, зокрема закликала «спалити Кобзар» як символ відмови від застарілих традицій.",
            StartDate = new DateOnly(1914, 1, 1),
            EndDate = new DateOnly(1917, 12, 31),
            Writers = new List<Writer> { mykhailoSemenko, vasylSemenko, pavloKovzhun }
        };

        var aspanfut = new Organization
        {
            Title = "Аспанфут",
            Description = "Асоціація панфутуристів (Аспанфут) – об’єднання, створене Михайлем Семенком у 1922 році. Група виступала за синтез усіх мистецтв (літератури, живопису, театру, кіно) у рамках футуристичного руху.",
            StartDate = new DateOnly(1922, 1, 1),
            EndDate = new DateOnly(1925, 12, 31),
            Writers = new List<Writer> { mykhailoSemenko }
        };

        _dbContext.Writers.AddRange(mykhailoSemenko, vasylSemenko, pavloKovzhun);
        _dbContext.Organizations.AddRange(kveroFuturists, aspanfut);

        await SeedSymonenkoAsync();

        // Один виклик SaveChangesAsync у кінці
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
                PublicationYear = new DateOnly(1960, 1, 1),
                Type = PublicationType.Poem,
                Text = "Ну скажи, хіба не фантастично,\nЩо у світі тім, де все продажне,\nЩе є душі, що живуть щасливо\nІ сміються дзвінко і ласкаво...",
                LiteratureDirection = new List<LiteratureDirection> { literatureDirection },
                Tags = new List<Tag> { tags[0], tags[1], tags[2] },
                Photos = new List<PublicationPhoto>
                {
                    new PublicationPhoto { PhotoUrl = "https://i.pinimg.com/736x/fd/48/04/fd48047d564dd0a5770e529da0fdfbfe.jpg", Type = PhotoType.MainPhoto },
                    new PublicationPhoto { PhotoUrl = "https://i.pinimg.com/736x/fd/48/04/fd48047d564dd0a5770e529da0fdfbfe.jpg", Type = PhotoType.SidePhoto }
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
                Tags = new List<Tag> { tags[0], tags[1], tags[3] }
            }
        };

        var writer = new Writer
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
            Photos = new List<WriterPhoto>
            {
                new WriterPhoto { PhotoUrl = "https://upload.wikimedia.org/wikipedia/uk/e/ee/%D0%A1%D0%B8%D0%BC%D0%BE%D0%BD%D0%B5%D0%BD%D0%BA%D0%BE.jpg", Type = PhotoType.MainPhoto }
            }
        };

        publications[0].Writers = new List<Writer> { writer };
        publications[1].Writers = new List<Writer> { writer };

        _dbContext.Writers.Add(writer);
        _dbContext.Publications.AddRange(publications);
    }
}