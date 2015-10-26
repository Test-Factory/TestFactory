﻿

using FluentMigrator;

namespace TestFactory.NHibernateDataProvider.DbVersioning.Migrations.Version1
{
    [Migration(201510250916)]
    public class M002_DefaultData  : Migration
    {
        public override void Up()
        {
            Execute.Sql(
                @"
                INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'12dc6a23-8454-419f-ac75-2ea0560d27ef', N'Filler');
                INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'316987d9-9e4e-4cc4-b32a-b64112ca20be', N'Editor');

                INSERT [dbo].[User] ([Id], [Email], [Password], [PasswordSalt], [FirstName], [LastName], [Roles_id]) VALUES (N'887acb6d-2ba6-4323-8d7f-826bb78fd455', N'FillerFICT@gmail.com', N'biSdGh423C1ZerAF+QdSSQ==', N'52avvcwgm0qgsPpKvqyGbRwDoYMYfi1d63+4FAI89Ow=', N'FillerFICT', N'FF', N'12dc6a23-8454-419f-ac75-2ea0560d27ef');
                INSERT [dbo].[User] ([Id], [Email], [Password], [PasswordSalt], [FirstName], [LastName], [Roles_id]) VALUES (N'9514c6d4-53b0-4a33-ada5-485c8cfb7566', N'EditorFICT@gmail.com', N'bKDuKNnsPxpa/4tk7Jve+A==', N'4wZQZMVGjWhQzdSfaCK0wp3BVBvz/iFPtm+gl9M1Deo=', N'EditorFICT', N'EF', N'316987d9-9e4e-4cc4-b32a-b64112ca20be');
                INSERT [dbo].[User] ([Id], [Email], [Password], [PasswordSalt], [FirstName], [LastName], [Roles_id]) VALUES (N'a076f912-02ef-471f-bc69-0ff6d442a7a1', N'FillerFOFF@gmail.com', N'N0BuSAl0RDGL31kYk5F/4Q==', N'Q+Iox6lA2kfQcUTpwXQ/UdA/wIeA46+BwD9G8Dggbgw=', N'FillerFOFF', N'FOF', N'12dc6a23-8454-419f-ac75-2ea0560d27ef');
                INSERT [dbo].[User] ([Id], [Email], [Password], [PasswordSalt], [FirstName], [LastName], [Roles_id]) VALUES (N'e40f9826-e553-4c1d-82f9-fa2c6ca90f13', N'EditorFOFF@gmail.com', N'1vjtSQiPBxkdRjOU48sMeg==', N'p462cFSbA7M8yiwBd96skvda1mFYvauZwmhQnN9bBTk=', N'EditorFOFF', N'EOF', N'316987d9-9e4e-4cc4-b32a-b64112ca20be');
                
                
                INSERT [dbo].[Category] ([Id], [Name], [Code], [ACloseTypes], [Appreciate], [Details], [Features], [Likes], [OppositeType], [PreferredAreasOfActivity]) VALUES (N'028ac41d-ce0b-49e3-8ec7-3cd016e8f14b', N'Реалістичний', N'R', N'інтелектуальний і конвенціональний', N'Фізичну форму; Здоров''я; Силу; Природу; Пригоди; Ефективність; Традиції; Практичність; Здоровий Сенс; Чесність; Ризик;', N'Люди кажуть, що ви практичні, вправні і надійні. Чесно кажучи, ви віддаєте перевагу робити, а не говорити. Ваша пристрасть - фізична праця і спорт, особливо на відкритому повітрі. Коли потрібно щось полагодити, саме до вас іде всі друзі та знайомі. Ви можете полагодити практично будь-який механізм або прилад, і коли він нарешті запрацює, виявиться, що ви блискуче можете цим керувати. Ви не та людина, яка мріє кожен день одягати костюм і їхати на метро в свій офіс в хмарочосі. Ви для цього занадто незалежні. І хоча вам подобаються ризиковані спортивні ігри на зразок американського футболу, в глибині душі ви мрієте про стабільну, рутинній роботі', N'емоційна стабільність; моторика і спритність розвинена на високому рівні; просторова уява;', N'налагоджувати машини, механізми; праця на свіжому повітрі; працювати руками; фізична активність; робота з електронним обладнанням; керувати машинами, механізмами; працювати поодинці; робити або лагодити речі; догляд і дресирування тварин; пригоди і гострі відчуття', N'соціальний', N'професії з чіткими завданнями і результатами. Мають шанси домогтися успіху в спорті, фізики, хімії, економіці, інформатиці. Людей реалістичного типу часто називають ""виконавцями"". Вони напористі і люблять змагатися. Такі люди воліють працювати з реальними об''єктами, машинами та інструментами. Їм подобається діяльність, що вимагає координації, спритності і сили. Люди реалістичного типу володіють практичним складом розуму і орієнтуються на сьогодення. Вони воліють вирішувати проблеми, пробуючи щось зробити замість того, щоб довго думати або обговорювати. Їм подобається ""працювати руками"", і тому вони люблять розбиратися в техніці, електриці або сільському господарстві. Такі люди часто займаються спортом, намагаються багато часу проводити на природі і ведуть активний спосіб життя');
                INSERT [dbo].[Category] ([Id], [Name], [Code], [ACloseTypes], [Appreciate], [Details], [Features], [Likes], [OppositeType], [PreferredAreasOfActivity]) VALUES (N'15082a29-69ac-46f0-a81c-0db95ae639bc', N'Дослідницький', N'I', N'реалістичний і артистичний', N'Цікавість; Незалежність; Навчання; Точність; Знання; Складність; Дослідження; Вченість; Технології', N'Ви пишаєтеся тим, що ви - логічний, точний і раціональний. Інші визнають і часто хвалять ваші здібності вирішувати проблеми і працювати самостійно. Ви, безумовно, мотивуєте себе самі. Коли ви займаєтеся здобиччю знань або дослідженням світу навколо себе, ви намагаєтеся дійти до самого кінця і дізнатися істину.', N'аналітичний розум; раціональність; теоретичне мислення; творчий підхід', N'дослідити гіпотези; комп''ютери; самостійна робота; лабораторні експерименти; читання наукових і технічних журналів; аналіз даних; робота з абстракціями; проводити дослідження', N'підприємницький', N'часто пов''язані з точними та природничими науками: фізика, математика, астрономія, геологія, географія. Людей дослідницького типу часто називають «мислителями». Вони домагаються успіхів в математиці і точних науках. Люди дослідницького типу відрізняються яскраво вираженими аналітичними здібностями і теоретіческіммишленіем. Віддають перевагу вирішенню складних дослідницьких завдань, що вимагають задіяння абстрактного мислення. Інтроверти. У бесідах, як правило, є джерелом інформації. Цим людям подобається наука і дослідження, і вони часто продовжують свою освіту, щоб вирішувати загадки на передньому краї науки. Ви часто можете бачити, як вони спостерігають який-небудь механізм або процес, намагаючись зрозуміти, як все влаштовано. Мета їхнього життя - дізнаватися нове, перевіряти, досліджувати, аналізувати, ставити експерименти і вирішувати проблеми');
                INSERT [dbo].[Category] ([Id], [Name], [Code], [ACloseTypes], [Appreciate], [Details], [Features], [Likes], [OppositeType], [PreferredAreasOfActivity]) VALUES (N'165c96f1-5774-41d7-9a6d-754dddabe051', N'Артистичний', N'A', N'дослідницький і соціальний', N'оригінальність; самовираження; творчий потенціал; незалежність; інтуїцію; творче вираження ідей або емоцій; красу; уява; інновації; естетику', N'Рутинна робота за правилами вас пригнічує: рутина вбиває натхнення. Ваш найвищий пріоритет - творче самовираження. Інші люди часто описують вас як оригінального і незалежної людини. У вас добре виходить писати, малювати, займатися музикою, грати на сцені. Ваші друзі іноді говорять, що ви дуже напружені, інтелектуально або емоційно.', N'емоційна чутливість; розвинена інтуїція і уява; бажання виділятися з натавпу', N'відвідування музеїв; читання художньої літератури; ремесла (виготовлення предметів); заняття фотографією; робота з неоднозначними ідеями; концерти, театр; творче самовираження; робота в неструктурованих або вільних умовах', N'систематичний', N'пов''язані з музикою, театром, кіно, літературою, образотворчим мистецтвом. Людей артистичного типу часто називають «творцями». Вони воліють вільні, нерегламентірованниевіди діяльності, які дозволяють їм творити і насолоджуватися результатами своєї творчості. Люди цього типу успішні у вивченні мов, живопису, музиці, театрі та літератури. Вони незалежні і часто потребують простору для самовираження. Люди артистичного типу володіють незвичайним поглядом на життя, глибоким емоційним сприйняттям дійсності, гнучкістю мислення. Вони будують відносини з людьми, спираючись на інтуїцію, емоції і уяву. Більшість людей такого типу воліють роботу з гнучким графіком, не виносять жорсткої регламентації, не орієнтуються на соціальні норми і схвалення громадськості');
                INSERT [dbo].[Category] ([Id], [Name], [Code], [ACloseTypes], [Appreciate], [Details], [Features], [Likes], [OppositeType], [PreferredAreasOfActivity]) VALUES (N'aaa7c15c-47ad-4ddc-8c03-a8f18f7b481c', N'Соціальний', N'S', N'артистичний і підприємницький', N'Фізичну форму; Здоров''я; Силу; Природу; Пригоди; Ефективність; Традиції; Практичність; Здоровий Сенс; Чесність; Ризик;', N'Ваші друзі часто приходять до вас зі своїми проблемами, адже вони знають, який ви надійний товариш і чудовий слухач. Ви оптимістичні і доброзичливі, і це допомагає вам організовувати людей на заходи і отримувати задоволення від справ на благо суспільства. У вас природний дар розуміти людей, спілкуватися і будувати відносини. Вам також подобаються діти, і коли імнужно щось пояснити, у вас це добре виходить. Робота в команді доставляє вам більше задоволення, ніж індивідуальні завдання, і ви не можете дочекатися, щоб почати щось робити в групі людей, які один одному симпатичні і разом рухаються до успіху', N'потреби в численних соціальних контактах; комунікативні навички на високому рівні; емоційність і чутливість; гуманність; наявність здібностей співпереживати й співчувати', N'робота в групах; допомога людям з проблемами; рішення проблем через обговорення; зустрічі; волонтерська робота; робота з молоддю; командні види спорту; служіння іншим', N'реалістичний', N'психологія, педагогіка, медицина. Людей соціального типу часто називають «помічниками». Їм подобається допомагати іншим вирішувати проблеми і розвивати свій потенціал. Таким людям приносить задоволення працювати з людьми і виконувати соціальну роботу - надихати, інформувати, розвивати або лікувати. Люди соціального типу намагаються триматися осторонь від інтелектуальних проблем. Вони вирішують проблеми, керуючись почуттями та емоціями. Залежать від думки оточуючих');
                INSERT [dbo].[Category] ([Id], [Name], [Code], [ACloseTypes], [Appreciate], [Details], [Features], [Likes], [OppositeType], [PreferredAreasOfActivity]) VALUES (N'ab014eb1-881d-4d7e-acbe-ac3fa710260b', N'Підприємницький', N'E', N'систематичний і соціальний', N'Впевненість у собі; Владу; Cилу; Соціальний статус; Фінансову вигоду; Ентузіазм; Відповідальність; Лідерство; Ризик; Змагання; Репутацію; Політику', N'Одне з ваших кращих властивостей - те, що ваша енергія б''є через край. Ви товариські, честолюбні і не проти опинитися в центрі уваги, особливо, коли відчуваєте себе експертом в якійсь області. А завдяки своєму дару переконання, ви можете переконати в цьому та інших. Нові завдання вас не лякають. Ви любите пригоди, дієте спонтанно і готові ризикнути. Ви могли б бути одним з брокерів з Уолл-Стріт, яке б містило мільярдні оборудки. Або, можливо, ви почнете власну справу. Люди кажуть, що ви - природжений лідер, і ви завжди намагаєтеся бути кращим, номером один, переможцем усіх змагань', N'розвинені організаторські та лідерські здібності; винахідливість, імпульсивність, ентузіазм; соціальна активність; готовність ризикувати; практичність', N'починати проекти; планувати події; змагатися; прагнути до влади або високому становищу; продаж речей приймаючи рішення, які впливають на інших; брати участь у виборах; завойовувати нагороди; починати власний бізнес; проводити політичні кампанії; зустрічатися з важливими людьми; просувати ідеї', N'дослідницький', N'які задіюють організаторські здібності: вплив, переконання, натхнення або управління людьми для досягнення цілей організації або отримання прибутку. Людей підприємницького типу часто називають «переконуючими». Цим людям подобається бути лідерами, вони можуть переконувати і вести за собою інших. Люди підприємницького типу володіють розвиненими комунікативними здібностями, але не переносять скрупульозної роботи, що вимагає тривалої концентрації. Для таких людей важливо матеріальне благополуччя. Вони готові важко працювати, щоб отримати владу, статус і гроші. Часто це дуже цілеспрямовані люди, які прагнуть до більш високих посад у своїй організації');
                INSERT [dbo].[Category] ([Id], [Name], [Code], [ACloseTypes], [Appreciate], [Details], [Features], [Likes], [OppositeType], [PreferredAreasOfActivity]) VALUES (N'bd122217-a9c7-4804-af96-9f588c196acb', N'Систематичний', N'C', N'підприємницький і реалістичний', N'Передбачуваність; Репутацію; Точність; Статус; Стабільність; Ефективність; Надійність; Фінансовий успіх; Чіткі правила і ролі; Організованість; Відповідальність', N'Ви - правильна людина, коли потрібно розібратися в купі сирих даних або налагодити якийсь процес. Інші люди часто хвалять вашу сумлінність і надійність. Ви пишаєтеся тим, наскільки ви ефективні. Найпевніше ви відчуваєте себе в ситуаціях, де є ясні правила і зрозуміло, що від вас очікується. Ви всіма силами намагаєтеся уникнути невизначеностей і двозначностей. Ви звертаєте увагу на деталі і задаєте масу питань, намагаючись з''ясувати, як все працює. Вам подобаються ігри на кшталт «Монополії» або шашок, де є ясні правила, на яких можна побудувати стратегію виграшу. Оскільки ви акуратно звертаєтеся з великими обсягами даних, вас часто просять бути секретарем в різних групах і спільнотах', N'пунктуальність; акуратність; практичність; орієнтація на соціальні норми', N'слідувати чітко описаним процедурам; працювати з комп''ютерами; працювати з цифрами; збір і організація інформації; аналіз даних виконання структурованої або систематизованої діяльності; робота в організованих і передбачуваних врегулюваннях; працювати з оргтехнікою', N'артистичний', N'пов''язані з роботою з інформацією, її опрацюванням і систематизацією. Людей систематичного типу часто називають ""організаторами"". Їм подобаються чітко налагоджені процеси і добре сформульовані завдання. Часто такі люди працюють в офісі, де вони знаходять застосування своїм організаторським талантам і схильності до роботи з цифрами. Такі люди люблять працювати з текстами, цифрами, формулами, документами. Вони сумлінні, точно дотримуються інструкцій і орієнтовані на деталі. Їх здібності до роботи з цифрами допомагають їм ефективно працювати з великими обсягами даних. Люди цього типу схильні до чітко регламентованої роботі, що не вимагає прийняття відповідальних рішень. Віддають перевагу заздалегідь спланованою і структурованої рутинній роботі, що вимагає посидючості, до монотонної діяльності. Люди даного типу цінують матеріальне благополуччя вище, ніж інші');
            ");
        }

        public override void Down()
        {
            Execute.Sql(
                @"
                DELETE FROM [dbo].[Category];
                DELETE FROM [dbo].[User];
                DELETE FROM [dbo].[Role];  
            ");
        }
    }
}
