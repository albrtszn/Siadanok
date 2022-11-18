using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Entity;
using DataBase.Enum;

namespace DataBase
{
    public class InitdData
    {
        private static byte[] ImageToByteArray(string path)
        {
            Image pic = Image.FromFile(path);
            using (var ms = new MemoryStream())
            {
                pic.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static void Init(EFDBContext context)
        {
            //context.cartItemsRepo.EnsureDeleted(,);
            //context.Database.EnsureCreated();
            if(!context.userRepo.Any()){
                context.userRepo.Add(new User() { Id=Guid.NewGuid().ToString(),Number= "+375436386666", Password=Base64Encode("Password1234"), FirstName ="Dexter", SecondName="Horn"});
                context.SaveChanges();
            }
            if (!context.itemRepo.Any()){
                // Exotic
                context.itemRepo.Add(new Item() { Name = "Пьяная креветка", Picture=ImageToByteArray(@"D:\\Visual_Stidio_projects\\Siadanok\\Pictures\\PyanyeKrevetki.jpg"), Type=ItemType.Meal.ToString(), IsExotic=true.ToString(), Price = 120.00m, Descryption = "Это не шутка и не название бара, а вполне реальное китайское блюдо, весьма популярное в некоторых регионах страны. Креветку поедают живьем, но сначала она хорошенько отмокает в крепком ликере. В США креветок тоже маринуют в алкоголе, но там не пренебрегают термической обработкой этого морепродукта, чего не скажешь об азиатах с их любовью к сырой еде." });
                context.itemRepo.Add(new Item() { Name = "Устрицы Скалистых гор", Picture = ImageToByteArray(@"D:\\Visual_Stidio_projects\\Siadanok\\Pictures\\ustrici.jpg"), Type = ItemType.Meal.ToString(), IsExotic = true.ToString(), Price = 80.00m, Descryption = "Это блюдо было придумано в Северной Америке, и готовится оно далеко не из устриц. На самом деле это очищенные от кожи, порубленные на кусочки и зажаренные яички быков. Соль, перец, кетчуп?" });
                context.itemRepo.Add(new Item() { Name = "Сушеные ящерицы", Picture = ImageToByteArray(@"D:\\Visual_Stidio_projects\\Siadanok\\Pictures\\DryLizards.jpg"), Type = ItemType.Meal.ToString(), IsExotic = true.ToString(), Price = 42.00m, Descryption = "В некоторых азиатских культурах этих ящериц пускают на суп. Иногда их заспиртовывают, чтобы потом использовать в медицинских целях. Процесс доведения ящерицы до нужной кондиции в этом случае занимает целые годы…" });
                context.itemRepo.Add(new Item() { Name = "Икизикури ", Picture = ImageToByteArray(@"D:\\Visual_Stidio_projects\\Siadanok\\Pictures\\ikizukuri.jpg"), Type = ItemType.Meal.ToString(), IsExotic = true.ToString(), Price = 29.00m, Descryption = "Если говорить о брутальности, этому блюду из морепродуктов его не занимать. Его даже официально запретили в нескольких странах мира, включая Австралию и Германию. Сначала клиент выбирает животное, которое он хочет попробовать на вкус. Затем повар живьем разрезает несчастное создание прямо на глазах заказчика. На тарелку рыба (кальмар, осьминог или креветка) кладется вскрытой, порезанной на кусочки и с еще бьющимся сердцем. Иногда такую рыбу возвращают в аквариум, где она плавает еще буквально несколько секунд, пока не испустит последний дух, и тогда ее возвращают на тарелку клиента." });
                context.itemRepo.Add(new Item() { Name = "Пенис яка", Picture = ImageToByteArray(@"D:\\Visual_Stidio_projects\\Siadanok\\Pictures\\Penis.jpg"), Type = ItemType.Meal.ToString(), IsExotic = true.ToString(), Price = 12.50m, Descryption = "Более романтичное название этого деликатеса звучит примерно как «дракон во пламени желания», и оно считается фирменным блюдом в сети ресторанов Guolizhuang из Пекина. Для иностранцев поедание гениталий животных может показаться странным, а вот в Китае люди верят, что это очень полезная еда, почти как шпинат." });
                context.itemRepo.Add(new Item() { Name = "Сердце атлантического тупика", Picture = ImageToByteArray(@"D:\\Visual_Stidio_projects\\Siadanok\\Pictures\\Tupic.jpg"), Type = ItemType.Meal.ToString(), IsExotic = true.ToString(), Price = 92.00m, Descryption = "Тупик – это морская птица из семейства чистиковых, обитающая в северном полушарии, преимущественно в европейской его части. Сердце этой птицы в Исландии считается деликатесом. Впрочем, блюдо это сейчас уже так просто не закажешь, потому что атлантические тупики были занесены в Красную книгу и нуждаются в защите как уязвимый вид." });
                context.itemRepo.Add(new Item() { Name = "Глаз тунца", Picture = ImageToByteArray(@"D:\\Visual_Stidio_projects\\Siadanok\\Pictures\\EyeTunec.jpg"), Type = ItemType.Meal.ToString(), IsExotic = true.ToString(), Price = 33.30m, Descryption = "Между японцами и тунцом сущетсвует какая-то особая связь - японцы поедают основную часть мирового вылова этой рыбы за год. В супермаркетах частенько можно увидеть целый отдел, посвященный тунцам, в котором можно приобрести любые части рыбы. Самое экзотическое предложение - глаза тунца. Их поджаривают и подают с рисом и соевым соусом. " });
                // Poor
                context.itemRepo.Add(new Item() { Name = "Лепешка из грязи", Picture = ImageToByteArray(@"D:\\Visual_Stidio_projects\\Siadanok\\Pictures\\Gryaz.jpg"), Type = ItemType.Meal.ToString(), IsExotic = false.ToString(), Price = 0.05m, Descryption = "Это блюдо также появилось не от хорошей жизни: Гаити одно из самых бедных государств западного полушария. Малоимущие гаитянцы вынуждены кормиться лепешками из прибрежной грязи. В состав лепешек могут входить кусочки овощей, растительный жир или маргарин. Грязевые печеньки сушат прямо на солнце, после чего развозят по местным магазинам. " });
                context.itemRepo.Add(new Item() { Name = "Стакан воды", Picture = ImageToByteArray(@"D:\\Visual_Stidio_projects\\Siadanok\\Pictures\\Water.jpg"), Type = ItemType.Drink.ToString(), IsExotic = false.ToString(), Price = 0.08m, Descryption = "Чтобы смочить горло перед экзотическим блюдом." });

                context.SaveChanges();
            }
        }
    }
}
