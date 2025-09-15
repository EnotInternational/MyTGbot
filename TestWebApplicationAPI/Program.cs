using TestWebApplicationAPI;
using TgBotClassLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//using (ApplicationContext db = new ApplicationContext())
//{
//    // создаем два объекта User
//    UserInfo tom = new UserInfo { Name = "Tom", lastSeen = DateTime.Today.AddDays(-1) };
//    UserInfo alice = new UserInfo { Name = "Alice", lastSeen = DateTime.Today.AddDays(-1) };

//    // добавляем их в бд
//    db.Users.Add(tom);
//    db.Users.Add(alice);
//    db.SaveChanges();
//    Console.WriteLine("Объекты успешно сохранены");

//    // получаем объекты из бд и выводим на консоль
//    var users = db.Users.ToList();
//}

app.Run();

